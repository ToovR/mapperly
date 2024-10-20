using Riok.Mapperly.Abstractions;

namespace Riok.Mapperly.Symbols;

public class NameMappingStrategyComparer : StringComparer
{
    private readonly Func<string, string> _formatString = (s) => s;
    private readonly Func<string, string> _formatStringIgnoreUnderscore = (s) => s.Replace("_", string.Empty, StringComparison.Ordinal);
    private readonly StringComparison _stringComparison;

    public static NameMappingStrategyComparer CaseUnderscoreIgnoreComparer { get; } =
        new NameMappingStrategyComparer(PropertyNameMappingStrategy.CaseInsensitive | PropertyNameMappingStrategy.UnderscoreIgnore);
    public static NameMappingStrategyComparer UnderscoreIgnoreComparer { get; } =
        new NameMappingStrategyComparer(PropertyNameMappingStrategy.UnderscoreIgnore);

    public NameMappingStrategyComparer(PropertyNameMappingStrategy nameMappingStrategy)
    {
        var ignoreCase = (nameMappingStrategy & PropertyNameMappingStrategy.CaseInsensitive) == PropertyNameMappingStrategy.CaseInsensitive;
        var ignoreUndersccore =
            (nameMappingStrategy & PropertyNameMappingStrategy.UnderscoreIgnore) == PropertyNameMappingStrategy.UnderscoreIgnore;
        _stringComparison = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
        if (ignoreUndersccore)
        {
            _formatString = _formatStringIgnoreUnderscore;
        }
    }

    public override int Compare(string x, string y)
    {
        return string.Compare(_formatString(x), _formatString(y), _stringComparison);
    }

    public override bool Equals(string x, string y)
    {
        return _formatString(x).Equals(_formatString(y), _stringComparison);
    }

    public override int GetHashCode(string obj)
    {
        return _stringComparison.GetHashCode();
    }
}
