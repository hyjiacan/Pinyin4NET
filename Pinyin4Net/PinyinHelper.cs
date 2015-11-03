using System;
using System.Collections.Generic;
using System.Text;
using hyjiacan.util.p4n.format;

namespace hyjiacan.util.p4n
{
    public class PinyinHelper
    {
        /**
    * Get all unformmatted Hanyu Pinyin presentations of a single Chinese
    * character (both Simplified and Tranditional)
    * 
    * <p>
    * For example, <br/> If the input is '间', the return will be an array with
    * two Hanyu Pinyin strings: <br/> "jian1" <br/> "jian4" <br/> <br/> If the
    * input is '李', the return will be an array with single Hanyu Pinyin
    * string: <br/> "li3"
    * 
    * <p>
    * <b>Special Note</b>: If the return is "none0", that means the input
    * Chinese character exists in Unicode CJK talbe, however, it has no
    * pronounciation in Chinese
    * 
    * @param ch
    *            the given Chinese character
    * 
    * @return a String array contains all unformmatted Hanyu Pinyin
    *         presentations with tone numbers; null for non-Chinese character
    * 
    */
        static public String[] toHanyuPinyinStringArray(char ch)
        {
            return getUnformattedHanyuPinyinStringArray(ch);
        }

        /**
         * Get all Hanyu Pinyin presentations of a single Chinese character (both
         * Simplified and Tranditional)
         * 
         * <p>
         * For example, <br/> If the input is '间', the return will be an array with
         * two Hanyu Pinyin strings: <br/> "jian1" <br/> "jian4" <br/> <br/> If the
         * input is '李', the return will be an array with single Hanyu Pinyin
         * string: <br/> "li3"
         * 
         * <p>
         * <b>Special Note</b>: If the return is "none0", that means the input
         * Chinese character is in Unicode CJK talbe, however, it has no
         * pronounciation in Chinese
         * 
         * @param ch
         *            the given Chinese character
         * @param outputFormat
         *            describes the desired format of returned Hanyu Pinyin String
         * 
         * @return a String array contains all Hanyu Pinyin presentations with tone
         *         numbers; return null for non-Chinese character
         * 
         * @throws BadHanyuPinyinOutputFormatCombination
         *             if certain combination of output formats happens
         * 
         * @see HanyuPinyinOutputFormat
         * @see BadHanyuPinyinOutputFormatCombination
         * 
         */
        static public String[] toHanyuPinyinStringArray(char ch,
                HanyuPinyinOutputFormat outputFormat)
        {
            return getFormattedHanyuPinyinStringArray(ch, outputFormat);
        }

        /**
         * Return the formatted Hanyu Pinyin representations of the given Chinese
         * character (both in Simplified and Tranditional) in array format.
         * 
         * @param ch
         *            the given Chinese character
         * @param outputFormat
         *            Describes the desired format of returned Hanyu Pinyin string
         * @return The formatted Hanyu Pinyin representations of the given codepoint
         *         in array format; null if no record is found in the hashtable.
         */
        static private String[] getFormattedHanyuPinyinStringArray(char ch,
                HanyuPinyinOutputFormat outputFormat)
        {
            String[] pinyinStrArray = getUnformattedHanyuPinyinStringArray(ch);

            if (null != pinyinStrArray)
            {

                for (int i = 0; i < pinyinStrArray.Length; i++)
                {
                    pinyinStrArray[i] = PinyinFormatter.formatHanyuPinyin(pinyinStrArray[i], outputFormat);
                }

                return pinyinStrArray;

            }
            else
                return null;
        }

        /**
         * Delegate function
         * 
         * @param ch
         *            the given Chinese character
         * @return unformatted Hanyu Pinyin strings; null if the record is not found
         */
        private static String[] getUnformattedHanyuPinyinStringArray(char ch)
        {
            return ChineseToPinyinResource.getInstance().getHanyuPinyinStringArray(ch);
        }

