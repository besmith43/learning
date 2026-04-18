package object;

import main.ResourceLoader;

import java.io.IOException;

public class OBJ_Door extends SuperObject {

    public OBJ_Door() {

        name = "Door";

        try {
            image = ResourceLoader.readImage("/objects/door.png");
        } catch (IOException e) {
            e.printStackTrace();
        }

        collision = true;
    }
}
