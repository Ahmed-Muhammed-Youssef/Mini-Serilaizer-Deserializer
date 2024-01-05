using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace Mini_Serializer_Deserializer.Serializers
{
    public static class MiniXmlSerializer
    {
        public static int indentationStep = 3;
        private static int CurrentIdentation = 0;
        // obj must always be of type T at runtime
        public static string Serialize<T>(T obj)
        {
            // to sotre alreday serialized objects, To pervent circular referncing
            HashSet<object> serialized = [];
            StringBuilder result = new("<?xml version=\"1.0\" encoding=\"utf-16\"?>\n");
            SerializeImp(obj, result, serialized);
            return result.ToString();
        }
        private static void SerializeImp(object? obj, StringBuilder resultBuilder, HashSet<object> serialized, string? name = null)
        {
            // ignore objects with null values
            if (obj is null)
            {
                return;
            }
            // this will not give you the runtime type of obj if obj is an instance of a subclass of T.
            var objectType = obj.GetType();

            string tagName = name ?? objectType.Name;
            // open the tag
            string openingTag = $"<{tagName}>";
            resultBuilder.Append($"{IndentLine(openingTag)}");
            
            // Circular reference detected
            if (serialized.Contains(obj))
            {
                HandleCircularReference(resultBuilder);

                var closingTag = $"</{tagName}>";
                resultBuilder.AppendLine($"{IndentLine(closingTag)}");
                return;
            }
            // for primitive types
            else if (objectType.IsValueType || objectType == typeof(string))
            {
                resultBuilder.AppendLine($"{obj}</{tagName}>");
                return;
            }
            // complex type is detected
            else
            {
                resultBuilder.AppendLine();
                serialized.Add(obj);
            }

            // iterate through all the type's properties and add them to a list.
            IList<PropertyInfo> props = new List<PropertyInfo>(objectType.GetProperties());
            Indent();
            foreach (var prop in props)
            {
                SerializeImp(Convert.ChangeType(prop.GetValue(obj, null), prop.PropertyType), resultBuilder, serialized, prop.Name);
            }
            Unindent();
            serialized.Remove(obj);

            // close the tag
            var closeTage = $"</{tagName}>";
            resultBuilder.AppendLine($"{IndentLine(closeTage)}");
        }

        private static void HandleCircularReference(StringBuilder resultBuilder)
        {
            Indent();
            resultBuilder.AppendLine($"{IndentLine("<CircularReference />")}");
            Unindent();
        }

        private static string IndentLine(string line) => $"{new string(' ', CurrentIdentation)}{line}";
        private static void Indent()
        {
            CurrentIdentation += indentationStep;
        }
        private static void Unindent()
        {
            CurrentIdentation -= indentationStep;
        }
    }
}
