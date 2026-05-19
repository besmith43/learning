package com.example.officetime.service;

import com.example.officetime.domain.*;
import com.example.officetime.repo.ApprovalRequestRepository;
import com.example.officetime.repo.TimeEntryRepository;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;

@Service
public class ApprovalService {
    private final ApprovalRequestRepository requests;
    private final TimeEntryRepository entries;

    public ApprovalService(ApprovalRequestRepository requests, TimeEntryRepository entries) {
        this.requests = requests;
        this.entries = entries;
    }

    @Transactional(readOnly = true)
    public List<ApprovalRequest> pendingFor(AppUser approver) {
        return requests.findByStatusOrderByCreatedAtAsc(ApprovalStatus.PENDING).stream()
                .filter(request -> canApprove(approver, request))
                .toList();
    }

    @Transactional
    public ApprovalRequest decide(AppUser approver, Long requestId, boolean approve) {
        ApprovalRequest request = requests.findById(requestId).orElseThrow(() -> new NotFoundException("Approval request not found"));
        if (!canApprove(approver, request)) {
            throw new ForbiddenException("Only a direct manager or site admin may decide this request");
        }
        ApprovalStatus decision = approve ? ApprovalStatus.APPROVED : ApprovalStatus.REJECTED;
        request.decide(decision, approver);
        if (request.getRequestType() == RequestType.OVERTIME && request.getTimeEntry() != null) {
            if (approve) {
                request.getTimeEntry().approve();
            } else {
                request.getTimeEntry().reject();
            }
        }
        if (approve && request.getRequestType() == RequestType.DELETE_DAY) {
            entries.findByUserIdAndDeletedFalseAndWorkingDate(request.getRequester().getId(), request.getWorkingDate())
                    .forEach(TimeEntry::softDelete);
        }
        return request;
    }

    private boolean canApprove(AppUser approver, ApprovalRequest request) {
        if (approver.getRole() == UserRole.SITE_ADMIN) {
            return true;
        }
        return approver.getRole() == UserRole.MANAGER
                && request.getRequester().getManager() != null
                && request.getRequester().getManager().getId().equals(approver.getId());
    }
}
