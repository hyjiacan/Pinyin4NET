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
    }
}
