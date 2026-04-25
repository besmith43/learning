package com.example.opendst.orders.http;

import com.example.opendst.orders.domain.InventoryStore;
import com.example.opendst.orders.domain.OrderCoordinator;
import com.example.opendst.orders.domain.PaymentAuthorizer;
import java.util.Map;

public final class LocalClusterMain {
    private LocalClusterMain() {
    }

    public static void main(String[] args) throws Exception {
        int basePort = args.length > 0 ? Integer.parseInt(args[0]) : 18080;
        InventoryServer inventory = new InventoryServer(basePort + 1, new InventoryStore(Map.of("ABC", 3)));
        PaymentServer payment = new PaymentServer(basePort + 2, new PaymentAuthorizer());
        GatewayServer gateway = new GatewayServer(basePort,
                new OrderCoordinator(new InventoryHttpClient("http://127.0.0.1:" + (basePort + 1)),
                        new PaymentHttpClient("http://127.0.0.1:" + (basePort + 2))));

        inventory.start();
        payment.start();
        gateway.start();
        Runtime.getRuntime().addShutdownHook(new Thread(() -> {
            gateway.stop();
            payment.stop();
            inventory.stop();
        }));
        Thread.currentThread().join();
    }
}
