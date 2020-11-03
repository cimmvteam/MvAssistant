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
                var step = recipe.AddStep("Load Port A Dock");
                step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacMsLoadPortState.IdleForGetPOD);
                step.AddCmd(EnumMachineID.MID_LP_A_ASB, EnumMacMsLoadPortTransition.DockStart_DockIng);
                step.AddAfterState(EnumMachineID.MID_LP_A_ASB, EnumMacMsLoadPortState.IdleForGetMask);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Load Port A Catch Mask");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.LPHome);
                step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacMsLoadPortState.IdleForGetMask);

                step.AddCmd(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferTransition.MoveToLoadPortA);

                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.LPHomeClamped);
                step.AddAfterState(EnumMachineID.MID_LP_A_ASB, EnumMacMsLoadPortState.IdleForGetMask);
            }
            #endregion MT LPA
            #region MT IC
            {
                var step = recipe.AddStep("Mask Transfer Change Direction To ICHomeClamped");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.LPHomeClamped);

                step.AddCmd( EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferTransition.ChangeDirectionToICHomeClampedFromLPHomeClamped);
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ICHomeClamped);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Inspection Chamber Release Mask(Glass Side Up)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ICHomeClamped);
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacMsInspectionChState.Idle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.MoveToInspectionChGlassForRelease.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ICHome);
                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacMsInspectionChState.Idle);
            }

            {
                var step = recipe.AddStep("Inspect Mask(Glass Side Up) In Inspection Chamber");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ICHome);
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacMsInspectionChState.Idle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsInspectionChTransition.ReceiveTriggerToInspectGlass.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ICHome);
                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacMsInspectionChState.WaitingForReleaseGlass);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Inspection Chamber Catch Mask(Glass Side Up)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ICHome);
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacMsInspectionChState.WaitingForReleaseGlass);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.MoveToInspectionChGlass.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ICHomeClamped);
                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacMsInspectionChState.WaitingForReleaseGlass);
            }

            {
                var step = recipe.AddStep("Inspection Chamber Change State To Idle");
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacMsInspectionChState.WaitingForReleaseGlass);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsInspectionChTransition.ReceiveTriggerToIdleAfterReleaseGlass.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacMsInspectionChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Inspection Chamber Release Mask(Pellicle Side Up)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ICHomeClamped);
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacMsInspectionChState.WaitingForReleasePellicle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.MoveToInspectionChForRelease.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ICHome);
                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacMsInspectionChState.WaitingForReleasePellicle);
            }

            {
                var step = recipe.AddStep("Inspect Mask(Pellicle Side Up) In Inspection Chamber");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ICHome);
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacMsInspectionChState.Idle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsInspectionChTransition.ReceiveTriggerToInspectPellicle.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ICHome);
                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacMsInspectionChState.WaitingForReleasePellicle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Inspection Chamber Catch Mask(Pellicle Side Up)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ICHome);
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacMsInspectionChState.WaitingForReleasePellicle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.MoveToInspectionCh.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ICHomeClamped);
                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacMsInspectionChState.WaitingForReleasePellicle);
            }

            {
                var step = recipe.AddStep("Inspection Chamber Change State To Idle");
                step.AddBeforeState(EnumMachineID.MID_IC_A_ASB, EnumMacMsInspectionChState.WaitingForReleasePellicle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsInspectionChTransition.ReceiveTriggerToIdleAfterReleasePellicle.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_IC_A_ASB, EnumMacMsInspectionChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Change Mask State Afer Inspect");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ICHomeClamped);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.InspectedAtICHomeClamped.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ICHomeInspected);
            }
            #endregion MT IC
            #region MT CC
            {
                var step = recipe.AddStep("Mask Transfer Change Direction To CCHomeClamped");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ICHomeInspected);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.ChangeDirectionToCCHomeClampedFromICHomeInspected.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.CCHomeClamped);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Into Clean Chamber(Glass Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.CCHomeClamped);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.MoveToCleanChGlass.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ClampedInCleanChTargetGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Into Clean Chamber(Glass Side Down) Clean Glass");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ClampedInCleanChTargetGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.MoveToCleanGlass.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.MovingInCleanChToCleanGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Cleanning Glass On The Air Gun");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.MovingInCleanChToCleanGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.CleanGlass.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.CleaningGlassInCleanCh);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Clean Chamber Turn On Air Gun To Clean Glass");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.CleaningGlassInCleanCh);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsCleanChTransition.ReceiveTriggerToCleanGlass.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.CleaningGlassInCleanCh);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.CleaningGlass);
            }

            {
                var step = recipe.AddStep("Clean Chamber Turn Off Air Gun After Cleaned Glass");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.CleaningGlassInCleanCh);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.CleaningGlass);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsCleanChTransition.ReceiveTriggerToReturnToIdleAfterCleanGlass.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.CleaningGlassInCleanCh);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Into Clean Chamber(Glass Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.CleaningGlassInCleanCh);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.MoveAfterCleanedGlass.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ClampedInCleanChTargetGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Into Clean Chamber(Glass Side Down) Inspect Glass");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ClampedInCleanChTargetGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.MoveToInspectGlass.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.MovingInCleanChToInspectGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Inspecting Glass On The Camera");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.MovingInCleanChToInspectGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.InspectGlass.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.InspectingGlassInCleanCh);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Clean Chamber Catch Image To Inspect Glass");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.InspectingGlassInCleanCh);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsCleanChTransition.ReceiveTriggerToInspectGlass.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.InspectingGlassInCleanCh);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.InspectingGlass);
            }

            {
                var step = recipe.AddStep("Clean Chamber Return To Idle After Inspected Glass");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.InspectingGlassInCleanCh);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.InspectingGlass);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsCleanChTransition.ReceiveTriggerToReturnToIdleAfterInspectGlass.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.InspectingGlassInCleanCh);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Into Clean Chamber(Glass Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.InspectingGlassInCleanCh);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.MoveAfterInspectedGlass.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ClampedInCleanChTargetGlass);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To CCHomeClamped After Clean Glass");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ClampedInCleanChTargetGlass);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.MoveToCCHomeClampedFromCleanChGlass.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.CCHomeClamped);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);
            }


            {
                var step = recipe.AddStep("Mask Transfer Move Into Clean Chamber(Pellicle Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.CCHomeClamped);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.MoveToCleanChPellicle.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ClampedInCleanChTargetPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Into Clean Chamber(Pellicle Side Down) Clean Pellicle");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ClampedInCleanChTargetPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.MoveToCleanPellicle.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.MovingInCleanChToCleanPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Cleanning Pellicle On The Air Gun");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.MovingInCleanChToCleanPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.CleanPellicle.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.CleaningPellicleInCleanCh);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Clean Chamber Turn On Air Gun To Clean Pellicle");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.CleaningPellicleInCleanCh);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsCleanChTransition.ReceiveTriggerToCleanPellicle.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.CleaningPellicleInCleanCh);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.CleaningPellicle);
            }

            {
                var step = recipe.AddStep("Clean Chamber Turn Off Air Gun After Cleaned Pellicle");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.CleaningPellicleInCleanCh);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.CleaningPellicle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsCleanChTransition.ReceiveTriggerToReturnToIdleAfterCleanPellicle.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.CleaningPellicleInCleanCh);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Into Clean Chamber(Pellicle Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.CleaningPellicleInCleanCh);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.MoveAfterCleanedPellicle.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ClampedInCleanChTargetPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Into Clean Chamber(Pellicle Side Down) Inspect Pellicle");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ClampedInCleanChTargetPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.MoveToInspectPellicle.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.MovingInCleanChToInspectPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Inspecting Pellicle On The Camera");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.MovingInCleanChToInspectPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.InspectPellicle.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.InspectingPellicleInCleanCh);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Clean Chamber Catch Image To Inspect Pellicle");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.InspectingPellicleInCleanCh);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsCleanChTransition.ReceiveTriggerToInspectPellicle.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.InspectingPellicleInCleanCh);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.InspectingPellicle);
            }

            {
                var step = recipe.AddStep("Clean Chamber Return To Idle After Inspected Pellicle");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.InspectingPellicleInCleanCh);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.InspectingPellicle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsCleanChTransition.ReceiveTriggerToReturnToIdleAfterInspectPellicle.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.InspectingPellicleInCleanCh);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move Into Clean Chamber(Pellicle Side Down)");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.InspectingPellicleInCleanCh);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.MoveAfterInspectedPellicle.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ClampedInCleanChTargetPellicle);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To CCHomeClamped After Clean Pellicle");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.ClampedInCleanChTargetPellicle);
                step.AddBeforeState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.MoveToCCHomeClampedFromCleanCh.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.CCHomeClamped);
                step.AddAfterState(EnumMachineID.MID_CC_A_ASB, EnumMacMsCleanChState.Idle);
            }

            {
                var step = recipe.AddStep("Mask Transfer Change Mask State Afer Clean");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.CCHomeClamped);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.CleanedAtCCHomeClamped.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.CCHomeCleaned);
            }
            #endregion MT CC
            #region MT LPA
            {
                var step = recipe.AddStep("Mask Transfer Move To Load Port A Release Mask");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.LPHomeCleaned);
                step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacMsLoadPortState.IdleForGetMask);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.MoveToLoadPortACleanedForRelease.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.LPHome);
                step.AddAfterState(EnumMachineID.MID_LP_A_ASB, EnumMacMsLoadPortState.IdleForGetMask);
            }

            {
                var step = recipe.AddStep("Load Port A Undock");
                step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacMsLoadPortState.IdleForGetMask);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_LP_A_ASB.ToString(),
                    Value = EnumMacMsLoadPortTransition.UndockStart_UndockIng.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_LP_A_ASB, EnumMacMsLoadPortState.IdleForGetPOD);
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
                step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacMsLoadPortState.IdleForGetPOD);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_LP_A_ASB.ToString(),
                    Value = EnumMacMsLoadPortTransition.DockStart_DockIng.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_LP_A_ASB, EnumMacMsLoadPortState.IdleForGetMask);
            }

            {
                var step = recipe.AddStep("Mask Transfer Move To Load Port A Catch Mask");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.LPHome);
                step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacMsLoadPortState.IdleForGetMask);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.MoveToLoadPortA.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.LPHomeClamped);
                step.AddAfterState(EnumMachineID.MID_LP_A_ASB, EnumMacMsLoadPortState.IdleForGetMask);
            }

            {
                var step = recipe.AddStep("Load Port A Undock");
                step.AddBeforeState(EnumMachineID.MID_LP_A_ASB, EnumMacMsLoadPortState.IdleForGetMask);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_LP_A_ASB.ToString(),
                    Value = EnumMacMsLoadPortTransition.UndockStart_UndockIng.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_LP_A_ASB, EnumMacMsLoadPortState.IdleForGetPOD);
            }
            #endregion MT LPA
            #region MT OS
            {
                var step = recipe.AddStep("Mask Transfer Move To Open Stage Release Mask");
                step.AddBeforeState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.LPHomeClamped);
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacMsOpenStageState.WaitingForInputMask);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_MT_A_ASB.ToString(),
                    Value = EnumMacMsMaskTransferTransition.MoveToOpenStageForRelease.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_MT_A_ASB, EnumMacMsMaskTransferState.LPHome);
                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacMsOpenStageState.WaitingForInputMask);
            }
            #endregion MT OS
            #region OS BT
            {
                var step = recipe.AddStep("Open Stage Close Box With Mask");
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacMsOpenStageState.WaitingForInputMask);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_OS_A_ASB.ToString(),
                    Value = EnumMacMsOpenStageTransition.ReceiveTriggerToCloseBoxWithMask.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacMsOpenStageState.WaitingForLockWithMask);
            }

            {
                var step = recipe.AddStep("Box Transfer Move To Open Stage Lock Box With Mask");
                step.AddBeforeState(EnumMachineID.MID_BT_A_ASB, EnumMacMsBoxTransferState.CB1Home);
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacMsOpenStageState.WaitingForLockWithMask);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_BT_A_ASB.ToString(),
                    Value = EnumMacMsBoxTransferTransition.MoveToLock.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_BT_A_ASB, EnumMacMsBoxTransferState.CB1Home);
                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacMsOpenStageState.WaitingForLockWithMask);
            }

            {
                var step = recipe.AddStep("Open Stage Release Box With Mask");
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacMsOpenStageState.WaitingForLockWithMask);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_OS_A_ASB.ToString(),
                    Value = EnumMacMsOpenStageTransition.ReceiveTriggerToCloseBoxWithMask.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacMsOpenStageState.WaitingForReleaseBoxWithMask);
            }

            {
                var step = recipe.AddStep("Box Transfer Move To Open Stage Catch Box With Mask");
                step.AddBeforeState(EnumMachineID.MID_BT_A_ASB, EnumMacMsBoxTransferState.CB1Home);
                step.AddBeforeState(EnumMachineID.MID_OS_A_ASB, EnumMacMsOpenStageState.WaitingForReleaseBoxWithMask);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_BT_A_ASB.ToString(),
                    Value = EnumMacMsBoxTransferTransition.MoveToOpenStage.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_BT_A_ASB, EnumMacMsBoxTransferState.CB1HomeClamped);
                step.AddAfterState(EnumMachineID.MID_OS_A_ASB, EnumMacMsOpenStageState.WaitingForReleaseBoxWithMask);
            }
            #endregion OS BT
            #region BT DW_01_01
            {
                var step = recipe.AddStep("Drawer_01_01 Move Tray To In");
                step.AddBeforeState(EnumMachineID.MID_BT_A_ASB, EnumMacMsBoxTransferState.CB1HomeClamped);
                step.AddBeforeState(EnumMachineID.MID_DRAWER_01_01, EnumMacCabinetDrawerState.UnloadMoveTrayToHomeComplete);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_DRAWER_01_01.ToString(),
                    Value = EnumMacCabinetDrawerTransition.UnloadMoveTrayToInIng_UnloadMoveTrayToInComplete.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_BT_A_ASB, EnumMacMsBoxTransferState.CB1HomeClamped);
                step.AddAfterState(EnumMachineID.MID_DRAWER_01_01, EnumMacCabinetDrawerState.UnloadMoveTrayToInComplete);
            }

            {
                var step = recipe.AddStep("Box Transfer Move To Drawer_01_01 Release Box With Mask");
                step.AddBeforeState(EnumMachineID.MID_BT_A_ASB, EnumMacMsBoxTransferState.CB1HomeClamped);
                step.AddBeforeState(EnumMachineID.MID_DRAWER_01_01, EnumMacCabinetDrawerState.UnloadMoveTrayToHomeComplete);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_DRAWER_01_01.ToString(),
                    Value = EnumMacMsBoxTransferTransition.MoveToCB0101ForRelease.ToString(),
                });
                step.AddAfterState(EnumMachineID.MID_BT_A_ASB, EnumMacMsBoxTransferState.CB1Home);
                step.AddAfterState(EnumMachineID.MID_DRAWER_01_01, EnumMacCabinetDrawerState.UnloadMoveTrayToInComplete);
            }

            {
                var step = recipe.AddStep("Drawer_01_01 Move Tray To Home");
                step.AddBeforeState(EnumMachineID.MID_DRAWER_01_01, EnumMacCabinetDrawerState.UnloadMoveTrayToInComplete);

                step.StatesCmd.Add(new MacRecipeMachineState()
                {
                    Key = EnumMachineID.MID_DRAWER_01_01.ToString(),
                    Value = EnumMacCabinetDrawerTransition.UnloadMoveTrayToHomeIng_UnloadMoveTrayToHomeComplete.ToString(),
                });
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


        }

    }
}
