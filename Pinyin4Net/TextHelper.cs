using System;
using System.Collections.Generic;
using System.Text;

namespace hyjiacan.util.p4n
{

    /**
     * Contains the utility functions supporting text processing
     * 
     * @author Li Min (xmlerlimin@gmail.com)
     * 
     */
    class TextHelper
    {

        /**
         * @param hanyuPinyinWithToneNumber
         * @return Hanyu Pinyin string without tone number
         */
        public static String extractToneNumber(String hanyuPinyinWithToneNumber)
        {
            return hanyuPinyinWithToneNumber.Substring(hanyuPinyinWithToneNumber.Length - 1);
        }

        /**
         * @param hanyuPinyinWithToneNumber
         * @return only tone number
         */
        public static String extractPinyinString(String hanyuPinyinWithToneNumber)
        {
            return hanyuPinyinWithToneNumber.Substring(0, hanyuPinyinWithToneNumber.Length - 1);
        }

    }
}
