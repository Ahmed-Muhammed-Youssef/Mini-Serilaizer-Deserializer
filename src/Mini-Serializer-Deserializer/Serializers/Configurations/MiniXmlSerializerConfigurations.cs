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


        private string dateTimeFormat = "yyyy-MM-ddTHH:mm:ss";

        /// <summary>
        /// Gets or sets the format for DateTime serialization. This format is used with the ToString() function
        /// to ensure that DateTime objects are serialized in a consistent manner. The default format is "yyyy-MM-ddTHH:mm:ss",
        /// which is compliant with the ISO 8601 standard. If an invalid format is provided, it will revert to the default format.
        /// </summary>
        public string DateTimeFormat
        {
            get { return dateTimeFormat; }
            set
            {
                // Check if the format is valid
                try
                {
                    DateTime.Now.ToString(value);
                    dateTimeFormat = value;
                }
                catch (FormatException)
                {
                    // If the format is invalid, revert to the default format
                    dateTimeFormat = "yyyy-MM-ddTHH:mm:ss";
                }
            }
        }
    }
}