        /**
         * Get all unformmatted Tongyong Pinyin presentations of a single Chinese
         * character (both Simplified and Tranditional)
         * 
         * @param ch
         *            the given Chinese character
         * 
         * @return a String array contains all unformmatted Tongyong Pinyin
         *         presentations with tone numbers; null for non-Chinese character
         * 
         * @see #toHanyuPinyinStringArray(char)
         * 
         */
        static public String[] toTongyongPinyinStringArray(char ch)
        {
            return convertToTargetPinyinStringArray(ch, PinyinRomanizationType.TONGYONG_PINYIN);
        }

        /**
         * Get all unformmatted Wade-Giles presentations of a single Chinese
         * character (both Simplified and Tranditional)
         * 
         * @param ch
         *            the given Chinese character
         * 
         * @return a String array contains all unformmatted Wade-Giles presentations
         *         with tone numbers; null for non-Chinese character
         * 
         * @see #toHanyuPinyinStringArray(char)
         * 
         */
        static public String[] toWadeGilesPinyinStringArray(char ch)
        {
            return convertToTargetPinyinStringArray(ch, PinyinRomanizationType.WADEGILES_PINYIN);
        }

        /**
         * Get all unformmatted MPS2 (Mandarin Phonetic Symbols 2) presentations of
         * a single Chinese character (both Simplified and Tranditional)
         * 
         * @param ch
         *            the given Chinese character
         * 
         * @return a String array contains all unformmatted MPS2 (Mandarin Phonetic
         *         Symbols 2) presentations with tone numbers; null for non-Chinese
         *         character
         * 
         * @see #toHanyuPinyinStringArray(char)
         * 
         */
        static public String[] toMPS2PinyinStringArray(char ch)
        {
            return convertToTargetPinyinStringArray(ch, PinyinRomanizationType.MPS2_PINYIN);
        }

        /**
         * Get all unformmatted Yale Pinyin presentations of a single Chinese
         * character (both Simplified and Tranditional)
         * 
         * @param ch
         *            the given Chinese character
         * 
         * @return a String array contains all unformmatted Yale Pinyin
         *         presentations with tone numbers; null for non-Chinese character
         * 
         * @see #toHanyuPinyinStringArray(char)
         * 
         */
        static public String[] toYalePinyinStringArray(char ch)
        {
            return convertToTargetPinyinStringArray(ch, PinyinRomanizationType.YALE_PINYIN);
        }

        /**
         * @param ch
         *            the given Chinese character
         * @param targetPinyinSystem
         *            indicates target Chinese Romanization system should be
         *            converted to
         * @return string representations of target Chinese Romanization system
         *         corresponding to the given Chinese character in array format;
         *         null if error happens
         * 
         * @see PinyinRomanizationType
         */
        private static String[] convertToTargetPinyinStringArray(char ch,
                PinyinRomanizationType targetPinyinSystem)
        {
            String[] hanyuPinyinStringArray = getUnformattedHanyuPinyinStringArray(ch);

            if (null != hanyuPinyinStringArray)
            {
                String[] targetPinyinStringArray = new String[hanyuPinyinStringArray.Length];

                for (int i = 0; i < hanyuPinyinStringArray.Length; i++)
                {
                    targetPinyinStringArray[i] = PinyinRomanizationTranslator.convertRomanizationSystem(hanyuPinyinStringArray[i], PinyinRomanizationType.HANYU_PINYIN, targetPinyinSystem);
                }

                return targetPinyinStringArray;

            }
            else
                return null;
        }

        /**
         * Get all unformmatted Gwoyeu Romatzyh presentations of a single Chinese
         * character (both Simplified and Tranditional)
         * 
         * @param ch
         *            the given Chinese character
         * 
         * @return a String array contains all unformmatted Gwoyeu Romatzyh
         *         presentations with tone numbers; null for non-Chinese character
         * 
         * @see #toHanyuPinyinStringArray(char)
         * 
         */
        static public String[] toGwoyeuRomatzyhStringArray(char ch)
        {
            return convertToGwoyeuRomatzyhStringArray(ch);
        }

