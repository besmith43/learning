package com.example.officetime.api;

import com.example.officetime.domain.*;
import jakarta.validation.constraints.DecimalMin;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Size;

import java.math.BigDecimal;
import java.time.LocalDate;
import java.util.List;

public final class Dto {
    private Dto() {
    }

    public record CurrentUser(Long id, String username, String displayName, UserRole role, String team, LocalDate standardDate) {
    }

    public record TeamItem(Long id, String name) {
    }

    public record TaskItem(Long id, String name, boolean paidTimeOff) {
    }

    public record ReferenceData(CurrentUser user, List<TeamItem> teams, List<TaskItem> tasks) {
    }

    public record EntryLine(
            @NotNull Long teamId,
            @NotNull Long taskId,
            @Size(max = 2000) String comments,
            @NotNull @DecimalMin(value = "0.25") BigDecimal hours) {
    }

    public record StandardSubmission(List<EntryLine> lines) {
    }

    public record OvertimeSubmission(@NotNull LocalDate workingDate, @NotNull Long teamId, @NotNull Long taskId, @Size(max = 2000) String comments, @NotNull @DecimalMin(value = "0.25") BigDecimal hours) {
    }

    public record DeleteRequest(@NotNull LocalDate workingDate) {
    }

    public record EntryView(Long id, LocalDate workingDate, String user, String team, String task, String comments, BigDecimal hours, EntryType entryType, ApprovalStatus approvalStatus) {
        public static EntryView from(TimeEntry entry) {
            return new EntryView(
                    entry.getId(),
                    entry.getWorkingDate(),
                    entry.getUser().getDisplayName(),
                    entry.getTeam().getName(),
                    entry.getTask().getName(),
                    entry.getComments(),
                    entry.getHours(),
                    entry.getEntryType(),
                    entry.getApprovalStatus());
        }
    }

    public record ApprovalView(Long id, RequestType requestType, String requester, LocalDate workingDate, String task, BigDecimal hours, ApprovalStatus status) {
        public static ApprovalView from(ApprovalRequest request) {
            TimeEntry entry = request.getTimeEntry();
            return new ApprovalView(
                    request.getId(),
                    request.getRequestType(),
                    request.getRequester().getDisplayName(),
                    request.getWorkingDate(),
                    entry == null ? null : entry.getTask().getName(),
                    entry == null ? null : entry.getHours(),
                    request.getStatus());
        }
    }
}
