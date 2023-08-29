using Shops.Exceptions;

namespace Shops.Models;

public class ProductSet
{
    internal ProductSet(Product product, decimal price, int count)
    {
        if (price < 0)
        {
            throw ProductSetException.InvalidPrice();
        }

        if (count < 1)
        {
            throw ProductSetException.InvalidCountOfProducts();
        }

        Product = product ?? throw new ArgumentNullException(nameof(product));
        Price = price;
        Count = count;
    }

    public Product Product { get; }
    public decimal Price { get; private set; }
    public int Count { get; private set; }

    internal void ReduceCount(int value)
    {
        if (value < 0)
        {
            throw ProductSetException.InvalidOperationWithCountOfProducts(value);
        }

        if (Count - value < 0)
        {
            throw ProductSetException.InvalidOperationWithCountOfProducts(value);
        }

        Count -= value;
    }

    internal void IncreaseCount(int value)
    {
        if (value < 0)
        {
            throw ProductSetException.InvalidOperationWithCountOfProducts(value);
        }

        Count += value;
    }

    internal void ChangePrice(decimal newPrice)
    {
        if (newPrice < 0)
        {
            throw ProductSetException.InvalidPrice();
        }

        Price = newPrice;
    }
}