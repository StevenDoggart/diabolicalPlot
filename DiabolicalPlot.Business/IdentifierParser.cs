using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace DiabolicalPlot.Business
{
    public class IdentifierParser : IIdentifierParser
    {
        public IEnumerable<IdentifierPart> Parse(string identifier)
        {
            const string pattern = @"
                (?<=\p{Ll})(?=[\p{Lu}])           |
                (?<=[^_])(?=_)                    |
                (?<=_)(?=[^_])                    |
                (?<=\d)(?=\D)                     |
                (?<=\D)(?=\d)                     |
                (?<=[\p{Lu}])(?=[\p{Lu}][\p{Ll}])
                ";
            string[] parts;
            if (identifier != null)
                parts = Regex.Split(identifier, pattern, RegexOptions.IgnorePatternWhitespace);
            else
                parts = new string[] { };
            return parts
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select((part, index) => evaluatePart(part, parts, index));
        }


        private IdentifierPart evaluatePart(string part, string[] parts, int index)
        {
            IdentifierPartTypeEnum? type = null;
            if ((index == 0) && (part.Length == 1) && ("IT".Contains(part)))
                type = IdentifierPartTypeEnum.Other;
            if ((!type.HasValue) && Regex.IsMatch(part, @"^[^_]+$"))
                type = IdentifierPartTypeEnum.Word;
            return new IdentifierPart(type.HasValue ? type.Value : IdentifierPartTypeEnum.Other, part);
        }
    }


    public interface IIdentifierParser
    {
        IEnumerable<IdentifierPart> Parse(string identifier);
    }


    public class IdentifierPart
    {
        public IdentifierPart(IdentifierPartTypeEnum type, string value)
        {
            Type = type;
            Value = value;
        }

        public IdentifierPartTypeEnum Type { get; }
        public string Value { get; }
    }


    public enum IdentifierPartTypeEnum
    {
        Other,
        Word
    }
}
