using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace hyjiacan.util.p4n
{

    /**
     * Contains the logic translating among different Chinese Romanization systems
     * 
     * @author Li Min (xmlerlimin@gmail.com)
     * 
     */
    class PinyinRomanizationTranslator
    {
        /**
         * convert the given unformatted Pinyin string from source Romanization
         * system to target Romanization system
         * 
         * @param sourcePinyinStr
         *            the given unformatted Pinyin string
         * @param sourcePinyinSystem
         *            the Romanization system which is currently used by the given
         *            unformatted Pinyin string
         * @param targetPinyinSystem
         *            the Romanization system that should be converted to
         * @return unformatted Pinyin string in target Romanization system; null if
         *         error happens
         */
        public static String convertRomanizationSystem(String sourcePinyinStr,
                PinyinRomanizationType sourcePinyinSystem,
                PinyinRomanizationType targetPinyinSystem)
        {
            String pinyinString = TextHelper.extractPinyinString(sourcePinyinStr);
            String toneNumberStr = TextHelper.extractToneNumber(sourcePinyinStr);

            // return value
            String targetPinyinStr = null;
            try
            {
                // find the node of source Pinyin system
                String xpathQuery1 = "//" + sourcePinyinSystem.getTagName()
                        + "[text()='" + pinyinString + "']";

                XmlDocument pinyinMappingDoc = PinyinRomanizationResource.getInstance().getPinyinMappingDoc();

                XmlNode hanyuNode = pinyinMappingDoc.SelectSingleNode(xpathQuery1);

                if (null != hanyuNode)
                {
                    // find the node of target Pinyin system
                    String xpathQuery2 = "../" + targetPinyinSystem.getTagName()
                            + "/text()";
                    String targetPinyinStrWithoutToneNumber = hanyuNode.SelectSingleNode(xpathQuery2).Value;

                    targetPinyinStr = targetPinyinStrWithoutToneNumber
                            + toneNumberStr;
                }
            }
            catch (Exception e)
            {
                Util.Log(e);
            }

            return targetPinyinStr;
        }
    }

}
