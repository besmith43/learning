package com.example.expenses;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.List;

public final class ExpenseAnalyzerService {
    private final ExpenseCsvParser csvParser = new ExpenseCsvParser();
    private final RChartService rChartService = new RChartService();

    public AnalysisResult analyze(Path csvPath) throws IOException {
        List<ExpenseRecord> records = csvParser.parse(csvPath);
        if (records.isEmpty()) {
            throw new IllegalArgumentException("The CSV file is empty.");
        }

        Path outputDir = Files.createTempDirectory("expense-chart-");
        Path chartPath = outputDir.resolve("expense-chart.png");
        String runtimeDetails = rChartService.renderChart(records, chartPath);

        double totalAmount = records.stream()
            .mapToDouble(record -> record.amount().doubleValue())
            .sum();
        long categoryCount = records.stream()
            .map(ExpenseRecord::category)
            .distinct()
            .count();

        return new AnalysisResult(
            csvPath,
            chartPath,
            records.size(),
            totalAmount,
            (int) categoryCount,
            runtimeDetails
        );
    }
}
