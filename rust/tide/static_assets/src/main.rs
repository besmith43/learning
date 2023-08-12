use tide::prelude::*;
use async_std::prelude::*;
use tide::Request;
use anyhow::Result;
use thiserror::Error;

#[async_std::main]
async fn main() -> Result<()> {
    let mut app = tide::new();

    app.with(tide::log::LogMiddleware::new());
    // app.at("/www").serve_dir("www/")?; // this causes the api to fail unless it's something other than root

    let mut api = tide::new();
    api.at("/hello").get(hello);
    api.at("/hello2").get(|_| async { Ok("Hello, world from my api!") });

    app.at("/api").nest(api);

    app.listen("0.0.0.0:8080").await?;

    Ok(())
}

async fn hello(req: Request<()>) -> tide::Result<String> {
    Ok("Hello, world from my api!".to_string())
}
