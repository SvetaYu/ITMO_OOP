namespace Shops.Exceptions;

public class ProductSetException : Exception
{
    private ProductSetException(string message)
        : base(message)
    {
    }

    public static ProductSetException InvalidPrice()
    {
        return new ProductSetException("price must not be less than 0");
    }

    public static ProductSetException InvalidCountOfProducts()
    {
        return new ProductSetException("count of products must not be less 1");
    }

    public static ProductSetException InvalidOperationWithCountOfProducts(int count)
    {
        return new ProductSetException($"not enough products to reduce their count by {count}");
    }
}