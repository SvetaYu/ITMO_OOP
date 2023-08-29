namespace Shops.Exceptions;

public class ShoppingListItemException : Exception
{
    private ShoppingListItemException(string message)
        : base(message)
    {
    }

    public static ShoppingListItemException InvalidCountOfProducts()
    {
        return new ShoppingListItemException("count of products must not be less 1");
    }
}