using System;
using MaskAutoCleaner.v1_0.Machine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant;

namespace MaskAutoCleaner.v1_0.TestMy.Flow
{
    [TestClass]
    public class UtFlowOcap
    {
        [TestMethod]
        public void TestMethod1()
        {
            TestMyUtil.RegisterLog();

            using (var machineMgr = new MacMachineMgr())
            {
                try
                {
                    machineMgr.MvCfInit();
                    machineMgr.MvCfLoad();



                    machineMgr.RecipeMgr.LoaddRecipe("../../UserData/Recipe/RecipeFlow_Ocap.xml");
                    machineMgr.SimulateFakeNormalAsyn();


                    machineMgr.RecipeMgr.Execute();

                    machineMgr.MvCfUnload();
                    machineMgr.MvCfFree();
                }
                catch (Exception ex) { MvLog.WarnNs(this, ex); }
            }



        }
    }
}
