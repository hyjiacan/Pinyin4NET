using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable MemberCanBeMadeStatic.Local

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
            var pinyin = Pinyin4Name.GetFirstLetter("李");
            Assert.AreEqual("l", pinyin);
        }

        /// <summary>
        /// 复姓的首个字母
        /// </summary>
        [TestMethod]
        public void FirstOfMultiPinyinName()
        {
            var pinyin = Pinyin4Name.GetFirstLetter("单于");
            Assert.AreEqual("c y", pinyin);
        }

        /// <summary>
        /// 不存在的姓
        /// </summary>
        [TestMethod]
        public void NonExistsName()
        {
            var pinyin = Pinyin4Name.GetFirstLetter("佳");
            Assert.IsNull(pinyin);
        }

        /// <summary>
        /// 测试输出格式 (默认格式)
        /// </summary>
        [TestMethod]
        public void FormatTest1()
        {
            var format = PinyinFormat.None;

            PinyinFormatAssert("李", "li3", format);
            PinyinFormatAssert("单于", "chan2 yu2", format);
            PinyinFormatAssert("乐", "yue4", format);
            PinyinFormatAssert("厍", "she4", format);
            PinyinFormatAssert("欧", "ou1", format);
        }

        /// <summary>
        /// 测试输出格式2 (全小写)
        /// </summary>
        [TestMethod]
        public void FormatTest2()
        {
            var format = PinyinFormat.WITH_TONE_MARK |
                PinyinFormat.LOWERCASE |
                PinyinFormat.WITH_U_UNICODE;
            PinyinFormatAssert("李", "lĭ", format);
            PinyinFormatAssert("单于", "chán yú", format);
            PinyinFormatAssert("乐", "yuè", format);
            PinyinFormatAssert("厍", "shè", format);
            PinyinFormatAssert("欧", "ōu", format);
        }

        /// <summary>
        /// 测试输出格式 (首拼音大写)
        /// </summary>
        [TestMethod]
        public void FormatTest3()
        {
            var format = PinyinFormat.WITH_TONE_MARK |
                PinyinFormat.CAPITALIZE_FIRST_LETTER |
                PinyinFormat.WITH_U_UNICODE;
            PinyinFormatAssert("李", "Lĭ", format);
            PinyinFormatAssert("单于", "Chán Yú", format);
            PinyinFormatAssert("乐", "Yuè", format);
            PinyinFormatAssert("厍", "Shè", format);
            PinyinFormatAssert("欧", "Ōu", format);
        }
        /// <summary>
        /// 测试输出格式 (全大写)
        /// </summary>
        [TestMethod]
        public void FormatTest4()
        {
            var format = PinyinFormat.WITH_TONE_MARK | 
                PinyinFormat.UPPERCASE | 
                PinyinFormat.WITH_U_UNICODE;
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
            var actual = Pinyin4Name.GetPinyin(hanzi);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// 判断拼音格式
        /// </summary>
        /// <param name="hanzi"></param>
        /// <param name="expected"></param>
        /// <param name="format"></param>
        private void PinyinFormatAssert(string hanzi, string expected, PinyinFormat format)
        {
            var fmted = Pinyin4Name.GetPinyin(hanzi, format);

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
