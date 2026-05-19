package com.example.officetime.repo;

import com.example.officetime.domain.TimeEntry;
import org.springframework.data.jpa.repository.EntityGraph;
import org.springframework.data.jpa.repository.JpaRepository;

import java.time.LocalDate;
import java.util.List;

public interface TimeEntryRepository extends JpaRepository<TimeEntry, Long> {
    @EntityGraph(attributePaths = {"user", "team", "task"})
    List<TimeEntry> findByUserIdAndDeletedFalseAndWorkingDateBetweenOrderByWorkingDateDescCreatedAtDesc(Long userId, LocalDate start, LocalDate end);

    @EntityGraph(attributePaths = {"user", "team", "task"})
    List<TimeEntry> findByUserIdAndDeletedFalseAndWorkingDate(Long userId, LocalDate workingDate);

    @EntityGraph(attributePaths = {"user", "team", "task"})
    List<TimeEntry> findByUserManagerIdAndDeletedFalseOrderByWorkingDateDescCreatedAtDesc(Long managerId);

    @EntityGraph(attributePaths = {"user", "team", "task"})
    List<TimeEntry> findByDeletedFalseOrderByWorkingDateDescCreatedAtDesc();
}
