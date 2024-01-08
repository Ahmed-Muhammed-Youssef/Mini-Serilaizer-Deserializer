namespace Mini_Serializer_Deserializer.Serializers.ValueObjects
{
    public static class FixedValues
    {
        public static readonly Dictionary<string, string> TypeAliases = new Dictionary<string, string>
        {
            { "Boolean", "boolean" },
            { "Byte", "byte" },
            { "SByte", "sbyte" },
            { "Char", "char" },
            { "Decimal", "decimal" },
            { "Double", "double" },
            { "Single", "float" },
            { "Int32", "int" },
            { "UInt32", "uint" },
            { "Int64", "long" },
            { "UInt64", "ulong" },
            { "Object", "object" },
            { "Int16", "short" },
            { "UInt16", "ushort" },
            { "String", "string" }
        };
    }
}
