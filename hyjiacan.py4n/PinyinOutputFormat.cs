using System;
using hyjiacan.py4n.format;

namespace hyjiacan.py4n
{
    /// <summary>
    /// 拼音输出格式
    /// </summary>
    public class PinyinOutputFormat
    {
        // 声调格式
        // 大小写格式
        // 字符v的格式

        /// <summary>
        /// 获取声调格式
        /// </summary>
        public ToneFormat GetToneFormat { get; private set; }

        /// <summary>
        /// 获取大小写格式
        /// </summary>
        public CaseFormat GetCaseFormat { get; private set; }

        /// <summary>
        /// 获取字符v的格式
        /// </summary>
        public VCharFormat GetVCharFormat { get; private set; }

        /// <summary>
        /// 使用默认值初始化输出格式
        /// ToneFormat.WITH_TONE_MARK,
        /// CaseFormat.LOWERCASE,
        /// VCharFormat.WITH_U_UNICODE
        /// </summary>
        public PinyinOutputFormat()
        {
            GetToneFormat = ToneFormat.WITH_TONE_MARK;
            GetCaseFormat = CaseFormat.LOWERCASE;
            GetVCharFormat = VCharFormat.WITH_U_UNICODE;
        }
        /// <summary>
        /// 通过构造初始化输入格式
        /// </summary>
        /// <param name="toneFormat">声调格式</param>
        /// <param name="caseFormat">大小写格式</param>
        /// <param name="vCharFormat">字符V的格式</param>
        public PinyinOutputFormat(ToneFormat toneFormat, CaseFormat caseFormat, VCharFormat vCharFormat)
        {
            SetFormat(toneFormat, caseFormat, vCharFormat);
        }
        /// <summary>
        /// 通过构造初始化输入格式
        /// </summary>
        /// <param name="toneFormat">声调格式字符串</param>
        /// <param name="caseFormat">大小写格式字符串</param>
        /// <param name="vCharFormat">字符V的格式字符串</param>
        /// <see cref="ToneFormat"/>
        /// <see cref="CaseFormat"/>
        /// <see cref="VCharFormat"/>
        public PinyinOutputFormat(string toneFormat, string caseFormat, string vCharFormat)
        {
            SetFormat(toneFormat, caseFormat, vCharFormat);
        }
        /// <summary>
        ///  设置输入格式
        /// </summary>
        /// <param name="toneFormat">声调格式</param>
        /// <param name="caseFormat">大小写格式</param>
        /// <param name="vCharFormat">字符V的格式</param>
        public void SetFormat(ToneFormat toneFormat, CaseFormat caseFormat, VCharFormat vCharFormat)
        {
            GetToneFormat = toneFormat;
            GetCaseFormat = caseFormat;
            GetVCharFormat = vCharFormat;
        }
        /// <summary>
        /// 设置输入格式
        /// </summary>
        /// <param name="toneFormat">声调格式字符串</param>
        /// <param name="caseFormat">大小写格式字符串</param>
        /// <param name="vCharFormat">字符V的格式字符串</param>
        /// <see cref="ToneFormat"/>
        /// <see cref="CaseFormat"/>
        /// <see cref="VCharFormat"/>
        public void SetFormat(string toneFormat, string caseFormat, string vCharFormat)
        {
            if (!string.IsNullOrEmpty(toneFormat))
            {
                GetToneFormat = (ToneFormat)Enum.Parse(typeof(ToneFormat), toneFormat);
            }

            if (!string.IsNullOrEmpty(caseFormat))
            {
                GetCaseFormat = (CaseFormat)Enum.Parse(typeof(CaseFormat), caseFormat);
            }

            if (!string.IsNullOrEmpty(vCharFormat))
            {
                GetVCharFormat = (VCharFormat)Enum.Parse(typeof(VCharFormat), vCharFormat);
            }
        }
    }
}
