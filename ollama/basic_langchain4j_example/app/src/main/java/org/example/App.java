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


public class App {
    public static void main(String[] args) {
        // Configure the model connection
        ChatModel model = OllamaChatModel.builder()
                .baseUrl("http://localhost:11434")
                .modelName("gemma4:12b-nvfp4")
                .build();

        // Send a prompt and print the response
        String response = model.chat("what is the series title, season number, and episode number in this directory name: My.Adventures.with.Superman.S03E04.1080p.WEB.h264-EDITH\noutput in the following format: SERIES TITLE - s00e00");
        System.out.println(response);
    }
}
