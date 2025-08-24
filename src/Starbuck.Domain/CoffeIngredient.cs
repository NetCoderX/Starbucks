namespace Starbuck.Domain;
public class CoffeIngredient
{
    public Guid CoffeId { get; set; }
    public Guid IngredientId { get; set; }
    public Ingredient? Ingredient { get; set; }
    public Coffe? Coffe { get; set; }
}
