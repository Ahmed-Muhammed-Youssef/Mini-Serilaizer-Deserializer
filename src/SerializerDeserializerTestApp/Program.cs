using Mini_Serializer_Deserializer.Serializers;
using SerializerDeserializerTestApp.Models;
using System.Xml.Serialization;

namespace SerializerDeserializerTestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestPrimtive();
            TestCase1();
            TestCase2();
        }
        private static void TestPrimtive()
        {
            int x = 1;

            var res1 = MiniXmlSerializer.Serialize(x);
            // another way
            var xmlStandrardSerializer = new XmlSerializer(typeof(int));
            var res2Stream = new StringWriter();
            xmlStandrardSerializer.Serialize(res2Stream, x);

            Console.WriteLine($"1: serialized object:\n{res1}");
            Console.WriteLine($"\n\n2: serialized object:\n{res2Stream}");
        }

        private static void TestCase1()
        {
            var assignment = new Assignment()
            {
                Id = Guid.NewGuid().ToString(),
                Name = null,
                DueDate = DateTime.Now
            };

            var res1 = MiniXmlSerializer.Serialize(assignment);
            // another way
            var xmlStandrardSerializer = new XmlSerializer(typeof(Assignment));
            var res2Stream = new StringWriter();
            xmlStandrardSerializer.Serialize(res2Stream, assignment);

            Console.WriteLine($"1: serialized object:\n{res1}");
            Console.WriteLine($"\n\n2: serialized object:\n{res2Stream}");
        }

        private static void TestCase2()
        {
            var assignment = new Assignment()
            {
                Id = Guid.NewGuid().ToString(),
                Name = null,
                DueDate = DateTime.Now
            };
            var course = new Course()
            {
                Id = Guid.NewGuid().ToString(),
                Name = null,
                Assignments = assignment
            };
            var res1 = MiniXmlSerializer.Serialize(course);
            // another way
            var xmlStandrardSerializer = new XmlSerializer(typeof(Course));
            var res2Stream = new StringWriter();
            xmlStandrardSerializer.Serialize(res2Stream, course);

            Console.WriteLine($"1: serialized object:\n{res1}");
            Console.WriteLine($"\n\n2: serialized object:\n{res2Stream}");
        }
    }
}
