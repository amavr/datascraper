using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace DataScraper
{
    /// <summary>
    /// TypeConverter для разных кодировок
    /// </summary>
    public class EncodingTypeConverter : StringConverter
    {
        /// <summary>
        /// Будем предоставлять выбор из списка
        /// </summary>
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// ... и только из списка
        /// </summary>
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            // false - можно вводить вручную
            // true - только выбор из списка
            return true;
        }

        /// <summary>
        /// А вот и список
        /// </summary>
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            ArrayList list = new ArrayList();
            foreach (EncodingInfo info in Encoding.GetEncodings())
//                list.Add(info.DisplayName);
                  list.Add(info.Name);

            list.Sort();
            // возвращаем список строк из настроек программы
            // (базы данных, интернет и т.д.)
            return new StandardValuesCollection(list);
        }
    }

}
