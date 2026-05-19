package com.example.officetime.domain;

import jakarta.persistence.*;
import java.time.Instant;
import java.time.LocalDate;

@Entity
public class ApprovalRequest {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Enumerated(EnumType.STRING)
    @Column(nullable = false)
    private RequestType requestType;

    @Enumerated(EnumType.STRING)
    @Column(nullable = false)
    private ApprovalStatus status = ApprovalStatus.PENDING;

    @ManyToOne(optional = false, fetch = FetchType.LAZY)
    private AppUser requester;

    @ManyToOne(fetch = FetchType.LAZY)
    private TimeEntry timeEntry;

    private LocalDate workingDate;

    @Column(nullable = false)
    private Instant createdAt = Instant.now();

    private Instant decidedAt;

    @ManyToOne(fetch = FetchType.LAZY)
    private AppUser decidedBy;

    protected ApprovalRequest() {
    }

    public ApprovalRequest(RequestType requestType, AppUser requester, TimeEntry timeEntry, LocalDate workingDate) {
        this.requestType = requestType;
        this.requester = requester;
        this.timeEntry = timeEntry;
        this.workingDate = workingDate;
    }

    public Long getId() { return id; }
    public RequestType getRequestType() { return requestType; }
    public ApprovalStatus getStatus() { return status; }
    public AppUser getRequester() { return requester; }
    public TimeEntry getTimeEntry() { return timeEntry; }
    public LocalDate getWorkingDate() { return workingDate; }
    public Instant getCreatedAt() { return createdAt; }

    public void decide(ApprovalStatus status, AppUser decidedBy) {
        if (status != ApprovalStatus.APPROVED && status != ApprovalStatus.REJECTED) {
            throw new IllegalArgumentException("Decision must approve or reject");
        }
        this.status = status;
        this.decidedBy = decidedBy;
        this.decidedAt = Instant.now();
    }
}
