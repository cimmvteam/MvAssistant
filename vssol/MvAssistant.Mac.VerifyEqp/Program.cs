//#define OnlyPositionGetter
//#define DrawerTest
using MaskCleanerVerify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MvAssistantMacVerifyEqp
{
    static class Program
    {

        public static ProgramMgr ProgMgr;
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            ProgMgr = new ProgramMgr();

#if OnlyPositionGetter
            Application.Run(new FmRobotPath());
#elif DrawerTest
            Application.Run(new FrmDrawerTest());
#else
            Application.Run(new FmMain());
#endif      
        }
    }
}
