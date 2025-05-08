import lombok.Getter;
import lombok.Setter;

// package pets;

public class Pet {
   @Getter @Setter public String Name; 

   @Override public String toString() {
        return String.format("%s", Name);
   }
}
