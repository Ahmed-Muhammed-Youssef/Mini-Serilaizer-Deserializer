using System.Reflection;
using System.Text;

namespace Mini_Serializer_Deserializer.Serializers
{
    public static class XmlSerializer
    {
        private static string SerializeImp<T>(T obj, HashSet<object> serialized)
        {
            if (obj is not null && serialized.Contains(obj))
            {
                return "<CircularReference />";
            }

            // this will not give you the runtime type of obj if obj is an instance of a subclass of T.
            var myType = typeof(T);

            // build the result
            var resultBuilder = new StringBuilder();
            resultBuilder.AppendLine($"<{myType.Name}>");


            // if the object is null return wil null value
            if(obj is null)
            {
                return resultBuilder.AppendLine($"null</{myType.Name}>").ToString();
            }

            // iterate through all the type's properties and add them to a list.
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

            foreach (var prop in props)
            {
                SerializeProperty(obj, serialized, resultBuilder, prop);
            }

            resultBuilder.AppendLine($"</{myType.Name}>");
            return resultBuilder.ToString();
        }
        private static void SerializeProperty<T>(T obj, HashSet<object> serialized, StringBuilder resultBuilder, PropertyInfo prop)
        {
            // Get the property value
            var propValue = prop.GetValue(obj, null);
            resultBuilder.Append($"    <{prop.Name}>");

            if (propValue == null)
            {
                // write a special value
                resultBuilder.AppendLine($"null</{prop.Name}>");
                return;
            }

            // Check if the property is a complex type
            if (!prop.PropertyType.IsValueType && prop.PropertyType != typeof(string))
            {
                // If the property is a complex type, serialize it using a recursive call
                resultBuilder.Append(SerializeImp(propValue, serialized));
            }
            else
            {
                resultBuilder.Append(propValue);
            }
            resultBuilder.AppendLine($"</{prop.Name}>");
        }

        // obj must always be of type T at runtime
        public static string Serialize<T>(T obj)
        {
            if(obj == null)
            {
                return "";
            }
            // to sotre alreday serialized objects, To pervent circular referncing
            HashSet<object> serialized = new HashSet<object>();
            return SerializeImp<T>(obj, serialized);
        }
    }
}
