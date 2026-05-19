package com.example.officetime.repo;

import com.example.officetime.domain.AppUser;
import com.example.officetime.domain.UserRole;
import org.springframework.data.jpa.repository.EntityGraph;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;
import java.util.Optional;

public interface UserRepository extends JpaRepository<AppUser, Long> {
    @EntityGraph(attributePaths = {"team", "manager"})
    Optional<AppUser> findByUsername(String username);

    List<AppUser> findByRole(UserRole role);

    List<AppUser> findByManagerId(Long managerId);
}
