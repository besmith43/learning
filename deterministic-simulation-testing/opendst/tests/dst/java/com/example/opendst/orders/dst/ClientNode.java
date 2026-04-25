package com.example.opendst.orders.dst;

import com.pingidentity.opendst.sdk.Assert;
import com.pingidentity.opendst.sdk.Signals;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.net.Socket;

public final class ClientNode {
    private ClientNode() {
    }

    public static void main(String[] args) throws Exception {
        String gatewayHost = args[0];
        int gatewayPort = Integer.parseInt(args[1]);
        String inventoryHost = args[2];
        int inventoryPort = Integer.parseInt(args[3]);
        String prefix = args[4];

        Signals.ready();

        String confirmed = place(gatewayHost, gatewayPort, prefix + "-confirmed", 1, "ok-card");
        Assert.always("CONFIRMED".equals(confirmed), "client-can-confirm-order", null);

        String rejectedPayment = place(gatewayHost, gatewayPort, prefix + "-declined", 1, "bad-card");
        Assert.always("REJECTED_PAYMENT".equals(rejectedPayment), "payment-rejection-is-visible", null);

        String duplicateA = place(gatewayHost, gatewayPort, prefix + "-duplicate", 1, "ok-card");
        String duplicateB = place(gatewayHost, gatewayPort, prefix + "-duplicate", 1, "ok-card");
        Assert.always(duplicateA.equals(duplicateB), "duplicate-order-is-idempotent", null);

        int remaining = stock(inventoryHost, inventoryPort);
        Assert.always(remaining >= 0, "inventory-never-negative", null);
        Assert.reachable("client-finished", null);
    }

    private static String place(String host, int port, String orderId, int quantity, String token) throws Exception {
        try (Socket socket = new Socket(host, port);
             var in = new DataInputStream(socket.getInputStream());
             var out = new DataOutputStream(socket.getOutputStream())) {
            out.writeUTF(orderId);
            out.writeInt(quantity);
            out.writeUTF(token);
            return in.readUTF();
        }
    }

    private static int stock(String host, int port) throws Exception {
        try (Socket socket = new Socket(host, port);
             var in = new DataInputStream(socket.getInputStream());
             var out = new DataOutputStream(socket.getOutputStream())) {
            out.writeUTF("STOCK");
            return in.readInt();
        }
    }
}
