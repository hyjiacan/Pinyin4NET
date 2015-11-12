using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

        private const string dbname = "pinyin.dat";
        private string dbpath;

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
            dbpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dbname);

            map = new Dictionary<string, string[]>();

            loadResource();
        }

        /// <summary>
        /// 加载拼音库资源
        /// </summary>
        private void loadResource()
        {
            using (StreamReader reader = new StreamReader(dbpath))
            {
                string buf = string.Empty;
                string code = string.Empty;
                string pinyin = string.Empty;

                while (!reader.EndOfStream)
                {
                    buf = reader.ReadLine();

                    if (string.IsNullOrEmpty(buf))
                    {
                        continue;
                    }
                    // 取unicode码 
                    code = buf.Substring(0, 4);

                    // 取拼音串 去掉环绕拼音的括号
                    pinyin = buf.Substring(5).Trim('(', ')');

                    map.Add(code, pinyin.Split(','));
                }
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
    }
}
