using System;
using MaskAutoCleaner.v1_0.Machine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MaskAutoCleaner.v1_0.TestMy.Flow
{
    [TestClass]
    public class UtFlowBankIn
    {
        [TestMethod]
        public void TestMethod1()
        {

            using (var machineMgr = new MacMachineMgr())
            {



                machineMgr.MvCfInit();
                machineMgr.MvCfLoad();



                machineMgr.RecipeMgr.LoaddRecipe("UserData/Recipe/RecipeFlow_BankIn.xml");
                machineMgr.RecipeMgr.Execute();



                machineMgr.MvCfUnload();
                machineMgr.MvCfFree();



            }




        }
    }
}
