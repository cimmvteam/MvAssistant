using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.Mac
{
    public enum EnumMacHalId
    {
        Eqp,
        RecipeAgent,
        SecsMgr,
        ActiveMask,

        __SourceLoadPort__,     //關鍵字
        __SourceDrawer__,       //關鍵字
        __Empty__,//清空
        __None__,//不指定Machine, 即 不會變動Mask From/To




#if DEBUG
        /* 此區為開發人員使用, 主要目的是產生設定檔
         * 實際運作的程式不需使用到
         */

        HID_BT_A_ASSY,
        HID_CC_A_ASSY,
        HID_CB_A_ASSY, //20191003 DE_DR_A_ASB=>DE_CB_A_ASB(cabinet)
        HID_IC_A_ASSY,
        HID_LP_A_ASSY,
        HID_LP_B_ASSY,
        HID_MT_A_ASSY,
        HID_OS_A_ASSY,
        HID_EQP_A_ASSY,//PLC的Assembly只能有一個, 但底下可以有多個PLC Device
       



        #region Box Transfer

        HID_BT_01,
        HID_BT_02,
        HID_BT_03,
        HID_BT_04,
        HID_BT_05,
        HID_BT_06,
        HID_BT_07,

        #endregion

        #region Clean Chamber

        HID_CC_A_01,
        HID_CC_A_02,
        HID_CC_A_03,
        HID_CC_A_04,
        HID_CC_A_05,
        HID_CC_A_06,
        HID_CC_A_07,
        HID_CC_A_08,
        HID_CC_A_09,
        HID_CC_A_10,
        HID_CC_A_11,
        HID_CC_A_12,
        HID_CC_A_13,
        HID_CC_A_14,
        HID_CC_A_15,
        HID_CC_A_16,
        HID_CC_A_17,
        HID_CC_A_18,
        HID_CC_A_19,
        HID_CC_A_20,
        HID_CC_A_21,
        HID_CC_A_22,
        HID_CC_A_23,
        HID_CC_A_24,
        HID_CC_A_25,
        HID_CC_A_26,
        HID_CC_A_27,
        HID_CC_A_28,
        HID_CC_A_29,
        HID_CC_A_30,
        HID_CC_A_31,
        HID_CC_A_32,
        HID_CC_A_33,
        HID_CC_A_34,
        HID_CC_A_35,
        HID_CC_A_36,
        HID_CC_A_37,
        HID_CC_A_38,
        HID_CC_A_39,
        HID_CC_A_40,
        HID_CC_A_41,
        HID_CC_A_42,
        HID_CC_A_43,
        HID_CC_A_44,
        HID_CC_A_45,
        HID_CC_A_46,
        HID_CC_A_47,
        HID_CC_A_48,
        HID_CC_A_49,
        HID_CC_A_50,

        #endregion

        #region Cabinet
        HID_CB_A_01_01,
        HID_CB_A_01_02,
        HID_CB_A_01_03,
        HID_CB_A_01_04,
        HID_CB_A_01_05,
        HID_CB_A_02_01,
        HID_CB_A_02_02,
        HID_CB_A_02_03,
        HID_CB_A_02_04,
        HID_CB_A_02_05,
        HID_CB_A_03_01,
        HID_CB_A_03_02,
        HID_CB_A_03_03,
        HID_CB_A_03_04,
        HID_CB_A_03_05,
        HID_CB_A_04_01,
        HID_CB_A_04_02,
        HID_CB_A_04_03,
        HID_CB_A_04_04,
        HID_CB_A_04_05,
        HID_CB_A_05_01,
        HID_CB_A_05_02,
        HID_CB_A_05_03,
        HID_CB_A_05_04,
        HID_CB_A_05_05,
        HID_CB_A_06_01,
        HID_CB_A_06_02,
        HID_CB_A_06_03,
        HID_CB_A_06_04,
        HID_CB_A_06_05,
        HID_CB_A_07_01,
        HID_CB_A_07_02,
        HID_CB_A_07_03,
        HID_CB_A_07_04,
        HID_CB_A_07_05,
        #endregion

        #region Inspection Chamber

        HID_IC_A_01,
        HID_IC_A_02,
        HID_IC_A_03,
        HID_IC_A_04,
        HID_IC_A_05,
        HID_IC_A_06,
        HID_IC_A_07,
        HID_IC_A_08,
        HID_IC_A_09,
        HID_IC_A_10,
        HID_IC_A_11,
        HID_IC_A_12,
        HID_IC_A_13,
        HID_IC_A_14,
        HID_IC_A_15,
        HID_IC_A_16,
        HID_IC_A_17,
        HID_IC_A_18,
        HID_IC_A_19,
        HID_IC_A_20,
        HID_IC_A_21,
        HID_IC_A_22,
        HID_IC_A_23,
        HID_IC_A_24,
        HID_IC_A_25,
        HID_IC_A_26,
        HID_IC_A_27,
        HID_IC_A_28,
        HID_IC_A_29,


        #endregion

        #region Load Port A

        HID_LP_A_01,
        HID_LP_A_02,
        HID_LP_A_03,
        HID_LP_A_04,
        HID_LP_A_05,
        HID_LP_A_06,
        HID_LP_A_07,
        HID_LP_A_08,
        HID_LP_A_09,
        HID_LP_A_10,
        HID_LP_A_11,
        HID_LP_A_12,
        HID_LP_A_13,
        HID_LP_A_14,
        HID_LP_A_15,
        HID_LP_A_16,
        HID_LP_A_17,
        HID_LP_A_18,
        HID_LP_A_19,
        HID_LP_A_20,
        HID_LP_A_21,
        HID_LP_A_22,
        HID_LP_A_23,
        HID_LP_A_24,
        HID_LP_A_25,
        HID_LP_A_26,
        HID_LP_A_27,
        HID_LP_A_28,
        HID_LP_A_29,

        #endregion

        #region LoBd Port B

        HID_LP_B_01,
        HID_LP_B_02,
        HID_LP_B_03,
        HID_LP_B_04,
        HID_LP_B_05,
        HID_LP_B_06,
        HID_LP_B_07,
        HID_LP_B_08,
        HID_LP_B_09,
        HID_LP_B_10,
        HID_LP_B_11,
        HID_LP_B_12,
        HID_LP_B_13,
        HID_LP_B_14,
        HID_LP_B_15,
        HID_LP_B_16,
        HID_LP_B_17,
        HID_LP_B_18,
        HID_LP_B_19,
        HID_LP_B_20,
        HID_LP_B_21,
        HID_LP_B_22,
        HID_LP_B_23,
        HID_LP_B_24,
        HID_LP_B_25,
        HID_LP_B_26,
        HID_LP_B_27,
        HID_LP_B_28,
        HID_LP_B_29,

        #endregion

        #region Mask Transfer

        HID_MT_A_01,
        HID_MT_A_02,
        HID_MT_A_03,
        HID_MT_A_04,
        HID_MT_A_05,
        HID_MT_A_06,
        HID_MT_A_07,
        HID_MT_A_08,
        HID_MT_A_09,
        HID_MT_A_10,
        HID_MT_A_11,
        HID_MT_A_12,
        HID_MT_A_13,
        HID_MT_A_14,
        HID_MT_A_15,
        HID_MT_A_16,
        HID_MT_A_17,
        HID_MT_A_18,
        HID_MT_A_19,
        HID_MT_A_20,
        HID_MT_A_21,
        HID_MT_A_22,
        HID_MT_A_23,
        HID_MT_A_24,
        HID_MT_A_25,
        HID_MT_A_26,
        HID_MT_A_27,
        HID_MT_A_28,
        HID_MT_A_29,
        HID_MT_A_30,
        HID_MT_A_31,
        HID_MT_A_32,
        HID_MT_A_33,
        HID_MT_A_34,
        HID_MT_A_35,
        HID_MT_A_36,
        HID_MT_A_37,
        HID_MT_A_38,
        HID_MT_A_39,
        HID_MT_A_40,
        HID_MT_A_41,
        HID_MT_A_42,
        HID_MT_A_43,
        HID_MT_A_44,
        HID_MT_A_45,
        HID_MT_A_46,
        HID_MT_A_47,
        HID_MT_A_48,
        HID_MT_A_49,
        HID_MT_A_50,

        #endregion

        #region Open Stage

        HID_OS_A_01,
        HID_OS_A_02,
        HID_OS_A_03,
        HID_OS_A_04,
        HID_OS_A_05,
        HID_OS_A_06,
        HID_OS_A_07,
        HID_OS_A_08,
        HID_OS_A_09,
        HID_OS_A_10,
        HID_OS_A_11,
        HID_OS_A_12,
        HID_OS_A_13,
        HID_OS_A_14,
        HID_OS_A_15,
        HID_OS_A_16,
        HID_OS_A_17,
        HID_OS_A_18,
        HID_OS_A_19,
        HID_OS_A_20,
        HID_OS_A_21,
        HID_OS_A_22,
        HID_OS_A_23,
        HID_OS_A_24,
        HID_OS_A_25,
        HID_OS_A_26,
        HID_OS_A_27,
        HID_OS_A_28,
        HID_OS_A_29,
        HID_OS_A_30,
        HID_OS_A_31,
        HID_OS_A_32,
        HID_OS_A_33,
        HID_OS_A_34,
        HID_OS_A_35,
        HID_OS_A_36,
        HID_OS_A_37,
        HID_OS_A_38,
        HID_OS_A_39,
        HID_OS_A_40,
        HID_OS_A_41,
        HID_OS_A_42,
        HID_OS_A_43,
        HID_OS_A_44,
        HID_OS_A_45,
        HID_OS_A_46,
        HID_OS_A_47,
        HID_OS_A_48,
        HID_OS_A_49,
        HID_OS_A_50,

        #endregion


        #region Universal

        HID_UNI_A_01,
        HID_UNI_A_02,

        #endregion


        #region True Drawer(BoxSlot)
        //DE_BS_ID_MOPEN    sensor:TE端開盒最大距離光遮斷
        //DE_BS_ID_HOME     sensor:HOME點光遮斷
        //DE_BS_ID_ROPEN    sensor:ROBOT端開盒最大距離光遮斷
        //DE_BS_ID_BTN      面板控制按鈕
        HID_CB_01_ASB,
        HID_CB_01_MOPEN_LIMIT,
        HID_CB_01_MOPEN,
        HID_CB_01_HOME,
        HID_CB_01_ROPEN,
        HID_CB_01_ROPEN_LIMIT,
        HID_CB_01_BTN,
        HID_CB_01_PRESENT,
        HID_CB_02_ASB,
        HID_CB_02_MOPEN,
        HID_CB_02_MOPEN_LIMIT,
        HID_CB_02_HOME,
        HID_CB_02_ROPEN,
        HID_CB_02_ROPEN_LIMIT,
        HID_CB_02_BTN,
        HID_CB_02_PRESENT,
        HID_CB_03_ASB,
        HID_CB_03_MOPEN,
        HID_CB_03_MOPEN_LIMIT,
        HID_CB_03_HOME,
        HID_CB_03_ROPEN,
        HID_CB_03_ROPEN_LIMIT,
        HID_CB_03_BTN,
        HID_CB_03_PRESENT,
        HID_CB_04_ASB,
        HID_CB_04_MOPEN,
        HID_CB_04_MOPEN_LIMIT,
        HID_CB_04_HOME,
        HID_CB_04_ROPEN,
        HID_CB_04_ROPEN_LIMIT,
        HID_CB_04_BTN,
        HID_CB_04_PRESENT,
        HID_CB_05_ASB,
        HID_CB_05_MOPEN,
        HID_CB_05_MOPEN_LIMIT,
        HID_CB_05_HOME,
        HID_CB_05_ROPEN,
        HID_CB_05_ROPEN_LIMIT,
        HID_CB_05_BTN,
        HID_CB_05_PRESENT,
        HID_CB_06_ASB,
        HID_CB_06_MOPEN,
        HID_CB_06_MOPEN_LIMIT,
        HID_CB_06_HOME,
        HID_CB_06_ROPEN,
        HID_CB_06_ROPEN_LIMIT,
        HID_CB_06_BTN,
        HID_CB_06_PRESENT,
        HID_CB_07_ASB,
        HID_CB_07_MOPEN,
        HID_CB_07_MOPEN_LIMIT,
        HID_CB_07_HOME,
        HID_CB_07_ROPEN,
        HID_CB_07_ROPEN_LIMIT,
        HID_CB_07_BTN,
        HID_CB_07_PRESENT,
        HID_CB_08_ASB,
        HID_CB_08_MOPEN,
        HID_CB_08_MOPEN_LIMIT,
        HID_CB_08_HOME,
        HID_CB_08_ROPEN,
        HID_CB_08_ROPEN_LIMIT,
        HID_CB_08_BTN,
        HID_CB_08_PRESENT,

        #endregion


#endif






    }
}
