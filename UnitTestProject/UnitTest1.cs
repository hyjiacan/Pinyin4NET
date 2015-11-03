using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using hyjiacan.util.p4n.format;
using hyjiacan.util.p4n;
using hyjiacan.util.p4n.format.exception;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void testToTongyongPinyinStringArray()
        {
            // any input of non-Chinese characters will return null
            {
                Assert.IsNull(PinyinHelper.toTongyongPinyinStringArray('A'));
                Assert.IsNull(PinyinHelper.toTongyongPinyinStringArray('z'));
                Assert.IsNull(PinyinHelper.toTongyongPinyinStringArray(','));
                Assert.IsNull(PinyinHelper.toTongyongPinyinStringArray('。'));
            }

            // Chinese characters
            // single pronounciation
            {
                String[] expectedPinyinArray = new String[] { "li3" };
                String[] pinyinArray = PinyinHelper.toTongyongPinyinStringArray('李');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }
            {
                String[] expectedPinyinArray = new String[] { "ciou2" };
                String[] pinyinArray = PinyinHelper.toTongyongPinyinStringArray('球');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }
            {
                String[] expectedPinyinArray = new String[] { "jhuang1" };
                String[] pinyinArray = PinyinHelper.toTongyongPinyinStringArray('桩');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }

            // multiple pronounciations
            {
                String[] expectedPinyinArray = new String[] { "chuan2", "jhuan4" };
                String[] pinyinArray = PinyinHelper.toTongyongPinyinStringArray('传');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }

            {
                String[] expectedPinyinArray = new String[] { "lyu4", "lu4" };
                String[] pinyinArray = PinyinHelper.toTongyongPinyinStringArray('绿');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }
        }
        [TestMethod]
        public void testToWadeGilesPinyinStringArray()
        {
            // any input of non-Chinese characters will return null
            {
                Assert.IsNull(PinyinHelper.toWadeGilesPinyinStringArray('A'));
                Assert.IsNull(PinyinHelper.toWadeGilesPinyinStringArray('z'));
                Assert.IsNull(PinyinHelper.toWadeGilesPinyinStringArray(','));
                Assert.IsNull(PinyinHelper.toWadeGilesPinyinStringArray('。'));
            }

            // Chinese characters
            // single pronounciation
            {
                String[] expectedPinyinArray = new String[] { "li3" };
                String[] pinyinArray = PinyinHelper.toWadeGilesPinyinStringArray('李');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }
            {
                String[] expectedPinyinArray = new String[] { "ch`iu2" };
                String[] pinyinArray = PinyinHelper.toWadeGilesPinyinStringArray('球');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }
            {
                String[] expectedPinyinArray = new String[] { "chuang1" };
                String[] pinyinArray = PinyinHelper.toWadeGilesPinyinStringArray('桩');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }

            // multiple pronounciations
            {
                String[] expectedPinyinArray = new String[] { "ch`uan2", "chuan4" };
                String[] pinyinArray = PinyinHelper.toWadeGilesPinyinStringArray('传');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }

            {
                String[] expectedPinyinArray = new String[] { "lu:4", "lu4" };
                String[] pinyinArray = PinyinHelper.toWadeGilesPinyinStringArray('绿');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }
        }
        [TestMethod]
        public void testToMPS2PinyinStringArray()
        {
            // any input of non-Chinese characters will return null
            {
                Assert.IsNull(PinyinHelper.toMPS2PinyinStringArray('A'));
                Assert.IsNull(PinyinHelper.toMPS2PinyinStringArray('z'));
                Assert.IsNull(PinyinHelper.toMPS2PinyinStringArray(','));
                Assert.IsNull(PinyinHelper.toMPS2PinyinStringArray('。'));
            }

            // Chinese characters
            // single pronounciation
            {
                String[] expectedPinyinArray = new String[] { "li3" };
                String[] pinyinArray = PinyinHelper.toMPS2PinyinStringArray('李');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }
            {
                String[] expectedPinyinArray = new String[] { "chiou2" };
                String[] pinyinArray = PinyinHelper.toMPS2PinyinStringArray('球');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }
            {
                String[] expectedPinyinArray = new String[] { "juang1" };
                String[] pinyinArray = PinyinHelper.toMPS2PinyinStringArray('桩');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }

            // multiple pronounciations
            {
                String[] expectedPinyinArray = new String[] { "chuan2", "juan4" };
                String[] pinyinArray = PinyinHelper.toMPS2PinyinStringArray('传');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }

            {
                String[] expectedPinyinArray = new String[] { "liu4", "lu4" };
                String[] pinyinArray = PinyinHelper.toMPS2PinyinStringArray('绿');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }
        }
        [TestMethod]
        public void testToYalePinyinStringArray()
        {
            // any input of non-Chinese characters will return null
            {
                Assert.IsNull(PinyinHelper.toYalePinyinStringArray('A'));
                Assert.IsNull(PinyinHelper.toYalePinyinStringArray('z'));
                Assert.IsNull(PinyinHelper.toYalePinyinStringArray(','));
                Assert.IsNull(PinyinHelper.toYalePinyinStringArray('。'));
            }

            // Chinese characters
            // single pronounciation
            {
                String[] expectedPinyinArray = new String[] { "li3" };
                String[] pinyinArray = PinyinHelper.toYalePinyinStringArray('李');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }
            {
                String[] expectedPinyinArray = new String[] { "chyou2" };
                String[] pinyinArray = PinyinHelper.toYalePinyinStringArray('球');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }
            {
                String[] expectedPinyinArray = new String[] { "jwang1" };
                String[] pinyinArray = PinyinHelper.toYalePinyinStringArray('桩');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }

            // multiple pronounciations
            {
                String[] expectedPinyinArray = new String[] { "chwan2", "jwan4" };
                String[] pinyinArray = PinyinHelper.toYalePinyinStringArray('传');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }

            {
                String[] expectedPinyinArray = new String[] { "lyu4", "lu4" };
                String[] pinyinArray = PinyinHelper.toYalePinyinStringArray('绿');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }
        }
        [TestMethod]
        public void testToGwoyeuRomatzyhStringArray()
        {
            // any input of non-Chinese characters will return null
            {
                Assert.IsNull(PinyinHelper.toGwoyeuRomatzyhStringArray('A'));
                Assert.IsNull(PinyinHelper.toGwoyeuRomatzyhStringArray('z'));
                Assert.IsNull(PinyinHelper.toGwoyeuRomatzyhStringArray(','));
                Assert.IsNull(PinyinHelper.toGwoyeuRomatzyhStringArray('。'));
            }

            // Chinese characters
            // single pronounciation
            {
                String[] expectedPinyinArray = new String[] { "lii" };
                String[] pinyinArray = PinyinHelper.toGwoyeuRomatzyhStringArray('李');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }
            {
                String[] expectedPinyinArray = new String[] { "chyou" };
                String[] pinyinArray = PinyinHelper.toGwoyeuRomatzyhStringArray('球');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }
            {
                String[] expectedPinyinArray = new String[] { "juang" };
                String[] pinyinArray = PinyinHelper.toGwoyeuRomatzyhStringArray('桩');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }

            {
                String[] expectedPinyinArray = new String[] { "fuh" };
                String[] pinyinArray = PinyinHelper.toGwoyeuRomatzyhStringArray('付');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }

            // multiple pronounciations
            {
                String[] expectedPinyinArray = new String[] { "chwan", "juann" };
                String[] pinyinArray = PinyinHelper.toGwoyeuRomatzyhStringArray('传');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }

            {
                String[] expectedPinyinArray = new String[] { ".me", ".mha", "iau" };
                String[] pinyinArray = PinyinHelper.toGwoyeuRomatzyhStringArray('么');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }

            {
                String[] expectedPinyinArray = new String[] { "liuh", "luh" };
                String[] pinyinArray = PinyinHelper.toGwoyeuRomatzyhStringArray('绿');

                Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                for (int i = 0; i < expectedPinyinArray.Length; i++)
                {
                    Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                }
            }
        }
        [TestMethod]
        public void testToHanyuPinyinStringArray()
        {

            // any input of non-Chinese characters will return null
            {
                HanyuPinyinOutputFormat defaultFormat = new HanyuPinyinOutputFormat();
                try
                {
                    Assert.IsNull(PinyinHelper.toHanyuPinyinStringArray('A', defaultFormat));
                    Assert.IsNull(PinyinHelper.toHanyuPinyinStringArray('z', defaultFormat));
                    Assert.IsNull(PinyinHelper.toHanyuPinyinStringArray(',', defaultFormat));
                    Assert.IsNull(PinyinHelper.toHanyuPinyinStringArray('。', defaultFormat));
                }
                catch (BadHanyuPinyinOutputFormatCombination e)
                {
                    Util.Log(e);
                }
            }

            // Chinese characters
            // single pronounciation
            {
                try
                {
                    HanyuPinyinOutputFormat defaultFormat = new HanyuPinyinOutputFormat();

                    String[] expectedPinyinArray = new String[] { "li3" };
                    String[] pinyinArray = PinyinHelper.toHanyuPinyinStringArray('李', defaultFormat);

                    Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                    for (int i = 0; i < expectedPinyinArray.Length; i++)
                    {
                        Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                    }
                }
                catch (BadHanyuPinyinOutputFormatCombination e)
                {
                    Util.Log(e);
                }
            }
            {
                try
                {
                    HanyuPinyinOutputFormat upperCaseFormat = new HanyuPinyinOutputFormat();
                    upperCaseFormat.setCaseType(HanyuPinyinCaseType.UPPERCASE);

                    String[] expectedPinyinArray = new String[] { "LI3" };
                    String[] pinyinArray = PinyinHelper.toHanyuPinyinStringArray('李', upperCaseFormat);

                    Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                    for (int i = 0; i < expectedPinyinArray.Length; i++)
                    {
                        Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                    }
                }
                catch (BadHanyuPinyinOutputFormatCombination e)
                {
                    Util.Log(e);
                }
            }
            {
                try
                {
                    HanyuPinyinOutputFormat defaultFormat = new HanyuPinyinOutputFormat();

                    String[] expectedPinyinArray = new String[] { "lu:3" };
                    String[] pinyinArray = PinyinHelper.toHanyuPinyinStringArray('吕', defaultFormat);

                    Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                    for (int i = 0; i < expectedPinyinArray.Length; i++)
                    {
                        Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                    }
                }
                catch (BadHanyuPinyinOutputFormatCombination e)
                {
                    Util.Log(e);
                }
            }
            {
                try
                {
                    HanyuPinyinOutputFormat vCharFormat = new HanyuPinyinOutputFormat();
                    vCharFormat.setVCharType(HanyuPinyinVCharType.WITH_V);

                    String[] expectedPinyinArray = new String[] { "lv3" };
                    String[] pinyinArray = PinyinHelper.toHanyuPinyinStringArray('吕', vCharFormat);

                    Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                    for (int i = 0; i < expectedPinyinArray.Length; i++)
                    {
                        Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                    }
                }
                catch (BadHanyuPinyinOutputFormatCombination e)
                {
                    Util.Log(e);
                }
            }

            // multiple pronounciations
            {
                try
                {
                    HanyuPinyinOutputFormat defaultFormat = new HanyuPinyinOutputFormat();

                    String[] expectedPinyinArray = new String[] { "jian1", "jian4" };
                    String[] pinyinArray = PinyinHelper.toHanyuPinyinStringArray('间', defaultFormat);

                    Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                    for (int i = 0; i < expectedPinyinArray.Length; i++)
                    {
                        Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                    }
                }
                catch (BadHanyuPinyinOutputFormatCombination e)
                {
                    Util.Log(e);
                }
            }

            {
                try
                {
                    HanyuPinyinOutputFormat defaultFormat = new HanyuPinyinOutputFormat();

                    String[] expectedPinyinArray = new String[] { "hao3", "hao4" };
                    String[] pinyinArray = PinyinHelper.toHanyuPinyinStringArray('好', defaultFormat);

                    Assert.AreEqual(expectedPinyinArray.Length, pinyinArray.Length);

                    for (int i = 0; i < expectedPinyinArray.Length; i++)
                    {
                        Assert.AreEqual(expectedPinyinArray[i], pinyinArray[i]);
                    }
                }
                catch (BadHanyuPinyinOutputFormatCombination e)
                {
                    Util.Log(e);
                }
            }
        }

        /**
         * test for combination of output formats
         */
        [TestMethod]
        public void testOutputCombination()
        {
            try
            {
                HanyuPinyinOutputFormat outputFormat = new HanyuPinyinOutputFormat();

                // fix case type to lowercase firstly, change VChar and Tone
                // combination
                outputFormat.setCaseType(HanyuPinyinCaseType.LOWERCASE);

                // WITH_U_AND_COLON and WITH_TONE_NUMBER
                outputFormat.setVCharType(HanyuPinyinVCharType.WITH_U_AND_COLON);
                outputFormat.setToneType(HanyuPinyinToneType.WITH_TONE_NUMBER);

                Assert.AreEqual("lu:3", PinyinHelper.toHanyuPinyinStringArray('吕', outputFormat)[0]);

                // WITH_V and WITH_TONE_NUMBER
                outputFormat.setVCharType(HanyuPinyinVCharType.WITH_V);
                outputFormat.setToneType(HanyuPinyinToneType.WITH_TONE_NUMBER);

                Assert.AreEqual("lv3", PinyinHelper.toHanyuPinyinStringArray('吕', outputFormat)[0]);

                // WITH_U_UNICODE and WITH_TONE_NUMBER
                outputFormat.setVCharType(HanyuPinyinVCharType.WITH_U_UNICODE);
                outputFormat.setToneType(HanyuPinyinToneType.WITH_TONE_NUMBER);

                Assert.AreEqual("lü3", PinyinHelper.toHanyuPinyinStringArray('吕', outputFormat)[0]);

                // // WITH_U_AND_COLON and WITHOUT_TONE
                outputFormat.setVCharType(HanyuPinyinVCharType.WITH_U_AND_COLON);
                outputFormat.setToneType(HanyuPinyinToneType.WITHOUT_TONE);

                Assert.AreEqual("lu:", PinyinHelper.toHanyuPinyinStringArray('吕', outputFormat)[0]);

                // WITH_V and WITHOUT_TONE
                outputFormat.setVCharType(HanyuPinyinVCharType.WITH_V);
                outputFormat.setToneType(HanyuPinyinToneType.WITHOUT_TONE);

                Assert.AreEqual("lv", PinyinHelper.toHanyuPinyinStringArray('吕', outputFormat)[0]);

                // WITH_U_UNICODE and WITHOUT_TONE
                outputFormat.setVCharType(HanyuPinyinVCharType.WITH_U_UNICODE);
                outputFormat.setToneType(HanyuPinyinToneType.WITHOUT_TONE);

                Assert.AreEqual("lü", PinyinHelper.toHanyuPinyinStringArray('吕', outputFormat)[0]);

                // WITH_U_AND_COLON and WITH_TONE_MARK is forbidden

                // WITH_V and WITH_TONE_MARK is forbidden

                // WITH_U_UNICODE and WITH_TONE_MARK
                outputFormat.setVCharType(HanyuPinyinVCharType.WITH_U_UNICODE);
                outputFormat.setToneType(HanyuPinyinToneType.WITH_TONE_MARK);

                Assert.AreEqual("lǚ", PinyinHelper.toHanyuPinyinStringArray('吕', outputFormat)[0]);

                // fix case type to UPPERCASE, change VChar and Tone
                // combination
                outputFormat.setCaseType(HanyuPinyinCaseType.UPPERCASE);

                // WITH_U_AND_COLON and WITH_TONE_NUMBER
                outputFormat.setVCharType(HanyuPinyinVCharType.WITH_U_AND_COLON);
                outputFormat.setToneType(HanyuPinyinToneType.WITH_TONE_NUMBER);

                Assert.AreEqual("LU:3", PinyinHelper.toHanyuPinyinStringArray('吕', outputFormat)[0]);

                // WITH_V and WITH_TONE_NUMBER
                outputFormat.setVCharType(HanyuPinyinVCharType.WITH_V);
                outputFormat.setToneType(HanyuPinyinToneType.WITH_TONE_NUMBER);

                Assert.AreEqual("LV3", PinyinHelper.toHanyuPinyinStringArray('吕', outputFormat)[0]);

                // WITH_U_UNICODE and WITH_TONE_NUMBER
                outputFormat.setVCharType(HanyuPinyinVCharType.WITH_U_UNICODE);
                outputFormat.setToneType(HanyuPinyinToneType.WITH_TONE_NUMBER);

                Assert.AreEqual("LÜ3", PinyinHelper.toHanyuPinyinStringArray('吕', outputFormat)[0]);

                // // WITH_U_AND_COLON and WITHOUT_TONE
                outputFormat.setVCharType(HanyuPinyinVCharType.WITH_U_AND_COLON);
                outputFormat.setToneType(HanyuPinyinToneType.WITHOUT_TONE);

                Assert.AreEqual("LU:", PinyinHelper.toHanyuPinyinStringArray('吕', outputFormat)[0]);

                // WITH_V and WITHOUT_TONE
                outputFormat.setVCharType(HanyuPinyinVCharType.WITH_V);
                outputFormat.setToneType(HanyuPinyinToneType.WITHOUT_TONE);

                Assert.AreEqual("LV", PinyinHelper.toHanyuPinyinStringArray('吕', outputFormat)[0]);

                // WITH_U_UNICODE and WITHOUT_TONE
                outputFormat.setVCharType(HanyuPinyinVCharType.WITH_U_UNICODE);
                outputFormat.setToneType(HanyuPinyinToneType.WITHOUT_TONE);

                Assert.AreEqual("LÜ", PinyinHelper.toHanyuPinyinStringArray('吕', outputFormat)[0]);

                // WITH_U_AND_COLON and WITH_TONE_MARK is forbidden

                // WITH_V and WITH_TONE_MARK is forbidden

                // WITH_U_UNICODE and WITH_TONE_MARK
                outputFormat.setVCharType(HanyuPinyinVCharType.WITH_U_UNICODE);
                outputFormat.setToneType(HanyuPinyinToneType.WITH_TONE_MARK);

                Assert.AreEqual("LǙ", PinyinHelper.toHanyuPinyinStringArray('吕', outputFormat)[0]);
            }
            catch (BadHanyuPinyinOutputFormatCombination e)
            {
                Util.Log(e);
            }
        }
    }
}
