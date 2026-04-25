package com.example.opendst.orders.http;

import com.example.opendst.orders.domain.OrderCoordinator;
import com.example.opendst.orders.domain.OrderRequest;
import com.sun.net.httpserver.HttpServer;
import java.io.IOException;
import java.net.InetSocketAddress;
import java.util.Map;
import java.util.concurrent.Executors;

public final class GatewayServer {
    private final HttpServer server;

    public GatewayServer(int port, OrderCoordinator coordinator) throws IOException {
        this.server = HttpServer.create(new InetSocketAddress(port), 0);
        server.setExecutor(Executors.newVirtualThreadPerTaskExecutor());
        server.createContext("/health", exchange -> HttpJson.write(exchange, 200, Map.of("status", "UP")));
        server.createContext("/orders", exchange -> {
            if (!"POST".equals(exchange.getRequestMethod())) {
                HttpJson.write(exchange, 405, Map.of("error", "method not allowed"));
                return;
            }
            OrderRequest request = HttpJson.read(exchange, OrderRequest.class);
            HttpJson.write(exchange, request.isValid() ? 200 : 400, coordinator.place(request));
        });
    }

    public void start() {
        server.start();
    }

    public void stop() {
        server.stop(0);
    }
}
