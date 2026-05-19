package com.example.officetime.config;

import com.example.officetime.domain.*;
import com.example.officetime.repo.*;
import org.springframework.boot.ApplicationArguments;
import org.springframework.boot.ApplicationRunner;
import org.springframework.boot.autoconfigure.condition.ConditionalOnProperty;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Component;
import org.springframework.transaction.annotation.Transactional;

import java.math.BigDecimal;
import java.time.LocalDate;

@Component
@ConditionalOnProperty(name = "app.seed.enabled", havingValue = "true", matchIfMissing = true)
public class SeedData implements ApplicationRunner {
    private final UserRepository users;
    private final TeamRepository teams;
    private final TaskRepository tasks;
    private final TimeEntryRepository entries;
    private final ApprovalRequestRepository requests;
    private final PasswordEncoder encoder;

    public SeedData(UserRepository users, TeamRepository teams, TaskRepository tasks, TimeEntryRepository entries, ApprovalRequestRepository requests, PasswordEncoder encoder) {
        this.users = users;
        this.teams = teams;
        this.tasks = tasks;
        this.entries = entries;
        this.requests = requests;
        this.encoder = encoder;
    }

    @Override
    @Transactional
    public void run(ApplicationArguments args) {
        if (users.findByUsername("admin").isPresent()) {
            return;
        }

        Team platform = teams.save(new Team("Platform"));
        Team operations = teams.save(new Team("Operations"));
        Task development = tasks.save(new Task("Development", false));
        Task support = tasks.save(new Task("Customer Support", false));
        Task meetings = tasks.save(new Task("Meetings", false));
        tasks.save(new Task("Annual Leave", true));
        tasks.save(new Task("Sick Leave", true));

        AppUser admin = users.save(new AppUser("admin", encoder.encode("password"), "Site Admin", UserRole.SITE_ADMIN, platform, null));
        AppUser managerA = users.save(new AppUser("manager1", encoder.encode("password"), "Morgan Manager", UserRole.MANAGER, platform, null));
        AppUser managerB = users.save(new AppUser("manager2", encoder.encode("password"), "Riley Manager", UserRole.MANAGER, operations, null));
        AppUser employeeA = users.save(new AppUser("employee1", encoder.encode("password"), "Alex Employee", UserRole.EMPLOYEE, platform, managerA));
        AppUser employeeB = users.save(new AppUser("employee2", encoder.encode("password"), "Jamie Employee", UserRole.EMPLOYEE, platform, managerA));
        AppUser employeeC = users.save(new AppUser("employee3", encoder.encode("password"), "Taylor Employee", UserRole.EMPLOYEE, operations, managerB));

        LocalDate yesterday = LocalDate.now().minusDays(1);
        entries.save(new TimeEntry(employeeA, yesterday, platform, development, "Feature work", new BigDecimal("6.00"), EntryType.STANDARD, ApprovalStatus.NOT_REQUIRED));
        entries.save(new TimeEntry(employeeA, yesterday, platform, meetings, "Planning", new BigDecimal("2.00"), EntryType.STANDARD, ApprovalStatus.NOT_REQUIRED));
        entries.save(new TimeEntry(employeeB, yesterday, platform, support, "Ticket queue", new BigDecimal("8.00"), EntryType.STANDARD, ApprovalStatus.NOT_REQUIRED));
        TimeEntry overtime = entries.save(new TimeEntry(employeeC, yesterday, operations, support, "Production support", new BigDecimal("2.00"), EntryType.OVERTIME, ApprovalStatus.PENDING));
        requests.save(new ApprovalRequest(RequestType.OVERTIME, employeeC, overtime, yesterday));

        TimeEntry adminSample = entries.save(new TimeEntry(admin, yesterday, platform, meetings, "Admin review", new BigDecimal("1.00"), EntryType.OVERTIME, ApprovalStatus.APPROVED));
        adminSample.approve();
    }
}
