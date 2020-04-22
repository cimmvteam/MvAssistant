using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Gripper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.Gripper
{
    [GuidAttribute("B81CFCFA-8320-4111-9E19-03CB900C5E69")]
    public class HalPlcOmronCustom01 : HalPlcOmronBase, IHalGripper
    {

        public const string PlcVarKey_Enable = "var_enable";
        public const string PlcVarKey_Direction = "var_direction";
        public const string PlcVarKey_DoMovee = "var_do_move";
        public const string PlcVarKey_SpeedLevel = "var_speed_level";
        public const string PlcVarKey_Step = "var_step";
        public const string PlcVarKey_StepTarget = "var_step_target";
        public const string PlcVarKey_StepReset = "var_step_reset";


        protected string PlcVarEnable { get { return this.DevSettings[PlcVarKey_Enable]; } }
        protected string PlcVarDirection { get { return this.DevSettings[PlcVarKey_Direction]; } }
        protected string PlcVarDoMove { get { return this.DevSettings[PlcVarKey_DoMovee]; } }
        protected string PlcVarSpeedLevel { get { return this.DevSettings[PlcVarKey_SpeedLevel]; } }
        protected string PlcVarStep { get { return this.DevSettings[PlcVarKey_Step]; } }
        protected string PlcVarStepTarget { get { return this.DevSettings[PlcVarKey_StepTarget]; } }
        protected string PlcVarStepReset { get { return this.DevSettings[PlcVarKey_StepReset]; } }








        #region Override

        public override int HalConnect()
        {
            this.PlcSetup();
            //var flag = this.PlcGetValue(this.PlcVarEnable) != null;
            return 0;
        }
        public override bool HalIsConnected()
        {
            var plc = this.Plc();
            if (plc == null) return false;
            if (!plc.IsConnected()) return false;
            return this.PlcGetValue(this.PlcVarEnable) != null;
        }


        public override int HalClose()
        {
            return 0;
        }


        #endregion


        #region Implementation

        public void HalMove(HalGripperCmd cmd)
        {
            if (cmd.Direction != HalEnumGripperDirection.None)
                this.PlcSetValue(this.PlcVarDirection, cmd.Direction == HalEnumGripperDirection.Clockwise ? true : false);

            if (cmd.SpeedLevel.HasValue)
                this.PlcSetValue(this.PlcVarSpeedLevel, (int)cmd.SpeedLevel.Value);

            if (cmd.Position.HasValue)
                this.PlcSetValue(this.PlcVarStepTarget, cmd.Position.Value);

            if (cmd.Offset.HasValue)
            {
                var step = (int)this.PlcGetValue(this.PlcVarStep);
                var target = step + cmd.Offset.Value;
                this.PlcSetValue(this.PlcVarStepTarget, cmd.Offset.Value);
            }

            if (cmd.Enable.HasValue)
                this.PlcSetValue(this.PlcVarEnable, cmd.Enable.Value);

            if (cmd.Offset.HasValue || cmd.Position.HasValue)
                this.PlcSetValue(this.PlcVarDoMove, true);



        }

        public float HalGetPosition() { return (float)this.PlcGetValue(this.PlcVarStep); }

        public bool HalIsCompleted() { return !(bool)this.PlcGetValue(this.PlcVarDoMove); }

        public void HalStop()
        {
            this.PlcSetValue(this.PlcVarDoMove, false);
            this.PlcSetValue(this.PlcVarEnable, false);
        }

        public bool HalZeroReset()
        {
            this.PlcSetValue(this.PlcVarStepReset, true);
            Thread.Sleep(200);
            this.PlcSetValue(this.PlcVarStepReset, false);
            return true;
        }
        #endregion


    }
}
