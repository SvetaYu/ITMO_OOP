using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;
using Shops.Services;
using Xunit;

namespace Shops.Test;

public class ShopsTests
{
    private readonly ShopManager _manager = new ();

    private Product _milk = new Product("milk");

    private Product _bread = new Product("bread");

    private Product _chocolate = new Product("chocolate");
    private Shop _shop;

    public ShopsTests()
    {
        _shop = _manager.AddShop("Magnit",  "Pushkinskaya 5");
    }

    [Theory]
    [InlineData(90, 25)]
    [InlineData(45, 13)]
    [InlineData(123, 100)]
    public void AddProducts_ShopHasProducts(decimal price, int count)
    {
        ProductSet products = _manager.AddProductsInShop(_shop, _chocolate, price, count);
        Assert.Contains(products, _shop.Products);
        Assert.Equal(count, products.Count);
        Assert.Equal(price, products.Price);
    }

    [Theory]
    [InlineData(-1, 25)]
    [InlineData(45, 0)]
    [InlineData(123, -100)]
    public void AddProductsWithInvalidOptions_ThrowException(decimal price, int count)
    {
        Assert.Throws<ShopException>(() => _manager.AddProductsInShop(_shop, _chocolate, price, count));
    }

    [Theory]
    [InlineData(-25)]
    [InlineData(0)]
    public void AddShoppingListItemWithInvalidCount_ThrowException(int count)
    {
        var shoppingList = new ShoppingList();
        Assert.Throws<ShoppingListException>(() => shoppingList.AddItem(_chocolate, count));
    }

    [Theory]
    [InlineData(5, 10)]
    [InlineData(1, 1)]
    public void AddTwoIdenticalProductsInShoppingList_ShoppingListHasOneItemWithSumCounts(int count1, int count2)
    {
        var shoppingList = new ShoppingList();
        ShoppingListItem item = shoppingList.AddItem(_chocolate, count1);
        shoppingList.AddItem(_chocolate, count2);
        Assert.Contains(item, shoppingList.Buy);
        Assert.Equal(count1 + count2, item.Count);
    }

    [Theory]
    [InlineData(15, 4, 1)]
    [InlineData(15, 150, 5)]
    [InlineData(1, 1, 1)]
    public void BuyProducts(int countMilk, int countBread, int countChocolate)
    {
        int startCountMilk = 15;
        int startCountBread = 150;
        int startCountChocolate = 5;
        var buyer = new Buyer("Sveta", 10000);
        var milkSet = _manager.AddProductsInShop(_shop, _milk, 90, startCountMilk);
        var breadSet = _manager.AddProductsInShop(_shop, _bread, 43, startCountBread);
        var chocolateSet = _manager.AddProductsInShop(_shop, _chocolate, 123, startCountChocolate);
        var shoppingList = new ShoppingList();
        shoppingList.AddItem(_milk, countMilk);
        shoppingList.AddItem(_bread, countBread);
        shoppingList.AddItem(_chocolate, countChocolate);
        _manager.BuyProducts(buyer, _shop, shoppingList);
        if (countMilk == startCountMilk)
            Assert.DoesNotContain(milkSet, _shop.Products);
        else
            Assert.Equal(startCountMilk - countMilk, milkSet.Count);

        if (countBread == startCountBread)
            Assert.DoesNotContain(breadSet, _shop.Products);
        else
            Assert.Equal(startCountBread - countBread, breadSet.Count);

        if (countChocolate == startCountChocolate)
            Assert.DoesNotContain(chocolateSet, _shop.Products);
        else
            Assert.Equal(startCountChocolate - countChocolate, chocolateSet.Count);
    }

    [Fact]
    public void FindCheapShop()
    {
        var buyer = new Buyer("Sveta", 10000);
        decimal exceptedMilkPrice = 90, exceptedBreadPrice = 4, exceptedChocolatePrice = 200;
        _manager.AddProductsInShop(_shop, _milk, 90, 15);
        _manager.AddProductsInShop(_shop, _bread, 43, 150);
        _manager.AddProductsInShop(_shop, _chocolate, 123, 5);
        var shop2 = _manager.AddShop("peterochka", "lenskaya 1");
        _manager.AddProductsInShop(shop2, _milk, 90, 15);
        _manager.AddProductsInShop(shop2, _bread, 1, 150);
        var shop3 = _manager.AddShop("lenta", "lomonosova 18");
        _manager.AddProductsInShop(shop3, _milk, exceptedMilkPrice, 15);
        _manager.AddProductsInShop(shop3, _bread, exceptedBreadPrice, 150);
        _manager.AddProductsInShop(shop3, _chocolate, exceptedChocolatePrice, 5);
        var shop4 = _manager.AddShop("fixPrice", "Lenina 3");
        _manager.AddProductsInShop(shop4, _milk, 90, 15);
        _manager.AddProductsInShop(shop4, _bread, 40, 150);
        _manager.AddProductsInShop(shop4, _chocolate, 123, 5);
        var shop5 = _manager.AddShop("diksi", "kronverkski prospect 22");
        _manager.AddProductsInShop(shop5, _milk, 9, 15);
        _manager.AddProductsInShop(shop5, _bread, 0.5m, 1);
        _manager.AddProductsInShop(shop5, _chocolate, 1, 5);
        var shoppingList = new ShoppingList();
        int milkCount = 15, breadCount = 4, chocolateCount = 1;
        shoppingList.AddItem(_milk, milkCount);
        shoppingList.AddItem(_bread, breadCount);
        shoppingList.AddItem(_chocolate, chocolateCount);
        var cheapShop = _manager.FindCheapShop(buyer, shoppingList, out decimal price);
        Assert.Equal(shop3, cheapShop);
        decimal exceptedTotalPrice = (milkCount * exceptedMilkPrice) + (breadCount * exceptedBreadPrice) +
                                     (chocolateCount * exceptedChocolatePrice);
        Assert.Equal(exceptedTotalPrice, price);
    }
}