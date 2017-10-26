using System.Collections.Generic;
using System.Linq;
using System.Text;
using hyjiacan.py4n.exception;
using hyjiacan.py4n.format;
using hyjiacan.py4n.data;
using System;

namespace hyjiacan.py4n
{
    public static class Pinyin4Net
    {
        #region // 获取单字拼音
        /// <summary>
        /// 获取汉字的拼音数组
        /// </summary>
        /// <param name="hanzi">要查询拼音的汉字字符</param>
        /// <returns>汉字的拼音数组，若未找到汉字拼音，则返回空数组</returns>
        /// <exception cref="UnsupportedUnicodeException">当要获取拼音的字符不是汉字时抛出此异常</exception>
        public static string[] GetPinyin(char hanzi)
        {
            if (PinyinUtil.IsHanzi(hanzi))
            {
                return PinyinDB.Instance.GetPinyin(hanzi);
            }
            // 不是汉字
            throw new UnsupportedUnicodeException("不支持的字符: 请输入汉字");
        }

        /// <summary>
        /// 获取唯一拼音(单音字)或者第一个拼音(多音字)
        /// </summary>
        /// <param name="hanzi">要查询拼音的汉字字符</param>
        /// <returns>返回唯一拼音(单音字)或者第一个拼音(多音字)</returns>
        /// <exception cref="UnsupportedUnicodeException">当要获取拼音的字符不是汉字时抛出此异常</exception>
        public static string GetUniqueOrFirstPinyin(char hanzi)
        {
            return GetPinyin(hanzi)[0];
        }
        /// <summary>
        /// 获取格式化后的拼音
        /// </summary>
        /// <param name="hanzi">要查询拼音的汉字字符</param>
        /// <param name="format">拼音输出格式化参数</param>
        /// <see cref="PinyinOutputFormat"/>
        /// <seealso cref="PinyinFormatter"/>
        /// <returns>经过格式化的拼音</returns>
        /// <exception cref="UnsupportedUnicodeException">当要获取拼音的字符不是汉字时抛出此异常</exception>
        public static string[] GetPinyinWithFormat(char hanzi, PinyinOutputFormat format)
        {
            return GetPinyin(hanzi).Select(item => PinyinFormatter.Format(item, format)).ToArray();
        }

        /// <summary>
        /// 获取格式化后的唯一拼音(单音字)或者第一个拼音(多音字)
        /// </summary>
        /// <param name="hanzi">要查询拼音的汉字字符</param>
        /// <param name="format">拼音输出格式化参数</param>
        /// <see cref="PinyinOutputFormat"/>
        /// <seealso cref="PinyinFormatter"/>
        /// <returns>格式化后的唯一拼音(单音字)或者第一个拼音(多音字)</returns>
        /// <exception cref="UnsupportedUnicodeException">当要获取拼音的字符不是汉字时抛出此异常</exception>
        public static string GetUniqueOrFirstPinyinWithFormat(char hanzi, PinyinOutputFormat format)
        {
            return PinyinFormatter.Format(GetUniqueOrFirstPinyin(hanzi), format);
        }
        #endregion

        /// <summary>
        /// 获取一个字符串内所有汉字的拼音（多音字取第一个读音，带格式）
        /// </summary>
        /// <param name="text">要获取拼音的汉字字符串</param>
        /// <param name="format">拼音输出格式化参数</param>
        /// <param name="caseSpread">是否将前面的格式中的大小写扩展到其它非拼音字符，默认为false。firstLetterOnly为false时有效 </param>
        /// <param name="firstLetterOnly">是否只取拼音首字母，为true时，format无效</param>
        /// <param name="multiFirstLetter">firstLetterOnly为true时有效，多音字的多个读音首字母是否全取，如果多音字拼音首字母相同，只保留一个</param>
        /// <returns>firstLetterOnly为true时，只取拼音首字母格式为[L]，后面追加空格；multiFirstLetter为true时，多音字的多个拼音首字母格式为[L, H]，后面追加空格</returns>
        public static string GetPinyin(string text, PinyinOutputFormat format, bool caseSpread, bool firstLetterOnly, bool multiFirstLetter)
        {
            if (string.IsNullOrEmpty(text)) return "";

            var pinyin = new StringBuilder();
            var firstLetterBuf = new List<string>();

            foreach (var item in text)
            {
                if (!PinyinUtil.IsHanzi(item))
                {
                    pinyin.Append(item);
                    continue;
                }

                if (!firstLetterOnly)
                {
                    pinyin.Append(GetUniqueOrFirstPinyinWithFormat(item, format) + " ");
                    continue;
                }
                if (!multiFirstLetter)
                {
                    pinyin.AppendFormat("[{0}] ", GetUniqueOrFirstPinyin(item)[0]);
                    continue;
                }

                firstLetterBuf.Clear();

                firstLetterBuf.AddRange(GetPinyin(item)
                    .Select(py => py[0].ToString())
                    // 这句是处理多音字，多音字的拼音可能首字母是一样的，
                    // 如果是一样的，肯定就只返回一次
                    .Distinct());

                pinyin.AppendFormat("[{0}] ", string.Join(",", firstLetterBuf.ToArray()));
            }

            #region // 扩展大小写格式
            if (firstLetterOnly || !caseSpread)
            {
                return pinyin.ToString().Trim();
            }

            switch (format.GetCaseFormat)
            {
                case CaseFormat.CAPITALIZE_FIRST_LETTER:
                    return CapitalizeFirstLetter(pinyin);
                case CaseFormat.LOWERCASE:
                    return pinyin.ToString().Trim().ToLower();
                case CaseFormat.UPPERCASE:
                    return pinyin.ToString().Trim().ToUpper();
                default:
                    return pinyin.ToString().Trim();
            }
            #endregion
        }

