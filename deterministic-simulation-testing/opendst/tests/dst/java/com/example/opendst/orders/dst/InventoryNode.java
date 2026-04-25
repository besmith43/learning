package com.example.opendst.orders.dst;

import com.pingidentity.opendst.sdk.Signals;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.net.ServerSocket;
import java.util.HashMap;
import java.util.Map;

public final class InventoryNode {
    private final Map<String, Integer> stock = new HashMap<>();
    private final Map<String, Integer> reservations = new HashMap<>();

    private InventoryNode(int initialStock) {
        stock.put("ABC", initialStock);
    }

    public static void main(String[] args) throws Exception {
        int port = Integer.parseInt(args[0]);
        int initialStock = Integer.parseInt(args[1]);
        InventoryNode node = new InventoryNode(initialStock);

        try (ServerSocket server = new ServerSocket(port)) {
            Signals.ready();
            while (true) {
                var socket = server.accept();
                Thread.startVirtualThread(() -> {
                    try (socket;
                         var in = new DataInputStream(socket.getInputStream());
                         var out = new DataOutputStream(socket.getOutputStream())) {
                        node.handle(in, out);
                    } catch (Exception e) {
                        throw new IllegalStateException(e);
                    }
                });
            }
        }
    }

    private synchronized void handle(DataInputStream in, DataOutputStream out) throws Exception {
        String command = in.readUTF();
        if ("RESERVE".equals(command)) {
            String orderId = in.readUTF();
            int quantity = in.readInt();
            Integer existing = reservations.get(orderId);
            int available = stock.getOrDefault("ABC", 0);
            if (existing != null) {
                out.writeBoolean(existing == quantity);
                out.writeInt(available);
                return;
            }
            if (available >= quantity && quantity > 0) {
                reservations.put(orderId, quantity);
                stock.put("ABC", available - quantity);
                out.writeBoolean(true);
                out.writeInt(available - quantity);
            } else {
                out.writeBoolean(false);
                out.writeInt(available);
            }
        } else if ("RELEASE".equals(command)) {
            String orderId = in.readUTF();
            Integer quantity = reservations.remove(orderId);
            if (quantity != null) {
                stock.merge("ABC", quantity, Integer::sum);
            }
            out.writeBoolean(true);
            out.writeInt(stock.getOrDefault("ABC", 0));
        } else if ("STOCK".equals(command)) {
            out.writeInt(stock.getOrDefault("ABC", 0));
        } else {
            throw new IllegalArgumentException("unknown command " + command);
        }
    }
}
