using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo.Models
{
    public class RequestModel
    {
        /// <summary>
        /// 输入的汉字或拼音，这是需要被处理的数据
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 请求的命令 0: 汉字->拼音  1: 拼音->汉字
        /// </summary>
        public string Cmd { get; set; }

        /// <summary>
        /// 用于控制多音字的返回， 有两种取值 first：取第1个音，all：取所有音 默认为取第1个音
        /// </summary>
        public string Multi { get; set; }


        public string CaseType { get; set; }

        public string ToneType { get; set; }

        public string VType { get; set; }
    }
}
