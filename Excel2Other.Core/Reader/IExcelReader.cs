namespace Excel2Other
{
    public interface IExcelReader
    {
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>是否读取成功</returns>
        bool Read(string path);
    }
}
