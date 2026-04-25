package com.example.expenses;

import java.io.IOException;
import java.math.BigDecimal;
import java.nio.file.Files;
import java.nio.file.Path;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Locale;
import java.util.Map;

public final class ExpenseCsvParser {
    private static final List<String> REQUIRED_HEADERS = List.of("date", "category", "amount", "vendor");

    public List<ExpenseRecord> parse(Path csvPath) throws IOException {
        List<String> lines = Files.readAllLines(csvPath);
        if (lines.isEmpty()) {
            throw new IllegalArgumentException("The CSV file is empty.");
        }

        List<String> headerCells = parseLine(lines.get(0));
        Map<String, Integer> headerIndex = buildHeaderIndex(headerCells);
        List<ExpenseRecord> records = new ArrayList<>();

        for (int lineNumber = 1; lineNumber < lines.size(); lineNumber++) {
            String line = lines.get(lineNumber);
            if (line.isBlank()) {
                continue;
            }

            List<String> cells = parseLine(line);
            records.add(parseRecord(cells, headerIndex, lineNumber + 1));
        }

        return records;
    }

    private Map<String, Integer> buildHeaderIndex(List<String> headerCells) {
        Map<String, Integer> headerIndex = new HashMap<>();
        for (int i = 0; i < headerCells.size(); i++) {
            headerIndex.put(headerCells.get(i).trim().toLowerCase(Locale.ROOT), i);
        }

        for (String requiredHeader : REQUIRED_HEADERS) {
            if (!headerIndex.containsKey(requiredHeader)) {
                throw new IllegalArgumentException(
                    "Missing required header '%s'. Expected headers: %s"
                        .formatted(requiredHeader, REQUIRED_HEADERS)
                );
            }
        }
        return headerIndex;
    }

    private ExpenseRecord parseRecord(List<String> cells, Map<String, Integer> headerIndex, int lineNumber) {
        LocalDate date = LocalDate.parse(readCell(cells, headerIndex, "date", lineNumber));
        String category = readCell(cells, headerIndex, "category", lineNumber);
        BigDecimal amount;
        try {
            amount = new BigDecimal(readCell(cells, headerIndex, "amount", lineNumber));
        } catch (NumberFormatException exception) {
            throw new IllegalArgumentException("Invalid amount at line %d.".formatted(lineNumber), exception);
        }
        if (amount.signum() < 0) {
            throw new IllegalArgumentException("Negative amounts are not supported at line %d.".formatted(lineNumber));
        }
        String vendor = readCell(cells, headerIndex, "vendor", lineNumber);
        return new ExpenseRecord(date, category, amount, vendor);
    }

    private String readCell(List<String> cells, Map<String, Integer> headerIndex, String header, int lineNumber) {
        int index = headerIndex.get(header);
        if (index >= cells.size()) {
            throw new IllegalArgumentException("Missing '%s' value at line %d.".formatted(header, lineNumber));
        }
        String value = cells.get(index).trim();
        if (value.isEmpty()) {
            throw new IllegalArgumentException("Blank '%s' value at line %d.".formatted(header, lineNumber));
        }
        return value;
    }

    private List<String> parseLine(String line) {
        List<String> cells = new ArrayList<>();
        StringBuilder current = new StringBuilder();
        boolean inQuotes = false;

        for (int i = 0; i < line.length(); i++) {
            char ch = line.charAt(i);
            if (ch == '"') {
                if (inQuotes && i + 1 < line.length() && line.charAt(i + 1) == '"') {
                    current.append('"');
                    i++;
                } else {
                    inQuotes = !inQuotes;
                }
            } else if (ch == ',' && !inQuotes) {
                cells.add(current.toString());
                current.setLength(0);
            } else {
                current.append(ch);
            }
        }

        if (inQuotes) {
            throw new IllegalArgumentException("Malformed CSV line with unclosed quotes: " + line);
        }

        cells.add(current.toString());
        return cells;
    }
}
