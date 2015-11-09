using hyjiacan.util.p4n;
using hyjiacan.util.p4n.format;
using System;
using System.Collections.Generic;
using System.Web;

namespace Pinyin4NetWebDemo
{
    /// <summary>
    /// Pinyin4NetHandler 的摘要说明
    /// </summary>
    public class Pinyin4NetHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest req = context.Request;
            HttpResponse res = context.Response;

            // 这里设置UTF8，避免乱码
            res.Charset = "UTF-8";
            res.ContentEncoding = System.Text.Encoding.UTF8;
            res.ContentType = "text/plain";

            string pinyin = string.Empty;

            try
            {
                string hanzi = req["hanzi"];
                // 用于控制多音字的返回， 有两种取值 first：取第1个音，all：取所有音 默认为取第1个音
                string multi = req["multi"];
                
                // 请求参数不为空才处理
                if (!string.IsNullOrEmpty(hanzi))
                {
                    #region // 解析从客户端来的输出格式设置
                    HanyuPinyinOutputFormat format = new HanyuPinyinOutputFormat();

                    format.setCaseType(new HanyuPinyinCaseType(req["caseType"]));
                    format.setToneType(new HanyuPinyinToneType(req["toneType"]));
                    format.setVCharType(new HanyuPinyinVCharType(req["vType"]));
                    #endregion

                    foreach (char ch in hanzi)
                    {
                        if (Util.IsHanzi(ch))
                        {
                            // 是汉字才处理
                            string[] py = PinyinHelper.toHanyuPinyinStringArray(ch, format);
                            
                            if (multi.Equals("first", StringComparison.OrdinalIgnoreCase) || py.Length == 1)
                            {
                                // 拼音间追加一个空格，这里如果是多间字，拼音可能不准确
                                pinyin += py[0] + " ";
                            }
                            else
                            {
                                pinyin += "(" + string.Join(",", py) + ") ";
                            }
                        }
                        else
                        {// 不是汉字直接追加
                            pinyin += ch.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                pinyin = ex.Message;
            }

            res.Write(pinyin);
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