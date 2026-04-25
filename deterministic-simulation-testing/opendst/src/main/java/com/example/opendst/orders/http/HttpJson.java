package com.example.opendst.orders.http;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.sun.net.httpserver.HttpExchange;
import java.io.IOException;
import java.io.UncheckedIOException;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;

final class HttpJson {
    static final ObjectMapper MAPPER = new ObjectMapper();

    private HttpJson() {
    }

    static <T> T read(HttpExchange exchange, Class<T> type) throws IOException {
        return MAPPER.readValue(exchange.getRequestBody(), type);
    }

    static void write(HttpExchange exchange, int statusCode, Object value) throws IOException {
        byte[] body = MAPPER.writeValueAsBytes(value);
        exchange.getResponseHeaders().set("Content-Type", "application/json");
        exchange.sendResponseHeaders(statusCode, body.length);
        exchange.getResponseBody().write(body);
        exchange.close();
    }

    static <T> T post(HttpClient client, String url, Object request, Class<T> responseType) {
        try {
            HttpRequest httpRequest = HttpRequest.newBuilder(URI.create(url))
                    .header("Content-Type", "application/json")
                    .POST(HttpRequest.BodyPublishers.ofByteArray(MAPPER.writeValueAsBytes(request)))
                    .build();
            HttpResponse<byte[]> response = client.send(httpRequest, HttpResponse.BodyHandlers.ofByteArray());
            if (response.statusCode() / 100 != 2) {
                throw new IllegalStateException("HTTP " + response.statusCode() + " from " + url);
            }
            return MAPPER.readValue(response.body(), responseType);
        } catch (IOException e) {
            throw new UncheckedIOException(e);
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
            throw new IllegalStateException("interrupted calling " + url, e);
        }
    }
}
