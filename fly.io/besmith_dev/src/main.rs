use askama::Template;

use axum::{
    http::StatusCode,
    response::{Html, IntoResponse, Response},
    routing::get,
    Router,
};
use tower_http::services::ServeDir;

#[tokio::main]
async fn main() {
    // initialize tracing
    tracing_subscriber::fmt::init();

    // build our application with a route
    let app = Router::new()
        // `GET /` goes to `root`
        .route("/", get(root))
        .route("/btn", get(get_button))
        .route("/list", get(get_list))
        .nest_service("/assets", ServeDir::new("./assets"));

    // run our app with hyper, listening globally on port 3000
    let listener = tokio::net::TcpListener::bind("0.0.0.0:3000").await.unwrap();
    axum::serve(listener, app).await.unwrap();
}

// basic handler that responds with a static string
async fn root() -> impl IntoResponse {
    let index = IndexTemplate {};
    HtmlTemplate(index)
}

#[derive(Template)]
#[template(path = "index.html")]
struct IndexTemplate;

async fn get_button() -> impl IntoResponse {
    let btn_template = ButtonTemplate {
        label: "does nothing".to_string(),
    };
    HtmlTemplate(btn_template)
}

#[derive(Template)]
#[template(path = "button.html")]
struct ButtonTemplate {
    label: String,
}

async fn get_list() -> impl IntoResponse {
    let list_template = ListTemplate {
        list: vec![
            "item 1".to_string(),
            "item 2".to_string(),
            "item 3".to_string(),
        ],
    };
    HtmlTemplate(list_template)
}

#[derive(Template)]
#[template(path = "list.html")]
struct ListTemplate {
    list: Vec<String>,
}

struct HtmlTemplate<T>(T);

impl<T> IntoResponse for HtmlTemplate<T>
where
    T: Template,
{
    fn into_response(self) -> Response {
        match self.0.render() {
            Ok(html) => Html(html).into_response(),
            Err(err) => (
                StatusCode::INTERNAL_SERVER_ERROR,
                format!("Failed to render template. Error: {err}"),
            )
                .into_response(),
        }
    }
}
