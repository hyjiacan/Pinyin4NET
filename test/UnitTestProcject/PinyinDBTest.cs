using hyjiacan.py4n;
using hyjiacan.py4n.format;
using Xunit;

namespace PinyinDBTest
{
    public class PinyinDBTest
    {
        /// <summary>
        /// 单音字
        /// </summary>
        [Fact]
        public void SinglePinyin()
        {
            HanziAssert('李', new string[] { "li3" });
        }
        /// <summary>
        /// 多音字
        /// </summary>
        [Fact]
        public void MultiPinyin()
        {
            HanziAssert('传', new string[] { "chuan2", "zhuan4" });
        }

        /// <summary>
        /// 多音字的首个拼音
        /// </summary>
        [Fact]
        public void FirstOfMultiPinyin()
        {
            string pinyin = Pinyin4Net.GetUniqueOrFirstPinyin('传');
            Assert.Equal<string>("chuan2", pinyin);
        }

        /// <summary>
        /// 测试输出格式
        /// </summary>
        [Fact]
        public void FormatTest1()
        {
            PinyinOutputFormat format = new PinyinOutputFormat(ToneFormat.WITHOUT_TONE, CaseFormat.LOWERCASE, VCharFormat.WITH_U_UNICODE);
            Assert.Equal<ToneFormat>(ToneFormat.WITHOUT_TONE, format.GetToneFormat);
            Assert.Equal<CaseFormat>(CaseFormat.LOWERCASE, format.GetCaseFormat);
            Assert.Equal<VCharFormat>(VCharFormat.WITH_U_UNICODE, format.GetVCharFormat);
        }
        /// <summary>
        /// 测试输出格式
        /// </summary>
        [Fact]
        public void FormatTest2()
        {
            PinyinOutputFormat format = new PinyinOutputFormat("WITHOUT_TONE", "LOWERCASE", "WITH_U_UNICODE");
            Assert.Equal<ToneFormat>(ToneFormat.WITHOUT_TONE, format.GetToneFormat);
            Assert.Equal<CaseFormat>(CaseFormat.LOWERCASE, format.GetCaseFormat);
            Assert.Equal<VCharFormat>(VCharFormat.WITH_U_UNICODE, format.GetVCharFormat);
        }
        /// <summary>
        /// 测试输出格式
        /// </summary>
        [Fact]
        public void FormatTest3()
        {
            PinyinOutputFormat format = new PinyinOutputFormat(ToneFormat.WITH_TONE_MARK, CaseFormat.LOWERCASE, VCharFormat.WITH_U_UNICODE);
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
        [Fact]
        public void TestString1()
        {
            string s = "Javascript 爱好者 传说";
            PinyinOutputFormat format = new PinyinOutputFormat();
            string expected = "Javascript ài hăo zhĕ  chuán shuō";
            string pinyin = Pinyin4Net.GetPinyin(s, format);
            Assert.Equal<string>(expected, pinyin);
        }

        /// <summary>
        /// 测试字符串
        /// </summary>
        [Fact]
        public void TestString2()
        {
            string s = "Javascript 爱好者 传说";
            PinyinOutputFormat format = new PinyinOutputFormat(null, CaseFormat.CAPITALIZE_FIRST_LETTER.ToString(), VCharFormat.WITH_U_UNICODE.ToString());
            string expected = "Javascript Ài Hăo Zhĕ  Chuán Shuō";
            string pinyin = Pinyin4Net.GetPinyin(s, format);
            Assert.Equal<string>(expected, pinyin);
        }
        /// <summary>
        /// 测试字符串
        /// </summary>
        [Fact]
        public void TestString3()
        {
            string s = "Javascript 爱好者 传说";
            PinyinOutputFormat format = new PinyinOutputFormat(null, CaseFormat.UPPERCASE.ToString(), VCharFormat.WITH_U_UNICODE.ToString());
            string expected = "Javascript ÀI HĂO ZHĔ  CHUÁN SHUŌ";
            string pinyin = Pinyin4Net.GetPinyin(s, format);
            Assert.Equal<string>(expected, pinyin);
        }
        /// <summary>
        /// 测试字符串
        /// </summary>
        [Fact]
        public void TestString4()
        {
            string s = "Javascript 爱好者 传说";
            PinyinOutputFormat format = new PinyinOutputFormat(null, CaseFormat.UPPERCASE.ToString(), VCharFormat.WITH_U_UNICODE.ToString());
            string expected = "JAVASCRIPT ÀI HĂO ZHĔ  CHUÁN SHUŌ";
            string pinyin = Pinyin4Net.GetPinyin(s, format, true, false, false);
            Assert.Equal<string>(expected, pinyin);
        }
        /// <summary>
        /// 测试字符串
        /// </summary>
        [Fact]
        public void TestString5()
        {
            string s = "JavaScript 爱好者 传说";
            PinyinOutputFormat format = new PinyinOutputFormat(null, CaseFormat.LOWERCASE.ToString(), VCharFormat.WITH_U_UNICODE.ToString());
            string expected = "JavaScript ài hăo zhĕ  chuán shuō";
            string pinyin = Pinyin4Net.GetPinyin(s, format);
            Assert.Equal<string>(expected, pinyin);
        }
        /// <summary>
        /// 测试字符串
        /// </summary>
        [Fact]
        public void TestString6()
        {
            string s = "JavaScript 爱好者 传说";
            PinyinOutputFormat format = new PinyinOutputFormat(null, CaseFormat.LOWERCASE.ToString(), VCharFormat.WITH_U_UNICODE.ToString());
            string expected = "javascript ài hăo zhĕ  chuán shuō";
            string pinyin = Pinyin4Net.GetPinyin(s, format, true, false, false);
            Assert.Equal<string>(expected, pinyin);
        }

        /// <summary>
        /// 取首字母
        /// </summary>
        [Fact]
        public void TestFirstLetter1()
        {
            string s = "JavaScript 爱好者 传说";
            string expected = "JavaScript [a] [h] [z]  [c] [s]";
            string pinyin = Pinyin4Net.GetPinyin(s, null, false, true, false);
            Assert.Equal<string>(expected, pinyin);
        }

        /// <summary>
        /// 取首字母多音字
        /// </summary>
        [Fact]
        public void TestFirstLetter2()
        {
            string s = "JavaScript 爱好者 传说";
            string expected = "JavaScript [a] [h] [z]  [c,z] [s,y]";
            string pinyin = Pinyin4Net.GetPinyin(s, null, false, true, true);
            Assert.Equal<string>(expected, pinyin);
        }

        /// <summary>
        /// 判断汉字与读音数组
        /// </summary>
        /// <param name="hanzi"></param>
        /// <param name="expected"></param>
        private void HanziAssert(char hanzi, string[] expected)
        {
            string[] actual = Pinyin4Net.GetPinyin(hanzi);
            assertArrayEquals(expected, actual);
        }
        /// <summary>
        /// 判断拼音格式
        /// </summary>
        /// <param name="hanzi"></param>
        /// <param name="expected"></param>
        private void PinyinFormatAssert(char hanzi, string expected, PinyinOutputFormat format)
        {
            string fmted = Pinyin4Net.GetUniqueOrFirstPinyinWithFormat(hanzi, format);

            Assert.Equal<string>(expected, fmted);
        }

        /// <summary>
        /// 判断两个字符串数组是否相等
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        private void assertArrayEquals(string[] expected, string[] actual)
        {
            Assert.Equal<int>(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal<string>(expected[i], actual[i]);
            }
        }
    }
}
