using FastColoredTextBoxNS;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Excel2Other.Winform
{
    public partial class CSharpConvertPage : TextConvertPage
    {
        public CSharpConvertPage()
        {
            InitializeComponent();
        }

        //CSstyles
        TextStyle keyWordStyle = new TextStyle(new SolidBrush(Color.FromArgb(255, 85, 156, 214)), null, FontStyle.Regular); //关键词
        TextStyle classStyle = new TextStyle(new SolidBrush(Color.FromArgb(255, 78, 201, 176)), null, FontStyle.Regular); //类名
        TextStyle commentStyle = new TextStyle(new SolidBrush(Color.FromArgb(255, 106, 153, 85)), null, FontStyle.Regular); //注释
        TextStyle structStyle = new TextStyle(new SolidBrush(Color.FromArgb(255, 134, 198, 115)), null, FontStyle.Regular); //注释

        private void CSharpSyntaxHighlight(TextChangedEventArgs e)
        {
            txtCode.LeftBracket = '(';
            txtCode.RightBracket = ')';
            txtCode.LeftBracket2 = '\x0';
            txtCode.RightBracket2 = '\x0';
            //clear style of changed range
            e.ChangedRange.ClearStyle(keyWordStyle, classStyle, commentStyle);

            //comment highlighting
            e.ChangedRange.SetStyle(commentStyle, @"//.*$", RegexOptions.Multiline);

            //class name highlighting
            e.ChangedRange.SetStyle(classStyle, @"\b(class|enum|struct|interface)\s+(?<range>\w+?)\b");

            //struct name highlighting
            e.ChangedRange.SetStyle(structStyle, @"\b(DateTime|Date|date)\b|#region\b|#endregion\b");

            //keyword highlighting
            e.ChangedRange.SetStyle(keyWordStyle, @"\b(abstract|as|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|long|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|virtual|void|volatile|while|add|alias|ascending|descending|dynamic|from|get|global|group|into|join|let|orderby|partial|remove|select|set|value|var|where|yield)\b|#region\b|#endregion\b");

            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();

            //set folding markers
            e.ChangedRange.SetFoldingMarkers("{", "}");//allow to collapse brackets block
            e.ChangedRange.SetFoldingMarkers(@"#region\b", @"#endregion\b");//allow to collapse #region blocks
            e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/");//allow to collapse comment block
        }

        private void txtCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            CSharpSyntaxHighlight(e);
        }
    }
}
