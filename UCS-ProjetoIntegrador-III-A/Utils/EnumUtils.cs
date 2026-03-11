using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UCS_ProjetoIntegrador_III_A.Utils
{
    public static class EnumUtils
    {
        public static string GetEnumDescription(Enum value)
        {
            if (value == null) return string.Empty;
            var fi = value.GetType().GetField(value.ToString());
            if (fi == null) return value.ToString();
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}
