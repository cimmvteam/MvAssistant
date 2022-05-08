using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2
{
    public interface IMvaContextFlow
    {
        /// <summary>
        /// 初始化資源/參數
        /// </summary>
        /// <returns></returns>
        int MvaCfBookup();
        /// <summary>
        /// 載入資源
        /// </summary>
        /// <returns></returns>
        int MvaCfLoad();
        /// <summary>
        /// 御載資源
        /// </summary>
        /// <returns></returns>
        int MvaCfUnload();
        /// <summary>
        /// 釋放資源
        /// </summary>
        /// <returns></returns>
        int MvaCfFree();

    }
}
