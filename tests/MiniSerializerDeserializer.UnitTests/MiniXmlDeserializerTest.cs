using Mini_Serializer_Deserializer.Deserializers;
using MiniSerializerDeserializer.UnitTests.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSerializerDeserializer.UnitTests
{
    public class MiniXmlDeserializerTest
    {
        [Fact]
        public void TestDeserialize()
        {
            // Arrange
            string serializedData = "<Assignment><Id>123</Id><Name>Assignment 1</Name><DueDate>2024-01-31T00:00:00</DueDate></Assignment>";
            Assignment resultObject = new Assignment();
            MiniXmlDeserializer miniXmlDeserializer = new();
            // Act
            Assignment deserializedObject = miniXmlDeserializer.DeSerialize<Assignment>(serializedData, resultObject);

            // Assert
            Assert.Equal("123", deserializedObject.Id);
            Assert.Equal("Assignment 1", deserializedObject.Name);
            Assert.Equal(new DateTime(2024, 01, 31), deserializedObject.DueDate);
        }
    }
}
