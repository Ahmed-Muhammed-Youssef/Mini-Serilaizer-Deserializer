using Mini_Serializer_Deserializer.Serializers;
using Mini_Serializer_Deserializer.Serializers.Configurations;
using SerializerDeserializerTestApp.Models;
using System.Xml.Serialization;

namespace SerializerDeserializerTestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestCase1();
            TestCase2();
            TestCase3();
        }
        private static void TestCase1()
        {
            var assignment = new Assignment()
            {
                Id = Guid.NewGuid().ToString(),
                Name = null,
                DueDate = DateTime.Now
            };
            var miniXmlSerializer = new MiniXmlSerializer();
            var res1 = miniXmlSerializer.Serialize(assignment);
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
            var miniXmlSerializer = new MiniXmlSerializer();
            var res1 = miniXmlSerializer.Serialize(course);
            // another way
            var xmlStandrardSerializer = new XmlSerializer(typeof(Course));
            var res2Stream = new StringWriter();
            xmlStandrardSerializer.Serialize(res2Stream, course);

            Console.WriteLine($"1: serialized object:\n{res1}");
            Console.WriteLine($"\n\n2: serialized object:\n{res2Stream}");
        }

        private static void TestCase3()
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

            var student = new Student()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Ahmed",
                StudentLevel = StudentLevel.Intermediate,
                Courses = new List<Course> { course }
            };

            var miniConfig = new MiniXmlSerializerConfigurations() { MapEnumsNumericValues = true };
            var miniXmlSerializer = new MiniXmlSerializer(miniConfig);
            var res1 = miniXmlSerializer.Serialize(student);
            // another way
            var xmlStandrardSerializer = new XmlSerializer(typeof(Student));
            var res2Stream = new StringWriter();
            xmlStandrardSerializer.Serialize(res2Stream, student);

            Console.WriteLine($"1: serialized object:\n{res1}");
            Console.WriteLine($"\n\n2: serialized object:\n{res2Stream}");
        }
    }
}
