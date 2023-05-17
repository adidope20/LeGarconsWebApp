using Ardalis.SmartEnum;
using Ardalis.SmartEnum.SystemTextJson;
using System.Text.Json.Serialization;

namespace MobyLabWebProgramming.Core.Enums
{
    [JsonConverter(typeof(SmartEnumNameConverter<PropertyTypeEnum, string>))]
    public sealed class PropertyTypeEnum : SmartEnum<PropertyTypeEnum, string>
    {
        public static readonly PropertyTypeEnum Studio = new(nameof(Studio), "Studio");
        public static readonly PropertyTypeEnum TwoRooms = new(nameof(TwoRooms), "TwoRooms");
        public static readonly PropertyTypeEnum ThreeRooms = new(nameof(ThreeRooms), "ThreeRooms");
        public static readonly PropertyTypeEnum FourRooms = new(nameof(FourRooms), "FourRooms");

        private PropertyTypeEnum(string name, string value) : base(name, value)
        {
        }
    }
}
