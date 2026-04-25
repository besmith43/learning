package com.example.opendst.orders.dst;

import com.pingidentity.opendst.sdk.Signals;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.net.ServerSocket;

public final class PaymentNode {
    private PaymentNode() {
    }

    public static void main(String[] args) throws Exception {
        int port = Integer.parseInt(args[0]);
        try (ServerSocket server = new ServerSocket(port)) {
            Signals.ready();
            while (true) {
                var socket = server.accept();
                Thread.startVirtualThread(() -> {
                    try (socket;
                         var in = new DataInputStream(socket.getInputStream());
                         var out = new DataOutputStream(socket.getOutputStream())) {
                        in.readUTF();
                        String token = in.readUTF();
                        int amount = in.readInt();
                        out.writeBoolean(amount > 0 && token.startsWith("ok-"));
                    } catch (Exception e) {
                        throw new IllegalStateException(e);
                    }
                });
            }
        }
    }
}
