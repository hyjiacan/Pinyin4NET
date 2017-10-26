using hyjiacan.py4n;
using System;
using System.Web;
using hyjiacan.py4n.format;

namespace Pinyin4NetWebDemo
{
    /// <summary>
    /// Pinyin4NetHandler 的摘要说明
    /// </summary>
    public class Pinyin4NetHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var req = context.Request;
            var res = context.Response;

            // 这里设置UTF8，避免乱码
            res.Charset = "UTF-8";
            res.ContentEncoding = System.Text.Encoding.UTF8;
            res.ContentType = "text/plain";

            var result = string.Empty;

            try
            {
                // 输入的汉字或拼音
                var key = req["key"];
                // 用于控制多音字的返回， 有两种取值 first：取第1个音，all：取所有音 默认为取第1个音
                var multi = req["multi"];

                // 请求参数不为空才处理
                if (!string.IsNullOrEmpty(key))
                {
                    if (req["cmd"] == "0")
                    {
                        #region // 汉字转拼音
                        // 解析从客户端来的输出格式设置
                        var format = new PinyinOutputFormat(req["toneType"], req["caseType"], req["vType"]);

                        foreach (var ch in key)
                        {
                            // 是汉字才处理
                            if (PinyinUtil.IsHanzi(ch))
                            {
                                // 是否只取第一个拼音
                                if (multi.Equals("first", StringComparison.OrdinalIgnoreCase))
                                {
                                    var py = Pinyin4Net.GetUniqueOrFirstPinyinWithFormat(ch, format);
                                    // 拼音间追加一个空格，这里如果是多间字，拼音可能不准确
                                    result += py + " ";
                                }
                                else
                                {
                                    var py = Pinyin4Net.GetPinyinWithFormat(ch, format);
                                    result += "(" + string.Join(",", py) + ") ";
                                }
                            }
                            else
                            {// 不是汉字直接追加
                                result += ch.ToString();
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region // 拼音查汉字
                        var hanzi = Pinyin4Net.GetHanzi(key, true);
                        result = string.Join(",", hanzi);
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            res.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}