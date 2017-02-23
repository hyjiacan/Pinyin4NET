using hyjiacan.py4n.data;
using hyjiacan.py4n.exception;
using System.Linq;

namespace hyjiacan.py4n
{
    /// <summary>
    /// 处理姓名专用
    /// </summary>
    public static class Pinyin4Name
    {
        #region // 获取单字拼音
        /// <summary>
        /// 获取汉字的拼音数组
        /// </summary>
        /// <param name="hanzi"></param>
        /// <returns>若未找到汉字拼音，则返回空数组</returns>
        /// <exception cref="UnsupportedUnicodeException">当要获取拼音的字符不是汉字时抛出此异常</exception>
        public static string GetPinyin(string hanzi)
        {
            if (hanzi.All(ch => PinyinUtil.IsHanzi(ch)))
            {
                return NameDB.Instance.GetPinyin(hanzi);
            }
            // 不是汉字
            throw new UnsupportedUnicodeException("不支持的字符: 请输入汉字字符");
        }

        /// <summary>
        /// 获取姓的首字母，如果是复姓则返回由空格的首字母
        /// </summary>
        /// <param name="hanzi"></param>
        /// <returns></returns>
        /// <exception cref="UnsupportedUnicodeException">当要获取拼音的字符不是汉字时抛出此异常</exception>
        public static string GetFirstLetter(string hanzi)
        {
            return string.Join(" ", GetPinyin(hanzi).Split(' ').Select(py => py[0]));
        }

        /// <summary>
        /// 获取格式化后的拼音
        /// </summary>
        /// <param name="hanzi"></param>
        /// <param name="format"></param>
        /// <see cref="PinyinOutputFormat"/>
        /// <seealso cref="PinyinFormatter"/>
        /// <returns></returns>
        /// <exception cref="UnsupportedUnicodeException">当要获取拼音的字符不是汉字时抛出此异常</exception>
        public static string GetPinyinWithFormat(string hanzi, PinyinOutputFormat format)
        {
            return string.Join(" ", GetPinyin(hanzi).Split(' ').Select(item => PinyinFormatter.Format(item, format)));
        }
        #endregion

        /// <summary>
        /// 根据拼音查询匹配的汉字
        /// </summary>
        /// <param name="pinyin"></param>
        /// <param name="matchAll">是否全部匹配，为true时，匹配整个拼音，否则匹配开头字符</param>
        /// <returns></returns>
        public static string[] GetHanzi(string pinyin, bool matchAll)
        {
            return NameDB.Instance.GetHanzi(pinyin.ToLower(), matchAll);
        }
    }
}
