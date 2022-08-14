using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Any.Winform
{
    [Entity(typeof(JsonEntity))]
    public class UIJsonEntity : UIEntity
    {
        public override string name { get; } = "Json";
        public override BaseConvertPage page { get; } = new JsonConvertPage();

        public override BaseSetting setting { get; set; } = new JsonSetting();

        public override int symbol { get; } = 261787;

        public override int pageIndex { get; } = 1001;
    }
}
