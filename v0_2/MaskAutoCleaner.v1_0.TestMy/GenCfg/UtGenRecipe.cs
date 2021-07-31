using System;
using System.IO;
using MaskAutoCleaner.v1_0.Machine.BoxTransfer;
using MaskAutoCleaner.v1_0.Machine.CabinetDrawer;
using MaskAutoCleaner.v1_0.Machine.CleanCh;
using MaskAutoCleaner.v1_0.Machine.InspectionCh;
using MaskAutoCleaner.v1_0.Machine.LoadPort;
using MaskAutoCleaner.v1_0.Machine.MaskTransfer;
using MaskAutoCleaner.v1_0.Machine.OpenStage;
using MaskAutoCleaner.v1_0.Recipe;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_2.Mac;
using MvAssistant.v0_2.Mac.JSon.RobotTransferFile;
using MvAssistant.v0_2.Mac.Manifest;

namespace MaskAutoCleaner.v1_0.TestMy.GenCfg
{
    [TestClass]
    public class UtGenRecipe
    {
        [TestMethod]
        public void GenRecipeOcap()
        {
            var recipe = new MacRecipe();

            #region MT LPA
            {
                var step = recipe.AddStep("Load Port A Get POD");
                step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.Idle);

                step.AddCmd(EnumMachineID.MID_LP_A_ASB, EnumMacLoadportCmd.ToGetPODWithMask);

                step.AddAfterState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForGetPODWithMask);
            }

            {
                var step = recipe.AddStep("Load Port A Dock");
                step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForGetPODWithMask);

                step.AddCmd(EnumMachineID.MID_LP_A_ASB, EnumMacLoadportCmd.DockWithMask);

