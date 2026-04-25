const { test, expect } = require('@playwright/test');

test('confirms an order when inventory and payment succeed', async ({ request }) => {
  const response = await request.post('/orders', {
    data: { orderId: 'e2e-success', sku: 'ABC', quantity: 1, paymentToken: 'ok-card' }
  });

  expect(response.ok()).toBeTruthy();
  expect(await response.json()).toEqual(expect.objectContaining({ status: 'CONFIRMED' }));
});

test('rejects an order when inventory is unavailable', async ({ request }) => {
  const response = await request.post('/orders', {
    data: { orderId: 'e2e-oos', sku: 'ABC', quantity: 99, paymentToken: 'ok-card' }
  });

  expect(response.ok()).toBeTruthy();
  expect(await response.json()).toEqual(expect.objectContaining({ status: 'REJECTED_OUT_OF_STOCK' }));
});

test('rejects payment and releases inventory', async ({ request }) => {
  const rejected = await request.post('/orders', {
    data: { orderId: 'e2e-payment-fail', sku: 'ABC', quantity: 1, paymentToken: 'bad-card' }
  });
  expect(rejected.ok()).toBeTruthy();
  expect(await rejected.json()).toEqual(expect.objectContaining({ status: 'REJECTED_PAYMENT' }));

  const confirmed = await request.post('/orders', {
    data: { orderId: 'e2e-after-release', sku: 'ABC', quantity: 1, paymentToken: 'ok-card' }
  });
  expect(confirmed.ok()).toBeTruthy();
  expect(await confirmed.json()).toEqual(expect.objectContaining({ status: 'CONFIRMED' }));
});

test('rejects invalid input', async ({ request }) => {
  const response = await request.post('/orders', {
    data: { orderId: 'e2e-invalid', sku: 'ABC', quantity: 0, paymentToken: 'ok-card' }
  });

  expect(response.status()).toBe(400);
  expect(await response.json()).toEqual(expect.objectContaining({ status: 'REJECTED_INVALID' }));
});