        /// <summary>
        /// 获取一个字符串内所有汉字的拼音（多音字取第一个读音，带格式）
        /// </summary>
        /// <param name="text">要获取拼音的汉字字符串</param>
        /// <param name="format">拼音输出格式化参数</param>
        /// <param name="caseSpread">是否将前面的格式中的大小写扩展到其它非拼音字符，默认为false。firstLetterOnly为false时有效 </param>
        /// <param name="pinyinHandler">
        /// 拼音处理器，在获取到拼音后通过这个来处理，
        /// 如果传null，则默认取第一个拼音（多音字），
        /// 参数：
        /// 1 string[] 拼音数组
        /// 2 char 当前的汉字
        /// 3 string 要转成拼音的字符串
        /// return 拼音字符串，这个返回值将作为这个汉字的拼音放到结果中
        /// </param>
        /// <returns>firstLetterOnly为true时，只取拼音首字母格式为[L]，后面追加空格；multiFirstLetter为true时，多音字的多个拼音首字母格式为[L, H]，后面追加空格</returns>
        public static string GetPinyin(string text, PinyinOutputFormat format, bool caseSpread, Func<string[], char, string, string> pinyinHandler)
        {
            if (string.IsNullOrEmpty(text)) return "";

            var pinyin = new StringBuilder();
            var firstLetterBuf = new List<string>();

            foreach (var item in text)
            {
                if (!PinyinUtil.IsHanzi(item))
                {
                    pinyin.Append(item);
                    continue;
                }

                var pinyinTemp = PinyinDB.Instance.GetPinyin(item);

                pinyin.Append(pinyinHandler == null ?
                    pinyinTemp[0] :
                    pinyinHandler.Invoke(pinyinTemp, item, text));

                firstLetterBuf.Clear();

                firstLetterBuf.AddRange(GetPinyin(item)
                    .Where(py => !firstLetterBuf.Contains(py[0].ToString()))
                    .Select(py => py[0].ToString()));

                pinyin.AppendFormat("[{0}] ", string.Join(",", firstLetterBuf.ToArray()));
            }
            #region // 扩展大小写格式
            if (!caseSpread)
            {
                return pinyin.ToString().Trim();
            }

            switch (format.GetCaseFormat)
            {
                case CaseFormat.CAPITALIZE_FIRST_LETTER:
                    return CapitalizeFirstLetter(pinyin).Trim();
                case CaseFormat.LOWERCASE:
                    return pinyin.ToString().ToLower();
                case CaseFormat.UPPERCASE:
                    return pinyin.ToString().ToUpper();
                default:
                    return pinyin.ToString();
            }
            #endregion
        }

        /// <summary>
        /// 获取一个字符串内所有汉字的拼音（多音字取第一个读音，带格式），format中指定的大小写模式不会扩展到非拼音字符
        /// </summary>
        /// <param name="text">要获取拼音的汉字字符串</param>
        /// <param name="format">拼音输出格式化参数</param>
        /// <returns>格式化后的拼音字符串</returns>
        public static string GetPinyin(string text, PinyinOutputFormat format)
        {
            return GetPinyin(text, format, false, false, false);
        }
        /// <summary>
        /// 根据单个拼音查询匹配的汉字
        /// </summary>
        /// <param name="pinyin">要查询汉字的单个拼音</param>
        /// <param name="matchAll">是否全部匹配，为true时，匹配整个拼音，否则匹配开头字符</param>
        /// <returns></returns>
        public static string[] GetHanzi(string pinyin, bool matchAll)
        {
            return PinyinDB.Instance.GetHanzi(pinyin.ToLower(), matchAll);
        }

        /// <summary>
        /// 将首字母搞成大写的
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private static string CapitalizeFirstLetter(StringBuilder buffer)
        {
            // 遇到空格后，将后面一个非空格的字符设置为大写
            for (var i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] != ' ')
                {
                    continue;
                }

                // 当前是最后一个字符
                if (i == buffer.Length - 1)
                {
                    continue;
                }

                var nextchar = buffer[i + 1];

                // 后一个字符是空格
                if (nextchar == ' ')
                {
                    continue;
                }

                buffer[i + 1] = char.ToUpper(nextchar);
            }

            return buffer.ToString();
        }
    }
}
