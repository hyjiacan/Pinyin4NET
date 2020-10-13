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

            var firstPinyinOnly = request.Multi == "first";

            return Pinyin4Net.GetPinyin(request.Key, format, true, false, firstPinyinOnly);
        }
    }
}
