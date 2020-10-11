using System.Collections.Generic;

namespace hyjiacan.py4n
{
    /// <summary>
    /// 调用 GetPinyinArray 时的返回项数据结构 
    /// </summary>
    public class PinyinItem : List<string>
    {
        /// <summary>
        /// 原始字符
        /// </summary>
        public char RawChar { get; set; }
        /// <summary>
        /// 是否是汉字
        /// </summary>
        public bool IsHanzi { get; private set; }

        public PinyinItem() { }
        public PinyinItem(char character)
        {
            RawChar = character;
            IsHanzi = PinyinUtil.IsHanzi(character);
        }
    }
}
