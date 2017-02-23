using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace hyjiacan.py4n.data
{
    /// <summary>
    /// 使用姓名数据库查询
    /// </summary>
    internal class NameDB
    {
        // 实例
        private static NameDB instance;

        private readonly Dictionary<string, string> map;

        /// <summary>
        ///  获取单实例
        /// </summary>
        public static NameDB Instance
        {
            get { return instance ?? (instance = new NameDB()); }
        }

        /// <summary>
        /// 私有构造
        /// </summary>
        private NameDB()
        {
            map = new Dictionary<string, string>();

            loadResource();
        }

        /// <summary>
        /// 加载拼音库资源
        /// </summary>
        private void loadResource()
        {
            string data = PinyinDbResource.name;

            string name = string.Empty;
            string pinyin = string.Empty;

            foreach (string buf in data.Split('\n').Where(buf => !string.IsNullOrEmpty(buf)))
            {
                var temp = buf.Split('=');
                // 取姓
                name = temp[0];

                // 取拼音串 小心有个 \r 的回车符号
                pinyin = temp[1].Trim();

                map.Add(name, pinyin.Replace('-', ' '));
            }
        }
        /// <summary>
        /// 获取汉字的拼音
        /// </summary>
        /// <param name="hanzi"></param>
        /// <returns>若未找到汉字拼音，则返回空</returns>
        public string GetPinyin(string hanzi)
        {
            return map.ContainsKey(hanzi) ? map[hanzi] : null;
        }

        /// <summary>
        /// 根据拼音获取汉字
        /// </summary>
        /// <param name="pinyin">拼音</param>
        /// <param name="matchAll">是否全部匹配，为true时，匹配整个拼音，否则匹配开头字符</param>
        /// <returns></returns>
        public string[] GetHanzi(string pinyin, bool matchAll)
        {
            Regex reg = new Regex("[0-9]");
            // 完全匹配
            if (matchAll)
            {
                // 查询到匹配的拼音的汉字
                return map.Where(item => reg.Replace(item.Value, "").Equals(pinyin))
                    .Select(item => item.Key).ToArray();
            }
            // 匹配开头部分
            else
            {
                // 查询到匹配的拼音的unicode编码
                return map.Where(item => reg.Replace(item.Value, "").StartsWith(pinyin))
                    .Select(item => item.Key).ToArray();
            }
        }
    }
}
