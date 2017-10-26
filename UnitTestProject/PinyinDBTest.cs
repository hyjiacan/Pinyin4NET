using hyjiacan.py4n.format;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable MemberCanBeMadeStatic.Local

namespace hyjiacan.py4n.test
{
    [TestClass]
    public class PinyinDBTest
    {

        [TestInitialize]
        public void Init()
        {
           
        }

        /// <summary>
        /// 单音字
        /// </summary>
        [TestMethod]
        public void SinglePinyin()
        {
            HanziAssert('李', new[] { "li3" });
        }
        /// <summary>
        /// 多音字
        /// </summary>
        [TestMethod]
        public void MultiPinyin()
        {
            HanziAssert('传', new[] { "chuan2", "zhuan4" });
        }

        /// <summary>
        /// 多音字的首个拼音
        /// </summary>
        [TestMethod]
        public void FirstOfMultiPinyin()
        {
            var pinyin = Pinyin4Net.GetUniqueOrFirstPinyin('传');
            Assert.AreEqual("chuan2", pinyin);
        }

        /// <summary>
        /// 测试输出格式
        /// </summary>
        [TestMethod]
        public void FormatTest1()
        {
            var format = new PinyinOutputFormat(ToneFormat.WITHOUT_TONE, CaseFormat.LOWERCASE, VCharFormat.WITH_U_UNICODE);
            Assert.AreEqual(ToneFormat.WITHOUT_TONE, format.GetToneFormat);
            Assert.AreEqual(CaseFormat.LOWERCASE, format.GetCaseFormat);
            Assert.AreEqual(VCharFormat.WITH_U_UNICODE, format.GetVCharFormat);
        }
        /// <summary>
        /// 测试输出格式
        /// </summary>
        [TestMethod]
        public void FormatTest2()
        {
            var format = new PinyinOutputFormat("WITHOUT_TONE","LOWERCASE", "WITH_U_UNICODE");
            Assert.AreEqual(ToneFormat.WITHOUT_TONE, format.GetToneFormat);
            Assert.AreEqual(CaseFormat.LOWERCASE, format.GetCaseFormat);
            Assert.AreEqual(VCharFormat.WITH_U_UNICODE, format.GetVCharFormat);
        }
        /// <summary>
        /// 测试输出格式
        /// </summary>
        [TestMethod]
        public void FormatTest3()
        {
            var format = new PinyinOutputFormat(ToneFormat.WITH_TONE_MARK, CaseFormat.LOWERCASE, VCharFormat.WITH_U_UNICODE);
            PinyinFormatAssert('啊', "a", format);
            PinyinFormatAssert('俄', "é", format);
            PinyinFormatAssert('李', "lĭ", format);
            PinyinFormatAssert('雨', "yŭ", format);
            PinyinFormatAssert('绿', "lǜ", format);
            PinyinFormatAssert('木', "mù", format);
            PinyinFormatAssert('按', "àn", format);
            PinyinFormatAssert('门', "mén", format);
            PinyinFormatAssert('欧', "ōu", format);
        }

        /// <summary>
        /// 测试字符串
        /// </summary>
        [TestMethod]
        public void TestString1()
        {
            const string s = "Javascript 爱好者 传说";
            var format = new PinyinOutputFormat();
            const string expected = "Javascript ài hăo zhĕ  chuán shuō";
            var pinyin = Pinyin4Net.GetPinyin(s, format);
            Assert.AreEqual(expected, pinyin);
        }

