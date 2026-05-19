package com.example.officetime.api;

import com.fasterxml.jackson.databind.ObjectMapper;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.web.server.LocalServerPort;
import org.springframework.test.context.DynamicPropertyRegistry;
import org.springframework.test.context.DynamicPropertySource;
import org.testcontainers.containers.PostgreSQLContainer;
import org.testcontainers.junit.jupiter.Container;
import org.testcontainers.junit.jupiter.Testcontainers;

import java.math.BigDecimal;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.nio.charset.StandardCharsets;
import java.util.Base64;
import java.util.List;
import java.util.Map;

import static org.assertj.core.api.Assertions.assertThat;

@Testcontainers(disabledWithoutDocker = true)
@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
class OfficeTimeApiIntegrationTest {
    @Container
    static PostgreSQLContainer<?> postgres = new PostgreSQLContainer<>("postgres:17-alpine")
            .withDatabaseName("office_time_test")
            .withUsername("office_time")
            .withPassword("office_time");

    @DynamicPropertySource
    static void datasource(DynamicPropertyRegistry registry) {
        registry.add("spring.datasource.url", postgres::getJdbcUrl);
        registry.add("spring.datasource.username", postgres::getUsername);
        registry.add("spring.datasource.password", postgres::getPassword);
        registry.add("spring.jpa.hibernate.ddl-auto", () -> "create-drop");
    }

    @LocalServerPort
    int port;

    @Autowired
    ObjectMapper objectMapper;

    private final HttpClient client = HttpClient.newHttpClient();

    @Test
    void basicAuthReturnsCurrentUserAndReferenceData() throws Exception {
        HttpResponse<String> response = send("GET", "/api/me", "employee1", null);

        assertThat(response.statusCode()).isEqualTo(200);
        assertThat(response.body()).contains("\"username\":\"employee1\"");
        assertThat(response.body()).contains("\"Annual Leave\"");
    }

    @Test
    void rejectsStandardSubmissionThatDoesNotEqualEightHours() throws Exception {
        Map<String, Object> body = Map.of("lines", List.of(Map.of(
                "teamId", 1,
                "taskId", 1,
                "comments", "short",
                "hours", new BigDecimal("7.50"))));

        HttpResponse<String> response = send("POST", "/api/entries/standard", "employee1", objectMapper.writeValueAsString(body));

        assertThat(response.statusCode()).isEqualTo(400);
        assertThat(response.body()).contains("Standard hours must total exactly 8.00");
    }

    @Test
    void managerCanDownloadCsvForDirectReports() throws Exception {
        HttpResponse<String> response = send("GET", "/api/manager/entries.csv", "manager1", null);

        assertThat(response.statusCode()).isEqualTo(200);
        assertThat(response.headers().firstValue("content-type")).hasValueSatisfying(value -> assertThat(value).contains("text/csv"));
        assertThat(response.body()).contains("Alex Employee");
    }

    private HttpResponse<String> send(String method, String path, String username, String body) throws Exception {
        HttpRequest.BodyPublisher publisher = body == null ? HttpRequest.BodyPublishers.noBody() : HttpRequest.BodyPublishers.ofString(body);
        HttpRequest request = HttpRequest.newBuilder(URI.create("http://127.0.0.1:" + port + path))
                .method(method, publisher)
                .header("Authorization", "Basic " + Base64.getEncoder().encodeToString((username + ":password").getBytes(StandardCharsets.UTF_8)))
                .header("Content-Type", "application/json")
                .build();
        return client.send(request, HttpResponse.BodyHandlers.ofString());
    }
}
