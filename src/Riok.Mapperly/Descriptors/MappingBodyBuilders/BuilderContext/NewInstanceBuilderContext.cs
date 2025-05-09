using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Riok.Mapperly.Abstractions;
using Riok.Mapperly.Descriptors.Mappings;
using Riok.Mapperly.Descriptors.Mappings.MemberMappings;
using Riok.Mapperly.Diagnostics;
using Riok.Mapperly.Symbols.Members;

namespace Riok.Mapperly.Descriptors.MappingBodyBuilders.BuilderContext;

/// <summary>
/// An implementation of <see cref="INewInstanceBuilderContext{T}"/>.
/// </summary>
/// <typeparam name="T">The type of the mapping.</typeparam>
public class NewInstanceBuilderContext<T>(MappingBuilderContext builderContext, T mapping)
    : MembersMappingBuilderContext<T>(builderContext, mapping),
        INewInstanceBuilderContext<T>
    where T : INewInstanceObjectMemberMapping
{
    public void AddConstructorParameterMapping(ConstructorParameterMapping mapping)
    {
        Mapping.AddConstructorParameterMapping(mapping);
        MappingAdded(mapping.MemberInfo, true);
    }

    public void AddInitMemberMapping(MemberAssignmentMapping mapping)
    {
        Mapping.AddInitMemberMapping(mapping);
        MappingAdded(mapping.MemberInfo);
    }

    public bool TryMatchInitOnlyMember(IMappableMember targetMember, [NotNullWhen(true)] out MemberMappingInfo? memberInfo)
    {
        if (TryMatchMember(targetMember, out memberInfo))
            return true;

        if (TryGetMemberConfigs(targetMember.Name, PropertyNameMappingStrategy.CaseSensitive, out var configs))
        {
            BuilderContext.ReportDiagnostic(
                DiagnosticDescriptors.InitOnlyMemberDoesNotSupportPaths,
                Mapping.TargetType,
                configs[0].Target.FullName
            );
            ConsumeMemberConfig(configs[0]);
            return false;
        }

        return false;
    }

    public bool TryMatchParameter(IParameterSymbol parameter, [NotNullWhen(true)] out MemberMappingInfo? memberInfo) =>
        TryMatchMember(
            new ConstructorParameterMember(parameter, BuilderContext.SymbolAccessor),
            PropertyNameMappingStrategy.CaseInsensitive | PropertyNameMappingStrategy.UnderscoreIgnore,
            out memberInfo
        );
}
