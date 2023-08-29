using Shops.Exceptions;

namespace Shops.Models;

public class ShoppingListItem
{
    internal ShoppingListItem(Product product, int count)
    {
        if (count < 1)
        {
            throw ShoppingListItemException.InvalidCountOfProducts();
        }

        Product = product ?? throw new ArgumentNullException(nameof(product));
        Count = count;
    }

    public Product Product { get; }
    public int Count { get; private set; }

    internal void IncreaseCount(int value)
    {
        Count += value;
    }
}