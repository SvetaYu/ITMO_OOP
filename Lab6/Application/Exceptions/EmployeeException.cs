namespace Application.Exceptions;

public class EmployeeException : Exception
{
    private EmployeeException(string message)
        : base(message) { }

    public static EmployeeException UnavailableOperation()
    {
        return new EmployeeException("Insufficient permissions for the operation");
    }
}