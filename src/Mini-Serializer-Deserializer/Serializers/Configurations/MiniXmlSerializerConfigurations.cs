using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Serializer_Deserializer.Serializers.Configurations
{
    public class MiniXmlSerializerConfigurations
    {
        /// <summary>
        /// Gets or sets a value indicating whether to serialize Enum values as numeric values.
        /// If set to true, Enum values are serialized as their numeric values.
        /// If set to false (default), Enum values are serialized as their names.
        /// </summary>
        public bool MapEnumsNumericValues { get; set; } = false;

        /// <summary>
        /// Gets or sets the number of spaces for each indentation level in the serialized XML.
        /// The default value is 3.
        /// </summary>
        public int IndentationStep { get; set; } = 3;
    }
}
