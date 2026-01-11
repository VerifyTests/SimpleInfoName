// Suppress AOT warnings - this library requires type metadata to be preserved
#pragma warning disable IL2072 // Target parameter doesn't have matching annotations

namespace SimpleInfoName;

public static partial class TypeNameConverter
{
    public static string SimpleName(this FieldInfo field) =>
        cache.GetOrAdd(
            field,
            _ =>
            {
                if (field.DeclaringType is null)
                {
                    return $"Module.{field.Name}";
                }

                var declaringType = SimpleName(field.DeclaringType);
                return $"{declaringType}.{field.Name}";
            });
}