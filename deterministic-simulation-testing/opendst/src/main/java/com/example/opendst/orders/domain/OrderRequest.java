package com.example.opendst.orders.domain;

public record OrderRequest(String orderId, String sku, int quantity, String paymentToken) {
    public boolean isValid() {
        return hasText(orderId) && hasText(sku) && quantity > 0 && hasText(paymentToken);
    }

    private static boolean hasText(String value) {
        return value != null && !value.isBlank();
    }
}
