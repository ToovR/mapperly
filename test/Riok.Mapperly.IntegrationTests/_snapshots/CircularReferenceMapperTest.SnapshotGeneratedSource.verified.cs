﻿// <auto-generated />
#nullable enable annotations
namespace Riok.Mapperly.IntegrationTests.Mapper
{
    public static partial class CircularReferenceMapper
    {
        [global::System.CodeDom.Compiler.GeneratedCode("Riok.Mapperly", "0.0.1.0")]
        public static partial global::Riok.Mapperly.IntegrationTests.Dto.CircularReferenceDto ToDto(global::Riok.Mapperly.IntegrationTests.Models.CircularReferenceObject obj)
        {
            return MapToCircularReferenceDto(
                obj,
                new global::Riok.Mapperly.Abstractions.ReferenceHandling.PreserveReferenceHandler()
            );
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Riok.Mapperly", "0.0.1.0")]
        private static global::Riok.Mapperly.IntegrationTests.Dto.CircularReferenceDto MapToCircularReferenceDto(global::Riok.Mapperly.IntegrationTests.Models.CircularReferenceObject source, global::Riok.Mapperly.Abstractions.ReferenceHandling.IReferenceHandler refHandler)
        {
            if (refHandler.TryGetReference<global::Riok.Mapperly.IntegrationTests.Models.CircularReferenceObject, global::Riok.Mapperly.IntegrationTests.Dto.CircularReferenceDto>(source, out var existingTargetReference))
                return existingTargetReference;
            var target = new global::Riok.Mapperly.IntegrationTests.Dto.CircularReferenceDto();
            refHandler.SetReference<global::Riok.Mapperly.IntegrationTests.Models.CircularReferenceObject, global::Riok.Mapperly.IntegrationTests.Dto.CircularReferenceDto>(source, target);
            target.Value = source.Value;
            if (source.Parent != null)
            {
                target.Parent = MapToCircularReferenceDto(source.Parent, refHandler);
            }
            else
            {
                target.Parent = null;
            }
            return target;
        }
    }
}