﻿//HintName: Mapper.g.cs
// <auto-generated />
#nullable enable
public partial class Mapper
{
    [global::System.CodeDom.Compiler.GeneratedCode("Riok.Mapperly", "0.0.1.0")]
    [return: global::System.Diagnostics.CodeAnalysis.NotNullIfNotNull(nameof(source))]
    private partial global::B? Map(global::A? source)
    {
        if (source == null)
            return default;
        var target = new global::B();
        target.Value = source.Value;
        return target;
    }
}