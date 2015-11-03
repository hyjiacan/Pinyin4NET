using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace hyjiacan.util.p4n
{
    /**
 * Contains the resource supporting translations among different Chinese
 * Romanization systems
 * 
 * @author Li Min (xmlerlimin@gmail.com)
 * 
 */
    class PinyinRomanizationResource
    {
        /**
         * A DOM model contains variable pinyin presentations
         */
        private XmlDocument pinyinMappingDoc;

        /**
         * @param pinyinMappingDoc
         *            The pinyinMappingDoc to set.
         */
        private void setPinyinMappingDoc(XmlDocument pinyinMappingDoc)
        {
            this.pinyinMappingDoc = pinyinMappingDoc;
        }

        /**
         * @return Returns the pinyinMappingDoc.
         */
        internal XmlDocument getPinyinMappingDoc()
        {
            return pinyinMappingDoc;
        }

        /**
         * Private constructor as part of the singleton pattern.
         */
        private PinyinRomanizationResource()
        {
            initializeResource();
        }

        /**
         * Initialiez a DOM contains variable PinYin representations
         */
        private void initializeResource()
        {
            try
            {
                String mappingFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"pinyindb/pinyin_mapping.xml");

                XmlDocument doc = new XmlDocument();
                doc.Load(mappingFileName);
                setPinyinMappingDoc(doc);
            }
            catch (FileNotFoundException ex)
            {
                Util.Log(ex);
            }
            catch (IOException ex)
            {
                Util.Log(ex);
            }
            catch (Exception ex)
            {
                Util.Log(ex);
            }
        }

        /**
         * Singleton factory method.
         * 
         * @return the one and only MySingleton.
         */
        public static PinyinRomanizationResource getInstance()
        {
            return PinyinRomanizationSystemResourceHolder.theInstance;
        }

        /**
         * Singleton implementation helper.
         */
        private static class PinyinRomanizationSystemResourceHolder
        {
            public static PinyinRomanizationResource theInstance = new PinyinRomanizationResource();
        }
    }
}
