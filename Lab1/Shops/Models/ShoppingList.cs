using Shops.Exceptions;

namespace Shops.Models;

public class ShoppingList
{
    private readonly List<ShoppingListItem> _buy = new List<ShoppingListItem>();

    public IReadOnlyCollection<ShoppingListItem> Buy => _buy;

    public ShoppingListItem AddItem(Product product, int count)
    {
        ShoppingListItem item = FindProduct(product);
        if (count <= 0)
        {
            throw ShoppingListException.InvalidCountOfProducts();
        }

        if (item is not null)
        {
            item.IncreaseCount(count);
            return item;
        }
        else
        {
            _buy.Add(new ShoppingListItem(product, count));
            return _buy.Last();
        }
    }

    private ShoppingListItem FindProduct(Product product)
    {
        return _buy.FirstOrDefault(item => item.Product == product);
    }
}