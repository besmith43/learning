import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;

public class NativeOllamaExample {
    public static void main(String[] args) throws Exception {
        HttpClient client = HttpClient.newHttpClient();
        
        // Prepare the JSON body payload
        String jsonPayload = """
        {
          "model": "gemma4:12b-nvfp4",
          "prompt": "what is the series title, season number, and episode number in this directory name: My.Adventures.with.Superman.S03E04.1080p.WEB.h264-EDITH",
          "stream": false
        }
        """;

        HttpRequest request = HttpRequest.newBuilder()
                .uri(URI.create("http://localhost:11434/api/generate"))
                .header("Content-Type", "application/json")
                .POST(HttpRequest.BodyPublishers.ofString(jsonPayload))
                .build();

        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        // System.out.println(response.body());
        String[] body = response.body().split(",");

        for (String line : body) {
            System.out.println(line);
        }
    }
}
