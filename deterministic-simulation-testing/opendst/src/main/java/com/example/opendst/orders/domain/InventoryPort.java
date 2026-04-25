package com.example.opendst.orders.domain;

public interface InventoryPort {
    ReservationResult reserve(ReservationRequest request);

    void release(ReservationRequest request);
}
