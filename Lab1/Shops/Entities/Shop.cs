using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Shop
{
    private List<ProductSet> _products = new List<ProductSet>();

    internal Shop(string name, string address)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Address = address ?? throw new ArgumentNullException(nameof(address));
        Id = Guid.NewGuid();
    }

    public string Name { get; }
    public Guid Id { get; }
    public string Address { get; }

    public IReadOnlyCollection<ProductSet> Products => _products;

    public ProductSet FindProductSet(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        return _products.FirstOrDefault(products => products.Product == product);
    }

    public ProductSet FindProductSet(Guid id)
    {
        return _products.FirstOrDefault(products => products.Product.Id == id);
    }

    internal ProductSet AddProducts(Product product, decimal price, int count)
    {
        ArgumentNullException.ThrowIfNull(product);

        if (count < 1)
        {
            throw ShopException.InvalidCountOfProducts();
        }

        if (price < 0)
        {
            throw ShopException.InvalidPrice();
        }

        ProductSet productSet = FindProductSet(product);
        if (productSet is null)
        {
            _products.Add(new ProductSet(product, price, count));
            return _products.Last();
        }

        productSet.IncreaseCount(count);
        productSet.ChangePrice(price);
        return productSet;
    }

    internal ProductSet AddProduct(Product product, decimal price)
    {
        return AddProducts(product, price, 1);
    }

    internal void ReduceTheNumberOfProducts(Product product, int value)
    {
        ArgumentNullException.ThrowIfNull(product);

        ProductSet set = FindProductSet(product);
        if (set is null)
        {
            throw ShopException.ProductNotFound();
        }

        set.ReduceCount(value);
        if (set.Count <= 0)
        {
            _products.Remove(set);
        }
    }
}