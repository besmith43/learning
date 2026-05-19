package com.example.officetime.service;

import com.example.officetime.domain.AppUser;
import com.example.officetime.domain.TimeEntry;
import com.example.officetime.domain.UserRole;
import com.example.officetime.repo.TimeEntryRepository;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

@Service
public class ManagerService {
    private final TimeEntryRepository entries;

    public ManagerService(TimeEntryRepository entries) {
        this.entries = entries;
    }

    @Transactional(readOnly = true)
    public List<TimeEntry> visibleEntries(AppUser user) {
        if (user.getRole() == UserRole.SITE_ADMIN) {
            return entries.findByDeletedFalseOrderByWorkingDateDescCreatedAtDesc();
        }
        if (user.getRole() != UserRole.MANAGER) {
            throw new ForbiddenException("Manager access required");
        }
        return entries.findByUserManagerIdAndDeletedFalseOrderByWorkingDateDescCreatedAtDesc(user.getId());
    }

    public String toCsv(List<TimeEntry> visibleEntries) {
        StringBuilder csv = new StringBuilder("user,working_date,team,task,entry_type,approval_status,hours,comments\n");
        for (TimeEntry entry : visibleEntries) {
            csv.append(escape(entry.getUser().getDisplayName())).append(',')
                    .append(entry.getWorkingDate()).append(',')
                    .append(escape(entry.getTeam().getName())).append(',')
                    .append(escape(entry.getTask().getName())).append(',')
                    .append(entry.getEntryType()).append(',')
                    .append(entry.getApprovalStatus()).append(',')
                    .append(entry.getHours()).append(',')
                    .append(escape(entry.getComments()))
                    .append('\n');
        }
        return csv.toString();
    }

    private String escape(String value) {
        if (value == null) {
            return "";
        }
        return "\"" + value.replace("\"", "\"\"") + "\"";
    }
}
