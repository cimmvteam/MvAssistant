using MvAssistant.DeviceDriver.HiwinStage;
using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Stage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MvAssistant.Mac.v1_0.Hal.Component.Stage
{
    [GuidAttribute("DD9C98E2-5B4B-4310-AF15-1E39FC87EB86")]
    public class HalStageHiwin : MacHalComponentBase, IHalInspectionStage
    {
        ComInfo connect_setting = new ComInfo();

        int ctrl_id = -1;
        int api_rtn = -1;
        int axis_id1 = 0;
        int axis_id2 = 1;
        int group_id = 0;
        int[] group_list = new int[2];


        bool IHalInspectionStage.HalMoveAbs(HalStageMotion p)
        {
            api_rtn = HimcApi.HIMC_LineAbs2D(ctrl_id, group_id, p.X, p.Y);
            int check_states = 0;
            while (check_states == 0)
            {
                api_rtn = HimcApi.HIMC_IsGrpInPos(ctrl_id, axis_id1, ref check_states);
            }
            return true;
        }



        #region IHal
        public override int HalConnect()
        {
            connect_setting.type = ComType.COM_TYPE_TCPIP;
            ComInfo.TCP_IP tcpp = new ComInfo.TCP_IP();
            tcpp.ip = new byte[20];
            Encoding.Default.GetBytes("169.254.188.20").CopyTo(tcpp.ip, 0);
            tcpp.port = new byte[12];
            Encoding.Default.GetBytes("5555").CopyTo(tcpp.port, 0);
            connect_setting.com_TCP = tcpp;

            // connect to HIMC        
            api_rtn = HimcApi.HIMC_ConnectCtrl(connect_setting, ref ctrl_id);

            return api_rtn;
        }
        public override int HalClose()
        {
            throw new NotImplementedException();
        }

        #endregion


        int SetupGroup()
        {
            group_list[0] = axis_id1;
            group_list[1] = axis_id2;
            api_rtn = HimcApi.HIMC_SetupGroup(ctrl_id, group_id, 2, group_list);

            return api_rtn;
        }

        int EnableAndRest()
        {
            api_rtn = HimcApi.HIMC_Enable(ctrl_id, axis_id1);
            api_rtn = HimcApi.HIMC_Enable(ctrl_id, axis_id2);
            api_rtn = HimcApi.HIMC_Reset(ctrl_id, axis_id1);
            api_rtn = HimcApi.HIMC_Reset(ctrl_id, axis_id2);

            return api_rtn;
        }





        public bool HalMoveIsComplete()
        {
            int check_states = 0;
            api_rtn = HimcApi.HIMC_IsGrpInPos(ctrl_id, axis_id1, ref check_states);
            return check_states != 0;
        }


        public bool HalMoveRel(HalStageMotion p)
        {
            api_rtn = HimcApi.HIMC_LineRel2D(ctrl_id, group_id, p.X, p.Y);
            int check_states = 0;
            while (check_states == 0)
            {
                api_rtn = HimcApi.HIMC_IsGrpInPos(ctrl_id, axis_id1, ref check_states);
            }
            return true;
        }



    }


}
