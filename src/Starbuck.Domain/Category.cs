using System.Diagnostics.CodeAnalysis;

namespace Starbuck.Domain;
public class Category
{
    [SetsRequiredMembers] //acepte valores nulos en propiedades required
    private Category(int id, string name) => (Id, Name) = (id, name);

    public int Id { get; set; }
    public required string Name { get; set; } 
    public string? Description { get; set; }
    public ICollection<Coffe> Coffes { get; set; } = [];


    public static Category Create(int id)
    {
        var categoryName = (CategoryEnum)id;
        string categotyNameString = categoryName.ToString();

        return new Category(id, categotyNameString);
    }
}

