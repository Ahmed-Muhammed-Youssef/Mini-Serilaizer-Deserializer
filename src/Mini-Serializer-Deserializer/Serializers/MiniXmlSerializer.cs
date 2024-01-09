using Mini_Serializer_Deserializer.Serializers.Configurations;
using System.Collections;
using System.Reflection;
using System.Text;
using Mini_Serializer_Deserializer.Serializers.ValueObjects;

namespace Mini_Serializer_Deserializer.Serializers
{
    public class MiniXmlSerializer
    {
        private int indentationStep;
        private int CurrentIdentation = 0;
        private readonly MiniXmlSerializerConfigurations _configurations;
        public MiniXmlSerializer()
        {
            _configurations = new MiniXmlSerializerConfigurations();
            indentationStep = _configurations.IndentationStep;
        }
        public MiniXmlSerializer(MiniXmlSerializerConfigurations configurations)
        {
            _configurations = configurations;
            indentationStep = _configurations.IndentationStep;
        }

        // obj must always be of type T at runtime
        public string Serialize<T>(T obj)
        {
            // to sotre alreday serialized objects, To pervent circular referncing
            HashSet<object> serialized = [];
            StringBuilder result = new("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n");
            SerializeImp(obj, result, serialized);
            return result.ToString();
        }
        private void SerializeImp(object? obj, StringBuilder resultBuilder, HashSet<object> serialized, string? name = null)
        {
            // ignore objects with null values
            if (obj is null)
            {
                return;
            }
            // this will not give you the runtime type of obj if obj is an instance of a subclass of T.
            var objectType = obj.GetType();

            if(name is null && FixedValues.TypeAliases.ContainsKey(objectType.Name))
            {
                name = FixedValues.TypeAliases[objectType.Name];
            }

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
                if (objectType.IsEnum && _configurations.MapEnumsNumericValues)
                {
                    resultBuilder.AppendLine($"{Convert.ToInt32(obj)}</{tagName}>");
                }
                else if (objectType == typeof(DateTime))
                {
                    resultBuilder.AppendLine($"{((DateTime)obj).ToString(_configurations.DateTimeFormat)}</{tagName}>");
                }
                else
                {
                    resultBuilder.AppendLine($"{obj}</{tagName}>");
                }
                return;
            }
            // if the object is a collection
            else if (typeof(IEnumerable).IsAssignableFrom(objectType))
            {
                resultBuilder.AppendLine();
                Indent();
                foreach (var item in (IEnumerable)obj)
                {
                    SerializeImp(item, resultBuilder, serialized);
                }
                Unindent();
            }
            // complex type is detected
            else
            {
                resultBuilder.AppendLine();
                serialized.Add(obj);

                // iterate through all the type's properties and add them to a list.
                IList<PropertyInfo> props = new List<PropertyInfo>(objectType.GetProperties());
                Indent();
                foreach (var prop in props)
                {
                    SerializeImp(Convert.ChangeType(prop.GetValue(obj, null), prop.PropertyType), resultBuilder, serialized, prop.Name);
                }
                Unindent();
                serialized.Remove(obj);
            }

            // close the tag
            var closeTage = $"</{tagName}>";
            resultBuilder.AppendLine($"{IndentLine(closeTage)}");
        }

        private void HandleCircularReference(StringBuilder resultBuilder)
        {
            Indent();
            resultBuilder.AppendLine($"{IndentLine("<CircularReference />")}");
            Unindent();
        }
        private string IndentLine(string line) => $"{new string(' ', CurrentIdentation)}{line}";
        private void Indent()
        {
            CurrentIdentation += indentationStep;
        }
        private void Unindent()
        {
            CurrentIdentation -= indentationStep;
        }
    }
}
