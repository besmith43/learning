package com.example.officetime.service;

import com.example.officetime.api.Dto;
import com.example.officetime.domain.*;
import com.example.officetime.repo.*;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.math.BigDecimal;
import java.time.Clock;
import java.time.LocalDate;
import java.util.List;

@Service
public class TimeEntryService {
    private static final BigDecimal STANDARD_TOTAL = new BigDecimal("8.00");

    private final TimeEntryRepository entries;
    private final ApprovalRequestRepository requests;
    private final TeamRepository teams;
    private final TaskRepository tasks;
    private final Clock clock;

    public TimeEntryService(TimeEntryRepository entries, ApprovalRequestRepository requests, TeamRepository teams, TaskRepository tasks, Clock clock) {
        this.entries = entries;
        this.requests = requests;
        this.teams = teams;
        this.tasks = tasks;
        this.clock = clock;
    }

    @Transactional
    public List<TimeEntry> submitStandard(AppUser user, Dto.StandardSubmission submission) {
        if (submission.lines() == null || submission.lines().isEmpty()) {
            throw new ValidationException("At least one line is required");
        }
        BigDecimal total = submission.lines().stream()
                .map(Dto.EntryLine::hours)
                .reduce(BigDecimal.ZERO, BigDecimal::add);
        if (total.compareTo(STANDARD_TOTAL) != 0) {
            throw new ValidationException("Standard hours must total exactly 8.00");
        }

        LocalDate today = LocalDate.now(clock);
        List<TimeEntry> existing = entries.findByUserIdAndDeletedFalseAndWorkingDate(user.getId(), today);
        if (!existing.isEmpty()) {
            throw new ValidationException("Standard hours already exist for today");
        }

        return submission.lines().stream()
                .map(line -> entries.save(new TimeEntry(
                        user,
                        today,
                        teams.findById(line.teamId()).orElseThrow(() -> new ValidationException("Unknown team")),
                        tasks.findById(line.taskId()).orElseThrow(() -> new ValidationException("Unknown task")),
                        line.comments(),
                        line.hours(),
                        EntryType.STANDARD,
                        ApprovalStatus.NOT_REQUIRED)))
                .toList();
    }

    @Transactional
    public TimeEntry submitOvertime(AppUser user, Dto.OvertimeSubmission submission) {
        LocalDate today = LocalDate.now(clock);
        if (submission.workingDate().isAfter(today)) {
            throw new ValidationException("Overtime cannot be submitted for a future date");
        }
        TimeEntry entry = entries.save(new TimeEntry(
                user,
                submission.workingDate(),
                teams.findById(submission.teamId()).orElseThrow(() -> new ValidationException("Unknown team")),
                tasks.findById(submission.taskId()).orElseThrow(() -> new ValidationException("Unknown task")),
                submission.comments(),
                submission.hours(),
                EntryType.OVERTIME,
                ApprovalStatus.PENDING));
        requests.save(new ApprovalRequest(RequestType.OVERTIME, user, entry, submission.workingDate()));
        return entry;
    }

    @Transactional(readOnly = true)
    public List<TimeEntry> search(AppUser user, LocalDate start, LocalDate end) {
        LocalDate effectiveEnd = end == null ? LocalDate.now(clock) : end;
        LocalDate effectiveStart = start == null ? effectiveEnd.minusDays(30) : start;
        return entries.findByUserIdAndDeletedFalseAndWorkingDateBetweenOrderByWorkingDateDescCreatedAtDesc(user.getId(), effectiveStart, effectiveEnd);
    }

    @Transactional
    public ApprovalRequest requestDelete(AppUser user, LocalDate workingDate) {
        List<TimeEntry> dayEntries = entries.findByUserIdAndDeletedFalseAndWorkingDate(user.getId(), workingDate);
        if (dayEntries.isEmpty()) {
            throw new ValidationException("No active entries exist for that day");
        }
        return requests.save(new ApprovalRequest(RequestType.DELETE_DAY, user, null, workingDate));
    }
}
