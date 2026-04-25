package com.example.opendst.orders;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertTrue;

import com.example.opendst.orders.domain.InventoryStore;
import com.example.opendst.orders.domain.OrderCoordinator;
import com.example.opendst.orders.domain.OrderRequest;
import com.example.opendst.orders.domain.OrderStatus;
import com.example.opendst.orders.domain.PaymentAuthorizer;
import java.util.Map;
import org.junit.jupiter.api.Test;

class OrderCoordinatorTest {
    @Test
    void confirmsOrderWhenInventoryAndPaymentSucceed() {
        InventoryStore inventory = new InventoryStore(Map.of("ABC", 5));
        OrderCoordinator coordinator = new OrderCoordinator(inventory, new PaymentAuthorizer());

        var result = coordinator.place(new OrderRequest("order-1", "ABC", 2, "ok-card"));

        assertEquals(OrderStatus.CONFIRMED, result.status());
        assertEquals(3, inventory.available("ABC"));
    }

    @Test
    void rejectsOutOfStockWithoutChangingInventory() {
        InventoryStore inventory = new InventoryStore(Map.of("ABC", 1));
        OrderCoordinator coordinator = new OrderCoordinator(inventory, new PaymentAuthorizer());

        var result = coordinator.place(new OrderRequest("order-1", "ABC", 2, "ok-card"));

        assertEquals(OrderStatus.REJECTED_OUT_OF_STOCK, result.status());
        assertEquals(1, inventory.available("ABC"));
    }

    @Test
    void releasesInventoryWhenPaymentFails() {
        InventoryStore inventory = new InventoryStore(Map.of("ABC", 2));
        OrderCoordinator coordinator = new OrderCoordinator(inventory, new PaymentAuthorizer());

        var result = coordinator.place(new OrderRequest("order-1", "ABC", 2, "bad-card"));

        assertEquals(OrderStatus.REJECTED_PAYMENT, result.status());
        assertEquals(2, inventory.available("ABC"));
    }

    @Test
    void duplicateOrderIdIsIdempotent() {
        InventoryStore inventory = new InventoryStore(Map.of("ABC", 2));
        OrderCoordinator coordinator = new OrderCoordinator(inventory, new PaymentAuthorizer());

        var first = coordinator.place(new OrderRequest("order-1", "ABC", 1, "ok-card"));
        var second = coordinator.place(new OrderRequest("order-1", "ABC", 1, "ok-card"));

        assertEquals(first, second);
        assertTrue(first.confirmed());
        assertEquals(1, inventory.available("ABC"));
    }
}
