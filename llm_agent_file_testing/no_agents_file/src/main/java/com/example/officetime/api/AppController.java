package com.example.officetime.api;

import com.example.officetime.domain.AppUser;
import com.example.officetime.repo.TaskRepository;
import com.example.officetime.repo.TeamRepository;
import com.example.officetime.service.CurrentUserService;
import org.springframework.security.core.Authentication;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.time.Clock;
import java.time.LocalDate;

@RestController
@RequestMapping("/api")
public class AppController {
    private final CurrentUserService currentUser;
    private final TeamRepository teams;
    private final TaskRepository tasks;
    private final Clock clock;

    public AppController(CurrentUserService currentUser, TeamRepository teams, TaskRepository tasks, Clock clock) {
        this.currentUser = currentUser;
        this.teams = teams;
        this.tasks = tasks;
        this.clock = clock;
    }

    @GetMapping("/me")
    Dto.ReferenceData me(Authentication authentication) {
        AppUser user = currentUser.require(authentication);
        Dto.CurrentUser dto = new Dto.CurrentUser(
                user.getId(),
                user.getUsername(),
                user.getDisplayName(),
                user.getRole(),
                user.getTeam() == null ? null : user.getTeam().getName(),
                LocalDate.now(clock));
        return new Dto.ReferenceData(
                dto,
                teams.findAll().stream().map(team -> new Dto.TeamItem(team.getId(), team.getName())).toList(),
                tasks.findAll().stream().map(task -> new Dto.TaskItem(task.getId(), task.getName(), task.isPaidTimeOff())).toList());
    }
}
