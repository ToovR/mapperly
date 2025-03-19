﻿//HintName: Mapper.g.cs
// <auto-generated />
#nullable enable
public partial class Mapper
{
    [global::System.CodeDom.Compiler.GeneratedCode("Riok.Mapperly", "0.0.1.0")]
    [return: global::System.Diagnostics.CodeAnalysis.NotNullIfNotNull(nameof(src))]
    public partial global::BaseDto? Map(global::Base? src)
    {
        if (src == null)
            return default;
        return src switch
        {
            global::A x => MapA(x),
            _ => throw new System.ArgumentException($"Cannot map {src.GetType()} to BaseDto as there is no known derived type mapping", nameof(src)),
        };
    }

    [global::System.CodeDom.Compiler.GeneratedCode("Riok.Mapperly", "0.0.1.0")]
    [return: global::System.Diagnostics.CodeAnalysis.NotNullIfNotNull(nameof(src))]
    public partial global::ADto? MapA(global::A? src)
    {
        if (src == null)
            return default;
        var target = new global::ADto(src.Value);
        return target;
    }
}