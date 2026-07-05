package org.example;


import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import dev.langchain4j.data.message.UserMessage;
import dev.langchain4j.model.chat.ChatModel;
import dev.langchain4j.model.chat.request.ChatRequest;
import dev.langchain4j.model.chat.request.ChatRequestParameters;
import dev.langchain4j.model.chat.request.ResponseFormat;
import dev.langchain4j.model.chat.request.ResponseFormatType;
import dev.langchain4j.model.chat.request.json.JsonObjectSchema;
import dev.langchain4j.model.chat.request.json.JsonSchema;
import dev.langchain4j.model.chat.response.ChatResponse;
import dev.langchain4j.model.ollama.OllamaChatModel;
import dev.langchain4j.service.AiServices;

import java.util.HashMap;

public class App {
    public static void main(String[] args) {
        // String model = "lfm2.5-thinking:latest";
        String model = "nemotron-3-nano:4b";
        // String model = "gemma4:12b-nvfp4";

        HashMap<String, String> movies = new HashMap<>();
        movies.put("Katrina Come Hell And High Water [2025] S01 1080p WEBRip 10bit EAC3 5 1 x265-iVy", "Katrina Come Hell And High Water (2025)");
        movies.put("Obsession.2025.NORDiC.1080p.WEB-DL.H.264-NORViNE", "Obsession (2025)");
        movies.put("Undertone.2025.2160p.iT.WEB-DL.DV.HDR10-BenTheMen-AsRequested", "Undertone (2025)");


        HashMap<String, String> tvShows = new HashMap<>();
        tvShows.put("My.Adventures.with.Superman.S03E04.1080p.WEB.h264-EDITH", "My Adventures with Superman - s03e04");
        tvShows.put("12.Monkeys.S04E01.The.End.1080p.BluRay.Dts-HDMa5.1.AVC-PiR8", "12 Monkeys - s04e01");
        tvShows.put("World.War.II.With.Tom.Hanks.S01E12.Battle.For.The.Skies.1080p.NOW.WEB-DL.AAC2.0.H.264-RAWR", "World War II With Tom Hanks - s01e12");
        tvShows.put("X-Men.The.Animated.Series.S05E07.Storm.Front.Part.1.480p.DVDRip.DD2.0.x264-SA89-BUYMORE", "X-Men The Animated Series - s05e07");


        for (String directory : movies.keySet()) {
            String response = AskOllamaMovie(model, directory);
            System.out.println("expected response: " + movies.get(directory));
            System.out.println("ollama response: " + response);
            System.out.println("matches: " + movies.get(directory).equals(response));
        }


        for (String directory : tvShows.keySet()) {
            String response = AskOllamaTVShow(model, directory);
            System.out.println("expected response: " + tvShows.get(directory));
            System.out.println("ollama response: " + response);
            System.out.println("matches: " + tvShows.get(directory).equals(response));
        }
    }


    static String AskOllamaMovie(String model, String movieDirectory) {
        String message = "what is the movie title and year in this directory name: %s\noutput in the following format: MOVIE TITLE (0000)".formatted(movieDirectory);

        ChatModel ollamaModel = OllamaChatModel.builder()
                .baseUrl("http://localhost:11434")
                .modelName(model)
                .build();

        // Send a prompt and print the response
        return ollamaModel.chat(message);
    }


    static String AskOllamaTVShow(String model, String tvShowDirectory) {
        String message = "what is the series title, season number, and episode number in this directory name: %s\noutput in the following format: SERIES TITLE - s00e00".formatted(tvShowDirectory);

        ChatModel ollamaModel = OllamaChatModel.builder()
                .baseUrl("http://localhost:11434")
                .modelName(model)
                .build();

        // Send a prompt and print the response
        return ollamaModel.chat(message);
    }
}
