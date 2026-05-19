package com.example.officetime.repo;

import com.example.officetime.domain.ApprovalRequest;
import com.example.officetime.domain.ApprovalStatus;
import org.springframework.data.jpa.repository.EntityGraph;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;

public interface ApprovalRequestRepository extends JpaRepository<ApprovalRequest, Long> {
    @EntityGraph(attributePaths = {"requester", "requester.team", "requester.manager", "timeEntry", "timeEntry.task"})
    List<ApprovalRequest> findByStatusOrderByCreatedAtAsc(ApprovalStatus status);
}
