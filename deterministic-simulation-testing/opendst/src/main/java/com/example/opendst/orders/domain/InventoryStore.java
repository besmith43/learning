package com.example.opendst.orders.domain;

import java.util.HashMap;
import java.util.Map;

public final class InventoryStore implements InventoryPort {
    private final Map<String, Integer> stock = new HashMap<>();
    private final Map<String, ReservationRequest> reservations = new HashMap<>();

    public InventoryStore(Map<String, Integer> initialStock) {
        initialStock.forEach((sku, count) -> stock.put(sku, Math.max(0, count)));
    }

    @Override
    public synchronized ReservationResult reserve(ReservationRequest request) {
        ReservationRequest existing = reservations.get(request.orderId());
        if (existing != null) {
            return new ReservationResult(existing.sku().equals(request.sku()) && existing.quantity() == request.quantity(),
                    stock.getOrDefault(request.sku(), 0));
        }

        int available = stock.getOrDefault(request.sku(), 0);
        if (request.quantity() <= 0 || available < request.quantity()) {
            return new ReservationResult(false, available);
        }

        stock.put(request.sku(), available - request.quantity());
        reservations.put(request.orderId(), request);
        return new ReservationResult(true, available - request.quantity());
    }

    @Override
    public synchronized void release(ReservationRequest request) {
        ReservationRequest existing = reservations.remove(request.orderId());
        if (existing != null) {
            stock.merge(existing.sku(), existing.quantity(), Integer::sum);
        }
    }

    public synchronized int available(String sku) {
        return stock.getOrDefault(sku, 0);
    }
}
