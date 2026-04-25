package com.example.opendst.orders.domain;

public record PaymentRequest(String orderId, String paymentToken, int amount) {
}
