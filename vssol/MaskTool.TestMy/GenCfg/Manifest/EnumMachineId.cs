using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.TestMy.GenCfg.Manifest
{
    public enum EnumMachineId
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

        DE_BT_A_ASB,
        DE_CC_A_ASB,
        DE_CB_A_ASB, //20191003 DE_DR_A_ASB=>DE_CB_A_ASB(cabinet)
        DE_IC_A_ASB,
        DE_LP_A_ASB,
        DE_LP_B_ASB,
        DE_MT_A_ASB,
        DE_OS_A_ASB,
        DE_UNI_A_ASB,//PLC的Assembly只能有一個, 但底下可以有多個PLC Device
       



        #region Box Transfer

        DE_BT_01,
        DE_BT_02,
        DE_BT_03,
        DE_BT_04,
        DE_BT_05,
        DE_BT_06,
        DE_BT_07,

        #endregion

        #region Clean Chamber

        DE_CC_A_01,
        DE_CC_A_02,
        DE_CC_A_03,
        DE_CC_A_04,
        DE_CC_A_05,
        DE_CC_A_06,
        DE_CC_A_07,
        DE_CC_A_08,
        DE_CC_A_09,
        DE_CC_A_10,
        DE_CC_A_11,
        DE_CC_A_12,
        DE_CC_A_13,
        DE_CC_A_14,
        DE_CC_A_15,
        DE_CC_A_16,
        DE_CC_A_17,
        DE_CC_A_18,
        DE_CC_A_19,
        DE_CC_A_20,
        DE_CC_A_21,
        DE_CC_A_22,
        DE_CC_A_23,
        DE_CC_A_24,
        DE_CC_A_25,
        DE_CC_A_26,
        DE_CC_A_27,
        DE_CC_A_28,
        DE_CC_A_29,
        DE_CC_A_30,
        DE_CC_A_31,
        DE_CC_A_32,
        DE_CC_A_33,
        DE_CC_A_34,
        DE_CC_A_35,
        DE_CC_A_36,
        DE_CC_A_37,
        DE_CC_A_38,
        DE_CC_A_39,
        DE_CC_A_40,
        DE_CC_A_41,
        DE_CC_A_42,
        DE_CC_A_43,
        DE_CC_A_44,
        DE_CC_A_45,
        DE_CC_A_46,
        DE_CC_A_47,
        DE_CC_A_48,
        DE_CC_A_49,
        DE_CC_A_50,

        #endregion

        #region Cabinet
        DE_CB_A_01_01,
        DE_CB_A_01_02,
        DE_CB_A_01_03,
        DE_CB_A_01_04,
        DE_CB_A_01_05,
        DE_CB_A_02_01,
        DE_CB_A_02_02,
        DE_CB_A_02_03,
        DE_CB_A_02_04,
        DE_CB_A_02_05,
        DE_CB_A_03_01,
        DE_CB_A_03_02,
        DE_CB_A_03_03,
        DE_CB_A_03_04,
        DE_CB_A_03_05,
        DE_CB_A_04_01,
        DE_CB_A_04_02,
        DE_CB_A_04_03,
        DE_CB_A_04_04,
        DE_CB_A_04_05,
        DE_CB_A_05_01,
        DE_CB_A_05_02,
        DE_CB_A_05_03,
        DE_CB_A_05_04,
        DE_CB_A_05_05,
        DE_CB_A_06_01,
        DE_CB_A_06_02,
        DE_CB_A_06_03,
        DE_CB_A_06_04,
        DE_CB_A_06_05,
        DE_CB_A_07_01,
        DE_CB_A_07_02,
        DE_CB_A_07_03,
        DE_CB_A_07_04,
        DE_CB_A_07_05,
        #endregion

        #region Inspection Chamber

        DE_IC_A_01,
        DE_IC_A_02,
        DE_IC_A_03,
        DE_IC_A_04,
        DE_IC_A_05,
        DE_IC_A_06,
        DE_IC_A_07,
        DE_IC_A_08,
        DE_IC_A_09,
        DE_IC_A_10,
        DE_IC_A_11,
        DE_IC_A_12,
        DE_IC_A_13,
        DE_IC_A_14,
        DE_IC_A_15,
        DE_IC_A_16,
        DE_IC_A_17,
        DE_IC_A_18,
        DE_IC_A_19,
        DE_IC_A_20,
        DE_IC_A_21,
        DE_IC_A_22,
        DE_IC_A_23,
        DE_IC_A_24,
        DE_IC_A_25,
        DE_IC_A_26,
        DE_IC_A_27,
        DE_IC_A_28,
        DE_IC_A_29,


        #endregion

        #region Load Port A

        DE_LP_A_01,
        DE_LP_A_02,
        DE_LP_A_03,
        DE_LP_A_04,
        DE_LP_A_05,
        DE_LP_A_06,
        DE_LP_A_07,
        DE_LP_A_08,
        DE_LP_A_09,
        DE_LP_A_10,
        DE_LP_A_11,
        DE_LP_A_12,
        DE_LP_A_13,
        DE_LP_A_14,
        DE_LP_A_15,
        DE_LP_A_16,
        DE_LP_A_17,
        DE_LP_A_18,
        DE_LP_A_19,
        DE_LP_A_20,
        DE_LP_A_21,
        DE_LP_A_22,
        DE_LP_A_23,
        DE_LP_A_24,
        DE_LP_A_25,
        DE_LP_A_26,
        DE_LP_A_27,
        DE_LP_A_28,
        DE_LP_A_29,

        #endregion

        #region LoBd Port B

        DE_LP_B_01,
        DE_LP_B_02,
        DE_LP_B_03,
        DE_LP_B_04,
        DE_LP_B_05,
        DE_LP_B_06,
        DE_LP_B_07,
        DE_LP_B_08,
        DE_LP_B_09,
        DE_LP_B_10,
        DE_LP_B_11,
        DE_LP_B_12,
        DE_LP_B_13,
        DE_LP_B_14,
        DE_LP_B_15,
        DE_LP_B_16,
        DE_LP_B_17,
        DE_LP_B_18,
        DE_LP_B_19,
        DE_LP_B_20,
        DE_LP_B_21,
        DE_LP_B_22,
        DE_LP_B_23,
        DE_LP_B_24,
        DE_LP_B_25,
        DE_LP_B_26,
        DE_LP_B_27,
        DE_LP_B_28,
        DE_LP_B_29,

        #endregion

        #region Mask Transfer

        DE_MT_A_01,
        DE_MT_A_02,
        DE_MT_A_03,
        DE_MT_A_04,
        DE_MT_A_05,
        DE_MT_A_06,
        DE_MT_A_07,
        DE_MT_A_08,
        DE_MT_A_09,
        DE_MT_A_10,
        DE_MT_A_11,
        DE_MT_A_12,
        DE_MT_A_13,
        DE_MT_A_14,
        DE_MT_A_15,
        DE_MT_A_16,
        DE_MT_A_17,
        DE_MT_A_18,
        DE_MT_A_19,
        DE_MT_A_20,
        DE_MT_A_21,
        DE_MT_A_22,
        DE_MT_A_23,
        DE_MT_A_24,
        DE_MT_A_25,
        DE_MT_A_26,
        DE_MT_A_27,
        DE_MT_A_28,
        DE_MT_A_29,
        DE_MT_A_30,
        DE_MT_A_31,
        DE_MT_A_32,
        DE_MT_A_33,
        DE_MT_A_34,
        DE_MT_A_35,
        DE_MT_A_36,
        DE_MT_A_37,
        DE_MT_A_38,
        DE_MT_A_39,
        DE_MT_A_40,
        DE_MT_A_41,
        DE_MT_A_42,
        DE_MT_A_43,
        DE_MT_A_44,
        DE_MT_A_45,
        DE_MT_A_46,
        DE_MT_A_47,
        DE_MT_A_48,
        DE_MT_A_49,
        DE_MT_A_50,

        #endregion

        #region Open Stage

        DE_OS_A_01,
        DE_OS_A_02,
        DE_OS_A_03,
        DE_OS_A_04,
        DE_OS_A_05,
        DE_OS_A_06,
        DE_OS_A_07,
        DE_OS_A_08,
        DE_OS_A_09,
        DE_OS_A_10,
        DE_OS_A_11,
        DE_OS_A_12,
        DE_OS_A_13,
        DE_OS_A_14,
        DE_OS_A_15,
        DE_OS_A_16,
        DE_OS_A_17,
        DE_OS_A_18,
        DE_OS_A_19,
        DE_OS_A_20,
        DE_OS_A_21,
        DE_OS_A_22,
        DE_OS_A_23,
        DE_OS_A_24,
        DE_OS_A_25,
        DE_OS_A_26,
        DE_OS_A_27,
        DE_OS_A_28,
        DE_OS_A_29,
        DE_OS_A_30,
        DE_OS_A_31,
        DE_OS_A_32,
        DE_OS_A_33,
        DE_OS_A_34,
        DE_OS_A_35,
        DE_OS_A_36,
        DE_OS_A_37,
        DE_OS_A_38,
        DE_OS_A_39,
        DE_OS_A_40,
        DE_OS_A_41,
        DE_OS_A_42,
        DE_OS_A_43,
        DE_OS_A_44,
        DE_OS_A_45,
        DE_OS_A_46,
        DE_OS_A_47,
        DE_OS_A_48,
        DE_OS_A_49,
        DE_OS_A_50,

        #endregion


        #region Universal

        DE_UNI_A_01,
        DE_UNI_A_02,

        #endregion


        #region True Drawer(BoxSlot)
        //DE_BS_ID_MOPEN    sensor:TE端開盒最大距離光遮斷
        //DE_BS_ID_HOME     sensor:HOME點光遮斷
        //DE_BS_ID_ROPEN    sensor:ROBOT端開盒最大距離光遮斷
        //DE_BS_ID_BTN      面板控制按鈕
        DE_CB_01_ASB,
        DE_CB_01_MOPEN_LIMIT,
        DE_CB_01_MOPEN,
        DE_CB_01_HOME,
        DE_CB_01_ROPEN,
        DE_CB_01_ROPEN_LIMIT,
        DE_CB_01_BTN,
        DE_CB_01_PRESENT,
        DE_CB_02_ASB,
        DE_CB_02_MOPEN,
        DE_CB_02_MOPEN_LIMIT,
        DE_CB_02_HOME,
        DE_CB_02_ROPEN,
        DE_CB_02_ROPEN_LIMIT,
        DE_CB_02_BTN,
        DE_CB_02_PRESENT,
        DE_CB_03_ASB,
        DE_CB_03_MOPEN,
        DE_CB_03_MOPEN_LIMIT,
        DE_CB_03_HOME,
        DE_CB_03_ROPEN,
        DE_CB_03_ROPEN_LIMIT,
        DE_CB_03_BTN,
        DE_CB_03_PRESENT,
        DE_CB_04_ASB,
        DE_CB_04_MOPEN,
        DE_CB_04_MOPEN_LIMIT,
        DE_CB_04_HOME,
        DE_CB_04_ROPEN,
        DE_CB_04_ROPEN_LIMIT,
        DE_CB_04_BTN,
        DE_CB_04_PRESENT,
        DE_CB_05_ASB,
        DE_CB_05_MOPEN,
        DE_CB_05_MOPEN_LIMIT,
        DE_CB_05_HOME,
        DE_CB_05_ROPEN,
        DE_CB_05_ROPEN_LIMIT,
        DE_CB_05_BTN,
        DE_CB_05_PRESENT,
        DE_CB_06_ASB,
        DE_CB_06_MOPEN,
        DE_CB_06_MOPEN_LIMIT,
        DE_CB_06_HOME,
        DE_CB_06_ROPEN,
        DE_CB_06_ROPEN_LIMIT,
        DE_CB_06_BTN,
        DE_CB_06_PRESENT,
        DE_CB_07_ASB,
        DE_CB_07_MOPEN,
        DE_CB_07_MOPEN_LIMIT,
        DE_CB_07_HOME,
        DE_CB_07_ROPEN,
        DE_CB_07_ROPEN_LIMIT,
        DE_CB_07_BTN,
        DE_CB_07_PRESENT,
        DE_CB_08_ASB,
        DE_CB_08_MOPEN,
        DE_CB_08_MOPEN_LIMIT,
        DE_CB_08_HOME,
        DE_CB_08_ROPEN,
        DE_CB_08_ROPEN_LIMIT,
        DE_CB_08_BTN,
        DE_CB_08_PRESENT,

        #endregion


#endif






    }
}
