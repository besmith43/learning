package com.example.officetime.service;

import com.example.officetime.api.Dto;
import com.example.officetime.repo.ApprovalRequestRepository;
import com.example.officetime.repo.TaskRepository;
import com.example.officetime.repo.TeamRepository;
import com.example.officetime.repo.TimeEntryRepository;
import org.junit.jupiter.api.Test;

import java.math.BigDecimal;
import java.time.Clock;

import static org.assertj.core.api.Assertions.assertThatThrownBy;
import static org.mockito.Mockito.mock;

class TimeEntryServiceTest {
    @Test
    void standardSubmissionMustTotalExactlyEightHours() {
        TimeEntryService service = new TimeEntryService(
                mock(TimeEntryRepository.class),
                mock(ApprovalRequestRepository.class),
                mock(TeamRepository.class),
                mock(TaskRepository.class),
                Clock.systemUTC());

        Dto.StandardSubmission submission = new Dto.StandardSubmission(
                java.util.List.of(new Dto.EntryLine(1L, 1L, "short day", new BigDecimal("7.75"))));

        assertThatThrownBy(() -> service.submitStandard(null, submission))
                .isInstanceOf(ValidationException.class)
                .hasMessageContaining("exactly 8.00");
    }
}
