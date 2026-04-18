package main;

import java.net.URL;
import javax.sound.sampled.Clip;
import javax.sound.sampled.AudioInputStream;
import javax.sound.sampled.AudioSystem;

public class Sound {
    Clip clip;
    URL soundURL[] = new URL[30];

    public Sound() {
        soundURL[0] = ResourceLoader.getResourceUrl("/sound/BlueBoyAdventure.wav");
        soundURL[1] = ResourceLoader.getResourceUrl("/sound/coin.wav");
        soundURL[2] = ResourceLoader.getResourceUrl("/sound/powerup.wav");
        soundURL[3] = ResourceLoader.getResourceUrl("/sound/unlock.wav");
        soundURL[4] = ResourceLoader.getResourceUrl("/sound/fanfare.wav");
    }

    public void setFile(int i) {
        try {
            AudioInputStream ais = AudioSystem.getAudioInputStream(soundURL[i]);
            clip = AudioSystem.getClip();
            clip.open(ais);
        } catch(Exception e) {}
    }

    public void play() {
        clip.start();
    }

    public void loop() {
        clip.loop(Clip.LOOP_CONTINUOUSLY);
    }
    
    public void stop() {
        clip.stop();
    }
}
