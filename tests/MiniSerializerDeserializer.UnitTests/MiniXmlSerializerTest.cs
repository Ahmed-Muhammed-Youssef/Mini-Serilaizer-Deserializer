using Mini_Serializer_Deserializer.Serializers;
using MiniSerializerDeserializer.UnitTests.Helpers.Models;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MiniSerializerDeserializer.UnitTests
{
    public class MiniXmlSerializerTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData("Hello")]
        [InlineData(true)]
        public void Serialize_PrimitveTypes<T>(T x)
        {
            // Arrange
            var miniXmlSerializer = new MiniXmlSerializer();
            var xmlStandrardSerializer = new XmlSerializer(typeof(T));

            // Act
            var res1 = miniXmlSerializer.Serialize(x).ToLower();
            var res2Stream = new StringWriter();
            xmlStandrardSerializer.Serialize(res2Stream, x);
            var res2 = res2Stream.ToString().ToLower();

            // Remove all whitespaces
            res1 = res1.Replace(" ", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty);
            res2 = res2.Replace(" ", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty);

            // Assert
            Assert.Equal(res2, res1);
        }

        [Theory]
        [InlineData("1edf1762-83d5-4efd-b7ef-6fba2d109114", "Ahmed")]
        [InlineData("2edf1762-83d5-4efd-b7ef-6fba2d109115", "Mohamed")]
        [InlineData(null, "Youssef")]
        [InlineData("2edf1762-83d5-4efd-b7ef-6fba2d109115", null)]
        [InlineData(null, null)]
        public void Serialize_ReferenceSimpleObjects(string? id, string? name)
        {
            // Arrange
            var assignment = new Assignment()
            {
                Id = id,
                Name = name,
                DueDate = new DateTime(2024, 1, 9)
            };
            var miniXmlSerializer = new MiniXmlSerializer();

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var xmlStandrardSerializer = new XmlSerializer(typeof(Assignment));

            // Act
            
            var res1 = miniXmlSerializer.Serialize(assignment).ToLower();
            var res2Stream = new StringWriter();
            xmlStandrardSerializer.Serialize(res2Stream, assignment, ns);
            var res2 = res2Stream.ToString().ToLower();

            res1 = res1.Replace(" ", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty);
            res2 = res2.Replace(" ", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty);

            // Assert
            Assert.Equal(res2, res1);
        }

        [Theory]
        [InlineData(null, null, null, null)]
        [InlineData("A1", "Assignment1", "C1", "Course1")]
        [InlineData("2edf1762-83d5-4efd-b7ef-6fba2d109115", "Test2", "2edf1762-83d5-4efd-b7ef-6fba2d109115", "CourseTesting")]
        public void Serialize_ComplexObjects(string? assignmentId, string? assignmentName, string? courseId, string? courseName)
        {
            // Arrange
            var assignment = new Assignment()
            {
                Id = assignmentId,
                Name = assignmentName,
                DueDate = new DateTime(2024, 1, 9)
            };
            var course = new Course()
            {
                Id = courseId,
                Name = courseName,
                Assignments = assignment
            };
            var miniXmlSerializer = new MiniXmlSerializer();

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var xmlStandrardSerializer = new XmlSerializer(typeof(Course));

            // Act

            var res1 = miniXmlSerializer.Serialize(course).ToLower();
            var res2Stream = new StringWriter();
            xmlStandrardSerializer.Serialize(res2Stream, course, ns);
            var res2 = res2Stream.ToString().ToLower();

            res1 = res1.Replace(" ", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty);
            res2 = res2.Replace(" ", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty);

            // Assert
            Assert.Equal(res2, res1);
        }

        [Theory]
        [InlineData(null, null, null, null, null, null, StudentLevel.Beginner)]
        [InlineData("A1", "Assignment1", "C1", "Course1", "S1", "Student1", StudentLevel.Advanced)]
        public void Serialize_CollectionObjects(string? assignmentId, string? assignmentName, string? courseId, string? courseName, string? studentId, string? studentName, StudentLevel studentLevel)
        {
            // Arrange
            var assignment = new Assignment()
            {
                Id = assignmentId,
                Name = assignmentName,
                DueDate = new DateTime(2024, 1, 9)
            };
            var course = new Course()
            {
                Id = courseId,
                Name = courseName,
                Assignments = assignment
            };
            var student = new Student()
            {
                Id = studentId,
                Name = studentName,
                StudentLevel = studentLevel,
                Courses = new List<Course> { course }
            };
            var miniXmlSerializer = new MiniXmlSerializer();

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var xmlStandrardSerializer = new XmlSerializer(typeof(Student));

            // Act

            var res1 = miniXmlSerializer.Serialize(student).ToLower();
            var res2Stream = new StringWriter();
            xmlStandrardSerializer.Serialize(res2Stream, student, ns);
            var res2 = res2Stream.ToString().ToLower();

            res1 = res1.Replace(" ", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty);
            res2 = res2.Replace(" ", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty);

            // Assert
            Assert.Equal(res2, res1);
        }
    }
}