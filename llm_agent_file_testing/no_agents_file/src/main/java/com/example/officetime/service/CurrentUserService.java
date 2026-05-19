package com.example.officetime.service;

import com.example.officetime.domain.AppUser;
import com.example.officetime.repo.UserRepository;
import org.springframework.security.core.Authentication;
import org.springframework.stereotype.Service;

@Service
public class CurrentUserService {
    private final UserRepository users;

    public CurrentUserService(UserRepository users) {
        this.users = users;
    }

    public AppUser require(Authentication authentication) {
        if (authentication == null || authentication.getName() == null) {
            throw new ForbiddenException("Authentication required");
        }
        return users.findByUsername(authentication.getName())
                .orElseThrow(() -> new ForbiddenException("Authenticated user was not found"));
    }
}