        /// <summary>
        /// 测试字符串
        /// </summary>
        [TestMethod]
        public void TestString2()
        {
            const string s = "Javascript 爱好者 传说";
            var format = new PinyinOutputFormat(null, CaseFormat.CAPITALIZE_FIRST_LETTER.ToString(), VCharFormat.WITH_U_UNICODE.ToString());
            const string expected = "Javascript Ài Hăo Zhĕ  Chuán Shuō";
            var pinyin = Pinyin4Net.GetPinyin(s, format);
            Assert.AreEqual(expected, pinyin);
        }
        /// <summary>
        /// 测试字符串
        /// </summary>
        [TestMethod]
        public void TestString3()
        {
            const string s = "Javascript 爱好者 传说";
            var format = new PinyinOutputFormat(null, CaseFormat.UPPERCASE.ToString(), VCharFormat.WITH_U_UNICODE.ToString());
            const string expected = "Javascript ÀI HĂO ZHĔ  CHUÁN SHUŌ";
            var pinyin = Pinyin4Net.GetPinyin(s, format);
            Assert.AreEqual(expected, pinyin);
        }
        /// <summary>
        /// 测试字符串
        /// </summary>
        [TestMethod]
        public void TestString4()
        {
            const string s = "Javascript 爱好者 传说";
            var format = new PinyinOutputFormat(null, CaseFormat.UPPERCASE.ToString(), VCharFormat.WITH_U_UNICODE.ToString());
            const string expected = "JAVASCRIPT ÀI HĂO ZHĔ  CHUÁN SHUŌ";
            var pinyin = Pinyin4Net.GetPinyin(s, format, true, false, false);
            Assert.AreEqual(expected, pinyin);
        }
        /// <summary>
        /// 测试字符串
        /// </summary>
        [TestMethod]
        public void TestString5()
        {
            const string s = "JavaScript 爱好者 传说";
            var format = new PinyinOutputFormat(null, CaseFormat.LOWERCASE.ToString(), VCharFormat.WITH_U_UNICODE.ToString());
            const string expected = "JavaScript ài hăo zhĕ  chuán shuō";
            var pinyin = Pinyin4Net.GetPinyin(s, format);
            Assert.AreEqual(expected, pinyin);
        }
        /// <summary>
        /// 测试字符串
        /// </summary>
        [TestMethod]
        public void TestString6()
        {
            const string s = "JavaScript 爱好者 传说";
            var format = new PinyinOutputFormat(null, CaseFormat.LOWERCASE.ToString(), VCharFormat.WITH_U_UNICODE.ToString());
            const string expected = "javascript ài hăo zhĕ  chuán shuō";
            var pinyin = Pinyin4Net.GetPinyin(s, format, true, false, false);
            Assert.AreEqual(expected, pinyin);
        }

        /// <summary>
        /// 取首字母
        /// </summary>
        [TestMethod]
        public void TestFirstLetter1()
        {
            const string s = "JavaScript 爱好者 传说";
            const string expected = "JavaScript [a] [h] [z]  [c] [s]";
            var pinyin = Pinyin4Net.GetPinyin(s, null, false, true, false);
            Assert.AreEqual(expected, pinyin);
        }

        /// <summary>
        /// 取首字母多音字
        /// </summary>
        [TestMethod]
        public void TestFirstLetter2()
        {
            const string s = "JavaScript 爱好者 传说";
            const string expected = "JavaScript [a] [h] [z]  [c,z] [s,y]";
            var pinyin = Pinyin4Net.GetPinyin(s, null, false, true, true);
            Assert.AreEqual(expected, pinyin);
        }

        /// <summary>
        /// 判断汉字与读音数组
        /// </summary>
        /// <param name="hanzi"></param>
        /// <param name="expected"></param>
        private void HanziAssert(char hanzi, string[] expected)
        {
            var actual = Pinyin4Net.GetPinyin(hanzi);
            assertArrayAreEquals(expected, actual);
        }

        /// <summary>
        /// 判断拼音格式
        /// </summary>
        /// <param name="hanzi"></param>
        /// <param name="expected"></param>
        /// <param name="format"></param>
        private void PinyinFormatAssert(char hanzi, string expected, PinyinOutputFormat format)
        {
            var fmted = Pinyin4Net.GetUniqueOrFirstPinyinWithFormat(hanzi, format);
            
            Assert.AreEqual(expected, fmted);
        }

        /// <summary>
        /// 判断两个字符串数组是否相等
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        private void assertArrayAreEquals(string[] expected, string[] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length);
            for (var i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
    }
}
