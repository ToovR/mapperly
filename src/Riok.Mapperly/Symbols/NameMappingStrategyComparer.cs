using Riok.Mapperly.Abstractions;
using Riok.Mapperly.Helpers;

namespace Riok.Mapperly.Symbols;

public class NameMappingStrategyComparer : StringComparer
{
    private readonly Func<string, string> _formatString = (s) => s;
    private readonly Func<string, string> _formatStringIgnoreUnderscore = (s) => s.Replace("_", string.Empty, StringComparison.Ordinal);
    private readonly StringComparison _stringComparison;

    static NameMappingStrategyComparer()
    {
        var NameMappingStrategies = PropertyNameMappingUtil.GetNameMappingStrategyCombinations();
        NameMappingComparers = NameMappingStrategies.ToDictionary(
            nameMapping => nameMapping,
            nameMapping => new NameMappingStrategyComparer(nameMapping)
        );
    }

    public static IDictionary<PropertyNameMappingStrategy, NameMappingStrategyComparer> NameMappingComparers { get; }

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
        return HashCode.Combine(_formatString.GetHashCode(), _stringComparison.GetHashCode());
    }
}
