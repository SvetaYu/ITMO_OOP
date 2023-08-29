using System.Text.RegularExpressions;
using Isu.Services;

namespace Isu.Extra.Models
{
    public class OgnpGroupNameValidator : IGroupNameValidator
    {
        private static readonly Regex GroupNameRegex = new (@"[A-Z a-z]+[0-9]+$", RegexOptions.Compiled);

        public bool Validate(string name)
        {
            ArgumentNullException.ThrowIfNull(name);
            return GroupNameRegex.IsMatch(name);
        }
    }
}