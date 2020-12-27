using MaskAutoCleaner.v1_0.Machine;
using MaskAutoCleaner.v1_0.Msg;
using MvAssistant.v0_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Recipe
{
    public class MacRecipeMgr
    {

        //只有一個Recipe, 先求有, 再求好, 之後再追加 Multi-Recipe Run
        public MacRecipe Recipe;
        protected MacMachineMgr MachineMgr;
        public MacRecipeMgr(MacMachineMgr machineMgr) { this.MachineMgr = machineMgr; }



        public void LoaddRecipe(string recipeName)
        {
            this.Recipe = MvaUtil.LoadFromXmlFile<MacRecipe>(recipeName);
        }


        public void Execute()
        {
            if (this.MachineMgr == null)
                throw new MacException("No assign MachineMgr");
            if (this.MachineMgr.CtrlMachines == null)
                throw new MacException("No initial CtrlMachines");


            foreach (var step in this.Recipe)
            {
                MvaLog.InfoNs(this, "Step Before Check: " + step.StepName);
                while (!this.CheckStatesBefore(step))
                    Thread.Sleep(100);



                this.SendStatesCmd(step);


                MvaLog.InfoNs(this, "Step After Check: " + step.StepName);
                while (!this.CheckStatesAfter(step))
                    Thread.Sleep(100);

            }

            MvaLog.InfoNs(this, "Complete Recipe: " + this.Recipe.RecipeName);



        }





        bool CheckStatesBefore(MacRecipeStep step)
        {

            foreach (var machineState in step.StatesBefore)
            {
                if (!this.MachineMgr.CtrlMachines.ContainsKey(machineState.Key))
                    throw new MacException("No exist control machine key");

                var cm = this.MachineMgr.CtrlMachines[machineState.Key];
                //if (cm.MsAssembly == null) throw new MacException("No initial state machine");//不應發生
                if (machineState.Value != cm.MsAssembly.CurrentStateName) return false;
            }

            return true;
        }

        bool SendStatesCmd(MacRecipeStep step)
        {
            foreach (var cmd in step.StatesCmd)
            {

                if (!this.MachineMgr.CtrlMachines.ContainsKey(cmd.Key))
                    throw new MacException("No exist control machine key");

                var cm = this.MachineMgr.CtrlMachines[cmd.Key];
                //if (cm.MsAssembly == null) throw new MacException("No initial state machine");//不應發生

                cm.RequestProcMsg(MacMsgCommand.Create(cmd.Value));
            }


            return true;
        }

        bool CheckStatesAfter(MacRecipeStep step)
        {

            foreach (var machineState in step.StatesAfter)
            {
                if (!this.MachineMgr.CtrlMachines.ContainsKey(machineState.Key))
                    throw new MacException("No exist control machine key");

                var cm = this.MachineMgr.CtrlMachines[machineState.Key];
                //if (cm.MsAssembly == null) throw new MacException("No initial state machine");//不應發生
                if (machineState.Value != cm.MsAssembly.CurrentStateName) return false;
            }


            return true;
        }

    }
}
