package object;

import main.ResourceLoader;

import java.io.IOException;

public class OBJ_Chest extends SuperObject {

    public OBJ_Chest() {

        name = "Chest";

        try {
            image = ResourceLoader.readImage("/objects/chest.png");
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
