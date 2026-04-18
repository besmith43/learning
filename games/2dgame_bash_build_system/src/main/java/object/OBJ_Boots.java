package object;

import main.ResourceLoader;

import java.io.IOException;

public class OBJ_Boots extends SuperObject {
    public OBJ_Boots() {

        name = "Boots";

        try {
            image = ResourceLoader.readImage("/objects/boots.png");
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
