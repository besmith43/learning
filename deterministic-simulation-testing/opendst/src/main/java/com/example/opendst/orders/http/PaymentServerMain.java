package com.example.opendst.orders.http;

import com.example.opendst.orders.domain.PaymentAuthorizer;

public final class PaymentServerMain {
    private PaymentServerMain() {
    }

    public static void main(String[] args) throws Exception {
        int port = args.length > 0 ? Integer.parseInt(args[0]) : 18082;
        new PaymentServer(port, new PaymentAuthorizer()).start();
        Thread.currentThread().join();
    }
}
