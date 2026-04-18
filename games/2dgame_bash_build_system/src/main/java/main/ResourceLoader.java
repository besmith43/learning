package main;

import javax.imageio.ImageIO;
import java.awt.image.BufferedImage;
import java.io.IOException;
import java.io.InputStream;
import java.net.URL;

public final class ResourceLoader {

    private ResourceLoader() {
    }

    public static InputStream openStream(String path) {
        InputStream inputStream = ResourceLoader.class.getResourceAsStream(path);
        if (inputStream == null) {
            throw new IllegalStateException("Missing resource on classpath: " + path);
        }
        return inputStream;
    }

    public static BufferedImage readImage(String path) throws IOException {
        try (InputStream inputStream = openStream(path)) {
            return ImageIO.read(inputStream);
        }
    }

    public static URL getResourceUrl(String path) {
        URL resourceUrl = ResourceLoader.class.getResource(path);
        if (resourceUrl == null) {
            throw new IllegalStateException("Missing resource on classpath: " + path);
        }
        return resourceUrl;
    }
}
