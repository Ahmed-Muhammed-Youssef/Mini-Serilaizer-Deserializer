using Mini_Serializer_Deserializer.Serializers.Configurations;
using System.Reflection;
using System.Xml.Linq;

namespace Mini_Serializer_Deserializer.Deserializers
{
    public class MiniXmlDeserializer
    {
        private readonly MiniXmlSerializerConfigurations _configurations;
        public MiniXmlDeserializer()
        {
            _configurations = new MiniXmlSerializerConfigurations();
        }
        public MiniXmlDeserializer(MiniXmlSerializerConfigurations configurations)
        {
            _configurations = configurations;
        }

        ///
        /// <summary>
        /// Returns the deserialized object.
        /// </summary>
        /// <param name="serializedData"></param>
        /// <param name="resultObject"></param>
        /// <returns></returns>
        public T DeSerialize<T>(string serializedData, T resultObject) where T : new()
        {
            if(resultObject == null)
            {
                return resultObject;
            }

            // Load the XML data into an XDocument
            XDocument doc = XDocument.Parse(serializedData);

            // Get the root element of the XML data
            XElement? root = doc.Root;

            if (root is null)
            {
                return resultObject;
            }

            // Loop through the child elements of the root element
            foreach (XElement element in root.Elements())
            {
                // Get the name and value of the element
                string name = element.Name.LocalName;
                string value = element.Value;

                // Use reflection to get the property of the object that matches the element name
                var prop = resultObject.GetType().GetProperty(name);
                if (prop is null)
                {
                    continue;
                }
                var propType = prop.PropertyType;
                // If the property exists and is writable, set its value
                if (prop.CanWrite && (propType.IsValueType || propType == typeof(string)))
                {
                    // Convert the element value to the property type
                    var convertedValue = Convert.ChangeType(value, prop.PropertyType);

                    // Set the property value of the object
                    prop.SetValue(resultObject, convertedValue);
                }
                else
                {
                    object? propertyObject = Activator.CreateInstance(prop.GetType());

                    prop.SetValue(resultObject, DeSerializeImp(element, propertyObject));
                }
            }

            // Return the deserialized object
            return resultObject;
        }
        private T DeSerializeImp<T>(XElement root, T objectResult)
        {
            if(objectResult is null)
            {
                return objectResult;
            }

            foreach (var element in root.Elements())
            {
                string name = element.Name.LocalName;
                string value = element.Value;

                // Use reflection to get the property of the object that matches the element name
                var prop = objectResult.GetType().GetProperty(name);

                if (prop is null)
                {
                    continue;
                }

                var propType = prop.PropertyType;

                if (prop.CanWrite && (propType.IsValueType || propType == typeof(string)))
                {
                    // Convert the element value to the property type
                    var convertedValue = Convert.ChangeType(value, prop.PropertyType);

                    // Set the property value of the object
                    prop.SetValue(objectResult, convertedValue);
                }
                else
                {
                    object? propertyObject = Activator.CreateInstance(prop.GetType());
                    prop.SetValue(objectResult, DeSerializeImp(element, propertyObject));
                }
            }

            return objectResult;
        }
    }
}
