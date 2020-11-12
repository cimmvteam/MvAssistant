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
                var step = recipe.AddStep("Load Port A Dock");
                step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForGetPOD);

                step.AddCmd(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortTransition.DockStart_DockIng);

                step.AddAfterState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForGetMask);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Load Port A Catch Mask");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHome);
                step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForGetMask);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToMoveToLoadPortA);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHomeClamped);
                step.AddAfterState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForGetMask);
            }

            {
                var step = recipe.AddStep("Load Port A Undock");
                step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForGetMask);

                step.AddCmd(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortTransition.UndockStart_UndockIng);

                step.AddAfterState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForGetPOD);
            }
            #endregion MT LPA
            #region MT OS
            {
                var step = recipe.AddStep("Mask Transfer Move To Open Stage Release Mask");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHomeClamped);
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForInputMask);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToMoveToOpenStageForRelease);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHome);
                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForInputMask);
            }
            #endregion MT OS
            #region OS BT
            {
                var step = recipe.AddStep("Open Stage Close Box With Mask");
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForInputMask);

                step.AddCmd(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageTransition.TriggerToCloseBoxWithMask);

                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForLockWithMask);
            }

            {
                var step = recipe.AddStep("Box Transfer Move To Open Stage Lock Box With Mask");
                step.AddBeforeState(EnumMachineID.MID_BT_A_ASB, EnumMacBoxTransferState.CB1Home);
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForLockWithMask);

                step.AddCmd(EnumMachineID.MID_BT_A_ASB, EnumMacBoxTransferTransition.MoveToLock);

                step.AddAfterState(EnumMachineID.MID_BT_A_ASB, EnumMacBoxTransferState.CB1Home);
                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForLockWithMask);
            }

            {
                var step = recipe.AddStep("Open Stage Release Box With Mask");
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForLockWithMask);

                step.AddCmd(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageTransition.TriggerToCloseBoxWithMask);

                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForReleaseBoxWithMask);
            }

            {
                var step = recipe.AddStep("Box Transfer Move To Open Stage Catch Box With Mask");
                step.AddBeforeState(EnumMachineID.MID_BT_A_ASB, EnumMacBoxTransferState.CB1Home);
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForReleaseBoxWithMask);

                step.AddCmd(EnumMachineID.MID_BT_A_ASB, EnumMacBoxTransferTransition.MoveToOpenStage);

                step.AddAfterState(EnumMachineID.MID_BT_A_ASB, EnumMacBoxTransferState.CB1HomeClamped);
                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForReleaseBoxWithMask);
            }
            #endregion OS BT
            #region BT DW_01_01
            {
                var step = recipe.AddStep("Drawer_01_01 Move Tray To In");
                step.AddBeforeState(EnumMachineID.MID_BT_A_ASB, EnumMacBoxTransferState.CB1HomeClamped);
                step.AddBeforeState(EnumMachineID.MID_DRAWER_01_01, EnumMacCabinetDrawerState.UnloadMoveTrayToHomeComplete);

                step.AddCmd(EnumMachineID.MID_DRAWER_01_01, EnumMacCabinetDrawerTransition.UnloadMoveTrayToInIng_UnloadMoveTrayToInComplete);

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
        public void GenRecipeBankOut()
        {
            var recipe = new MacRecipe();

            #region MT OS
            {
                var step = recipe.AddStep("Mask Transfer Move To Open Stage Catch Mask");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHome);
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForReleaseMask);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToMoveToOpenStage);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHomeClamped);
                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacOpenStageState.WaitingForReleaseMask);
            }
            #endregion MT OS
            #region MT IC
            {
                var step = recipe.AddStep("Mask Transfer Change Direction To ICHomeClamped");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHomeClamped);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToChangeDirectionToICHomeClampedFromLPHomeClamped);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHomeClamped);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Inspection Chamber Release Mask(Glass Side Up)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHomeClamped);
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToMoveToInspectionChGlassForRelease);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHome);
                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.Idle);
            }

            {
                var step = recipe.AddStep("Inspect Mask(Glass Side Up) In Inspection Chamber");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHome);
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacInspectionChTransition.TriggerToInspectGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHome);
                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.WaitingForReleaseGlass);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Inspection Chamber Catch Mask(Glass Side Up)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHome);
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.WaitingForReleaseGlass);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToMoveToInspectionChGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHomeClamped);
                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.WaitingForReleaseGlass);
            }

            {
                var step = recipe.AddStep("Inspection Chamber Change State To Idle");
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.WaitingForReleaseGlass);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacInspectionChTransition.TriggerToIdleAfterReleaseGlass);

                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Inspection Chamber Release Mask(Pellicle Side Up)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHomeClamped);
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.WaitingForReleasePellicle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToMoveToInspectionChPellicleForRelease);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHome);
                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.WaitingForReleasePellicle);
            }

            {
                var step = recipe.AddStep("Inspect Mask(Pellicle Side Up) In Inspection Chamber");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHome);
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacInspectionChTransition.TriggerToInspectPellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHome);
                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.WaitingForReleasePellicle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Inspection Chamber Catch Mask(Pellicle Side Up)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHome);
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.WaitingForReleasePellicle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToMoveToInspectionChPellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHomeClamped);
                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.WaitingForReleasePellicle);
            }

            {
                var step = recipe.AddStep("Inspection Chamber Change State To Idle");
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.WaitingForReleasePellicle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacInspectionChTransition.TriggerToIdleAfterReleasePellicle);

                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacInspectionChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Change Mask State Afer Inspect");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHomeClamped);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToChangeMaskStateToInspected);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHomeInspected);
            }
            #endregion MT IC
            #region MT CC
            {
                var step = recipe.AddStep("Mask Transfer Change Direction To CCHomeClamped");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ICHomeInspected);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToChangeDirectionToCCHomeClampedFromICHomeInspected);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CCHomeClamped);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Into Clean Chamber(Glass Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CCHomeClamped);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToMoveToCleanChGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Into Clean Chamber(Glass Side Down) Clean Glass");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToMoveToCleanGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.MovingToCleanGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Cleanning Glass On The Air Gun");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.MovingToCleanGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToCleanGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CleaningGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Clean Chamber Turn On Air Gun To Clean Glass");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CleaningGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacCleanChTransition.TriggerToCleanGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CleaningGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.CleaningGlass);
            }

            {
                var step = recipe.AddStep("Clean Chamber Turn Off Air Gun After Cleaned Glass");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CleaningGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.CleaningGlass);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacCleanChTransition.TriggerToReturnToIdleAfterCleanGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CleaningGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Into Clean Chamber(Glass Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CleaningGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToMoveToOriginAfterCleanedGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Into Clean Chamber(Glass Side Down) Inspect Glass");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToMoveToInspectGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.MovingToInspectGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Inspecting Glass On The Camera");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.MovingToInspectGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToInspectGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.InspectingGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Clean Chamber Catch Image To Inspect Glass");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.InspectingGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacCleanChTransition.TriggerToInspectGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.InspectingGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.InspectingGlass);
            }

            {
                var step = recipe.AddStep("Clean Chamber Return To Idle After Inspected Glass");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.InspectingGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.InspectingGlass);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacCleanChTransition.TriggerToReturnToIdleAfterInspectGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.InspectingGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Into Clean Chamber(Glass Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.InspectingGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToMoveToOriginAfterInspectedGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To CCHomeClamped After Clean Glass");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToMoveToCCHomeClampedFromCleanChGlass);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CCHomeClamped);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }


            {
                var step = recipe.AddStep("Mask Transfer Move Into Clean Chamber(Pellicle Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CCHomeClamped);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToMoveToCleanChPellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Into Clean Chamber(Pellicle Side Down) Clean Pellicle");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToMoveToCleanPellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.MovingToCleanPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Cleanning Pellicle On The Air Gun");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.MovingToCleanPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToCleanPellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CleaningPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Clean Chamber Turn On Air Gun To Clean Pellicle");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CleaningPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacCleanChTransition.TriggerToCleanPellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CleaningPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.CleaningPellicle);
            }

            {
                var step = recipe.AddStep("Clean Chamber Turn Off Air Gun After Cleaned Pellicle");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CleaningPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.CleaningPellicle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacCleanChTransition.TriggerToReturnToIdleAfterCleanPellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CleaningPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Into Clean Chamber(Pellicle Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CleaningPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToMoveToOriginAfterCleanedPellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Into Clean Chamber(Pellicle Side Down) Inspect Pellicle");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToMoveToInspectPellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.MovingToInspectPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Inspecting Pellicle On The Camera");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.MovingToInspectPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToInspectPellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.InspectingPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Clean Chamber Catch Image To Inspect Pellicle");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.InspectingPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacCleanChTransition.TriggerToInspectPellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.InspectingPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.InspectingPellicle);
            }

            {
                var step = recipe.AddStep("Clean Chamber Return To Idle After Inspected Pellicle");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.InspectingPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.InspectingPellicle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacCleanChTransition.TriggerToReturnToIdleAfterInspectPellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.InspectingPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Into Clean Chamber(Pellicle Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.InspectingPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToMoveToOriginAfterInspectedPellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To CCHomeClamped After Clean Pellicle");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.ClampedInCleanChAtOriginPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToMoveToCCHomeClampedFromCleanChPellicle);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CCHomeClamped);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Change Mask State Afer Clean");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CCHomeClamped);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToChangeMaskStateToCleaned);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.CCHomeCleaned);
            }
            #endregion MT CC
            #region MT LPA
            {
                var step = recipe.AddStep("Mask Transfer Move To Load Port A Release Mask");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHomeCleaned);
                step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForGetMask);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferTransition.TriggerToMoveToLoadPortACleanedForRelease);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMaskTransferState.LPHome);
                step.AddAfterState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForGetMask);
            }

            {
                var step = recipe.AddStep("Load Port A Undock");
                step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForGetMask);

                step.AddCmd(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortTransition.UndockStart_UndockIng);

                step.AddAfterState(EnumMachineID.MID_LP_A_ASB, EnumMacLoadPortState.IdleForGetPOD);
            }
            #endregion MT LPA

            var fn = "../../UserData/Recipe/RecipeFlow_BankOut.xml";
            var fi = new FileInfo(fn);
            if (!fi.Directory.Exists) fi.Directory.Create();
            recipe.SaveToXmlFile(fi.FullName);
        }

    }
}
