using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace hyjiacan.py4n
{
    /// <summary>
    /// 拼音数据库文件操作（单实例类）
    /// </summary>
    internal sealed class PinyinDB
    {
        // 实例
        private static PinyinDB instance;

        private Dictionary<string, string[]> map;

        /// <summary>
        ///  获取单实例
        /// </summary>
        public static PinyinDB Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PinyinDB();
                }

                return instance;
            }
        }

        /// <summary>
        /// 私有构造
        /// </summary>
        private PinyinDB()
        {
            map = new Dictionary<string, string[]>();

            loadResource();
        }

        /// <summary>
        /// 加载拼音库资源
        /// </summary>
        private void loadResource()
        {
            string data = PinyinDbResource.data;

            string code = string.Empty;
            string pinyin = string.Empty;

            foreach (string buf in data.Split('\n'))
            {
                if (string.IsNullOrEmpty(buf))
                {
                    continue;
                }
                // 取unicode码 
                code = buf.Substring(0, 4);

                // 取拼音串 去掉环绕拼音的括号
                pinyin = buf.Trim().Substring(5).Trim('(', ')');

                map.Add(code, pinyin.Split(','));
            }
        }
        /// <summary>
        /// 获取汉字的拼音数组。
        /// </summary>
        /// <param name="hanzi"></param>
        /// <returns>若未找到汉字拼音，则返回空数组</returns>
        public string[] GetPinyin(char hanzi)
        {
            string[] pinyin = { };
            // 汉字转换成十进制unicode编码
            int decCode = hanzi;
            // 十进制unicode编码转换成十六进制编码
            string code = decCode.ToString("x").ToUpper();

            if (map.ContainsKey(code))
            {
                pinyin = map[code];
            }

            return pinyin;
        }

        /// <summary>
        /// 根据拼音获取汉字
        /// </summary>
        /// <param name="pinyin">拼音</param>
        /// <param name="matchAll">是否全部匹配，为true时，匹配整个拼音，否则匹配开头字符</param>
        /// <returns></returns>
        public string[] GetHanzi(string pinyin, bool matchAll)
        {
            List<string> hanzi = new List<string>();
            Regex reg = new Regex("[0-9]");
            // 完全匹配
            if (matchAll)
            {
                // 查询到匹配的拼音的unicode编码
                foreach (string code in map.Keys)
                {
                    foreach (var item in map[code])
                    {
                        if (reg.Replace(item, "").Equals(pinyin))
                        {
                            // unicode编码转汉字
                            hanzi.Add(Convert.ToChar(Convert.ToInt32(code, 16)).ToString());
                            break;
                        }
                    }
                }
            }
            // 匹配开头部分
            else
            {
                // 查询到匹配的拼音的unicode编码
                foreach (string code in map.Keys)
                {
                    foreach (var item in map[code])
                    {
                        if (item.StartsWith(pinyin))
                        {
                            // unicode编码转汉字
                            hanzi.Add(Convert.ToChar(Convert.ToInt32(code, 16)).ToString());
                            break;
                        }
                    }
                }
            }

            return hanzi.ToArray();
        }
    }
}
