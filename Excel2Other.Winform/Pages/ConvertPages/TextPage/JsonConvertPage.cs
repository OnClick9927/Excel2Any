using FastColoredTextBoxNS;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Excel2Other.Winform
{
    public partial class JsonConvertPage : TextConvertPage
    {
        public JsonConvertPage() : base()
        {
            InitializeComponent();
            txtCode.Language = Language.Custom;
        }

        TextStyle keyStyle = new TextStyle(new SolidBrush(Color.FromArgb(255, 156, 200, 254)), null, FontStyle.Regular); //键
        TextStyle valueStyle = new TextStyle(new SolidBrush(Color.FromArgb(255, 206, 145, 120)), null, FontStyle.Regular); //值（字符串）
        TextStyle numStyle = new TextStyle(new SolidBrush(Color.FromArgb(255, 181, 206, 168)), null, FontStyle.Regular); //数字


        private void JsonSyntaxHighlight(TextChangedEventArgs e)
        {

            e.ChangedRange.ClearStyle(keyStyle, valueStyle, numStyle);
            //key highlighting
            e.ChangedRange.SetStyle(keyStyle, @"(?<range>""([^\\""]|\\"")*"")\s*:");
            //value highlighting
            e.ChangedRange.SetStyle(valueStyle, @"""([^\\""]|\\"")*""");
            //number highlighting
            e.ChangedRange.SetStyle(numStyle, @"\b(\d+[\.]?\d*|true|false|null)\b");




            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();

            //set folding markers
            e.ChangedRange.SetFoldingMarkers("{", "}");
        }

        private void txtCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            JsonSyntaxHighlight(e);
        }
    }
}
