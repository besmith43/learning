package com.example.opendst.orders.domain;

public record ReservationRequest(String orderId, String sku, int quantity) {
}
