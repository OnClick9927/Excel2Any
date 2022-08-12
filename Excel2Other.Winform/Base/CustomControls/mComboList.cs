using Sunny.UI;
using System.Windows.Forms;

namespace Excel2Other.Winform
{
    public partial class mComboList :UIDropControl,IToolTip
    {
        public mComboList()
        {
            InitializeComponent();
        }

        public Control ExToolTipControl()
        {
            return edit;
        }
    }
}