                step.AddAfterState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForReleaseMask);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Load Port A Catch Mask Return To LPHomeClamped");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHome);
                step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForReleaseMask);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.LPHomeToLPAGetMaskReturnToLPHomeClamped);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHomeClamped);
                step.AddAfterState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForReleaseMask);
            }
            #endregion MT LPA
            #region MT IC
            {
                var step = recipe.AddStep("Mask Transfer Change Direction To ICHomeClamped");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHomeClamped);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.LPHomeClampedToICHomeClamped);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHomeClamped);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Inspection Chamber Release Mask(Glass Side Up) Return To ICHome");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHomeClamped);
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.ICHomeClampedToICGlassReleaseReturnToICHome);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHome);
                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.Idle);
            }

            {
                var step = recipe.AddStep("Inspect Mask(Glass Side Up) In Inspection Chamber");
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.Idle);

                step.AddCmd(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChCmd.InspectGlass);

                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.WaitingForReleaseGlass);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Inspection Chamber Catch Mask(Glass Side Up) Return To ICHomeClamped");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHome);
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.WaitingForReleaseGlass);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.ICHomeToICGlassGetReturnToICClamped);
                step.AddCmd(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChCmd.ReturnToIdleAfterReleaseGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHomeClamped);
                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Inspection Chamber Release Mask(Pellicle Side Up) Return To ICHome");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHomeClamped);
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.ICHomeClampedToICPellicleReleaseReturnToICHome);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHome);
                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.Idle);
            }

            {
                var step = recipe.AddStep("Inspect Mask(Pellicle Side Up) In Inspection Chamber");
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.Idle);

                step.AddCmd(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChCmd.InspectPellicle);

                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.WaitingForReleasePellicle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Inspection Chamber Catch Mask(Pellicle Side Up) Return To ICHomeClamped");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHome);
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.WaitingForReleasePellicle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.ICHomeToICPellicleGetReturnToICClamped);
                step.AddCmd(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChCmd.ReturnToIdleAfterReleasePellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHomeClamped);
                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Change Mask State Afer Inspect");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHomeClamped);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.ICHomeClampedToICHomeInspected);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHomeInspected);
            }
            #endregion MT IC
            #region MT CC
            {
                var step = recipe.AddStep("Mask Transfer Change Direction To CCHomeClamped");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHomeInspected);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.ICHomeInspectedToCCHomeClamped);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CCHomeClamped);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Origin In Clean Chamber(Glass Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CCHomeClamped);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.CCHomeClampedToCCGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Over Air Gun(Glass Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.InCCGlassMoveToClean);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.GlassOnAirGun);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Cleanning Glass On The Air Gun");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.GlassOnAirGun);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.CleanGlass);
                step.AddCmd(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChCmd.CleanGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CleanedGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.CleanedGlass);
            }

            {
                var step = recipe.AddStep("Clean Chamber Cleaned Glass, Mask Transfer Move To Origin In Clean Chamber(Glass Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CleanedGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.CleanedGlass);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.CCGlassCleanedReturnInCCGlass);
                step.AddCmd(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChCmd.FinishCleanGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Over Camera(Glass Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.InCCGlassMoveToInspect);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.GlassOnCamera);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Inspecting Glass On The Camera");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.GlassOnCamera);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.InspectGlass);
                step.AddCmd(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChCmd.InspectGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.InspectedGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.InspectedGlass);
            }

            {
                var step = recipe.AddStep("Clean Chamber Inspected Glass, Mask Transfer Move To Origin In Clean Chamber(Glass Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.InspectedGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.InspectedGlass);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.CCGlassInspectedReturnInCCGlass);
                step.AddCmd(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChCmd.FinishInspectGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To CCHomeClamped After Clean Glass");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.InCCGlassToCCHomeClamped);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CCHomeClamped);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }



            {
                var step = recipe.AddStep("Mask Transfer Move To Origin In Clean Chamber(Pellicle Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CCHomeClamped);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.CCHomeClampedToCCPellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Over Air Gun(Pellicle Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.InCCPellicleMoveToClean);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.PellicleOnAirGun);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Cleanning Pellicle On The Air Gun");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.PellicleOnAirGun);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.CleanPellicle);
                step.AddCmd(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChCmd.CleanPellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CleanedPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.CleanedPellicle);
            }

            {
                var step = recipe.AddStep("Clean Chamber Cleaned Pellicle, Mask Transfer Move To Origin In Clean Chamber(Pellicle Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CleanedPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.CleanedPellicle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.CCPellicleCleanedReturnInCCPellicle);
                step.AddCmd(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChCmd.FinishCleanPellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Over Camera(Pellicle Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.InCCPellicleMoveToInspect);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.PellicleOnCamera);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Inspecting Pellicle On The Camera");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.PellicleOnCamera);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.InspectPellicle);
                step.AddCmd(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChCmd.InspectPellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.InspectedPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.InspectedPellicle);
            }

            {
                var step = recipe.AddStep("Clean Chamber Inspected Pellicle, Mask Transfer Move To Origin In Clean Chamber(Pellicle Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.InspectedPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.InspectedPellicle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.CCPellicleInspectedReturnInCCPellicle);
                step.AddCmd(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChCmd.FinishInspectPellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To CCHomeClamped After Clean Pellicle");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.InCCPellicleToCCHomeClamped);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CCHomeClamped);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Change Mask State Afer Clean");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CCHomeClamped);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.CCHomeClampedToCCHomeCleaned);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CCHomeCleaned);
            }
            #endregion MT CC
            #region MT LPA
            {
                var step = recipe.AddStep("Mask Transfer Change Direction To LPHomeCleaned");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CCHomeCleaned);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.CCHomeCleanedToLPHomeCleaned);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHomeCleaned);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Load Port A Release Mask Return To LPHome");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHomeCleaned);
                step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForReleaseMask);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.LPHomeCleanedToLPARelease);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHome);
                step.AddAfterState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForReleaseMask);
            }

            {
                var step = recipe.AddStep("Load Port A Undock With Mask");
                step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForReleaseMask);

                step.AddCmd(EnumMachineID.MID_LP_A_ASB, EnumMacLoadportCmd.UndockWithMaskFromIdleForRelesaseMask);

                step.AddAfterState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForReleasePODWithMask);
            }
            #endregion MT LPA

            var fn = "../../UserData/Recipe/RecipeFlow_Ocap.xml";
            var fi = new FileInfo(fn);
            if (!fi.Directory.Exists) fi.Directory.Create();
            recipe.SaveToXmlFile(fi.FullName);

        }


        [TestMethod]
        public void GenRecipeBankIn()
        {
            var recipe = new MacRecipe();

            #region MT LPA
            {
                var step = recipe.AddStep("Load Port A Get POD");
                step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.Idle);

                step.AddCmd(EnumMachineID.MID_LP_A_ASB, EnumMacLoadportCmd.ToGetPODWithMask);

                step.AddAfterState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForGetPODWithMask);
            }

            {
                var step = recipe.AddStep("Load Port A Dock");
                step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForGetPODWithMask);

                step.AddCmd(EnumMachineID.MID_LP_A_ASB, EnumMacLoadportCmd.DockWithMask);

                step.AddAfterState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForReleaseMask);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Load Port A Catch Mask Return To LPHomeClamped");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHome);
                step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForReleaseMask);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.LPHomeToLPAGetMaskReturnToLPHomeClamped);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHomeClamped);
                step.AddAfterState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForReleaseMask);
            }
            #endregion MT LPA
            #region OS BT
            {
                var step = recipe.AddStep("Open Stage Change State To Receive Box");
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.Idle);

                step.AddCmd(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageCmd.InputBox);

                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForInputBox);
            }

            {
                var step = recipe.AddStep("Open Stage Calibration Box");
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForInputBox);

                step.AddCmd(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageCmd.CalibrationClosedBox);

                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForUnlockWithMask);
            }

            {
                var step = recipe.AddStep("Box Transfer Move To Unlock Box");
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForUnlockWithMask);
                step.AddBeforeState(EnumMachineID.MID_BT_A_ASB, EnumMacBoxTransferState.CB1Home);

                step.AddCmd(EnumMachineID.MID_BT_A_ASB, EnumMacMcBoxTransferCmd.MoveToUnlock);

                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForUnlockWithMask);
                step.AddAfterState(EnumMachineID.MID_BT_A_ASB, EnumMacBoxTransferState.CB1Home);
            }

            {
                var step = recipe.AddStep("Open Stage Change State To Receive Mask");
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForUnlockWithMask);

                step.AddCmd(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageCmd.OpenBox);

                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForInputMask);
            }
            #endregion OS BT
            #region MT OS
            {
                var step = recipe.AddStep("Mask Transfer Move To Open Stage Release Mask Return To LPHome");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHomeClamped);
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForInputMask);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.LPHomeClampedToOSReleaseMaskReturnToLPHome);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHome);
                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForInputMask);
            }
            #endregion MT OS
            #region OS BT
            {
                var step = recipe.AddStep("Open Stage Close Box With Mask");
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForInputMask);

                step.AddCmd(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageCmd.CloseBoxWithMask);

                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForLockWithMask);
            }

            {
                var step = recipe.AddStep("Box Transfer Move To Lock Box With Mask");
                step.AddBeforeState(EnumMachineID.MID_BT_A_ASB, EnumMacBoxTransferState.CB1Home);
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForLockWithMask);

                step.AddCmd(EnumMachineID.MID_BT_A_ASB, EnumMacMcBoxTransferCmd.MoveToLock);

                step.AddAfterState(EnumMachineID.MID_BT_A_ASB, EnumMacBoxTransferState.CB1Home);
                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForLockWithMask);
            }

            {
                var step = recipe.AddStep("Open Stage Release Box With Mask");
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForLockWithMask);

                step.AddCmd(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageCmd.ReleaseBoxWithMask);

                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForReleaseBoxWithMask);
            }

            {
                var step = recipe.AddStep("Box Transfer Move To Open Stage Catch Box With Mask");
                step.AddBeforeState(EnumMachineID.MID_BT_A_ASB, EnumMacBoxTransferState.CB1Home);
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForReleaseBoxWithMask);

                step.AddCmd(EnumMachineID.MID_BT_A_ASB, EnumMacMcBoxTransferCmd.MoveToOpenStageGet);
                step.AddCmd(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageCmd.ReturnToIdleAfterReleaseBoxWithMask);

                step.AddAfterState(EnumMachineID.MID_BT_A_ASB, EnumMacBoxTransferState.CB1HomeClamped);
                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.Idle);
            }
            #endregion OS BT
            #region BT DW_01_01
            {
                var step = recipe.AddStep("Drawer_01_01 Move Tray To In");
                step.AddBeforeState(EnumMachineID.MID_BT_A_ASB, EnumMacBoxTransferState.CB1HomeClamped);
                step.AddBeforeState(EnumMachineID.MID_DRAWER_01_01, EnumMacCabinetDrawerState.UnloadMoveTrayToHomeComplete);

                step.AddCmd(EnumMachineID.MID_DRAWER_01_01, EnumMacMcCabinetDrawerCmd.Unload_MoveTrayToIn);

                step.AddAfterState(EnumMachineID.MID_BT_A_ASB, EnumMacBoxTransferState.CB1HomeClamped);
                step.AddAfterState(EnumMachineID.MID_DRAWER_01_01, EnumMacCabinetDrawerState.UnloadMoveTrayToInComplete);
            }

            {
                var step = recipe.AddStep("Box Transfer Move To Drawer_01_01 Release Box With Mask");
                step.AddBeforeState(EnumMachineID.MID_BT_A_ASB, EnumMacBoxTransferState.CB1HomeClamped);
                step.AddBeforeState(EnumMachineID.MID_DRAWER_01_01, EnumMacCabinetDrawerState.UnloadMoveTrayToHomeComplete);

                step.AddCmd(EnumMachineID.MID_DRAWER_01_01, EnumMacBoxTransferTransition.MoveToCB0101ForRelease);

                step.AddAfterState(EnumMachineID.MID_BT_A_ASB, EnumMacBoxTransferState.CB1Home);
                step.AddAfterState(EnumMachineID.MID_DRAWER_01_01, EnumMacCabinetDrawerState.UnloadMoveTrayToInComplete);
            }

            {
                var step = recipe.AddStep("Drawer_01_01 Move Tray To Home");
                step.AddBeforeState(EnumMachineID.MID_DRAWER_01_01, EnumMacCabinetDrawerState.UnloadMoveTrayToInComplete);

                step.AddCmd(EnumMachineID.MID_DRAWER_01_01, EnumMacCabinetDrawerTransition.UnloadMoveTrayToHomeIng_UnloadMoveTrayToHomeComplete);

                step.AddAfterState(EnumMachineID.MID_DRAWER_01_01, EnumMacCabinetDrawerState.UnloadMoveTrayToHomeComplete);
            }
            #endregion BT DW_01_01

            var fn = "../../UserData/Recipe/RecipeFlow_BankIn.xml";
            var fi = new FileInfo(fn);
            if (!fi.Directory.Exists) fi.Directory.Create();
            recipe.SaveToXmlFile(fi.FullName);
        }


        [TestMethod]
        [DataRow(EnumMacMaskBoxType.CrystalBox, EnumMachineID.MID_DRAWER_02_04)]
        public void GenRecipeBankOut(EnumMacMaskBoxType boxType, EnumMachineID drawerEnumMachineID)
        {
            var recipe = new MacRecipe();

            #region King Add :)
            var macEnumDeviceDrawerConvert = drawerEnumMachineID.ToMacEnumDeviceForDrawer();
            var boxrobotTransferLocationConvert = drawerEnumMachineID.ToBoxrobotTransferLocationForDrawer();
            var enumDeviceDrawer = macEnumDeviceDrawerConvert.Item2;
            var boxrobotTransferLocation = boxrobotTransferLocationConvert.Item2;


            #region Operator => Drawer
            {
                var step = recipe.AddStep("Move DrawerTray to Out to put a  mask box on Drawer Tray"); // Operator 將Tray 移到 Out 準放入盒子
                step.AddBeforeState(drawerEnumMachineID, EnumMacCabinetDrawerState.WaitingLoadInstruction);

                step.AddCmd(drawerEnumMachineID, EnumMacMcCabinetDrawerCmd.Load_MoveTrayToOut);

                step.AddAfterState(drawerEnumMachineID, EnumMacCabinetDrawerState.LoadWaitingPutBoxOnTray);
            }

            {
                var step = recipe.AddStep("Move DrawerTray to In and waiting  Boxtransfer to Clamp box"); //Operator 將 Box 放好後, 將Tray 移到 Home, 並檢查有没有盒子
                step.AddBeforeState(drawerEnumMachineID, EnumMacCabinetDrawerState.LoadWaitingPutBoxOnTray);

                step.AddCmd(drawerEnumMachineID, EnumMacMcCabinetDrawerCmd.Load_MoveTrayToIn);

                step.AddAfterState(drawerEnumMachineID, EnumMacCabinetDrawerState.LoadWaitingMoveTrayToIn);
            }
            #endregion
            #region Drawer => Box Transfer
            {

            }
            #endregion
            #region Box Transfer => Open Stage
            {

            }
            #endregion
            #endregion 

            #region BT DW_01_01
            {
                var step = recipe.AddStep("Box Transfer Move To Drawer_01_01 Get Box");
                step.AddBeforeState(EnumMachineID.MID_BT_A_ASB, EnumMacBoxTransferState.CB1Home);

                step.AddCmd(EnumMachineID.MID_BT_A_ASB, EnumMacMcBoxTransferCmd.MoveToCabinetGet);

                step.AddAfterState(EnumMachineID.MID_BT_A_ASB, EnumMacBoxTransferState.CB1HomeClamped);
            }
            #endregion BT DW_01_01
            #region OS BT
            {
                var step = recipe.AddStep("Open Stage Change State To Receive Box");
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.Idle);

                step.AddCmd(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageCmd.InputBoxWithMask);

                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForInputBoxWithMask);
            }

            {
                var step = recipe.AddStep("Box Transfer Move To Open Stage Release Box");
                step.AddBeforeState(EnumMachineID.MID_BT_A_ASB, EnumMacBoxTransferState.CB1HomeClamped);
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForInputBoxWithMask);

                step.AddCmd(EnumMachineID.MID_BT_A_ASB, EnumMacMcBoxTransferCmd.MoveToOpenStagePut);

                step.AddAfterState(EnumMachineID.MID_BT_A_ASB, EnumMacBoxTransferState.CB1Home);
                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForInputBoxWithMask);
            }

            {
                var step = recipe.AddStep("Open Stage Calibration Box");
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForInputBoxWithMask);

                step.AddCmd(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageCmd.CalibrationClosedBoxWithMask);

                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForUnlockWithMask);
            }

            {
                var step = recipe.AddStep("Box Transfer Move To Unlock Box");
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForUnlockWithMask);
                step.AddBeforeState(EnumMachineID.MID_BT_A_ASB, EnumMacBoxTransferState.CB1Home);

                step.AddCmd(EnumMachineID.MID_BT_A_ASB, EnumMacMcBoxTransferCmd.MoveToUnlock);

                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForUnlockWithMask);
                step.AddAfterState(EnumMachineID.MID_BT_A_ASB, EnumMacBoxTransferState.CB1Home);
            }

            {
                var step = recipe.AddStep("Open Stage Change State To Receive Mask");
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForUnlockWithMask);

                step.AddCmd(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageCmd.OpenBoxWithMask);

                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForReleaseMask);
            }
            #endregion OS BT
            #region MT OS
            {
                var step = recipe.AddStep("Mask Transfer Move To Open Stage Catch Mask");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHome);
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForReleaseMask);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.LPHomeToOSGetMaskReturnToLPHomeClamped);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHomeClamped);
                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForReleaseMask);
            }
            #endregion MT OS
            #region MT IC
            {
                var step = recipe.AddStep("Mask Transfer Change Direction To ICHomeClamped");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHomeClamped);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.LPHomeClampedToICHomeClamped);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHomeClamped);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Inspection Chamber Release Mask(Glass Side Up) Return To ICHome");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHomeClamped);
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.ICHomeClampedToICGlassReleaseReturnToICHome);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHome);
                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.Idle);
            }

            {
                var step = recipe.AddStep("Inspect Mask(Glass Side Up) In Inspection Chamber");
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.Idle);

                step.AddCmd(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChCmd.InspectGlass);

                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.WaitingForReleaseGlass);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Inspection Chamber Catch Mask(Glass Side Up) Return To ICHomeClamped");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHome);
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.WaitingForReleaseGlass);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.ICHomeToICGlassGetReturnToICClamped);
                step.AddCmd(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChCmd.ReturnToIdleAfterReleaseGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHomeClamped);
                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Inspection Chamber Release Mask(Pellicle Side Up) Return To ICHome");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHomeClamped);
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.ICHomeClampedToICPellicleReleaseReturnToICHome);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHome);
                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.Idle);
            }

            {
                var step = recipe.AddStep("Inspect Mask(Pellicle Side Up) In Inspection Chamber");
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.Idle);

                step.AddCmd(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChCmd.InspectPellicle);

                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.WaitingForReleasePellicle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Inspection Chamber Catch Mask(Pellicle Side Up) Return To ICHomeClamped");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHome);
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.WaitingForReleasePellicle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.ICHomeToICPellicleGetReturnToICClamped);
                step.AddCmd(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChCmd.ReturnToIdleAfterReleasePellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHomeClamped);
                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Change Mask State Afer Inspect");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHomeClamped);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.ICHomeClampedToICHomeInspected);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHomeInspected);
            }
            #endregion MT IC
            #region MT CC
            {
                var step = recipe.AddStep("Mask Transfer Change Direction To CCHomeClamped");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHomeInspected);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.ICHomeInspectedToCCHomeClamped);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CCHomeClamped);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Origin In Clean Chamber(Glass Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CCHomeClamped);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.CCHomeClampedToCCGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Over Air Gun(Glass Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.InCCGlassMoveToClean);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.GlassOnAirGun);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Cleanning Glass On The Air Gun");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.GlassOnAirGun);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.CleanGlass);
                step.AddCmd(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChCmd.CleanGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CleanedGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.CleanedGlass);
            }

            {
                var step = recipe.AddStep("Clean Chamber Cleaned Glass, Mask Transfer Move To Origin In Clean Chamber(Glass Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CleanedGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.CleanedGlass);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.CCGlassCleanedReturnInCCGlass);
                step.AddCmd(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChCmd.FinishCleanGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Over Camera(Glass Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.InCCGlassMoveToInspect);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.GlassOnCamera);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Inspecting Glass On The Camera");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.GlassOnCamera);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.InspectGlass);
                step.AddCmd(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChCmd.InspectGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.InspectedGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.InspectedGlass);
            }

            {
                var step = recipe.AddStep("Clean Chamber Inspected Glass, Mask Transfer Move To Origin In Clean Chamber(Glass Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.InspectedGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.InspectedGlass);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.CCGlassInspectedReturnInCCGlass);
                step.AddCmd(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChCmd.FinishInspectGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To CCHomeClamped After Clean Glass");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.InCCGlassToCCHomeClamped);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CCHomeClamped);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }



            {
                var step = recipe.AddStep("Mask Transfer Move To Origin In Clean Chamber(Pellicle Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CCHomeClamped);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.CCHomeClampedToCCPellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Over Air Gun(Pellicle Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.InCCPellicleMoveToClean);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.PellicleOnAirGun);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Cleanning Pellicle On The Air Gun");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.PellicleOnAirGun);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.CleanPellicle);
                step.AddCmd(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChCmd.CleanPellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CleanedPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.CleanedPellicle);
            }

            {
                var step = recipe.AddStep("Clean Chamber Cleaned Pellicle, Mask Transfer Move To Origin In Clean Chamber(Pellicle Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CleanedPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.CleanedPellicle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.CCPellicleCleanedReturnInCCPellicle);
                step.AddCmd(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChCmd.FinishCleanPellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Over Camera(Pellicle Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.InCCPellicleMoveToInspect);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.PellicleOnCamera);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Inspecting Pellicle On The Camera");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.PellicleOnCamera);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.InspectPellicle);
                step.AddCmd(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChCmd.InspectPellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.InspectedPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.InspectedPellicle);
            }

            {
                var step = recipe.AddStep("Clean Chamber Inspected Pellicle, Mask Transfer Move To Origin In Clean Chamber(Pellicle Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.InspectedPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.InspectedPellicle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.CCPellicleInspectedReturnInCCPellicle);
                step.AddCmd(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChCmd.FinishInspectPellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To CCHomeClamped After Clean Pellicle");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.InCCPellicleToCCHomeClamped);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CCHomeClamped);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Change Mask State Afer Clean");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CCHomeClamped);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.CCHomeClampedToCCHomeCleaned);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CCHomeCleaned);
            }
            #endregion MT CC
            #region MT LPA
            {
                var step = recipe.AddStep("Mask Transfer Change Direction To LPHomeCleaned");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CCHomeCleaned);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.CCHomeCleanedToLPHomeCleaned);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHomeCleaned);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Load Port A Release Mask Return To LPHome");
                step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.Idle);

                step.AddCmd(EnumMachineID.MID_LP_A_ASB, EnumMacLoadportCmd.Dock);
                
                step.AddAfterState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForGetMask);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Load Port A Release Mask Return To LPHome");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHomeCleaned);
                step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForGetMask);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferCmd.LPHomeCleanedToLPARelease);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHome);
                step.AddAfterState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForGetMask);
            }

            {
                var step = recipe.AddStep("Load Port A Undock With Mask");
                step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForGetMask);

                step.AddCmd(EnumMachineID.MID_LP_A_ASB, EnumMacLoadportCmd.UndockWithMaskFromIdleForGetMask);

                step.AddAfterState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForReleasePODWithMask);
            }
            #endregion MT LPA

            var fn = "../../UserData/Recipe/RecipeFlow_BankOut.xml";
            var fi = new FileInfo(fn);
            if (!fi.Directory.Exists) fi.Directory.Create();
            recipe.SaveToXmlFile(fi.FullName);
        }

    }
}
