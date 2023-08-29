namespace Isu.Extra.Exceptions;

public class OgnpGroupException : Exception
{
    private OgnpGroupException(string massage)
        : base(massage) { }

    public static OgnpGroupException OgnpGroupOverFlow()
    {
        return new OgnpGroupException("Ognp-group overflow");
    }
}