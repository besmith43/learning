package com.example.opendst.orders.domain;

public record OrderResult(String orderId, OrderStatus status, String message) {
    public boolean confirmed() {
        return status == OrderStatus.CONFIRMED;
    }
}
