using System;
using System.IO;
using MaskAutoCleaner.v1_0.Machine.LoadPort;
using MaskAutoCleaner.v1_0.Machine.MaskTransfer;
using MaskAutoCleaner.v1_0.Recipe;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MaskAutoCleaner.v1_0.TestMy.GenCfg
{
    [TestClass]
    public class UtGenRecipe
    {
        [TestMethod]
        public void GenRecipeOcap()
        {

            var recipe = new MacRecipe();

            var step = recipe.AddStep("MoveToLoadPort");
            step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.LPHome);
            step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacMsLoadPortState.IdleForGetMask);

            step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.LoadPortAClamping);
            step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacMsLoadPortState.IdleForGetMask);


            var fn = "../../UserData/RecipeOcap.xml";
            var fi = new FileInfo(fn);
            if (!fi.Directory.Exists) fi.Directory.Create();
            recipe.SaveToXmlFile(fi.FullName);




        }


        [TestMethod]
        public void GenRecipeBankIn()
        {


        }


        [TestMethod]
        public void GenRecipeBankOut()
        {


        }

    }
}
