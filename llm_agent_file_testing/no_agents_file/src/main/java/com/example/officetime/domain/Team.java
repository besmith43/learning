package com.example.officetime.domain;

import jakarta.persistence.*;

@Entity
public class Team {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(nullable = false, unique = true)
    private String name;

    protected Team() {
    }

    public Team(String name) {
        this.name = name;
    }

    public Long getId() { return id; }
    public String getName() { return name; }
}
