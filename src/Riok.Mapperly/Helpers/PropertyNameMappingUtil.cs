using Riok.Mapperly.Abstractions;

namespace Riok.Mapperly.Helpers;

internal static class PropertyNameMappingUtil
{
    internal static PropertyNameMappingStrategy[] GetNameMappingStrategyCombinations()
    {
        var values = Enum.GetValues(typeof(PropertyNameMappingStrategy)).Cast<PropertyNameMappingStrategy>();
        return values.Skip(1).GetCombinations().Select(v => v.Aggregate((a, b) => a | b)).ToArray();
    }
}
