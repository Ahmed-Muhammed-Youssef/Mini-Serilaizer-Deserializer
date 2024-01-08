using Mini_Serializer_Deserializer.Serializers;
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

        [Fact]
        public void Serialize_ReferenceSimpleObjects()
        {

        }

        [Fact]
        public void Serialize_ComplexObjects()
        {

        }

        [Fact]
        public void Serialize_CollectionObjects()
        {

        }
    }
}