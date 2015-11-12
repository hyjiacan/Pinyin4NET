using System;
using System.Collections.Generic;
using System.Text;

namespace hyjiacan.py4n.exception
{
    /// <summary>
    /// 拼音异常类
    /// </summary>
    public class PinyinException : Exception
    {
        public PinyinException(string message) : base(message)
        {

        }
    }
}
