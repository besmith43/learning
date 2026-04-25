package com.example.opendst.orders.http;

import com.example.opendst.orders.domain.OrderCoordinator;

public final class GatewayServerMain {
    private GatewayServerMain() {
    }

    public static void main(String[] args) throws Exception {
        int port = args.length > 0 ? Integer.parseInt(args[0]) : 18080;
        String inventoryUrl = args.length > 1 ? args[1] : "http://127.0.0.1:18081";
        String paymentUrl = args.length > 2 ? args[2] : "http://127.0.0.1:18082";
        OrderCoordinator coordinator = new OrderCoordinator(new InventoryHttpClient(inventoryUrl),
                new PaymentHttpClient(paymentUrl));
        new GatewayServer(port, coordinator).start();
        Thread.currentThread().join();
    }
}
