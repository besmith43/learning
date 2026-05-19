package com.example.officetime.config;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;

@Controller
public class FrontendController {
    @GetMapping(value = {"/", "/app", "/app/**"})
    public String index() {
        return "forward:/index.html";
    }
}
