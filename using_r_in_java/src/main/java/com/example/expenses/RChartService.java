package com.example.expenses;

import java.lang.reflect.Constructor;
import java.lang.reflect.InvocationHandler;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.lang.reflect.Proxy;
import java.net.URL;
import java.net.URLClassLoader;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.ArrayList;
import java.util.List;

public final class RChartService {
    public String renderChart(List<ExpenseRecord> records, Path chartPath) {
        try {
            JriRuntime runtime = JriRuntime.load();
            runtime.generateChart(records, chartPath);
            return "Generated with R via rJava JRI runtime";
        } catch (Exception exception) {
            throw new IllegalStateException(buildSetupMessage(exception), exception);
        }
    }

    private String buildSetupMessage(Exception cause) {
        return """
            R integration is not ready. Install R and the rJava package, then set either JRI_HOME or R_HOME.
            Expected JRI.jar under R_HOME/library/rJava/jri or R_HOME/site-library/rJava/jri.
            Root cause: %s
            """.formatted(cause.getMessage());
    }

    private static final class JriRuntime {
        private final Object engine;
        private final Method assignStringArrayMethod;
        private final Method assignDoubleArrayMethod;
        private final Method evalMethod;
        private final Method endMethod;
        private final URLClassLoader loader;

        private JriRuntime(
            Object engine,
            Method assignStringArrayMethod,
            Method assignDoubleArrayMethod,
            Method evalMethod,
            Method endMethod,
            URLClassLoader loader
        ) {
            this.engine = engine;
            this.assignStringArrayMethod = assignStringArrayMethod;
            this.assignDoubleArrayMethod = assignDoubleArrayMethod;
            this.evalMethod = evalMethod;
            this.endMethod = endMethod;
            this.loader = loader;
        }

        static JriRuntime load() throws Exception {
            Path jriHome = locateJriHome();
            loadNativeLibrary(jriHome);

            URLClassLoader loader = new URLClassLoader(
                new URL[] {jriHome.resolve("JRI.jar").toUri().toURL()},
                JriRuntime.class.getClassLoader()
            );
            Class<?> mainLoopCallbacks = loader.loadClass("org.rosuda.JRI.RMainLoopCallbacks");
            Class<?> rengineClass = loader.loadClass("org.rosuda.JRI.Rengine");
            Object callbacks = buildCallbacks(mainLoopCallbacks);
            Constructor<?> constructor = rengineClass.getConstructor(String[].class, boolean.class, mainLoopCallbacks);
            Object engine = constructor.newInstance(new String[] {"--no-save"}, false, callbacks);

            Method waitForRMethod = rengineClass.getMethod("waitForR");
            boolean ready = (boolean) waitForRMethod.invoke(engine);
            if (!ready) {
                throw new IllegalStateException("Rengine did not finish starting.");
            }

            return new JriRuntime(
                engine,
                rengineClass.getMethod("assign", String.class, String[].class),
                rengineClass.getMethod("assign", String.class, double[].class),
                rengineClass.getMethod("eval", String.class),
                rengineClass.getMethod("end"),
                loader
            );
        }

        void generateChart(List<ExpenseRecord> records, Path chartPath)
            throws InvocationTargetException, IllegalAccessException {
            try {
                String[] categories = records.stream().map(ExpenseRecord::category).toArray(String[]::new);
                double[] amounts = records.stream().mapToDouble(record -> record.amount().doubleValue()).toArray();

                assignStringArrayMethod.invoke(engine, "expense_categories", categories);
                assignDoubleArrayMethod.invoke(engine, "expense_amounts", amounts);

                for (String statement : buildRScript(chartPath)) {
                    evalMethod.invoke(engine, statement);
                }
            } finally {
                endMethod.invoke(engine);
                try {
                    loader.close();
                } catch (Exception ignored) {
                    // Nothing actionable for the demo if the classloader cannot close.
                }
            }
        }

        private List<String> buildRScript(Path chartPath) {
            List<String> statements = new ArrayList<>();
            statements.add("expense_df <- data.frame(category = expense_categories, amount = expense_amounts)");
            statements.add("expense_totals <- aggregate(amount ~ category, data = expense_df, FUN = sum)");
            statements.add("expense_totals <- expense_totals[order(expense_totals$amount, decreasing = TRUE), ]");
            statements.add("png(filename = " + quoteForR(chartPath.toAbsolutePath().toString()) + ", width = 1200, height = 700)");
            statements.add("par(mar = c(10, 5, 4, 2) + 0.1)");
            statements.add("""
                barplot(
                  height = expense_totals$amount,
                  names.arg = expense_totals$category,
                  col = "#2F6B7C",
                  border = NA,
                  las = 2,
                  main = "Business Expenditures by Category",
                  ylab = "Amount (USD)"
                )
                """.strip());
            statements.add("dev.off()");
            return statements;
        }

        private static Path locateJriHome() {
            List<Path> candidates = new ArrayList<>();

            String explicitJriHome = System.getenv("JRI_HOME");
            if (explicitJriHome != null && !explicitJriHome.isBlank()) {
                candidates.add(Path.of(explicitJriHome));
            }

            String rHome = System.getenv("R_HOME");
            if (rHome != null && !rHome.isBlank()) {
                candidates.add(Path.of(rHome, "library", "rJava", "jri"));
                candidates.add(Path.of(rHome, "site-library", "rJava", "jri"));
            }

            for (Path candidate : candidates) {
                if (Files.exists(candidate.resolve("JRI.jar"))) {
                    return candidate;
                }
            }

            throw new IllegalStateException("Unable to find JRI.jar. Set JRI_HOME or R_HOME.");
        }

        private static void loadNativeLibrary(Path jriHome) {
            List<String> names = List.of("libjri.jnilib", "libjri.dylib", "jri.dll", "libjri.so");
            for (String name : names) {
                Path candidate = jriHome.resolve(name);
                if (Files.exists(candidate)) {
                    System.load(candidate.toAbsolutePath().toString());
                    return;
                }
            }
            throw new IllegalStateException("Unable to find the JRI native library in " + jriHome);
        }

        private static Object buildCallbacks(Class<?> mainLoopCallbacks) {
            InvocationHandler handler = (proxy, method, args) -> {
                String name = method.getName();
                if ("rWriteConsole".equals(name) && args != null && args.length > 1 && args[1] instanceof String text) {
                    if (!text.isBlank()) {
                        System.out.print(text);
                    }
                    return null;
                }
                if ("rShowMessage".equals(name) && args != null && args.length > 1 && args[1] instanceof String message) {
                    System.out.println(message);
                    return null;
                }
                if ("rReadConsole".equals(name) || "rChooseFile".equals(name)) {
                    return null;
                }
                return null;
            };
            return Proxy.newProxyInstance(
                mainLoopCallbacks.getClassLoader(),
                new Class<?>[] {mainLoopCallbacks},
                handler
            );
        }

        private static String quoteForR(String value) {
            return "\"" + value
                .replace("\\", "\\\\")
                .replace("\"", "\\\"") + "\"";
        }
    }
}
