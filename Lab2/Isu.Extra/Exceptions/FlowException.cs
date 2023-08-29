namespace Isu.Extra.Exceptions;

public class FlowException : Exception
{
    private FlowException(string massage)
        : base(massage) { }

    public static FlowException FlowOverFlow()
    {
        return new FlowException("Flow overflow");
    }
}