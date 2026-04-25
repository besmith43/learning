package com.example.opendst.orders.http;

import com.example.opendst.orders.domain.InventoryStore;
import java.util.Map;

public final class InventoryServerMain {
    private InventoryServerMain() {
    }

    public static void main(String[] args) throws Exception {
        int port = args.length > 0 ? Integer.parseInt(args[0]) : 18081;
        int stock = args.length > 1 ? Integer.parseInt(args[1]) : 10;
        new InventoryServer(port, new InventoryStore(Map.of("ABC", stock))).start();
        Thread.currentThread().join();
    }
}
