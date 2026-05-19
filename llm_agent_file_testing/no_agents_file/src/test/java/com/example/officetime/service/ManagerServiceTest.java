package com.example.officetime.service;

import com.example.officetime.domain.*;
import com.example.officetime.repo.TimeEntryRepository;
import org.junit.jupiter.api.Test;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

import static org.assertj.core.api.Assertions.assertThat;
import static org.mockito.Mockito.mock;

class ManagerServiceTest {
    @Test
    void csvEscapesComments() {
        ManagerService service = new ManagerService(mock(TimeEntryRepository.class));
        Team team = new Team("Platform");
        Task task = new Task("Development", false);
        AppUser manager = new AppUser("manager", "hash", "Manager", UserRole.MANAGER, team, null);
        AppUser employee = new AppUser("employee", "hash", "Alex Employee", UserRole.EMPLOYEE, team, manager);
        TimeEntry entry = new TimeEntry(employee, LocalDate.of(2026, 4, 26), team, task, "said \"done\"", new BigDecimal("8.00"), EntryType.STANDARD, ApprovalStatus.NOT_REQUIRED);

        String csv = service.toCsv(List.of(entry));

        assertThat(csv).contains("\"said \"\"done\"\"\"");
    }
}
