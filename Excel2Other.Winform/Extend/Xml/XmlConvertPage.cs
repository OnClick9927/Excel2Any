
using FastColoredTextBoxNS;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Excel2Other.Winform
{
    public partial class XmlConvertPage : TextConvertPage
    {
        public XmlConvertPage()
        {
            InitializeComponent();
        }

        private static readonly Platform platformType = PlatformType.GetOperationSystemPlatform();
        public static RegexOptions RegexCompiledOption
        {
            get
            {
                if (platformType == Platform.X86)
                    return RegexOptions.Compiled;
                else
                    return RegexOptions.None;
            }
        }
        #region 画笔颜色
        TextStyle bracketStyle = new TextStyle(new SolidBrush(Color.FromArgb(255, 105, 105, 105)), null, FontStyle.Regular); //括号
        TextStyle tagStyle = new TextStyle(new SolidBrush(Color.FromArgb(255, 86, 156, 214)), null, FontStyle.Regular); //标签
        TextStyle attributeStyle = new TextStyle(new SolidBrush(Color.FromArgb(255, 156, 200, 254)), null, FontStyle.Regular); //属性
        TextStyle valueStyle = new TextStyle(new SolidBrush(Color.FromArgb(255, 206, 145, 120)), null, FontStyle.Regular); //字符串
        TextStyle commentStyle = new TextStyle(new SolidBrush(Color.FromArgb(255, 106, 153, 85)), null, FontStyle.Regular); //注释


        #endregion

        #region 正则
        Regex XMLCommentRegex1 = new Regex(@"(<!--.*?-->)|(<!--.*)", RegexOptions.Singleline | RegexCompiledOption);
        Regex XMLCommentRegex2 = new Regex(@"(<!--.*?-->)|(.*-->)", RegexOptions.Singleline | RegexOptions.RightToLeft | RegexCompiledOption);

        Regex XMLTagRegex = new Regex(@"<\?|</|>|<|/>|\?>", RegexCompiledOption);
        Regex XMLTagNameRegex = new Regex(@"<[?](?<range1>[x][m][l]{1})|<(?<range>[!\w:\-\.]+)", RegexCompiledOption);
        Regex XMLEndTagRegex = new Regex(@"</(?<range>[\w:\-\.]+)>", RegexCompiledOption);

        Regex XMLTagContentRegex = new Regex(@"<[^>]+>", RegexCompiledOption);
        Regex XMLAttrRegex =
                new Regex(
                    @"(?<range>[\w\d\-\:]+)[ ]*=[ ]*'[^']*'|(?<range>[\w\d\-\:]+)[ ]*=[ ]*""[^""]*""|(?<range>[\w\d\-\:]+)[ ]*=[ ]*[\w\d\-\:]+",
                    RegexCompiledOption);
        Regex XMLAttrValRegex =
                new Regex(
                    @"[\w\d\-]+?=(?<range>'[^']*')|[\w\d\-]+[ ]*=[ ]*(?<range>""[^""]*"")|[\w\d\-]+[ ]*=[ ]*(?<range>[\w\d\-]+)",
                    RegexCompiledOption);
        Regex XMLFoldingRegex = new Regex(@"<(?<range>/?[\w:\-\.]+)\s[^>]*?[^/]>|<(?<range>/?[\w:\-\.]+)\s*>", RegexOptions.Singleline | RegexCompiledOption);
        #endregion
        public void XMLSyntaxHighlight(TextChangedEventArgs e)
        {
            txtCode.CommentPrefix = null;
            txtCode.LeftBracket = '<';
            txtCode.RightBracket = '>';
            txtCode.LeftBracket2 = '(';
            txtCode.RightBracket2 = ')';
            txtCode.AutoIndentCharsPatterns = @"";
            //clear style of changed range
            e.ChangedRange.ClearStyle(bracketStyle, tagStyle, attributeStyle, valueStyle, commentStyle);

            //comment highlighting
            e.ChangedRange.SetStyle(commentStyle, XMLCommentRegex1);
            e.ChangedRange.SetStyle(commentStyle, XMLCommentRegex2);

            //tag brackets highlighting
            e.ChangedRange.SetStyle(bracketStyle, XMLTagRegex);

            //tag name
            e.ChangedRange.SetStyle(tagStyle, XMLTagNameRegex);

            //end of tag
            e.ChangedRange.SetStyle(tagStyle, XMLEndTagRegex);

            //attributes
            e.ChangedRange.SetStyle(attributeStyle, XMLAttrRegex);

            //attribute values
            e.ChangedRange.SetStyle(valueStyle, XMLAttrValRegex);

            //clear folding markers
            e.ChangedRange.ClearFoldingMarkers();

            //set folding markers
            XmlFolding(e.ChangedRange);
        }


        class XmlFoldingTag
        {
            public string Name;
            public int id;
            public int startLine;
            public string Marker { get { return Name + id; } }
        }

        private void XmlFolding(Range range)
        {
            var stack = new Stack<XmlFoldingTag>();
            var id = 0;
            var fctb = txtCode;
            //extract opening and closing tags (exclude open-close tags: <TAG/>)
            foreach (var r in range.GetRanges(XMLFoldingRegex))
            {
                var tagName = r.Text;
                var iLine = r.Start.iLine;
                //if it is opening tag...
                if (tagName[0] != '/')
                {
                    // ...push into stack
                    var tag = new XmlFoldingTag { Name = tagName, id = id++, startLine = r.Start.iLine };
                    stack.Push(tag);
                    // if this line has no markers - set marker
                    if (string.IsNullOrEmpty(fctb[iLine].FoldingStartMarker))
                        fctb[iLine].FoldingStartMarker = tag.Marker;
                }
                else
                {
                    //if it is closing tag - pop from stack
                    if (stack.Count > 0)
                    {
                        var tag = stack.Pop();
                        //compare line number
                        if (iLine == tag.startLine)
                        {
                            //remove marker, because same line can not be folding
                            if (fctb[iLine].FoldingStartMarker == tag.Marker) //was it our marker?
                                fctb[iLine].FoldingStartMarker = null;
                        }
                        else
                        {
                            //set end folding marker
                            if (string.IsNullOrEmpty(fctb[iLine].FoldingEndMarker))
                                fctb[iLine].FoldingEndMarker = tag.Marker;
                        }
                    }
                }
            }
        }
        private void txtCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            XMLSyntaxHighlight(e);
        }
    }
}
