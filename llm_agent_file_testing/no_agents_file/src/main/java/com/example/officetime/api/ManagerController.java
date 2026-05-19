package com.example.officetime.api;

import com.example.officetime.domain.AppUser;
import com.example.officetime.service.ApprovalService;
import com.example.officetime.service.CurrentUserService;
import com.example.officetime.service.ManagerService;
import org.springframework.http.HttpHeaders;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.Authentication;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/manager")
public class ManagerController {
    private final CurrentUserService currentUser;
    private final ManagerService managerService;
    private final ApprovalService approvalService;

    public ManagerController(CurrentUserService currentUser, ManagerService managerService, ApprovalService approvalService) {
        this.currentUser = currentUser;
        this.managerService = managerService;
        this.approvalService = approvalService;
    }

    @GetMapping("/entries")
    List<Dto.EntryView> entries(Authentication authentication) {
        AppUser user = currentUser.require(authentication);
        return managerService.visibleEntries(user).stream().map(Dto.EntryView::from).toList();
    }

    @GetMapping("/entries.csv")
    ResponseEntity<String> csv(Authentication authentication) {
        AppUser user = currentUser.require(authentication);
        String csv = managerService.toCsv(managerService.visibleEntries(user));
        return ResponseEntity.ok()
                .header(HttpHeaders.CONTENT_DISPOSITION, "attachment; filename=office-time-entries.csv")
                .contentType(MediaType.parseMediaType("text/csv"))
                .body(csv);
    }

    @GetMapping("/approvals")
    List<Dto.ApprovalView> approvals(Authentication authentication) {
        AppUser user = currentUser.require(authentication);
        return approvalService.pendingFor(user).stream().map(Dto.ApprovalView::from).toList();
    }

    @PostMapping("/approvals/{id}/approve")
    Dto.ApprovalView approve(Authentication authentication, @PathVariable Long id) {
        AppUser user = currentUser.require(authentication);
        return Dto.ApprovalView.from(approvalService.decide(user, id, true));
    }

    @PostMapping("/approvals/{id}/reject")
    Dto.ApprovalView reject(Authentication authentication, @PathVariable Long id) {
        AppUser user = currentUser.require(authentication);
        return Dto.ApprovalView.from(approvalService.decide(user, id, false));
    }
}
