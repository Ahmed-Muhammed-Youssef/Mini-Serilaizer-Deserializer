using Mini_Serializer_Deserializer.Serializers;
using SerializerDeserializerTestApp.Models;

namespace SerializerDeserializerTestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var xml = new Assignment()
            {
                Id = Guid.NewGuid().ToString(),
                Name = null,
                DueDate = DateTime.Now
            };
            var res = XmlSerializer.Serialize(xml);
            Console.WriteLine($"serialized object:\n{res}");
        }
    }

}
