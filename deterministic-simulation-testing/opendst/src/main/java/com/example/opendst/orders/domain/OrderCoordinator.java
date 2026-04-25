package com.example.opendst.orders.domain;

import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

public final class OrderCoordinator {
    private static final int UNIT_PRICE = 100;

    private final InventoryPort inventory;
    private final PaymentPort payment;
    private final Map<String, OrderResult> completed = new ConcurrentHashMap<>();

    public OrderCoordinator(InventoryPort inventory, PaymentPort payment) {
        this.inventory = inventory;
        this.payment = payment;
    }

    public OrderResult place(OrderRequest request) {
        if (request == null || !request.isValid()) {
            return new OrderResult(request == null ? "" : request.orderId(), OrderStatus.REJECTED_INVALID, "invalid order");
        }
        return completed.computeIfAbsent(request.orderId(), ignored -> placeNew(request));
    }

    private OrderResult placeNew(OrderRequest request) {
        ReservationRequest reservation = new ReservationRequest(request.orderId(), request.sku(), request.quantity());
        ReservationResult reserved = inventory.reserve(reservation);
        if (!reserved.reserved()) {
            return new OrderResult(request.orderId(), OrderStatus.REJECTED_OUT_OF_STOCK, "not enough stock");
        }

        PaymentResult authorized = payment.authorize(new PaymentRequest(request.orderId(), request.paymentToken(),
                request.quantity() * UNIT_PRICE));
        if (!authorized.authorized()) {
            inventory.release(reservation);
            return new OrderResult(request.orderId(), OrderStatus.REJECTED_PAYMENT, authorized.reason());
        }

        return new OrderResult(request.orderId(), OrderStatus.CONFIRMED, "order confirmed");
    }
}
