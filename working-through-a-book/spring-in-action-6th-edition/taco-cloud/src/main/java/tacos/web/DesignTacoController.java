package tacos.web;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.SessionAttributes;
import tacos.Ingredient;
// import tacos.Ingredient.Type;
import tacos.Taco;
import tacos.TacoOrder;
import javax.validation.Valid;
import org.springframework.validation.Errors;
import tacos.User;
import tacos.data.IngredientRepository;
import tacos.data.TacoRepository;
import tacos.data.UserRepository;

import java.lang.reflect.Type;
import java.security.Principal;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Objects;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;

@Controller
@RequestMapping("/design")
@SessionAttributes("tacoOrder")
public class DesignTacoController {

    private static final org.slf4j.Logger log = org.slf4j.LoggerFactory.getLogger(DesignTacoController.class);
    private final IngredientRepository ingredientRepository;

    private TacoRepository tacoRepo;

    private UserRepository userRepo;

    @Autowired
    public DesignTacoController(
            IngredientRepository ingredientRepo,
            TacoRepository tacoRepo,
            UserRepository userRepo) {
        this.ingredientRepository = ingredientRepo;
        this.tacoRepo = tacoRepo;
        this.userRepo = userRepo;
    }

    @ModelAttribute
    public void addIngredientsToModel(Model model) {
        // updating for use with a db
//        List<Ingredient> ingredients = Arrays.asList(
//            new Ingredient("FLTO", "Flour Tortilla", Ingredient.Type.WRAP),
//            new Ingredient("COTO", "Corn Tortilla", Ingredient.Type.WRAP),
//            new Ingredient("GRBF", "Ground Beef", Ingredient.Type.PROTEIN),
//            new Ingredient("CARN", "Carnitas", Ingredient.Type.PROTEIN),
//            new Ingredient("TMTO", "Diced Tomatoes", Ingredient.Type.VEGGIES),
//            new Ingredient("LETC", "Lettuce", Ingredient.Type.VEGGIES),
//            new Ingredient("CHED", "Cheddar", Ingredient.Type.CHEESE),
//            new Ingredient("JACK", "Monterrey Jack", Ingredient.Type.CHEESE),
//            new Ingredient("SLSA", "Salsa", Ingredient.Type.SAUCE),
//            new Ingredient("SRCR", "Sour Cream", Ingredient.Type.SAUCE)
//        );

        Iterable<Ingredient> ingredients = ingredientRepository.findAll();

        Ingredient.Type[] types = Ingredient.Type.values();

        for (Ingredient.Type type : types) {
            model.addAttribute(
                    type.toString().toLowerCase(),
                    filterByType(ingredients, type)
            );
        }
    }

    @ModelAttribute(name = "user")
    public User user(Principal principal) {
        String username = principal.getName();
        User user = userRepo.findByUsername(username);
        return user;
    }


    @ModelAttribute(name = "tacoOrder")
    public TacoOrder order() {
        return new TacoOrder();
    }

    @ModelAttribute(name = "taco")
    public Taco taco() {
        return new Taco();
    }

    @GetMapping
    public String showDesignForm() {
        log.info("design form hit");

        return "design";
    }

    @PostMapping
    public String processTaco(@Valid Taco taco, Errors errors, @ModelAttribute TacoOrder tacoOrder) {
        log.info("process taco hit");

        if (errors.hasErrors()) {
            log.error(errors.getAllErrors().toString());
            return "design";
        }

        tacoOrder.addTaco(taco);
        log.info("Processing taco: {}", taco);

        return "redirect:/orders/current";
    }

    // old version for use with a list of ingredients instead of type Iterable<T>
//    private Iterable<Ingredient> filterByType(List<Ingredient> ingredients, Ingredient.Type type) {
//        return ingredients.stream()
//                .filter(x -> x.getType().equals(type))
//                .collect(Collectors.toList());
//    }

    private Iterable<Ingredient> filterByType(
            Iterable<Ingredient> ingredients,
            Ingredient.Type type
    ) {
        return StreamSupport.stream(ingredients.spliterator(), false)
                .filter(i -> i.getType().equals(type))
                .collect(Collectors.toList());
    }
}
