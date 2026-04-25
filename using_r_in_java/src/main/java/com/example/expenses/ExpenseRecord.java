package com.example.expenses;

import java.math.BigDecimal;
import java.time.LocalDate;

public record ExpenseRecord(
    LocalDate date,
    String category,
    BigDecimal amount,
    String vendor
) {
}
