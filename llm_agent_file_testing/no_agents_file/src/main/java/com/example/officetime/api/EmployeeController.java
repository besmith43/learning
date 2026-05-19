package com.example.officetime.api;

import com.example.officetime.domain.AppUser;
import com.example.officetime.service.CurrentUserService;
import com.example.officetime.service.TimeEntryService;
import jakarta.validation.Valid;
import org.springframework.format.annotation.DateTimeFormat;
import org.springframework.security.core.Authentication;
import org.springframework.web.bind.annotation.*;

import java.time.LocalDate;
import java.util.List;

@RestController
@RequestMapping("/api/entries")
public class EmployeeController {
    private final CurrentUserService currentUser;
    private final TimeEntryService entries;

    public EmployeeController(CurrentUserService currentUser, TimeEntryService entries) {
        this.currentUser = currentUser;
        this.entries = entries;
    }

    @GetMapping
    List<Dto.EntryView> search(
            Authentication authentication,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate start,
            @RequestParam(required = false) @DateTimeFormat(iso = DateTimeFormat.ISO.DATE) LocalDate end) {
        AppUser user = currentUser.require(authentication);
        return entries.search(user, start, end).stream().map(Dto.EntryView::from).toList();
    }

    @PostMapping("/standard")
    List<Dto.EntryView> submitStandard(Authentication authentication, @Valid @RequestBody Dto.StandardSubmission submission) {
        AppUser user = currentUser.require(authentication);
        return entries.submitStandard(user, submission).stream().map(Dto.EntryView::from).toList();
    }

    @PostMapping("/overtime")
    Dto.EntryView submitOvertime(Authentication authentication, @Valid @RequestBody Dto.OvertimeSubmission submission) {
        AppUser user = currentUser.require(authentication);
        return Dto.EntryView.from(entries.submitOvertime(user, submission));
    }

    @PostMapping("/delete-requests")
    Dto.ApprovalView requestDelete(Authentication authentication, @Valid @RequestBody Dto.DeleteRequest request) {
        AppUser user = currentUser.require(authentication);
        return Dto.ApprovalView.from(entries.requestDelete(user, request.workingDate()));
    }
}
