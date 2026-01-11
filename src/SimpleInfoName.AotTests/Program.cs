using System.Reflection;
using SimpleInfoName;

public static class Program
{
    public static int Main()
    {
        try
        {
            TestTypeNames();
            TestMethodNames();
            TestPropertyNames();
            TestFieldNames();
            TestConstructorNames();
            TestParameterNames();
            TestGenericTypes();

            Console.WriteLine("All SimpleInfoName AOT tests passed!");
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Test failed: {ex}");
            return 1;
        }
    }

    static void TestTypeNames()
    {
        // Test simple type name (library uses lowercase C# aliases for built-in types)
        var name = typeof(string).SimpleName();
        Console.WriteLine($"string SimpleName: {name}");
        if (name != "string") throw new Exception($"Expected 'string', got '{name}'");

        // Test generic type name
        var listName = typeof(List<string>).SimpleName();
        Console.WriteLine($"List<string> SimpleName: {listName}");
        if (!listName.Contains("List")) throw new Exception($"Expected name containing 'List', got '{listName}'");

        // Test nested type
        var dictName = typeof(Dictionary<string, int>).SimpleName();
        Console.WriteLine($"Dictionary<string, int> SimpleName: {dictName}");
    }

    static void TestMethodNames()
    {
        var method = typeof(TestClass).GetMethod(nameof(TestClass.TestMethod));
        if (method == null) throw new Exception("Could not find TestMethod");

        var name = method.SimpleName();
        Console.WriteLine($"TestMethod SimpleName: {name}");
        if (!name.Contains("TestMethod")) throw new Exception($"Expected name containing 'TestMethod', got '{name}'");
    }

    static void TestPropertyNames()
    {
        var prop = typeof(TestClass).GetProperty(nameof(TestClass.TestProperty));
        if (prop == null) throw new Exception("Could not find TestProperty");

        var name = prop.SimpleName();
        Console.WriteLine($"TestProperty SimpleName: {name}");
        if (!name.Contains("TestProperty")) throw new Exception($"Expected name containing 'TestProperty', got '{name}'");
    }

    static void TestFieldNames()
    {
        var field = typeof(TestClass).GetField(nameof(TestClass.TestField));
        if (field == null) throw new Exception("Could not find TestField");

        var name = field.SimpleName();
        Console.WriteLine($"TestField SimpleName: {name}");
        if (!name.Contains("TestField")) throw new Exception($"Expected name containing 'TestField', got '{name}'");
    }

    static void TestConstructorNames()
    {
        var ctor = typeof(TestClass).GetConstructor(Type.EmptyTypes);
        if (ctor == null) throw new Exception("Could not find constructor");

        var name = ctor.SimpleName();
        Console.WriteLine($"TestClass constructor SimpleName: {name}");
    }

    static void TestParameterNames()
    {
        var method = typeof(TestClass).GetMethod(nameof(TestClass.MethodWithParams));
        if (method == null) throw new Exception("Could not find MethodWithParams");

        var parameters = method.GetParameters();
        foreach (var param in parameters)
        {
            var name = param.SimpleName();
            Console.WriteLine($"Parameter '{param.Name}' SimpleName: {name}");
        }
    }

    static void TestGenericTypes()
    {
        // Test open generic type
        var openGeneric = typeof(List<>).SimpleName();
        Console.WriteLine($"List<> SimpleName: {openGeneric}");

        // Test nullable type
        var nullable = typeof(int?).SimpleName();
        Console.WriteLine($"int? SimpleName: {nullable}");

        // Test array type
        var array = typeof(string[]).SimpleName();
        Console.WriteLine($"string[] SimpleName: {array}");
    }
}

public class TestClass
{
    public string? TestField;

    public TestClass() { }

    public string TestProperty { get; set; } = "";

    public void TestMethod() { }

    public void MethodWithParams(string name, int count) { }
}
