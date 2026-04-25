package com.example.opendst.orders.domain;

public final class PaymentAuthorizer implements PaymentPort {
    @Override
    public PaymentResult authorize(PaymentRequest request) {
        if (request.amount() <= 0) {
            return new PaymentResult(false, "amount must be positive");
        }
        if (request.paymentToken() != null && request.paymentToken().startsWith("ok-")) {
            return new PaymentResult(true, "authorized");
        }
        return new PaymentResult(false, "payment declined");
    }
}
