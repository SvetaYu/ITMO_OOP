using System.Reflection.Metadata.Ecma335;

namespace Banks.Exceptions;

public class ConfigurationException : Exception
{
    private ConfigurationException(string message)
        : base(message) { }

    public static ConfigurationException InvalidInterest()
    {
        return new ConfigurationException("invalid interest");
    }
}