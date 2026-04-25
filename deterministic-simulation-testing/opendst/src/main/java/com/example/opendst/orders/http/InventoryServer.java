package com.example.opendst.orders.http;

import com.example.opendst.orders.domain.InventoryStore;
import com.example.opendst.orders.domain.ReservationRequest;
import com.example.opendst.orders.domain.ReservationResult;
import com.sun.net.httpserver.HttpServer;
import java.io.IOException;
import java.net.InetSocketAddress;
import java.util.Map;
import java.util.concurrent.Executors;

public final class InventoryServer {
    private final HttpServer server;
    private final InventoryStore inventory;

    public InventoryServer(int port, InventoryStore inventory) throws IOException {
        this.inventory = inventory;
        this.server = HttpServer.create(new InetSocketAddress(port), 0);
        server.setExecutor(Executors.newVirtualThreadPerTaskExecutor());
        server.createContext("/health", exchange -> HttpJson.write(exchange, 200, Map.of("status", "UP")));
        server.createContext("/reserve", exchange -> {
            ReservationRequest request = HttpJson.read(exchange, ReservationRequest.class);
            HttpJson.write(exchange, 200, inventory.reserve(request));
        });
        server.createContext("/release", exchange -> {
            ReservationRequest request = HttpJson.read(exchange, ReservationRequest.class);
            inventory.release(request);
            HttpJson.write(exchange, 200, new ReservationResult(true, inventory.available(request.sku())));
        });
        server.createContext("/stock", exchange -> HttpJson.write(exchange, 200, Map.of("ABC", inventory.available("ABC"))));
    }

    public void start() {
        server.start();
    }

    public void stop() {
        server.stop(0);
    }
}
