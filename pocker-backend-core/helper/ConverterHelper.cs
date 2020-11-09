using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using pocker_backend_core.lobby;

namespace pocker_backend_core.helper
{
    [TypeConverter(typeof(StringArrayConverter))]
    public sealed class StringArrayConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(value is string)) throw new NotSupportedException();
            return ((string) value).Split(";").ToArray();
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            throw new NotSupportedException();
        }
    }

    [TypeConverter(typeof(LobbyConverter))]
    public class LobbyConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(value is string)) throw new NotSupportedException();
            return JsonHelper.Deserialize<Lobby>((string) value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            throw new NotSupportedException();
        }
    }
}