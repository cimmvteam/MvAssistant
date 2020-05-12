using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Hdl
{
    /// <summary>
    /// HDL: Hardware Description Language
    /// HdlMgr: HDL Manager
    /// 
    /// [目的] HDL是用來檢查vendor or 3-party 提供的driver DLLs or others reference files (like .ini or .config 等)的正確性與完整性
    ///       正確性: 確認檔案內容未被竄改 or 置換, checked by MD5 CheckSum
    ///       完整性: 確認所有的references都存在於被執行的作業環境中
    /// [File] HDL定義在Manifest.xml檔案裡, AssemblyDriver的References
    /// </summary>
    public class HdlMgr
    {
        public HdlMgr() { }
        ~HdlMgr() { }

        public bool GenerateCheckSum()
        {
            return true;
        }

        public bool Validate()
        {
            return true;
        }

        public bool ValidateCompleteness()
        {
            return true;
        }


        public bool ValidateCorrectness()
        {
            return true;
        }
    }
}
