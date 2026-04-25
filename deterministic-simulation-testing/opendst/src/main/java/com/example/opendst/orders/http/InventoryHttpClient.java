package com.example.opendst.orders.http;

import com.example.opendst.orders.domain.InventoryPort;
import com.example.opendst.orders.domain.ReservationRequest;
import com.example.opendst.orders.domain.ReservationResult;
import java.net.http.HttpClient;

public final class InventoryHttpClient implements InventoryPort {
    private final HttpClient client = HttpClient.newHttpClient();
    private final String baseUrl;

    public InventoryHttpClient(String baseUrl) {
        this.baseUrl = baseUrl;
    }

    @Override
    public ReservationResult reserve(ReservationRequest request) {
        return HttpJson.post(client, baseUrl + "/reserve", request, ReservationResult.class);
    }

    @Override
    public void release(ReservationRequest request) {
        HttpJson.post(client, baseUrl + "/release", request, ReservationResult.class);
    }
}
