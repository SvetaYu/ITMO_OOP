using Banks.Exceptions;

namespace Banks.Entities;

public interface INameBuilder
{
    ISurnameBuilder SetName(string name);
}

public interface ISurnameBuilder
{
    IClientBuilder SetSurname(string surname);
}

public interface IClientBuilder
{
    IClientBuilder SetPassport(int number);
    IClientBuilder SetAddress(string address);
    Client Build();
}

public class Client
{
    private const int MinPassportNumber = 100000;
    private const int MaxPassportNumber = 999999;
    private readonly List<IAccount> _accounts = new List<IAccount>();

    private Client(string name, string surname, string address, int? passportNumber)
    {
        Name = name;
        Surname = surname;
        Address = address;
        PassportNumber = passportNumber;
        Id = Guid.NewGuid();
    }

    public static INameBuilder Builder => new ClientBuilder();
    public IReadOnlyCollection<IAccount> Accounts => _accounts;
    public bool IsVerified => !string.IsNullOrWhiteSpace(Address) && PassportNumber.HasValue;
    public int? PassportNumber { get; private set; }
    public string Address { get; private set; }
    public string Name { get; }
    public Guid Id { get; }
    public string Surname { get; }

    public override string ToString()
    {
        return $"{Name} {Surname} {Id}";
    }

    public void SetPassport(int number)
    {
        if (number is < MinPassportNumber or >= MaxPassportNumber)
        {
            throw ClientException.InvalidPassportNumber();
        }

        PassportNumber = number;
    }

    public void SetAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            throw ClientException.InvalidAddress();
        }

        Address = address;
    }

    internal void SubscribeToBankNotifications(Bank bank)
    {
        bank.Config.Changed += (sender, args) =>
        {
            var config = sender as Configuration;

            // что-то сделать
        };
    }

    internal void AddAccount(IAccount account)
    {
        _accounts.Add(account);
    }

    internal void RemoveAccount(IAccount account)
    {
        _accounts.Remove(account);
    }

    internal IAccount FindAccount(Guid id)
    {
        return _accounts.FirstOrDefault(account => account.Id.Equals(id));
    }

    private class ClientBuilder : IClientBuilder, INameBuilder, ISurnameBuilder
    {
        private string _name;
        private string _surname;
        private string _address;
        private int? _passportNumber;

        public IClientBuilder SetPassport(int number)
        {
            if (number is < MinPassportNumber or >= MaxPassportNumber)
            {
                throw ClientException.InvalidPassportNumber();
            }

            _passportNumber = number;
            return this;
        }

        public IClientBuilder SetAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                throw ClientException.InvalidAddress();
            }

            _address = address;
            return this;
        }

        public Client Build()
        {
            return new Client(_name, _surname, _address, _passportNumber);
        }

        public ISurnameBuilder SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw ClientException.InvalidName();
            }

            _name = name;
            return this;
        }

        public IClientBuilder SetSurname(string surname)
        {
            if (string.IsNullOrWhiteSpace(surname))
            {
                throw ClientException.InvalidSurname();
            }

            _surname = surname;
            return this;
        }
    }
}