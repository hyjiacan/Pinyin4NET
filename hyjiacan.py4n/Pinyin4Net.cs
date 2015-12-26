using hyjiacan.py4n.exception;
using hyjiacan.py4n.format;
using System.Collections.Generic;
using System.Text;

namespace hyjiacan.py4n
{
    public static class Pinyin4Net
    {
        #region // 获取单字拼音
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
        #endregion

        /// <summary>
        /// 获取一个字符串内所有汉字的拼音（多音字取第一个读音，带格式）
        /// </summary>
        /// <param name="text"></param>
        /// <param name="format">拼音格式</param>
        /// <param name="caseSpread">是否将前面的格式中的大小写扩展到其它非拼音字符，默认为false。firstLetterOnly为false时有效 </param>
        /// <param name="firstLetterOnly">是否只取拼音首字母，为true时，format无效</param>
        /// <param name="multiFirstLetter">firstLetterOnly为true时有效，多音字的多个读音首字母是否全取，如果多音字拼音首字母相同，只保留一个</param>
        /// <returns>firstLetterOnly为true时，只取拼音首字母格式为[L]，后面追加空格；multiFirstLetter为true时，多音字的多个拼音首字母格式为[L, H]，后面追加空格</returns>
        public static string GetPinyin(string text, PinyinOutputFormat format, bool caseSpread, bool firstLetterOnly, bool multiFirstLetter)
        {
            StringBuilder pinyin = new StringBuilder();
            List<string> firstLetterBuf = new List<string>();
            if (!string.IsNullOrEmpty(text))
            {
                foreach (char item in text)
                {
                    if (!PinyinUtil.IsHanzi(item))
                    {
                        pinyin.Append(item);
                        continue;
                    }

                    if (firstLetterOnly)
                    {
                        if (multiFirstLetter)
                        {
                            firstLetterBuf.Clear();
                            foreach (string py in GetPinyin(item))
                            {
                                // 处理多音字拼音首字母相同的情况
                                if (!firstLetterBuf.Contains(py[0].ToString()))
                                {
                                    firstLetterBuf.Add(py[0].ToString());
                                }
                            }

                            pinyin.AppendFormat("[{0}] ", string.Join(",", firstLetterBuf.ToArray()));
                        }
                        else
                        {
                            pinyin.AppendFormat("[{0}] ", GetUniqueOrFirstPinyin(item)[0]);
                        }
                    }
                    else
                    {
                        pinyin.Append(GetUniqueOrFirstPinyinWithFormat(item, format) + " ");
                    }

                }
                #region // 扩展大小写格式
                if (!firstLetterOnly && caseSpread)
                {
                    string[] temp = null;
                    switch (format.GetCaseFormat)
                    {
                        case CaseFormat.CAPITALIZE_FIRST_LETTER:
                            temp = pinyin.ToString().Split(' ');
                            pinyin.Length = 0;

                            foreach (string item in temp)
                            {
                                pinyin.Append(item.Substring(0, 1).ToUpper());
                                if (item.Length > 1)
                                {
                                    pinyin.Append(item.Substring(1));
                                }
                                pinyin.Append(" ");
                            }
                            break;
                        case CaseFormat.LOWERCASE:
                            temp = new string[] { pinyin.ToString() };
                            pinyin.Length = 0;
                            pinyin.Append(temp[0].ToLower());
                            break;
                        case CaseFormat.UPPERCASE:
                            temp = new string[] { pinyin.ToString() };
                            pinyin.Length = 0;
                            pinyin.Append(temp[0].ToUpper());
                            break;
                    }
                }
                #endregion
            }

            return pinyin.ToString().Trim();
        }

        /// <summary>
        /// 获取一个字符串内所有汉字的拼音（多音字取第一个读音，带格式），format中指定的大小写模式不会扩展到非拼音字符
        /// </summary>
        /// <param name="text"></param>
        /// <param name="format">拼音格式</param>
        /// <returns></returns>
        public static string GetPinyin(string text, PinyinOutputFormat format)
        {
            return GetPinyin(text, format, false, false, false);
        }
        /// <summary>
        /// 根据拼音查询匹配的汉字
        /// </summary>
        /// <param name="pinyin"></param>
        /// <param name="matchAll">是否全部匹配，为true时，匹配整个拼音，否则匹配开头字符</param>
        /// <returns></returns>
        public static string[] GetHanzi(string pinyin, bool matchAll)
        {
            return PinyinDB.Instance.GetHanzi(pinyin.ToLower(), matchAll);
        }
    }
}
