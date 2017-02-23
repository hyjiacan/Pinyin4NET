using hyjiacan.py4n.format;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace hyjiacan.py4n.test
{
    [TestClass]
    public class NameDBTest
    {

        [TestInitialize]
        public void Init()
        {

        }

        /// <summary>
        /// 单音字
        /// </summary>
        [TestMethod]
        public void SinglePinyinName()
        {
            HanziAssert("李", "li3");
        }
        /// <summary>
        /// 多音字
        /// </summary>
        [TestMethod]
        public void MultiPinyinName()
        {
            HanziAssert("单", "shan4");
        }

        /// <summary>
        /// 复姓
        /// </summary>
        [TestMethod]
        public void ComplexName()
        {
            HanziAssert("单于", "chan2 yu2");
        }

        /// <summary>
        /// 首个字母
        /// </summary>
        [TestMethod]
        public void FirstLetterOfName()
        {
            string pinyin = Pinyin4Name.GetFirstLetter("李");
            Assert.AreEqual<string>("l", pinyin);
        }

        /// <summary>
        /// 复姓的首个字母
        /// </summary>
        [TestMethod]
        public void FirstOfMultiPinyinName()
        {
            string pinyin = Pinyin4Name.GetFirstLetter("单于");
            Assert.AreEqual<string>("c y", pinyin);
        }


        /// <summary>
        /// 测试输出格式
        /// </summary>
        [TestMethod]
        public void FormatTest1()
        {
            PinyinOutputFormat format = new PinyinOutputFormat(ToneFormat.WITH_TONE_MARK, CaseFormat.LOWERCASE, VCharFormat.WITH_U_UNICODE);
            PinyinFormatAssert("李", "lĭ", format);
            PinyinFormatAssert("单于", "chán yú", format);
            PinyinFormatAssert("乐", "yuè", format);
            PinyinFormatAssert("厍", "shè", format);
            PinyinFormatAssert("欧", "ōu", format);
        }

        /// <summary>
        /// 测试输出格式2
        /// </summary>
        [TestMethod]
        public void FormatTest2()
        {
            PinyinOutputFormat format = new PinyinOutputFormat();
            PinyinFormatAssert("李", "lĭ", format);
            PinyinFormatAssert("单于", "chán yú", format);
            PinyinFormatAssert("乐", "yuè", format);
            PinyinFormatAssert("厍", "shè", format);
            PinyinFormatAssert("欧", "ōu", format);
        }

        /// <summary>
        /// 测试输出格式
        /// </summary>
        [TestMethod]
        public void FormatTest3()
        {
            PinyinOutputFormat format = new PinyinOutputFormat(null, CaseFormat.CAPITALIZE_FIRST_LETTER.ToString(), VCharFormat.WITH_U_UNICODE.ToString());
            PinyinFormatAssert("李", "Lĭ", format);
            PinyinFormatAssert("单于", "Chán Yú", format);
            PinyinFormatAssert("乐", "Yuè", format);
            PinyinFormatAssert("厍", "Shè", format);
            PinyinFormatAssert("欧", "Ōu", format);
        }
        /// <summary>
        /// 测试输出格式
        /// </summary>
        [TestMethod]
        public void FormatTest4()
        {
            PinyinOutputFormat format = new PinyinOutputFormat(null, CaseFormat.UPPERCASE.ToString(), VCharFormat.WITH_U_UNICODE.ToString());
            PinyinFormatAssert("李", "LĬ", format);
            PinyinFormatAssert("单于", "CHÁN YÚ", format);
            PinyinFormatAssert("乐", "YUÈ", format);
            PinyinFormatAssert("厍", "SHÈ", format);
            PinyinFormatAssert("欧", "ŌU", format);
        }

        /// <summary>
        /// 根据拼音获取姓
        /// </summary>
        [TestMethod]
        public void GetNameByPinyin1()
        {
            var hanzi = Pinyin4Name.GetHanzi("li", true);

            assertArrayAreEquals(new[] { "黎", "李", "利", "厉", "郦" }, hanzi);
        }

        /// <summary>
        /// 根据拼音获取姓
        /// </summary>
        [TestMethod]
        public void GetNameByPinyin2()
        {
            var hanzi = Pinyin4Name.GetHanzi("chan yu", true);
            assertArrayAreEquals(new[] { "单于" }, hanzi);
        }
        /// <summary>
        /// 根据拼音获取姓
        /// </summary>
        [TestMethod]
        public void GetNameByPinyin3()
        {
            var hanzi = Pinyin4Name.GetHanzi("ou", false);
            assertArrayAreEquals(new[] { "欧阳", "欧" }, hanzi);
        }

        /// <summary>
        /// 根据拼音获取姓
        /// </summary>
        [TestMethod]
        public void GetNameByPinyin4()
        {
            var hanzi = Pinyin4Name.GetHanzi("f", false);
            assertArrayAreEquals(new[] { "樊", "范", "方", "房", "费", "丰", "封", "酆", "冯", "凤", "伏", "扶", "福", "符", "傅", "富" }, hanzi);
        }

        /// <summary>
        /// 根据拼音获取姓
        /// </summary>
        [TestMethod]
        public void GetNameByPinyin5()
        {
            var hanzi = Pinyin4Name.GetHanzi("fe", false);
            assertArrayAreEquals(new[] { "费", "丰", "封", "酆", "冯", "凤" }, hanzi);
        }

        /// <summary>
        /// 判断读音
        /// </summary>
        /// <param name="hanzi"></param>
        /// <param name="expected"></param>
        private void HanziAssert(string hanzi, string expected)
        {
            string actual = Pinyin4Name.GetPinyin(hanzi);
            Assert.AreEqual<string>(expected, actual);
        }
        /// <summary>
        /// 判断拼音格式
        /// </summary>
        /// <param name="hanzi"></param>
        /// <param name="expected"></param>
        private void PinyinFormatAssert(string hanzi, string expected, PinyinOutputFormat format)
        {
            string fmted = Pinyin4Name.GetPinyinWithFormat(hanzi, format);

            Assert.AreEqual<string>(expected, fmted);
        }

        /// <summary>
        /// 判断两个字符串数组是否相等
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        private void assertArrayAreEquals(string[] expected, string[] actual)
        {
            Assert.AreEqual<int>(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual<string>(expected[i], actual[i]);
            }
        }
    }
}
