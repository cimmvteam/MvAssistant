using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace MvAssistant.v0_2.Mac.Xml
{
    public class XmlHelper
    {
        public void CreatXml(string xmlPath)
        {
            XElement xElement = new XElement(
               new XElement("MaskRobotPosition")
               );

            //需要指定編碼格式，否則在讀取時會拋：根級別上的資料無效。 第 1 行 位置 1異常
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.Indent = true;
            XmlWriter xw = XmlWriter.Create(xmlPath, settings);
            xElement.Save(xw);
            //寫入檔案
            xw.Flush();
            xw.Close();
        }

        public void Insert(string xmlPath, string PR_No, string MotionType, string X, string Y, string Z, string W, string P, string R, string E1)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlPath);
                XmlNode node = doc.SelectSingleNode("MaskRobotPosition/PosInfo[@PR_No.=" + PR_No + "]"); //搜尋PR_No.符合條件的節點
                if (node == null)
                {
                    var root = doc.DocumentElement;//取到根結點
                    XmlElement PosInfo = doc.CreateElement("PosInfo");

                    PosInfo.SetAttribute("E1", E1);
                    PosInfo.SetAttribute("R", R);
                    PosInfo.SetAttribute("P", P);
                    PosInfo.SetAttribute("W", W);
                    PosInfo.SetAttribute("Z", Z);
                    PosInfo.SetAttribute("Y", Y);
                    PosInfo.SetAttribute("X", X);
                    PosInfo.SetAttribute("MotionType", MotionType);
                    PosInfo.SetAttribute("PR_No.", PR_No);

                    //新增為根元素的第一層子結點
                    root.AppendChild(PosInfo);
                    doc.Save(xmlPath);
                }
                else
                    throw new Exception("PR[" + PR_No + "] was already exist !!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(string xmlPath, string PR_No)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlPath);
                XmlNode node = doc.SelectSingleNode("MaskRobotPosition/PosInfo[@PR_No.=" + PR_No + "]"); //搜尋PR_No.符合條件的節點
                if (node != null)
                {
                    var root = doc.DocumentElement;
                    XmlElement PosInfo = (XmlElement)node;
                    root.RemoveChild(PosInfo);
                    doc.Save(xmlPath);
                }
                else
                    throw new Exception("PR[" + PR_No + "] is not exist !!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(string xmlPath, string PR_No, string MotionType, string X, string Y, string Z, string W, string P, string R, string E1)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlPath);
                XmlNode node = doc.SelectSingleNode("MaskRobotPosition/PosInfo[@PR_No.=" + PR_No + "]"); //搜尋PR_No.符合條件的節點
                if (node != null)
                {
                    XmlElement PosInfo = (XmlElement)node;
                    PosInfo.SetAttribute("MotionType", MotionType);
                    PosInfo.SetAttribute("X", X);
                    PosInfo.SetAttribute("Y", Y);
                    PosInfo.SetAttribute("Z", Z);
                    PosInfo.SetAttribute("W", W);
                    PosInfo.SetAttribute("P", P);
                    PosInfo.SetAttribute("R", R);
                    PosInfo.SetAttribute("E1", E1);
                    doc.Save(xmlPath);
                }
                else
                    throw new Exception("PR[" + PR_No + "] is not exist !!");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
