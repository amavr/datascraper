using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Xml;
using System.Runtime.Serialization;

namespace DataScraper
{
    public enum PeriodType
    {
        DAY,
        WEEK,
        MONTH
    }

    [Serializable]
    public class ScriptDateAction : ScriptAction
    {

        public ScriptDateAction()
        {
            ImageIndex = 9;
            Label = "Date action";
            NetFormat = "dd.MM.yyyy";
            Description = "(.Net date format) != (PHP date format)";
        }

        public ScriptDateAction(SerializationInfo Info, StreamingContext Context)
            : base(Info, Context)
        {
            ImageIndex = 9;
            LoadProps(Info);
        }

        protected override void InternalExecute()
        {
            DateTime dt = DateTime.Now;
            int days = 0;

            switch(Period)
            {
                case PeriodType.DAY:
                    dt = dt.AddDays(Offset);
                    break;
                case PeriodType.WEEK:
                    dt = dt.AddDays(Offset * 7);
                    if (AlignBound) days = (int)dt.DayOfWeek - 1; // Sunday is 0
                    break;
                case PeriodType.MONTH:
                    dt = dt.AddMonths(Offset);
                    if (AlignBound) days = (int)dt.Day - 1;
                    break;
            }
            dt = dt.AddDays(-days);

            OutputFlow = dt.ToString(NetFormat);
            ExecuteChild(OutputFlow);
        }

        public override void SetAttributes(XmlElement Element)
        {
            Element.SetAttribute("type", ScriptConsts.TYPE_DATE);
            Element.SetAttribute("format", NetFormat);
            Element.SetAttribute("format_php", PhpFormat);
            Element.SetAttribute("period", Period.ToString());
            Element.SetAttribute("offset", Offset.ToString());
            Element.SetAttribute("align", AlignBound.ToString());
        }

        public override void GetAttributes(XmlElement Element)
        {
            NetFormat = Element.GetAttribute("format");
            PhpFormat = Element.GetAttribute("format_php");
            Period = (PeriodType)Enum.Parse(typeof(PeriodType), Element.GetAttribute("period"));
            Offset = GetPropInt(Element, "offset", 0);
            AlignBound = GetPropBool(Element, "align", true);
        }

        protected override void SaveProps(SerializationInfo Info)
        {
            Info.AddValue("format", NetFormat);
            Info.AddValue("format_php", PhpFormat);
            Info.AddValue("period", Period.ToString());
            Info.AddValue("offset", Offset);
            Info.AddValue("align", AlignBound);
        }

        protected override void LoadProps(SerializationInfo Info)
        {
            NetFormat = Info.GetString("format");
            PhpFormat = Info.GetString("format_php");
            Period = (PeriodType)Enum.Parse(typeof(PeriodType), Info.GetString("period"));
            Offset = Info.GetInt32("offset");
            AlignBound = Info.GetBoolean("align");
        }

        [DisplayName(".Net format")]
        public string NetFormat { get; set; }

        [DisplayName("PHP format")]
        public string PhpFormat { get; set; }

        public PeriodType Period { get; set; }

        public int Offset { get; set; }

        [DisplayName("Align bound")]
        public bool AlignBound { get; set; }

    }
}
