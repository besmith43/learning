package com.example.opendst.orders.http;

import com.example.opendst.orders.domain.PaymentAuthorizer;
import com.example.opendst.orders.domain.PaymentRequest;
import com.sun.net.httpserver.HttpServer;
import java.io.IOException;
import java.net.InetSocketAddress;
import java.util.Map;
import java.util.concurrent.Executors;

public final class PaymentServer {
    private final HttpServer server;
    private final PaymentAuthorizer payment;

    public PaymentServer(int port, PaymentAuthorizer payment) throws IOException {
        this.payment = payment;
        this.server = HttpServer.create(new InetSocketAddress(port), 0);
        server.setExecutor(Executors.newVirtualThreadPerTaskExecutor());
        server.createContext("/health", exchange -> HttpJson.write(exchange, 200, Map.of("status", "UP")));
        server.createContext("/authorize", exchange -> {
            PaymentRequest request = HttpJson.read(exchange, PaymentRequest.class);
            HttpJson.write(exchange, 200, payment.authorize(request));
        });
    }

    public void start() {
        server.start();
    }

    public void stop() {
        server.stop(0);
    }
}
