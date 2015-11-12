using hyjiacan.py4n.exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace hyjiacan.py4n
{
    public static class Pinyin4Net
    {
        /// <summary>
        /// 获取汉字的拼音数组
        /// </summary>
        /// <param name="hanzi"></param>
        /// <returns>若未找到汉字拼音，则返回空数组</returns>
        /// <exception cref="UnsupportedUnicodeException">当要获取拼音的字符不是汉字时抛出此异常</exception>
        public static string[] GetPinyin(char hanzi)
        {
            if (PinyinUtil.IsHanzi(hanzi))
            {
                return PinyinDB.Instance.GetPinyin(hanzi);
            }
            else
            {
                // 不是汉字
                throw new UnsupportedUnicodeException("不支持的字符: 请输入汉字字符");
            }
        }

        /// <summary>
        /// 获取唯一拼音(单音字)或者第一个拼音(多音字)
        /// </summary>
        /// <param name="hanzi"></param>
        /// <returns></returns>
        /// <exception cref="UnsupportedUnicodeException">当要获取拼音的字符不是汉字时抛出此异常</exception>
        public static string GetUniqueOrFirstPinyin(char hanzi)
        {
            return GetPinyin(hanzi)[0];
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
        public static string[] GetPinyinWithFormat(char hanzi, PinyinOutputFormat format)
        {
            List<string> fmtedPY = new List<string>();
            foreach (string item in GetPinyin(hanzi))
            {
                fmtedPY.Add(PinyinFormatter.Format(item, format));
            }

            return fmtedPY.ToArray();
        }

        /// <summary>
        /// 获取格式化后的唯一拼音(单音字)或者第一个拼音(多音字)
        /// </summary>
        /// <param name="hanzi"></param>
        /// <param name="format"></param>
        /// <see cref="PinyinOutputFormat"/>
        /// <seealso cref="PinyinFormatter"/>
        /// <returns></returns>
        /// <exception cref="UnsupportedUnicodeException">当要获取拼音的字符不是汉字时抛出此异常</exception>
        public static string GetUniqueOrFirstPinyinWithFormat(char hanzi, PinyinOutputFormat format)
        {
            string pinyin = GetUniqueOrFirstPinyin(hanzi);
            string fmtedPY = PinyinFormatter.Format(pinyin, format);

            return fmtedPY;
        }
    }
}
