using Mini_Serializer_Deserializer.Serializers.Configurations;

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

        public T DeSerialize<T>(string serializeData, T target) where T : new()
        {
            return target;
        }
    }
}
