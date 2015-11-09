using System;
using System.Collections.Generic;
using System.Text;

namespace hyjiacan.util.p4n
{
    public static class Util
    {
        public static void Log(string message)
        {
            Console.WriteLine(message);
        }

        public static void Log(Exception ex)
        {
            if (ex == null) { return; }
            Log(ex.Message + Environment.NewLine + ex.StackTrace);
        }
        /// <summary>
        /// 判断字符是否是汉字
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool IsHanzi(char ch)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ch.ToString(), @"[\u4e00-\u9fbb]");
        }
    }
}
