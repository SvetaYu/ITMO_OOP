namespace Isu.Services;

using System.Text.RegularExpressions;

public class GroupNameValidatorItmo : IGroupNameValidator
{
    private static readonly Regex GroupNameRegex = new (@"^[A-Z][3-4](1\d{2}|[2-4]\d{2}[1-2])$", RegexOptions.Compiled);

    public bool Validate(string name)
    {
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        return GroupNameRegex.IsMatch(name);
    }
}