        /**
         * @param ch
         *            the given Chinese character
         * 
         * @return Gwoyeu Romatzyh string representations corresponding to the given
         *         Chinese character in array format; null if error happens
         * 
         * @see PinyinRomanizationType
         */
        private static String[] convertToGwoyeuRomatzyhStringArray(char ch)
        {
            String[] hanyuPinyinStringArray = getUnformattedHanyuPinyinStringArray(ch);

            if (null != hanyuPinyinStringArray)
            {
                String[] targetPinyinStringArray = new String[hanyuPinyinStringArray.Length];

                for (int i = 0; i < hanyuPinyinStringArray.Length; i++)
                {
                    targetPinyinStringArray[i] = GwoyeuRomatzyhTranslator.convertHanyuPinyinToGwoyeuRomatzyh(hanyuPinyinStringArray[i]);
                }

                return targetPinyinStringArray;

            }
            else
                return null;
        }

        /**
         * Get a string which all Chinese characters are replaced by corresponding
         * main (first) Hanyu Pinyin representation.
         * 
         * <p>
         * <b>Special Note</b>: If the return contains "none0", that means that
         * Chinese character is in Unicode CJK talbe, however, it has not
         * pronounciation in Chinese. <b> This interface will be removed in next
         * release. </b>
         * 
         * @param str
         *            A given string contains Chinese characters
         * @param outputFormat
         *            Describes the desired format of returned Hanyu Pinyin string
         * @param seperater
         *            The string is appended after a Chinese character (excluding
         *            the last Chinese character at the end of sentence). <b>Note!
         *            Seperater will not appear after a non-Chinese character</b>
         * @return a String identical to the original one but all recognizable
         *         Chinese characters are converted into main (first) Hanyu Pinyin
         *         representation
         * 
         * @deprecated DO NOT use it again because the first retrived pinyin string
         *             may be a wrong pronouciation in a certain sentence context.
         *             <b> This interface will be removed in next release. </b>
         */
        static public String toHanyuPinyinString(String str,
                HanyuPinyinOutputFormat outputFormat, String seperater)
        {

            StringBuilder resultPinyinStrBuf = new StringBuilder();

            for (int i = 0; i < str.Length; i++)
            {
                String mainPinyinStrOfChar = getFirstHanyuPinyinString(str[i], outputFormat);

                if (null != mainPinyinStrOfChar)
                {
                    resultPinyinStrBuf.Append(mainPinyinStrOfChar);
                    if (i != str.Length - 1)
                    { // avoid appending at the end
                        resultPinyinStrBuf.Append(seperater);
                    }
                }
                else
                {
                    resultPinyinStrBuf.Append(str[i]);
                }
            }

            return resultPinyinStrBuf.ToString();
        }

        /**
         * Get the first Hanyu Pinyin of a Chinese character <b> This function will
         * be removed in next release. </b>
         * 
         * @param ch
         *            The given Unicode character
         * @param outputFormat
         *            Describes the desired format of returned Hanyu Pinyin string
         * @return Return the first Hanyu Pinyin of given Chinese character; return
         *         null if the input is not a Chinese character
         * 
         * @deprecated DO NOT use it again because the first retrived pinyin string
         *             may be a wrong pronouciation in a certain sentence context.
         *             <b> This function will be removed in next release. </b>
         */
        static private String getFirstHanyuPinyinString(char ch,
                HanyuPinyinOutputFormat outputFormat)
        {
            String[] pinyinStrArray = getFormattedHanyuPinyinStringArray(ch, outputFormat);

            if ((null != pinyinStrArray) && (pinyinStrArray.Length > 0))
            {
                return pinyinStrArray[0];
            }
            else
            {
                return null;
            }
        }

        // ! Hidden constructor
        private PinyinHelper()
        {
        }
    }
}
