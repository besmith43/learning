package com.example.expenses;

import java.nio.file.Path;

public record AnalysisResult(
    Path sourceCsv,
    Path chartPath,
    int rowCount,
    double totalAmount,
    int categoryCount,
    String runtimeDetails
) {
}
