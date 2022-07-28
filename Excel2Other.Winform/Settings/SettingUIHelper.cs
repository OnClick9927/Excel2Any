using Sunny.UI;
using System.Drawing;
using System.Windows.Forms;

namespace Excel2Other.Winform
{
    public class SettingUIHelper
    {
        public static UILabel GetHeaderLabel(string headerText)
        {
            var headerLabel = new UILabel();
            headerLabel.Style = UIStyle.Custom;
            headerLabel.StyleCustomMode = true;
            headerLabel.Font = new Font("微软雅黑", 14.25f, FontStyle.Bold);
            headerLabel.ForeColor = Color.White;
            headerLabel.TextAlign = ContentAlignment.MiddleLeft;
            headerLabel.Size = new Size(500, 28);
            headerLabel.Text = headerText;
            return headerLabel;
        }

        public static UILabel GetContentLabel(string content)
        {
            var headerLabel = new UILabel();
            headerLabel.Style = UIStyle.Custom;
            headerLabel.StyleCustomMode = true;
            headerLabel.Font = new Font("微软雅黑", 12f);
            headerLabel.ForeColor = Color.White;
            headerLabel.TextAlign = ContentAlignment.MiddleLeft;
            headerLabel.Size = new Size(500, 21);
            headerLabel.Text = content;
            return headerLabel;
        }

        public static UITextBox GetInputBox(string def, StringType stringType)
        { 
            var inputBox = new UITextBox();
            inputBox.Style = UIStyle.Custom;
            inputBox.StyleCustomMode = true;
            inputBox.TextAlignment = ContentAlignment.MiddleLeft;
            inputBox.Text = def;

            return inputBox;
        }

        public static TabPage GetNewTabPage(string tabName)
        {
            var tabPage = new TabPage();
            tabPage.BackColor = Color.FromArgb(30, 30, 30);
            tabPage.Text = tabName;
            return tabPage;
        }

    }
}
