using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace hyjiacan.util.p4n
{
    /**
 * A class contains resource that translates from Hanyu Pinyin to Gwoyeu
 * Romatzyh
 * 
 * @author Li Min (xmlerlimin@gmail.com)
 * 
 */
    class GwoyeuRomatzyhResource
    {
        /**
         * A DOM model contains Hanyu Pinyin to Gwoyeu Romatzyh mapping
         */
        private XmlDocument pinyinToGwoyeuMappingDoc;

        /**
         * @param pinyinToGwoyeuMappingDoc
         *            The pinyinToGwoyeuMappingDoc to set.
         */
        private void setPinyinToGwoyeuMappingDoc(XmlDocument pinyinToGwoyeuMappingDoc)
        {
            this.pinyinToGwoyeuMappingDoc = pinyinToGwoyeuMappingDoc;
        }

        /**
         * @return Returns the pinyinToGwoyeuMappingDoc.
         */
        internal XmlDocument getPinyinToGwoyeuMappingDoc()
        {
            return pinyinToGwoyeuMappingDoc;
        }

        /**
         * Private constructor as part of the singleton pattern.
         */
        private GwoyeuRomatzyhResource()
        {
            initializeResource();
        }

        /**
         * Initialiez a DOM contains Hanyu Pinyin to Gwoyeu mapping
         */
        private void initializeResource()
        {
            try
            {
                String mappingFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"pinyindb/pinyin_gwoyeu_mapping.xml");
                XmlDocument doc = new XmlDocument();
                doc.Load(mappingFileName);
                setPinyinToGwoyeuMappingDoc(doc);
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
        internal static GwoyeuRomatzyhResource getInstance()
        {
            return GwoyeuRomatzyhSystemResourceHolder.theInstance;
        }

        /**
         * Singleton implementation helper.
         */
        private static class GwoyeuRomatzyhSystemResourceHolder
        {
            public static GwoyeuRomatzyhResource theInstance = new GwoyeuRomatzyhResource();
        }
    }
}
