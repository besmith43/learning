package object;

import main.ResourceLoader;

import java.io.IOException;

public class OBJ_Key extends SuperObject{
    public OBJ_Key() {

        name = "Key";

        try {
            image = ResourceLoader.readImage("/objects/key.png");
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
