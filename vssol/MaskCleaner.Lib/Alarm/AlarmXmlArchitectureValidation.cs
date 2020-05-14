using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.IO;

namespace MaskAutoCleaner.Alarm
{
    class SchemaValidation
    {
        private static bool isValid = true;

        /// 提供 驗證 XML 是否符合 XSD 格式
        public static bool ValidationSchemaNow(string xmlstring, string schemastring)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlstring);
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas.Add("urn:MaskCleanTool-schema", schemastring);
            settings.ValidationEventHandler += new ValidationEventHandler(MyValidationEventHandler);

            XmlNodeReader nodeReader = new XmlNodeReader(doc);
            XmlReader xr = XmlReader.Create(nodeReader, settings);
            try
            {
                while (xr.Read()) ;
            }
            catch (Exception e)
            {
                isValid = false;
                WriteErrorLogs("Validation Warning:   " + e.Message);
            }
            finally
            {
                xr.Close();
            }
            return isValid;
        }


        ///
        /// 驗證處理 Handler
        ///
        private static void MyValidationEventHandler(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
            {
                WriteErrorLogs("Validation Warning:   " + args.Message);
            }
            else
            {
                WriteErrorLogs("Validation Error:   " + args.Message);
            }
            isValid = false;
        }

        ///
        /// 錯誤 log 紀錄
        ///
        private static void WriteErrorLogs(string p)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(@"D:\xml\erro.txt", true, Encoding.UTF8);
                sw.WriteLine("[" + DateTime.Now.ToString() + "]" + p);
                sw.Flush();
                sw.Close();
            }
            catch { sw.Close(); }
        }
    }
}
