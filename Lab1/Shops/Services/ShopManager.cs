using Shops.Entities;
using Shops.Exceptions;

namespace Shops.Services;

using Models;

public class ShopManager
{
    private readonly List<Shop> _shops = new List<Shop>();

    public Shop AddShop(string name, string address)
    {
        _shops.Add(new Shop(name, address));
        return _shops.Last();
    }

    public ProductSet AddProductsInShop(Shop shop, Product product, decimal price, int count)
    {
        ArgumentNullException.ThrowIfNull(shop);
        return shop.AddProducts(product, price, count);
    }

    public ProductSet AddProductInShop(Shop shop, Product product, decimal price)
    {
        return AddProductsInShop(shop, product, price, 1);
    }

    public void ChangePrice(Shop shop, Product product, decimal newPrice)
    {
        ArgumentNullException.ThrowIfNull(shop);
        shop.FindProductSet(product).ChangePrice(newPrice);
    }

    public Shop FindShop(Guid id)
    {
        return _shops.Find(shop => shop.Id == id);
    }

    public void BuyProduct(Buyer buyer, Shop shop, Product product)
    {
        BuyProducts(buyer, shop, product, 1);
    }

    public void BuyProducts(Buyer buyer, Shop shop, ShoppingList shoppingList)
    {
        ArgumentNullException.ThrowIfNull(buyer);
        ArgumentNullException.ThrowIfNull(shop);
        ArgumentNullException.ThrowIfNull(shoppingList);

        decimal sumPrice = ShoppingListPrice(shop, shoppingList) ?? throw ShopManagerException.FailedToBuyProducts();
        if (sumPrice > buyer.Money)
        {
            throw ShopManagerException.FailedToBuyProducts();
        }

        buyer.TransferMoney(sumPrice);
        foreach (ShoppingListItem item in shoppingList.Buy)
        {
            shop.ReduceTheNumberOfProducts(item.Product, item.Count);
        }
    }

    public void BuyProducts(Buyer buyer, Shop shop, Product product, int count)
    {
        var shoppingList = new ShoppingList();
        shoppingList.AddItem(product, count);
        BuyProducts(buyer, shop, shoppingList);
    }

    public Shop FindCheapShop(Buyer buyer, ShoppingList shoppingList, out decimal price)
    {
        decimal? minSumPrice = null;
        Shop shopWithMinPrice = null;

        foreach (Shop shop in _shops)
        {
            var sumPrice = ShoppingListPrice(shop, shoppingList);

            if ((sumPrice is null
                 && shop == _shops.Last()
                 && shopWithMinPrice is null)
                || sumPrice > buyer.Money)
            {
                price = 0;
                return null;
            }

            if (minSumPrice is null || sumPrice < minSumPrice)
            {
                minSumPrice = sumPrice;
                shopWithMinPrice = shop;
            }
        }

        price = minSumPrice ?? 0;
        return shopWithMinPrice;
    }

    public void BuyProductsCheap(Buyer buyer, ShoppingList shoppingList)
    {
        ArgumentNullException.ThrowIfNull(buyer);
        ArgumentNullException.ThrowIfNull(shoppingList);

        Shop shopWithMinPrice = FindCheapShop(buyer, shoppingList, out decimal minSumPrice);
        buyer.TransferMoney(minSumPrice);
        foreach (ShoppingListItem item in shoppingList.Buy)
        {
            shopWithMinPrice?.FindProductSet(item.Product).ReduceCount(item.Count);
        }
    }

    private decimal? ShoppingListPrice(Shop shop, ShoppingList shoppingList)
    {
        ArgumentNullException.ThrowIfNull(shop);
        ArgumentNullException.ThrowIfNull(shoppingList);

        decimal sumPrice = 0;
        foreach (ShoppingListItem item in shoppingList.Buy)
        {
            ProductSet set = shop.FindProductSet(item.Product);
            if (set is null)
            {
                return null;
            }

            if (set.Count < item.Count)
            {
                return null;
            }

            sumPrice += set.Price * item.Count;
        }

        return sumPrice;
    }
}