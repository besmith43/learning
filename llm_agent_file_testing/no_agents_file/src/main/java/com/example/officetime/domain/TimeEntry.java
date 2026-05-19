package com.example.officetime.domain;

import jakarta.persistence.*;
import java.math.BigDecimal;
import java.time.Instant;
import java.time.LocalDate;

@Entity
public class TimeEntry {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @ManyToOne(optional = false, fetch = FetchType.LAZY)
    private AppUser user;

    @Column(nullable = false)
    private LocalDate workingDate;

    @ManyToOne(optional = false, fetch = FetchType.LAZY)
    private Team team;

    @ManyToOne(optional = false, fetch = FetchType.LAZY)
    private Task task;

    @Column(length = 2000)
    private String comments;

    @Column(nullable = false, precision = 5, scale = 2)
    private BigDecimal hours;

    @Enumerated(EnumType.STRING)
    @Column(nullable = false)
    private EntryType entryType;

    @Enumerated(EnumType.STRING)
    @Column(nullable = false)
    private ApprovalStatus approvalStatus;

    @Column(nullable = false)
    private boolean deleted;

    private Instant deletedAt;

    @Column(nullable = false)
    private Instant createdAt = Instant.now();

    protected TimeEntry() {
    }

    public TimeEntry(AppUser user, LocalDate workingDate, Team team, Task task, String comments, BigDecimal hours, EntryType entryType, ApprovalStatus approvalStatus) {
        this.user = user;
        this.workingDate = workingDate;
        this.team = team;
        this.task = task;
        this.comments = comments;
        this.hours = hours;
        this.entryType = entryType;
        this.approvalStatus = approvalStatus;
    }

    public Long getId() { return id; }
    public AppUser getUser() { return user; }
    public LocalDate getWorkingDate() { return workingDate; }
    public Team getTeam() { return team; }
    public Task getTask() { return task; }
    public String getComments() { return comments; }
    public BigDecimal getHours() { return hours; }
    public EntryType getEntryType() { return entryType; }
    public ApprovalStatus getApprovalStatus() { return approvalStatus; }
    public boolean isDeleted() { return deleted; }
    public Instant getCreatedAt() { return createdAt; }

    public void approve() { this.approvalStatus = ApprovalStatus.APPROVED; }
    public void reject() { this.approvalStatus = ApprovalStatus.REJECTED; }
    public void softDelete() {
        this.deleted = true;
        this.deletedAt = Instant.now();
    }
}
