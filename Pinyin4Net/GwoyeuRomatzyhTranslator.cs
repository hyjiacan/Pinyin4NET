using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace hyjiacan.util.p4n
{

    /**
     * A class contains logic that translates from Hanyu Pinyin to Gwoyeu Romatzyh
     * 
     * @author Li Min (xmlerlimin@gmail.com)
     * 
     */
    class GwoyeuRomatzyhTranslator
    {
        /**
         * @param hanyuPinyinStr
         *            Given unformatted Hanyu Pinyin with tone number
         * @return Corresponding Gwoyeu Romatzyh; null if no mapping is found.
         */
        internal static String convertHanyuPinyinToGwoyeuRomatzyh(String hanyuPinyinStr)
        {
            String pinyinString = TextHelper.extractPinyinString(hanyuPinyinStr);
            String toneNumberStr = TextHelper.extractToneNumber(hanyuPinyinStr);

            // return value
            String gwoyeuStr = null;
            try
            {
                // find the node of source Pinyin system
                String xpathQuery1 = "//"
                        + PinyinRomanizationType.HANYU_PINYIN.getTagName()
                        + "[text()='" + pinyinString + "']";

                XmlDocument pinyinToGwoyeuMappingDoc = GwoyeuRomatzyhResource.getInstance().getPinyinToGwoyeuMappingDoc();

                XmlNode hanyuNode = pinyinToGwoyeuMappingDoc.SelectSingleNode(xpathQuery1);

                if (null != hanyuNode)
                {
                    // find the node of target Pinyin system
                    String xpathQuery2 = "../"
                            + PinyinRomanizationType.GWOYEU_ROMATZYH.getTagName()
                            + tones[int.Parse(toneNumberStr) - 1]
                            + "/text()";
                    String targetPinyinStrWithoutToneNumber = hanyuNode.SelectSingleNode(xpathQuery2).Value;

                    gwoyeuStr = targetPinyinStrWithoutToneNumber;
                }
            }
            catch (Exception e)
            {
                Util.Log(e);
            }

            return gwoyeuStr;
        }

        /**
         * The postfixs to distinguish different tone of Gwoyeu Romatzyh
         * 
         * <i>Should be removed if new xPath parser supporting tag name with number.</i>
         */
        static private String[] tones = new String[] { "_I", "_II", "_III", "_IV",
            "_V" };
    }
}
