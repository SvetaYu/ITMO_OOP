namespace Shops.Exceptions;

public class ShopManagerException : Exception
{
    private ShopManagerException(string message)
        : base(message)
    {
    }

    public static ShopManagerException ProductNotFound()
    {
        return new ShopManagerException("there isn't product");
    }

    public static ShopManagerException NotEnoughProduct()
    {
        return new ShopManagerException("not enough product");
    }

    public static ShopManagerException NotEnoughMoney()
    {
        return new ShopManagerException("not enough money to buy products");
    }

    public static ShopManagerException SuitableShopNotFound()
    {
        return new ShopManagerException("no suitable shop");
    }

    public static ShopManagerException FailedToBuyProducts()
    {
        return new ShopManagerException("Failed To Buy Products");
    }
}