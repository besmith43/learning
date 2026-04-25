package com.example.opendst.orders.dst;

import com.pingidentity.opendst.sdk.Signals;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

public final class GatewayNode {
    private final String inventoryHost;
    private final int inventoryPort;
    private final String paymentHost;
    private final int paymentPort;
    private final Map<String, String> completed = new ConcurrentHashMap<>();

    private GatewayNode(String inventoryHost, int inventoryPort, String paymentHost, int paymentPort) {
        this.inventoryHost = inventoryHost;
        this.inventoryPort = inventoryPort;
        this.paymentHost = paymentHost;
        this.paymentPort = paymentPort;
    }

    public static void main(String[] args) throws Exception {
        int port = Integer.parseInt(args[0]);
        GatewayNode node = new GatewayNode(args[1], Integer.parseInt(args[2]), args[3], Integer.parseInt(args[4]));
        try (ServerSocket server = new ServerSocket(port)) {
            Signals.ready();
            while (true) {
                var socket = server.accept();
                Thread.startVirtualThread(() -> {
                    try (socket;
                         var in = new DataInputStream(socket.getInputStream());
                         var out = new DataOutputStream(socket.getOutputStream())) {
                        String orderId = in.readUTF();
                        int quantity = in.readInt();
                        String token = in.readUTF();
                        out.writeUTF(node.place(orderId, quantity, token));
                    } catch (Exception e) {
                        throw new IllegalStateException(e);
                    }
                });
            }
        }
    }

    private String place(String orderId, int quantity, String token) {
        return completed.computeIfAbsent(orderId, ignored -> placeNew(orderId, quantity, token));
    }

    private String placeNew(String orderId, int quantity, String token) {
        if (!reserve(orderId, quantity)) {
            return "REJECTED_OUT_OF_STOCK";
        }
        if (!authorize(token, quantity * 100)) {
            release(orderId);
            return "REJECTED_PAYMENT";
        }
        return "CONFIRMED";
    }

    private boolean reserve(String orderId, int quantity) {
        return withSocket(inventoryHost, inventoryPort, (in, out) -> {
            out.writeUTF("RESERVE");
            out.writeUTF(orderId);
            out.writeInt(quantity);
            return in.readBoolean();
        });
    }

    private void release(String orderId) {
        withSocket(inventoryHost, inventoryPort, (in, out) -> {
            out.writeUTF("RELEASE");
            out.writeUTF(orderId);
            in.readBoolean();
            return null;
        });
    }

    private boolean authorize(String token, int amount) {
        return withSocket(paymentHost, paymentPort, (in, out) -> {
            out.writeUTF("AUTHORIZE");
            out.writeUTF(token);
            out.writeInt(amount);
            return in.readBoolean();
        });
    }

    private static <T> T withSocket(String host, int port, SocketCall<T> call) {
        try (Socket socket = new Socket(host, port);
             var in = new DataInputStream(socket.getInputStream());
             var out = new DataOutputStream(socket.getOutputStream())) {
            return call.exchange(in, out);
        } catch (Exception e) {
            throw new IllegalStateException(e);
        }
    }

    @FunctionalInterface
    private interface SocketCall<T> {
        T exchange(DataInputStream in, DataOutputStream out) throws Exception;
    }
}
