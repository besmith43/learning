package com.example.opendst.orders;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertTrue;

import com.example.opendst.orders.domain.InventoryStore;
import com.example.opendst.orders.domain.OrderCoordinator;
import com.example.opendst.orders.domain.OrderRequest;
import com.example.opendst.orders.domain.OrderStatus;
import com.example.opendst.orders.domain.PaymentAuthorizer;
import java.util.Map;
import net.jqwik.api.ForAll;
import net.jqwik.api.Property;
import net.jqwik.api.constraints.IntRange;

class OrderWorkflowProperties {
    @Property
    void stockNeverGoesNegative(
            @ForAll @IntRange(min = 0, max = 20) int initialStock,
            @ForAll @IntRange(min = 1, max = 10) int quantity,
            @ForAll boolean paymentAccepted) {
        InventoryStore inventory = new InventoryStore(Map.of("ABC", initialStock));
        OrderCoordinator coordinator = new OrderCoordinator(inventory, new PaymentAuthorizer());
        String token = paymentAccepted ? "ok-generated" : "declined";

        var result = coordinator.place(new OrderRequest("order-1", "ABC", quantity, token));

        assertTrue(inventory.available("ABC") >= 0);
        if (result.status() == OrderStatus.CONFIRMED) {
            assertEquals(initialStock - quantity, inventory.available("ABC"));
        }
        if (result.status() == OrderStatus.REJECTED_PAYMENT) {
            assertEquals(initialStock, inventory.available("ABC"));
        }
    }

    @Property
    void duplicateOrderDoesNotReserveTwice(
            @ForAll @IntRange(min = 1, max = 20) int initialStock,
            @ForAll @IntRange(min = 1, max = 10) int quantity) {
        InventoryStore inventory = new InventoryStore(Map.of("ABC", initialStock));
        OrderCoordinator coordinator = new OrderCoordinator(inventory, new PaymentAuthorizer());
        OrderRequest request = new OrderRequest("same-order", "ABC", quantity, "ok-generated");

        var first = coordinator.place(request);
        var second = coordinator.place(request);

        assertEquals(first, second);
        if (quantity <= initialStock) {
            assertEquals(initialStock - quantity, inventory.available("ABC"));
        } else {
            assertEquals(initialStock, inventory.available("ABC"));
        }
    }
}
