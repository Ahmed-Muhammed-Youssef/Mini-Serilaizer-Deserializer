using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace Mini_Serializer_Deserializer.Serializers
{
    public static class MiniXmlSerializer
    {
        public static int indentationStep = 3;
        private static int CurrentIdentation = 0;
        private static string GetCurrentIdentation()
        {
            return new string(' ', CurrentIdentation);
        }
        private static void Indent()
        {
            CurrentIdentation += indentationStep;
        }
        private static void Unindent()
        {
            CurrentIdentation -= indentationStep;
        }
        private static void SerializeImp(object? obj, StringBuilder resultBuilder, HashSet<object> serialized, string? name = null)
        {
            // if the object is null return wil null value
            if (obj is null)
            {
                return;
            }
            // this will not give you the runtime type of obj if obj is an instance of a subclass of T.
            var objectType = obj.GetType();

            string tagName = name ?? objectType.Name;
            // open the tag
            resultBuilder.Append($"{GetCurrentIdentation()}<{tagName}>");
            
            // Circular reference detected
            if (serialized.Contains(obj))
            {
                Indent();
                resultBuilder.AppendLine($"{GetCurrentIdentation()}<CircularReference />");
                Unindent();
                resultBuilder.AppendLine($"{GetCurrentIdentation()}</{tagName}>");
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
            resultBuilder.AppendLine($"{GetCurrentIdentation()}</{tagName}>");
        }

        // obj must always be of type T at runtime
        public static string Serialize<T>(T obj)
        {
            // to sotre alreday serialized objects, To pervent circular referncing
            HashSet<object> serialized = [];

            var objectType = typeof(T);
            StringBuilder result = new("<?xml version=\"1.0\" encoding=\"utf-16\"?>\n");
            SerializeImp(obj, result, serialized);
            return result.ToString();
        }
    }
}
