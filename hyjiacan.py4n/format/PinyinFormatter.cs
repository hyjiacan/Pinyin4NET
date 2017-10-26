using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using hyjiacan.py4n.exception;

namespace hyjiacan.py4n.format
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
            // "v"或"u:"不能添加声调
            if (ToneFormat.WITH_TONE_MARK == format.GetToneFormat &&
                    (
                        VCharFormat.WITH_V == format.GetVCharFormat
                        || VCharFormat.WITH_U_AND_COLON == format.GetVCharFormat
                    )
                )
            {
                throw new PinyinException("\"v\"或\"u:\"不能添加声调");
            }
            var pinyin = py;
            switch (format.GetToneFormat)
            {
                case ToneFormat.WITHOUT_TONE:
                    // 不带声调
                    var reg = new Regex("[1-5]");
                    pinyin = reg.Replace(pinyin, "");
                    break;
                case ToneFormat.WITH_TONE_MARK:
                    // 带声调标志
                    pinyin = pinyin.Replace("u:", "v");
                    pinyin = convertToneNumber2ToneMark(pinyin);
                    break;
            }

            switch (format.GetVCharFormat)
            {
                case VCharFormat.WITH_V:
                    // 输出v
                    pinyin = pinyin.Replace("u:", "v");
                    break;
                case VCharFormat.WITH_U_UNICODE:
                    // 输出ü
                    pinyin = pinyin.Replace("u:", "ü");
                    break;
            }

            switch (format.GetCaseFormat)
            {
                case CaseFormat.UPPERCASE:
                    // 大写
                    pinyin = pinyin.ToUpper();
                    break;
                case CaseFormat.LOWERCASE:
                    // 小写
                    pinyin = pinyin.ToLower();
                    break;
                case CaseFormat.CAPITALIZE_FIRST_LETTER:
                    // 首字母大写

                    // 不处理单拼音 a e o
                    if (!new List<string> { "a", "e", "o"}.Contains(pinyin.ToLower()))
                    {
                        pinyin = pinyin.Substring(0, 1).ToUpper() + (pinyin.Length == 1 ? "" : pinyin.Substring(1));
                    }
                    break;
            }
            return pinyin;
        }


        /// <summary>
        /// 将拼音的声调数字转换成字符
        /// </summary>
        /// <param name="pinyin"></param>
        /// <returns></returns>
        private static string convertToneNumber2ToneMark(string pinyin)
        {
            var lowerCasePinyinStr = pinyin.ToLower();
            var reg = new Regex("[a-z]*[1-5]?");
            if (!reg.IsMatch(lowerCasePinyinStr)) return lowerCasePinyinStr;

            const char defautlCharValue = '$';
            const int defautlIndexValue = -1;

            var unmarkedVowel = defautlCharValue;
            var indexOfUnmarkedVowel = defautlIndexValue;

            const char charA = 'a';
            const char charE = 'e';
            const string ouStr = "ou";
            const string allUnmarkedVowelStr = "aeiouv";
            const string allMarkedVowelStr = "āáăàaēéĕèeīíĭìiōóŏòoūúŭùuǖǘǚǜü";
            reg = new Regex("[a-z]*[1-5]");
            if (!reg.IsMatch(lowerCasePinyinStr)) return lowerCasePinyinStr.Replace("v", "ü");

            var tuneNumber = (int)char.GetNumericValue(lowerCasePinyinStr[lowerCasePinyinStr.Length - 1]);

            var indexOfA = lowerCasePinyinStr.IndexOf(charA);
            var indexOfE = lowerCasePinyinStr.IndexOf(charE);
            var ouIndex = lowerCasePinyinStr.IndexOf(ouStr, StringComparison.Ordinal);

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
                    
                for (var i = lowerCasePinyinStr.Length - 1; i >= 0; i--)
                {
                    if (!reg.IsMatch(lowerCasePinyinStr[i].ToString())) continue;

                    indexOfUnmarkedVowel = i;
                    unmarkedVowel = lowerCasePinyinStr[i];
                    break;
                }
            }

            if (defautlCharValue == unmarkedVowel || defautlIndexValue == indexOfUnmarkedVowel)
                return lowerCasePinyinStr;

            var rowIndex = allUnmarkedVowelStr.IndexOf(unmarkedVowel);
            var columnIndex = tuneNumber - 1;

            var vowelLocation = rowIndex * 5 + columnIndex;

            var markedVowel = allMarkedVowelStr[vowelLocation];

            var resultBuffer = new StringBuilder();
            // 声母
            resultBuffer.Append(lowerCasePinyinStr.Substring(0, indexOfUnmarkedVowel).Replace("v", "ü"));
            // 有声调的部分
            resultBuffer.Append(markedVowel);
            // 剩下的部分
            resultBuffer.Append(lowerCasePinyinStr.Substring(indexOfUnmarkedVowel + 1).Replace("v", "ü"));

            var result = resultBuffer.ToString();
            // 替换声调数字
            result = new Regex("[0-9]").Replace(result, "");

            return result;
            // only replace v with ü (umlat) character
        }
    }
}