
using FastColoredTextBoxNS;
using Sunny.UI;
using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Excel2Any.Winform
{
    public partial class LuaConvertPage : TextConvertPage
    {
        protected Regex LuaCommentRegex1,
                      LuaCommentRegex2,
                      LuaCommentRegex3;

        protected Regex LuaKeywordRegex;
        protected Regex LuaNumberRegex;
        protected Regex LuaStringRegex;
        protected Regex LuaFunctionsRegex;
        public LuaConvertPage()
        {
            InitializeComponent();

            LuaStringRegex = new Regex(@"""""|''|"".*?[^\\]""|'.*?[^\\]'");
            LuaCommentRegex1 = new Regex(@"--.*$", RegexOptions.Multiline);
            LuaCommentRegex2 = new Regex(@"(--\[\[.*?\]\])|(--\[\[.*)", RegexOptions.Singleline);
            LuaCommentRegex3 = new Regex(@"(--\[\[.*?\]\])|(.*\]\])",
                                             RegexOptions.Singleline | RegexOptions.RightToLeft);
            LuaNumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            LuaKeywordRegex =
                new Regex(
                    @"\b(and|break|do|else|elseif|end|false|for|function|if|in|local|nil|not|or|repeat|return|then|true|until|while)\b");

            LuaFunctionsRegex =
                new Regex(
                    @"\b(assert|collectgarbage|dofile|error|getfenv|getmetatable|ipairs|load|loadfile|loadstring|module|next|pairs|pcall|print|rawequal|rawget|rawset|require|select|setfenv|setmetatable|tonumber|tostring|type|unpack|xpcall)\b");
        }


        Style StringStyle = new TextStyle(new SolidBrush(Color.FromArgb(255, 206, 146, 120)), null, FontStyle.Regular);
        Style CommentStyle = new TextStyle(new SolidBrush(Color.FromArgb(255, 106, 153, 85)), null, FontStyle.Regular);
        Style NumberStyle = new TextStyle(new SolidBrush(Color.FromArgb(255, 181, 206, 168)), null, FontStyle.Regular);
        Style KeywordStyle = new TextStyle(new SolidBrush(Color.FromArgb(255, 86, 156, 214)), null, FontStyle.Regular);
        Style FunctionsStyle = new TextStyle(new SolidBrush(Color.FromArgb(255, 220, 220, 170)), null, FontStyle.Regular);

        private void LuaSyntaxHighlight(TextChangedEventArgs e)
        {
            e.ChangedRange.tb.CommentPrefix = "--";
            e.ChangedRange.tb.LeftBracket = '(';
            e.ChangedRange.tb.RightBracket = ')';
            e.ChangedRange.tb.LeftBracket2 = '{';
            e.ChangedRange.tb.RightBracket2 = '}';
            e.ChangedRange.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;

            e.ChangedRange.tb.AutoIndentCharsPatterns
                = "\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>.+)\n";

            //clear style of changed range
            e.ChangedRange.ClearStyle(StringStyle, CommentStyle, NumberStyle, KeywordStyle, FunctionsStyle);
            //string highlighting
            e.ChangedRange.SetStyle(StringStyle, LuaStringRegex);
            //comment highlighting
            e.ChangedRange.SetStyle(CommentStyle, LuaCommentRegex1);
            e.ChangedRange.SetStyle(CommentStyle, LuaCommentRegex2);
            e.ChangedRange.SetStyle(CommentStyle, LuaCommentRegex3);
            //number highlighting
            e.ChangedRange.SetStyle(NumberStyle, LuaNumberRegex);
            //keyword highlighting
            e.ChangedRange.SetStyle(KeywordStyle, LuaKeywordRegex);
            //functions highlighting
            e.ChangedRange.SetStyle(FunctionsStyle, LuaFunctionsRegex);
            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();
            //set folding markers
            e.ChangedRange.SetFoldingMarkers("{", "}"); //allow to collapse brackets block
            e.ChangedRange.SetFoldingMarkers(@"--\[\[", @"\]\]"); //allow to collapse comment block

        }


        protected override void txtCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            LuaSyntaxHighlight(e);
        }
    }
}
