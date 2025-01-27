FROM lukemathwalker/cargo-chef:latest-rust-1 AS chef
WORKDIR /app

FROM chef AS planner
COPY . .
RUN cargo chef prepare --recipe-path recipe.json

FROM chef AS builder
COPY --from=planner /app/recipe.json recipe.json
# Build dependencies - this is the caching Docker layer!
RUN cargo chef cook --release --recipe-path recipe.json
# Build application
COPY . .
RUN cargo build --release --bin besmith_dev

# We do not need the Rust toolchain to run the binary!
FROM debian:bookworm-slim AS runtime
WORKDIR /usr/local/bin
COPY --from=builder /app/target/release/besmith_dev /usr/local/bin
COPY --from=builder /app/assets /usr/local/bin/assets
ENTRYPOINT ["/usr/local/bin/besmith_dev"]
