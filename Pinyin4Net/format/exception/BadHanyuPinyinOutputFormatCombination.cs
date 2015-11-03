using System;
using System.Collections.Generic;
using System.Text;

namespace hyjiacan.util.p4n.format.exception
{

    /**
     * An exception class indicates the wrong combination of pinyin output formats
     * 
     * @author Li Min (xmlerlimin@gmail.com)
     * 
     */
    public class BadHanyuPinyinOutputFormatCombination : Exception
    {
        /**
         * Constructor
         * 
         * @param message
         *            the exception message
         */
        public BadHanyuPinyinOutputFormatCombination(String message)
            : base(message)
        {

        }

        /**
         * Automatically generated ID
         */
        private const long serialVersionUID = -8500822088036526862L;
    }
}
