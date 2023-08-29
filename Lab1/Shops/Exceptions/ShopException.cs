namespace Shops.Exceptions;

public class ShopException : Exception
{
    private ShopException(string message)
        : base(message)
    {
    }

    public static ShopException InvalidShopName()
    {
        return new ShopException("invalid shop exception");
    }

    public static ShopException InvalidAddress()
    {
        return new ShopException("invalid shops address");
    }

    public static ShopException InvalidPrice()
    {
        return new ShopException("price must not be less than 0");
    }

    public static ShopException InvalidCountOfProducts()
    {
        return new ShopException("count of products must not be less 1");
    }

    public static ShopException ProductNotFound()
    {
        return new ShopException("this product is not in the shop");
    }
}