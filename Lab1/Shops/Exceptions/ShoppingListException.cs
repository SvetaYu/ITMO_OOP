namespace Shops.Exceptions;

public class ShoppingListException : Exception
{
    private ShoppingListException(string message)
        : base(message)
    {
    }

    public static ShoppingListException InvalidCountOfProducts()
    {
        return new ShoppingListException("count of products must not be less 1");
    }
}