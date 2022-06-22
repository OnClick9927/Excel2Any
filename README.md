# Excel2Other
这个工具可以将Excel转换成其他格式的文档，目前支持Json、Xml、C#，支持继续扩展核心代码，欢迎大家使用。

参考[neil3d/excel2json](https://github.com/neil3d/excel2json)优化了部分代码和增减了其他配置，界面使用了SunnyUI并且参考VSCode配色美化

实现思路：读取Excel到DataSet中，根据用户的配置读取DataSet并转为其他文本。Json部分使用Newtonsoft.Json处理；C#自己拼字符串；Xml使用了System.Xml生成。

## 不足之处
* 代码写的不好，特别是设置这一部分
* SunnyUI还是有些做不出来的效果，头疼
* **如果有出现bug或者有其他想法的请提交issue告知**

# 感谢
参考或使用的相关插件和仓库：
1. [NewtonSoft.Json](https://github.com/JamesNK/Newtonsoft.Json)
2. [SunnyUI](https://gitee.com/yhuse/SunnyUI)
3. [FastColoredTextBox](https://github.com/PavelTorgashov/FastColoredTextBox)
4. [neil3d/excel2json](https://github.com/neil3d/excel2json)

