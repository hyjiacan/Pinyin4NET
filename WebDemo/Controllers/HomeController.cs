using hyjiacan.py4n;
using Microsoft.AspNetCore.Mvc;
using WebDemo.Models;
using System;
using System.Text;

namespace WebDemo.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Index(RequestModel request)
        {
            try
            {
                // 请求参数不为空才处理
                if (string.IsNullOrEmpty(request.Key))
                {
                    return string.Empty;
                }
                if (request.Cmd == "0")
                {
                    return Hanzi2Pinyin(request);
                }

                string[] hanzi = Pinyin4Net.GetHanzi(request.Key, request.MatchAll);
                return string.Join(",", hanzi);
            }

            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public IActionResult Error()
        {
            return View();
        }

        private string Hanzi2Pinyin(RequestModel request)
        {
            var result = new StringBuilder();

            // 解析从客户端来的输出格式设置
            PinyinFormat format = PinyinUtil.ParseFormat(request.ToneType) |
                PinyinUtil.ParseFormat(request.CaseType) |
                PinyinUtil.ParseFormat(request.VType);

            foreach (char ch in request.Key)
            {
                if (!PinyinUtil.IsHanzi(ch))
                {// 不是汉字直接追加
                    result.Append(ch);
                    continue;
                }

                // 是汉字才处理

                // 是否只取第一个拼音
                if (request.Multi.Equals("first", StringComparison.OrdinalIgnoreCase))
                {
                    // 拼音间追加一个空格，这里如果是多间字，拼音可能不准确
                    result.AppendFormat("{0} ", Pinyin4Net.GetFirstPinyin(ch, format));
                    continue;
                }

                string[] py = Pinyin4Net.GetPinyin(ch, format);
                result.AppendFormat("({0}) ", string.Join(",", py));
            }

            return result.ToString();
        }
    }
}
