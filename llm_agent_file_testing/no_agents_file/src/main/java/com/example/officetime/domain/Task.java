package com.example.officetime.domain;

import jakarta.persistence.*;

@Entity
public class Task {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(nullable = false, unique = true)
    private String name;

    @Column(nullable = false)
    private boolean paidTimeOff;

    protected Task() {
    }

    public Task(String name, boolean paidTimeOff) {
        this.name = name;
        this.paidTimeOff = paidTimeOff;
    }

    public Long getId() { return id; }
    public String getName() { return name; }
    public boolean isPaidTimeOff() { return paidTimeOff; }
}
