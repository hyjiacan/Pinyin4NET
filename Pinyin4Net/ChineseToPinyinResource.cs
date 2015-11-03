using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace hyjiacan.util.p4n
{
    /**
 * Manage all external resources required in PinyinHelper class.
 * 
 * @author Li Min (xmlerlimin@gmail.com)
 * 
 */
    class ChineseToPinyinResource
    {
        /**
         * A hash table contains <Unicode, HanyuPinyin> pairs
         */
        private Dictionary<string, string> unicodeToHanyuPinyinTable;

        /**
         * @param unicodeToHanyuPinyinTable
         *            The unicodeToHanyuPinyinTable to set.
         */
        private void setUnicodeToHanyuPinyinTable(
                Dictionary<string, string> unicodeToHanyuPinyinTable)
        {
            this.unicodeToHanyuPinyinTable = unicodeToHanyuPinyinTable;
        }

        /**
         * @return Returns the unicodeToHanyuPinyinTable.
         */
        private Dictionary<string, string> getUnicodeToHanyuPinyinTable()
        {
            return unicodeToHanyuPinyinTable;
        }

        /**
         * Private constructor as part of the singleton pattern.
         */
        private ChineseToPinyinResource()
        {
            initializeResource();
        }

        /**
         * Initialize a hash-table contains <Unicode, HanyuPinyin> pairs
         */
        private void initializeResource()
        {
            try
            {
                String resourceName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pinyindb/unicode_to_hanyu_pinyin.txt");

                unicodeToHanyuPinyinTable = new Dictionary<string, string>();
                string line = string.Empty;
                string[] lineRes = null;
                using (StreamReader reader = new StreamReader(resourceName))
                {
                    while (!reader.EndOfStream)
                    {
                        line = reader.ReadLine();
                        if (string.IsNullOrEmpty(line))
                        {
                            continue;
                        }
                        line = line.Trim();
                        lineRes = line.Split(' ');
                        unicodeToHanyuPinyinTable.Add(lineRes[0], lineRes[1]);
                    }
                }

            }
            catch (FileNotFoundException ex)
            {
                Util.Log(ex);
            }
            catch (IOException ex)
            {
                Util.Log(ex);
            }
        }

        /**
         * Get the unformatted Hanyu Pinyin representations of the given Chinese
         * character in array format.
         * 
         * @param ch
         *            given Chinese character in Unicode
         * @return The Hanyu Pinyin strings of the given Chinese character in array
         *         format; return null if there is no corresponding Pinyin string.
         */
        internal String[] getHanyuPinyinStringArray(char ch)
        {
            String pinyinRecord = getHanyuPinyinRecordFromChar(ch);

            if (null != pinyinRecord)
            {
                int indexOfLeftBracket = pinyinRecord.IndexOf(Field.LEFT_BRACKET);
                int indexOfRightBracket = pinyinRecord.LastIndexOf(Field.RIGHT_BRACKET);

                String stripedString = pinyinRecord.Substring(indexOfLeftBracket + 1, indexOfRightBracket - 1);

                return stripedString.Split(Field.COMMA);

            }
            else
                return null; // no record found or mal-formatted record
        }

        /**
         * @param record
         *            given record string of Hanyu Pinyin
         * @return return true if record is not null and record is not "none0" and
         *         record is not mal-formatted, else return false
         */
        private bool isValidRecord(String record)
        {
            String noneStr = "(none0)";

            if ((null != record) && !record.Equals(noneStr)
                    && record.StartsWith(Field.LEFT_BRACKET.ToString())
                    && record.EndsWith(Field.RIGHT_BRACKET.ToString()))
            {
                return true;
            }
            else
                return false;
        }

        /**
         * @param ch
         *            given Chinese character in Unicode
         * @return corresponding Hanyu Pinyin Record in Properties file; null if no
         *         record found
         */
        private String getHanyuPinyinRecordFromChar(char ch)
        {
            // convert Chinese character to code point (integer)
            // please refer to http://www.unicode.org/glossary/#code_point
            // Another reference: http://en.wikipedia.org/wiki/Unicode
            int codePointOfChar = ch;

            String codepointHexStr = codePointOfChar.ToString("x").ToUpper();

            // fetch from hashtable
            Dictionary<string, string> dic = getUnicodeToHanyuPinyinTable();
            if (dic.ContainsKey(codepointHexStr))
            {
                String foundRecord = dic[codepointHexStr];

                return isValidRecord(foundRecord) ? foundRecord : null;
            }
            else
            {
                return null;
            }
        }

        /**
         * Singleton factory method.
         * 
         * @return the one and only MySingleton.
         */
        internal static ChineseToPinyinResource getInstance()
        {
            return ChineseToPinyinResourceHolder.theInstance;
        }

        /**
         * Singleton implementation helper.
         */
        private static class ChineseToPinyinResourceHolder
        {
            internal static ChineseToPinyinResource theInstance = new ChineseToPinyinResource();
        }

        /**
         * A class encloses common string constants used in Properties files
         * 
         * @author Li Min (xmlerlimin@gmail.com)
         */
        class Field
        {
            internal static char LEFT_BRACKET = '(';

            internal static char RIGHT_BRACKET = ')';

            internal static char COMMA = ',';
        }
    }
}
