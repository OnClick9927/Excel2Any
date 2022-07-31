using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Other.Winform
{
    public abstract class UIEntity
    {
        /// <summary>
        /// 转换名称
        /// </summary>
        public abstract string name { get; }
        /// <summary>
        /// 页面
        /// </summary>
        public abstract BaseConvertPage page { get; }
        /// <summary>
        /// 设置
        /// </summary>
        public abstract BaseSetting setting { get; set; }
        /// <summary>
        /// 图标ID
        /// </summary>
        public abstract int symbol { get; }

        public abstract int pageIndex { get; }
    }
}
