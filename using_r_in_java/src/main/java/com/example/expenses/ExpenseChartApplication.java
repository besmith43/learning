package com.example.expenses;

import java.awt.BorderLayout;
import java.awt.Dimension;
import java.awt.EventQueue;
import java.awt.FlowLayout;
import java.awt.Font;
import java.awt.Image;
import java.io.IOException;
import java.nio.file.Path;
import java.text.NumberFormat;
import java.util.Locale;
import javax.swing.BorderFactory;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JFileChooser;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTextArea;
import javax.swing.SwingUtilities;
import javax.swing.UIManager;
import javax.swing.filechooser.FileNameExtensionFilter;

public final class ExpenseChartApplication {
    private final ExpenseAnalyzerService analyzerService;
    private final JFrame frame;
    private final JTextArea statusArea;
    private final JLabel chartLabel;

    private ExpenseChartApplication() {
        this.analyzerService = new ExpenseAnalyzerService();
        this.frame = new JFrame("Business Expenditures Chart");
        this.statusArea = new JTextArea();
        this.chartLabel = new JLabel("Choose a CSV to generate a chart", JLabel.CENTER);
    }

    public static void main(String[] args) {
        EventQueue.invokeLater(() -> {
            installLookAndFeel();
            new ExpenseChartApplication().show();
        });
    }

    private static void installLookAndFeel() {
        try {
            UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
        } catch (Exception ignored) {
            // Fall back to the default LAF if the system theme is unavailable.
        }
    }

    private void show() {
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.setMinimumSize(new Dimension(980, 720));
        frame.setLayout(new BorderLayout(12, 12));

        JPanel topBar = new JPanel(new FlowLayout(FlowLayout.LEFT));
        JButton chooseButton = new JButton("Choose CSV");
        chooseButton.addActionListener(event -> chooseCsv());
        topBar.add(chooseButton);

        chartLabel.setVerticalAlignment(JLabel.TOP);
        chartLabel.setBorder(BorderFactory.createEmptyBorder(12, 12, 12, 12));

        statusArea.setEditable(false);
        statusArea.setLineWrap(true);
        statusArea.setWrapStyleWord(true);
        statusArea.setFont(new Font(Font.MONOSPACED, Font.PLAIN, 13));
        statusArea.setText("""
            Sample schema:
            - date
            - category
            - amount
            - vendor

            Use the sample CSV in sample-data/expenses.csv or choose your own file.
            """);

        JPanel content = new JPanel(new BorderLayout(12, 12));
        content.setBorder(BorderFactory.createEmptyBorder(12, 12, 12, 12));
        content.add(new JScrollPane(chartLabel), BorderLayout.CENTER);

        JScrollPane statusScroll = new JScrollPane(statusArea);
        statusScroll.setPreferredSize(new Dimension(300, 0));
        content.add(statusScroll, BorderLayout.EAST);

        frame.add(topBar, BorderLayout.NORTH);
        frame.add(content, BorderLayout.CENTER);
        frame.setLocationByPlatform(true);
        frame.setVisible(true);
    }

    private void chooseCsv() {
        JFileChooser chooser = new JFileChooser();
        chooser.setFileFilter(new FileNameExtensionFilter("CSV files", "csv"));
        if (chooser.showOpenDialog(frame) != JFileChooser.APPROVE_OPTION) {
            return;
        }

        Path csvPath = chooser.getSelectedFile().toPath();
        statusArea.setText("Loading " + csvPath + " ...");
        chartLabel.setIcon(null);
        chartLabel.setText("Generating chart...");

        Thread worker = new Thread(() -> analyze(csvPath), "expense-analyzer");
        worker.setDaemon(true);
        worker.start();
    }

    private void analyze(Path csvPath) {
        try {
            AnalysisResult result = analyzerService.analyze(csvPath);
            SwingUtilities.invokeLater(() -> updateUi(result));
        } catch (Exception exception) {
            SwingUtilities.invokeLater(() -> showError(csvPath, exception));
        }
    }

    private void updateUi(AnalysisResult result) {
        chartLabel.setText("");
        chartLabel.setIcon(loadScaledIcon(result.chartPath()));

        NumberFormat currency = NumberFormat.getCurrencyInstance(Locale.US);
        String text = """
            File: %s

            Rows loaded: %d
            Total spend: %s
            Categories charted: %d
            Chart path: %s

            %s
            """.formatted(
            result.sourceCsv(),
            result.rowCount(),
            currency.format(result.totalAmount()),
            result.categoryCount(),
            result.chartPath(),
            result.runtimeDetails()
        );
        statusArea.setText(text);
    }

    private void showError(Path csvPath, Exception exception) {
        chartLabel.setIcon(null);
        chartLabel.setText("Unable to generate chart");
        statusArea.setText("""
            File: %s

            %s
            """.formatted(csvPath, exception.getMessage()));
    }

    private ImageIcon loadScaledIcon(Path imagePath) {
        ImageIcon rawIcon = new ImageIcon(imagePath.toString());
        Image scaledImage = rawIcon.getImage().getScaledInstance(640, 480, Image.SCALE_SMOOTH);
        return new ImageIcon(scaledImage);
    }
}
