// Suppress AOT warnings - this library requires type metadata to be preserved
#pragma warning disable IL2072 // Target parameter doesn't have matching annotations

namespace SimpleInfoName;

public static partial class TypeNameConverter
{
    public static string SimpleName(this PropertyInfo property) =>
        cache.GetOrAdd(
            property,
            _ =>
            {
                if (property.DeclaringType is null)
                {
                    return $"Module.{property.Name}";
                }

                var declaringType = SimpleName(property.DeclaringType);
                return $"{declaringType}.{property.Name}";
            });
}