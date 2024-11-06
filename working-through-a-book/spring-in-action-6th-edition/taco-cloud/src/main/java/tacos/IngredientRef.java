package tacos;

import org.springframework.data.relational.core.mapping.Table;

import java.util.Objects;

@Table
public class IngredientRef {

    private final String ingredient;

    public IngredientRef(String ingredient) {
        this.ingredient = ingredient;
    }

    public String getIngredient() {
        return ingredient;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        IngredientRef that = (IngredientRef) o;
        return Objects.equals(ingredient, that.ingredient);
    }

    @Override
    public int hashCode() {
        return Objects.hashCode(ingredient);
    }
}
