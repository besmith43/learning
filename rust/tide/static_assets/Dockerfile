FROM rust:latest as builder

WORKDIR /build_repo

COPY Cargo.toml ./
COPY src/ ./src/

RUN cargo build --release

FROM nginx:latest

COPY nginx.conf /etc/nginx/nginx.conf

COPY --from=builder /build_repo/target/release/static_assets /app/static_assets
COPY www/ /var/www/html/

WORKDIR /app

RUN [ "/app/static_assets" ]
