using Shops.Exceptions;

namespace Shops.Models;

public class Product : IEquatable<Product>
{
    public Product(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Id = Guid.NewGuid();
    }

    public string Name { get; }
    public Guid Id { get; }

    public static bool operator ==(Product lhs, Product rhs)
    {
        return Equals(lhs, rhs);
    }

    public static bool operator !=(Product lhs, Product rhs)
    {
        return !Equals(lhs, rhs);
    }

    public bool Equals(Product other)
    {
        return other is not null && other.Id == this.Id;
    }

    public override bool Equals(object obj)
    {
        if (obj is Product product)
            return Equals(product);
        return false;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}