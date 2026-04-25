package com.example.opendst.orders.domain;

public interface PaymentPort {
    PaymentResult authorize(PaymentRequest request);
}
