package com.example.opendst.orders.domain;

public record PaymentResult(boolean authorized, String reason) {
}
