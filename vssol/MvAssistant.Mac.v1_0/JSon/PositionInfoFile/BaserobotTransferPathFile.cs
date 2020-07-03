using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.JSon.PositionInfoFile
{
  public  abstract class BaserobotTransferPathFile
    {
        /// <summary>附檔名稱(須前綴 . 符號)</summary>
        public string ExetendedFileName { protected set; get; }
        /// <summary>路徑名稱(須後綴 \ 符號) </summary>
        public string FilePath { protected set; get; }

        /// <summary>預設建構式</summary>
        public BaserobotTransferPathFile()
        {
            ExetendedFileName = ".json";
        }
         
        /// <summary>建構式(符檔名預設為 json)</summary>
        /// <param name="path">檔案路徑(後綴 \ 符號)</param>
        public BaserobotTransferPathFile(string path):this()
        {
            FilePath = path;
        }
        /// <summary>建構式</summary>
        /// <param name="path">檔案路徑(後綴 \ 符號)</param>
        /// <param name="extendName">檔案附檔名 ((前綴 . 符號)</param>
        public BaserobotTransferPathFile(string path,string extendName) : this()
        {
            ExetendedFileName = extendName;
            FilePath = path;
        }
        /// <summary>取得檔案名稱</summary>
        /// <param name="mainFileName">主檔案名稱</param>
        /// <returns></returns>
        protected string GetFileName(string mainFileName)
        {
            var fileName = FilePath + mainFileName + ExetendedFileName;
            return fileName;
        }
        /// <summary>取得檔案名稱</summary>
        /// <param name="path"></param>
        /// <param name="mainFileName"></param>
        /// <param name="extendedFileName"></param>
        /// <returns></returns>
        protected string GetFileName(string path,string mainFileName, string extendedFileName)
        {
            var fileName = path + mainFileName + extendedFileName;
            return fileName;
        }
    }
}
