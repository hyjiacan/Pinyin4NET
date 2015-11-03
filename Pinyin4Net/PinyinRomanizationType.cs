using System;
using System.Collections.Generic;
using System.Text;

namespace hyjiacan.util.p4n
{
    /**
 * The class describes variable Chinese Pinyin Romanization System
 * 
 * @author Li Min (xmlerlimin@gmail.com)
 * 
 */
    class PinyinRomanizationType
    {
        /**
         * Hanyu Pinyin system
         */
        public static PinyinRomanizationType HANYU_PINYIN = new PinyinRomanizationType("Hanyu");

        /**
         * Wade-Giles Pinyin system
         */
        public static PinyinRomanizationType WADEGILES_PINYIN = new PinyinRomanizationType("Wade");

        /**
         * Mandarin Phonetic Symbols 2 (MPS2) Pinyin system
         */
        public static PinyinRomanizationType MPS2_PINYIN = new PinyinRomanizationType("MPSII");

        /**
         * Yale Pinyin system
         */
        public static PinyinRomanizationType YALE_PINYIN = new PinyinRomanizationType("Yale");

        /**
         * Tongyong Pinyin system
         */
        public static PinyinRomanizationType TONGYONG_PINYIN = new PinyinRomanizationType("Tongyong");

        /**
         * Gwoyeu Romatzyh system
         */
        public static PinyinRomanizationType GWOYEU_ROMATZYH = new PinyinRomanizationType("Gwoyeu");

        /**
         * Constructor
         */
        protected PinyinRomanizationType(String tagName)
        {
            setTagName(tagName);
        }

        /**
         * @return Returns the tagName.
         */
        public String getTagName()
        {
            return tagName;
        }

        /**
         * @param tagName
         *            The tagName to set.
         */
        protected void setTagName(String tagName)
        {
            this.tagName = tagName;
        }

        protected String tagName;
    }
}
