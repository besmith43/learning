package com.example.opendst.orders.http;

import com.example.opendst.orders.domain.PaymentPort;
import com.example.opendst.orders.domain.PaymentRequest;
import com.example.opendst.orders.domain.PaymentResult;
import java.net.http.HttpClient;

public final class PaymentHttpClient implements PaymentPort {
    private final HttpClient client = HttpClient.newHttpClient();
    private final String baseUrl;

    public PaymentHttpClient(String baseUrl) {
        this.baseUrl = baseUrl;
    }

    @Override
    public PaymentResult authorize(PaymentRequest request) {
        return HttpJson.post(client, baseUrl + "/authorize", request, PaymentResult.class);
    }
}
