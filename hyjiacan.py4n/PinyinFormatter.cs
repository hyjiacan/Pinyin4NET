using hyjiacan.py4n.exception;
using hyjiacan.py4n.format;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace hyjiacan.py4n
{
    /// <summary>
    /// 拼音格式化
    /// </summary>
    public static class PinyinFormatter
    {
        /// <summary>
        /// 将拼音格式化成指定的格式
        /// </summary>
        /// <param name="py">待格式化的拼音</param>
        /// <param name="format">格式</param>
        /// <see cref="ToneFormat"/>
        /// <see cref="CaseFormat"/>
        /// <see cref="VCharFormat"/>
        /// <returns></returns>
        public static string Format(string py, PinyinOutputFormat format)
        {
            /// "v"或"u:"不能添加声调
            if ((ToneFormat.WITH_TONE_MARK == format.GetToneFormat) &&
                    (
                        (VCharFormat.WITH_V == format.GetVCharFormat)
                        || (VCharFormat.WITH_U_AND_COLON == format.GetVCharFormat)
                    )
                )
            {
                throw new PinyinException("\"v\"或\"u:\"不能添加声调");
            }
            string pinyin = py;
            if (ToneFormat.WITHOUT_TONE == format.GetToneFormat)
            {
                // 不带声调
                Regex reg = new Regex("[1-5]");
                pinyin = reg.Replace(pinyin, "");
            }
            else if (ToneFormat.WITH_TONE_MARK == format.GetToneFormat)
            {
                // 带声调标志
                pinyin = pinyin.Replace("u:", "v");
                pinyin = convertToneNumber2ToneMark(pinyin);
            }

            if (VCharFormat.WITH_V == format.GetVCharFormat)
            {
                // 输出v
                pinyin = pinyin.Replace("u:", "v");
            }
            else if (VCharFormat.WITH_U_UNICODE == format.GetVCharFormat)
            {
                // 输出ü
                pinyin = pinyin.Replace("u:", "ü");
            }

            if (CaseFormat.UPPERCASE == format.GetCaseFormat)
            {
                // 大写
                pinyin = pinyin.ToUpper();
            }
            else if (CaseFormat.LOWERCASE == format.GetCaseFormat)
            {
                // 小写
                pinyin = pinyin.ToLower();
            }
            else if (CaseFormat.CAPITALIZE_FIRST_LETTER == format.GetCaseFormat)
            {
                // 首字母大写

                // 不处理单拼音 a e o
                if (!new List<string>() { "a", "e", "o"}.Contains(pinyin.ToLower()))
                {
                    pinyin = pinyin.Substring(0, 1).ToUpper() + (pinyin.Length == 1 ? "" : pinyin.Substring(1));
                }
            }
            return pinyin;
        }


        /// <summary>
        /// 将拼音的声调数字转换成字符
        /// </summary>
        /// <param name="pinyin"></param>
        /// <returns></returns>
        private static string convertToneNumber2ToneMark(String pinyin)
        {
            String lowerCasePinyinStr = pinyin.ToLower();
            Regex reg = new Regex("[a-z]*[1-5]?");
            if (reg.IsMatch(lowerCasePinyinStr))
            {
                string match = reg.Match(lowerCasePinyinStr).Value;
                const char defautlCharValue = '$';
                const int defautlIndexValue = -1;

                char unmarkedVowel = defautlCharValue;
                int indexOfUnmarkedVowel = defautlIndexValue;

                const char charA = 'a';
                const char charE = 'e';
                const String ouStr = "ou";
                const String allUnmarkedVowelStr = "aeiouv";
                const String allMarkedVowelStr = "āáăàaēéĕèeīíĭìiōóŏòoūúŭùuǖǘǚǜü";
                reg = new Regex("[a-z]*[1-5]");
                if (reg.IsMatch(lowerCasePinyinStr))
                {

                    int tuneNumber = (int)Char.GetNumericValue(lowerCasePinyinStr[lowerCasePinyinStr.Length - 1]);

                    int indexOfA = lowerCasePinyinStr.IndexOf(charA);
                    int indexOfE = lowerCasePinyinStr.IndexOf(charE);
                    int ouIndex = lowerCasePinyinStr.IndexOf(ouStr);

                    if (-1 != indexOfA)
                    {
                        indexOfUnmarkedVowel = indexOfA;
                        unmarkedVowel = charA;
                    }
                    else if (-1 != indexOfE)
                    {
                        indexOfUnmarkedVowel = indexOfE;
                        unmarkedVowel = charE;
                    }
                    else if (-1 != ouIndex)
                    {
                        indexOfUnmarkedVowel = ouIndex;
                        unmarkedVowel = ouStr[0];
                    }
                    else
                    {
                        reg = new Regex("[" + allUnmarkedVowelStr + "]");

                        for (int i = lowerCasePinyinStr.Length - 1; i >= 0; i--)
                        {
                            if (reg.IsMatch(lowerCasePinyinStr[i].ToString()))
                            {
                                indexOfUnmarkedVowel = i;
                                unmarkedVowel = lowerCasePinyinStr[i];
                                break;
                            }
                        }
                    }

                    if ((defautlCharValue != unmarkedVowel)
                            && (defautlIndexValue != indexOfUnmarkedVowel))
                    {
                        int rowIndex = allUnmarkedVowelStr.IndexOf(unmarkedVowel);
                        int columnIndex = tuneNumber - 1;

                        int vowelLocation = rowIndex * 5 + columnIndex;

                        char markedVowel = allMarkedVowelStr[vowelLocation];

                        StringBuilder resultBuffer = new StringBuilder();
                        // 声母
                        resultBuffer.Append(lowerCasePinyinStr.Substring(0, indexOfUnmarkedVowel).Replace("v", "ü"));
                        // 有声调的部分
                        resultBuffer.Append(markedVowel);
                        // 剩下的部分
                        resultBuffer.Append(lowerCasePinyinStr.Substring(indexOfUnmarkedVowel + 1).Replace("v", "ü"));

                        string result = resultBuffer.ToString();
                        // 替换声调数字
                        result = new Regex("[0-9]").Replace(result, "");

                        return result;

                    }
                    else
                    // error happens in the procedure of locating vowel
                    {
                        return lowerCasePinyinStr;
                    }
                }
                else
                // input string has no any tune number
                {
                    // only replace v with ü (umlat) character
                    return lowerCasePinyinStr.Replace("v", "ü");
                }
            }
            else
            // bad format
            {
                return lowerCasePinyinStr;
            }
        }
    }
}