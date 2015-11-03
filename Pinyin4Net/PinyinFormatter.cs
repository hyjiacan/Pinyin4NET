using System;
using System.Collections.Generic;
using System.Text;
using hyjiacan.util.p4n.format;
using hyjiacan.util.p4n.format.exception;
using System.Text.RegularExpressions;

namespace hyjiacan.util.p4n
{
    public class PinyinFormatter
    {
        internal static String formatHanyuPinyin(String pinyinStr,
            HanyuPinyinOutputFormat outputFormat)
        {
            if ((HanyuPinyinToneType.WITH_TONE_MARK == outputFormat.getToneType())
                    && ((HanyuPinyinVCharType.WITH_V == outputFormat.getVCharType()) || (HanyuPinyinVCharType.WITH_U_AND_COLON == outputFormat.getVCharType())))
            {
                throw new BadHanyuPinyinOutputFormatCombination("tone marks cannot be added to v or u:");
            }

            if (HanyuPinyinToneType.WITHOUT_TONE == outputFormat.getToneType())
            {
                Regex reg = new Regex("[1-5]");
                pinyinStr = reg.Replace(pinyinStr, "");
            }
            else if (HanyuPinyinToneType.WITH_TONE_MARK == outputFormat.getToneType())
            {
                pinyinStr = pinyinStr.Replace("u:", "v");
                pinyinStr = convertToneNumber2ToneMark(pinyinStr);
            }

            if (HanyuPinyinVCharType.WITH_V == outputFormat.getVCharType())
            {
                pinyinStr = pinyinStr.Replace("u:", "v");
            }
            else if (HanyuPinyinVCharType.WITH_U_UNICODE == outputFormat.getVCharType())
            {
                pinyinStr = pinyinStr.Replace("u:", "ü");
            }

            if (HanyuPinyinCaseType.UPPERCASE == outputFormat.getCaseType())
            {
                pinyinStr = pinyinStr.ToUpper();
            }
            return pinyinStr;
        }
        private static string convertToneNumber2ToneMark(String pinyinStr)
        {
            String lowerCasePinyinStr = pinyinStr.ToLower();
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
