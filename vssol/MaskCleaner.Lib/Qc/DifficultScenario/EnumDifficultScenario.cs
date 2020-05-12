using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Qc.DifficultScenario
{
    /// <summary>
    /// 困難情境ID
    /// 使用六碼編碼ABCDEF
    ///  - A: 1: Normal Process
    ///       2: Abnormal Process
    ///  - B: 1: OCAP
    ///       2: BankIn
    ///       3: BankOut
    ///  - C: 0: 整機
    ///       1: LoadPort
    ///       2: Inspection CHM
    ///       3: Clean CHM
    ///       4: Open Stage
    ///       5: Drawer
    ///       6: Mask Transfer
    ///       7: Box Transfer
    ///  - DEF: 流水號; 從000開始
    /// </summary>
    public enum DifficultScenarioID
    {
        #region Normal Processes
        [Description("OCAP正常流程")]
        OcapNorm0001 = 110000,

        [Description("BankIn正常流程")]
        BankInNorm0001 = 120000,

        [Description("BankIn + OCAP正常流程")]
        BankInNorm0002 = 120001,

        [Description("BankOut正常流程")]
        BankOutNorm0001 = 13000,

        [Description("BankOut + OCAP正常流程")]
        BankOutNorm0002 = 130001,
        #endregion Normal Processes

        #region Abnormal Processes
        #region LoadPort
        [Description("ART下錯LoadPort")]
        OcapAbn1001F = 211000,

        [Description("ART與Loadport位置不準")]
        OcapAbn1002F = 211001,

        [Description("進入LoadPort後POD底座未UnLock")]
        OcapAbn1003F = 211002,

        [Description("POD下貨方向相反")]
        OcapAbn1004F = 211003,

        [Description("要下貨之LoadPort已有POD")]
        OcapAbn1005F = 211004,

        [Description("Read POD RFID Failed")]
        OcapAbn1006F = 211005,

        [Description("RFID對帳錯誤")]
        OcapAbn1007F = 211006,

        [Description("Mask EBO Num.與POD對帳錯誤")]
        OcapAbn1008F = 211007,

        [Description("POD上蓋開啟失敗")]
        OcapAbn1009F = 211008,

        [Description("POD內部無Mask")]
        OcapAbn1010F = 211009,

        [Description("Down flow異常引起Particle Issue, PA Counter小於下限")]
        OcapAbn1011L = 211010,

        [Description("Down flow異常引起Particle Issue, PA Counter大於上限")]
        OcapAbn1011U = 211011,

        [Description("Stage未往下降")]
        OcapAbn1012F = 211012,

        [Description("托鏈系統產生Particle (同OcapAbn1011)")]
        OcapAbn1013F = 211013,

        [Description("光源異常, 光源照度小於下限")]
        OcapAbn1014L = 211014,

        [Description("光源異常, 光源照度大於上限")]
        OcapAbn1014U = 211015,

        [Description("Mask Barcode對帳錯誤")]
        OcapAbn1015F = 211016,

        [Description("Read Mask Barcode Failed")]
        OcapAbn1016F = 211017,

        [Description("Gripper夾取處崩角")]
        OcapAbn1017F = 211018,

        [Description("POD內光罩X方向偏移, X偏移小於反向下限")]
        OcapAbn1018L = 211019,

        [Description("POD內光罩X方向偏移, X偏移大於正向上限")]
        OcapAbn1018U = 211020,

        [Description("POD內光罩Y方向偏移, Y偏移小於反向下限")]
        OcapAbn1019L = 211021,

        [Description("POD內光罩Y方向偏移, X偏移大於正向上限")]
        OcapAbn1019U = 211022,

        [Description("POD內光罩Z方向偏移, Z偏移小於反向下限")]
        OcapAbn1020L = 211023,

        [Description("POD內光罩Z方向偏移, Z偏移大於正向上限")]
        OcapAbn1020U = 211024,

        [Description("POD內光罩正反面擺放錯誤")]
        OcapAbn1021F = 211025,

        [Description("POD內光罩Star位置擺放錯誤")]
        OcapAbn1022F = 211026,

        [Description("POD內光罩Row方向偏移, Row偏移小於反向下限")]
        OcapAbn1023L = 211027,

        [Description("POD內光罩Row方向偏移, Row偏移大於正向上限")]
        OcapAbn1023U = 211028,

        [Description("POD內光罩Pitch方向偏移, Pitch偏移小於反向下限")]
        OcapAbn1024L = 211029,

        [Description("POD內光罩Pitch方向偏移, Pitch偏移大於正向上限")]
        OcapAbn1024U = 211030,

        [Description("POD內光罩Yaw方向偏移, Yaw偏移小於反向下限")]
        OcapAbn1025L = 211031,

        [Description("POD內光罩Yaw方向偏移, Yaw偏移大於正向上限")]
        OcapAbn1025U = 211032,

        [Description("離開LoadPort前POD底座未Lock")]
        OcapAbn1026F = 211033,

        [Description("CCD發生位移/髒汙/失焦等狀況")]
        OcapAbn1027F = 211034,
        #endregion LoadPort

        #region Inspection Chamber
        [Description("光源造成光罩損傷 (ex: 雷射光功率/波長不符合FAB規範)")]
        OcapAbn2001F = 212000,

        [Description("Stage 軸承潤滑劑汙染Mask")]
        OcapAbn2002F = 212001,

        [Description("Mask run錯recipe，導致影像無法正確對焦")]
        OcapAbn2003F = 212002,

        [Description("光源控制異常或光源衰退導致檢測異常, 光源照度小於下限")]
        OcapAbn2004L = 212003,

        [Description("光源控制異常或光源衰退導致檢測異常, 光源照度大於上限")]
        OcapAbn2004U = 212004,

        [Description("鏡頭髒污造成誤判為particle")]
        OcapAbn2005F = 212005,

        [Description("CCD異常 無法正常取像")]
        OcapAbn2006F = 212006,

        [Description("相機遭誤觸碰，造成取像位置或對焦跑掉")]
        OcapAbn2007F = 212007,

        [Description("stage 調整Z軸異常，導致像機無法對焦, Z軸小於下限")]
        OcapAbn2008L = 212008,

        [Description("stage 調整Z軸異常，導致像機無法對焦, Z軸大於上限")]
        OcapAbn2008U = 212009,

        [Description("環境中的particle 掉落在光罩上")]
        OcapAbn2009F = 212010,

        [Description("外來強光影響缺陷識別結果")]
        OcapAbn2010F = 212011,

        [Description("Stage 長期使用後，精準度變差, 造成X軸偏移")]
        OcapAbn2011F = 212012,

        [Description("Stage 長期使用後，精準度變差, 造成Y軸偏移")]
        OcapAbn2012F = 212013,

        [Description("Stage 長期使用後，精準度變差, 造成Z軸偏移")]
        OcapAbn2013F = 212014,

        [Description("Stage 長期使用後，精準度變差, 造成θ偏移")]
        OcapAbn2014F = 212015,

        [Description("夾取/下放光罩至Inspection Stage時, 施放過程有誤, 造成光罩損壞, Mask Tactile Sensor小於下限")]
        OcapAbn2015L = 212016,

        [Description("夾取/下放光罩至Inspection Stage時, 施放過程有誤, 造成光罩損壞, Mask Tactile Sensor大於上限")]
        OcapAbn2015U = 212017,

        [Description("夾取/下放光罩至Inspection Stage時, 施放過程有誤, 造成光罩損壞, Mask Force Sensor小於下限")]
        OcapAbn2016L = 212018,

        [Description("夾取/下放光罩至Inspection Stage時, 施放過程有誤, 造成光罩損壞, Mask Force Sensor大於上限")]
        OcapAbn2016U = 212019,

        [Description("Inspection Stage無法移動至指定位置 (ex: Home點)")]
        OcapAbn2017F = 212020,

        [Description("無法判斷Inspection Stage目前有無光罩在上面")]
        OcapAbn2018F = 212021,

        [Description("光罩未正確被放置在Inspection Stage的PIN腳上")]
        OcapAbn2019F = 212022,

        [Description("光罩放置時, 方向錯誤")]
        OcapAbn2020F = 212023,

        [Description("光罩放置時, 正反面錯誤")]
        OcapAbn2021F = 212024,

        [Description("檢測到膠框上的PA, 卻進行吹除, 造成PA跑入Pellicle裡面的風險")]
        OcapAbn2022F = 212025,

        [Description("使用Ionizer bar會產生O3臭氧, 可能造成塑膠物件侵蝕, 進而變成機台PA來源")]
        OcapAbn2023F = 212026,
        #endregion Inspection Chamber

        #region Clean Chamber
        [Description("吹除氣壓過大導致光罩損傷")]
        OcapAbn3001F = 213000,

        [Description("吹除氣壓過小導致無法清除")]
        OcapAbn3002F = 213001,

        [Description("吹除氣體含髒污, 導致光罩汙損")]
        OcapAbn3003F = 213002,

        [Description("HEPA流場有異常導致Particle回揚, PA Counter小於下限")]
        OcapAbn3004L = 213003,

        [Description("HEPA流場帶有Particle, 導致光罩汙損, PA Counter高於上限")]
        OcapAbn3004U = 213004,

        [Description("nozzle/CCD/光源等device位移問題 (ex: 機台震動 / 遭受到撞擊)")]
        OcapAbn3005F = 213005,

        [Description("無法取得 or 取錯 Clean chamber nozzle on/off/psi offset information")]
        OcapAbn3006F = 213006,

        [Description("對焦過程中，手臂移動時造成光罩損傷")]
        OcapAbn3007F = 213007,

        [Description("光源控制異常或光源衰退導致檢測異常, 光源照度小於下限")]
        OcapAbn3008L = 213008,

        [Description("光源控制異常或光源衰退導致檢測異常, 光源照度大於上限")]
        OcapAbn3008U = 213009,

        [Description("鏡頭髒污造成誤判為particle")]
        OcapAbn3009F = 213010,

        [Description("CCD異常 無法正常取像")]
        OcapAbn3010F = 213011,

        [Description("手臂靜止時之晃動，影響對焦成像之結果")]
        OcapAbn3011F = 213012,

        [Description("外來強光影響缺陷識別結果")]
        OcapAbn3012F = 213013,

        [Description("吹除時間過久，導致光罩Pellicle損傷")]
        OcapAbn3013F = 213014,

        [Description("吹除次數過多，導致光罩Pellicle損傷")]
        OcapAbn3014F = 213015,

        [Description("nozzle與光罩定距5cm偏移問題 ，導致噴頭過於接近光罩，導致光罩損傷")]
        OcapAbn3015F = 213016,

        [Description("nozzle與光罩角度偏移問題 ，導致吹除角度異常，造成光罩損傷")]
        OcapAbn3016F = 213017,

        [Description("軟體異常導致指令應執行卻未執行")]
        OcapAbn3017F = 213018,

        [Description("Mask run錯recipe，導致影像無法正確對焦")]
        OcapAbn3018F = 213019,

        [Description("手臂移動有誤差，造成前後值影像的位置不同")]
        OcapAbn3019F = 213020,

        [Description("光罩方向/正反面錯誤, 使用不恰當的吹除參數, 造成光罩損壞")]
        OcapAbn3020F = 213021,

        [Description("光源造成Mask傷害 (ex: Pellicle/Pattern)")]
        OcapAbn3021F = 213022,

        [Description("手臂未移動至該吹除的位置")]
        OcapAbn3022F = 213023,

        [Description("光源未關閉")]
        OcapAbn3023F,

        #endregion Clean Chamber

        #region Mask Transfer
        [Description("Robot Shift, 造成定位不準")]
        OcapAbn6001F = 216000,

        [Description("Gripper 與光罩電位差過大造成光罩ESD damage")]
        OcapAbn6002F = 216001,

        [Description("Gripper 移動/取放光罩時，因位置誤差而撞傷光罩")]
        OcapAbn6003F = 216002,

        [Description("Gripper 電流異常造成夾持不穩導致光罩滑落, Mask Tactile Sensor小於下限")]
        OcapAbn6004L = 216003,

        [Description("Gripper 電流異常造成夾持不穩導致光罩滑落, Mask Tactile Sensor大於上限")]
        OcapAbn6004U = 216004,

        [Description("Run錯指令造成手臂異常作動導致夾取或放置失敗")]
        OcapAbn6005F = 216005,

        [Description("閘門(Load Port/Drawer)未開啟, 導致Robot無法執行指令")]
        OcapAbn6006F = 216006,

        [Description("Robot讀值(軟體Bug或硬體報值)異常導致無法定位")]
        OcapAbn6007F = 216007,

        [Description("軟體或硬體異常導致指令應執行卻未執行")]
        OcapAbn6008F = 216008,

        [Description("環境震動過大，影響robot傳送穩定與精準度, 震動Sensor小於下限")]
        OcapAbn6009L = 216009,

        [Description("環境震動過大，影響robot傳送穩定與精準度, 震動Sensor大於上限")]
        OcapAbn6009U = 216010,

        [Description("Gripper已有光罩, 系統卻誤判無光罩, 並進行夾取動作, 造成原夾取光罩掉落")]
        OcapAbn6010F = 216011,


        #endregion

        #region Open Stage
        [Description("OpenStage 無固定裝置，導致開盒過程中光罩盒滑動, AutoSwitch低於下限, 可能發生Box脫落現象")]
        BankInOutAbn4001L = 224000,

        [Description("OpenStage 無固定裝置，導致開盒過程中光罩盒滑動, AutoSwitch高於上限, 可能會造成Box夾壞")]
        BankInOutAbn4001U = 224001,

        [Description("無光罩盒卻進行開盒動作")]
        BankInOutAbn4002F = 224002,

        [Description("開啟光罩盒無光罩，導致後續夾取失敗")]
        BankInOutAbn4003F = 224003,

        [Description("光罩位置偏移，造成夾取失敗, X偏移量小於反向下限")]
        BankInOutAbn4004L = 224004,

        [Description("光罩位置偏移，造成夾取失敗, X偏移量大於正向上限")]
        BankInOutAbn4004U = 224005,

        [Description("光罩位置偏移，造成夾取失敗, Y偏移量小於反向下限")]
        BankInOutAbn4005L = 224006,

        [Description("光罩位置偏移，造成夾取失敗, Y偏移量大於正向上限")]
        BankInOutAbn4005U = 224007,
        
        [Description("光罩位置偏移，造成夾取失敗, Z偏移量小於反向下限")]
        BankInOutAbn4006L = 224008,

        [Description("光罩位置偏移，造成夾取失敗, Z偏移量大於正向上限")]
        BankInOutAbn4006U = 224009,

        [Description("光罩位置偏移，造成夾取失敗, Row偏移量小於反向下限")]
        BankInOutAbn4007L = 224010,

        [Description("光罩位置偏移，造成夾取失敗, Row偏移量大於正向上限")]
        BankInOutAbn4007U = 224011,

        [Description("光罩位置偏移，造成夾取失敗, Yaw偏移量小於反向下限")]
        BankInOutAbn4008L = 224012,

        [Description("光罩位置偏移，造成夾取失敗, Yaw偏移量大於正向上限")]
        BankInOutAbn4008U = 224013,

        [Description("光罩位置偏移，造成夾取失敗, Pitch偏移量小於反向下限")]
        BankInOutAbn4009L = 224014,

        [Description("光罩位置偏移，造成夾取失敗, Pitch偏移量大於正向上限")]
        BankInOutAbn4009U = 224015,

        [Description("光罩未水平放置，造成夾取失敗")]
        BankInOutAbn4010F = 224016,

        [Description("光罩正反面放錯，造成夾取失敗")]
        BankInOutAbn4011F = 224017,

        [Description("光罩方向錯誤，造成夾取失敗")]
        BankInOutAbn4012F = 224018,

        [Description("光罩盒位置偏移，造成夾取失敗, X偏移量小於反向下限")]
        BankInOutAbn4013L = 224019,

        [Description("光罩盒位置偏移，造成夾取失敗, X偏移量大於正向上限")]
        BankInOutAbn4013U = 224020,

        [Description("光罩盒位置偏移，造成夾取失敗, Y偏移量小於反向下限")]
        BankInOutAbn4014L = 224021,

        [Description("光罩盒位置偏移，造成夾取失敗, Y偏移量大於正向上限")]
        BankInOutAbn4014U = 224022,

        [Description("光罩盒位置偏移，造成夾取失敗, Z偏移量小於反向下限")]
        BankInOutAbn4015L = 224023,

        [Description("光罩盒位置偏移，造成夾取失敗, Z偏移量大於正向上限")]
        BankInOutAbn4015U = 224024,

        [Description("光罩盒位置偏移，造成夾取失敗, Row偏移量小於反向下限")]
        BankInOutAbn4016L = 224025,

        [Description("光罩盒位置偏移，造成夾取失敗, Row偏移量大於正向上限")]
        BankInOutAbn4016U = 224026,

        [Description("光罩盒位置偏移，造成夾取失敗, Yaw偏移量小於反向下限")]
        BankInOutAbn4017L = 224027,

        [Description("光罩盒位置偏移，造成夾取失敗, Yaw偏移量大於正向上限")]
        BankInOutAbn4017U = 224028,

        [Description("光罩盒位置偏移，造成夾取失敗, Pitch偏移量小於反向下限")]
        BankInOutAbn4018L = 224029,

        [Description("光罩盒位置偏移，造成夾取失敗, Pitch偏移量大於正向上限")]
        BankInOutAbn4018U = 224030,

        [Description("光罩盒未水平放置，造成夾取失敗")]
        BankInOutAbn4019F = 224031,

        [Description("光罩盒正反面放錯，造成開盒失敗")]
        BankInOutAbn4020F = 224032,

        [Description("光罩盒方向錯誤，造成開盒失敗")]
        BankInOutAbn4021F = 224033,

        [Description("光罩盒放置錯誤stage(水晶盒，鐵盒)")]
        BankInOutAbn4022F = 224034,

        [Description("Mask barcode 讀取失敗")]
        BankInOutAbn4023F = 224035,

        [Description("Lock損壞，卻仍開啟光罩盒")]
        BankInOutAbn4024F = 224036,

        [Description("CCD異常 無法正常取像 導致相機功能失效")]
        BankInOutAbn4025F = 224037,

        [Description("相機遭誤觸碰，造成取像位置或對焦跑掉")]
        BankInOutAbn4026F = 224038,

        [Description("未將光罩盒完全開啟，造成取放光罩異常")]
        BankInOutAbn4027F = 224039,

        [Description("鏡頭髒污造成誤判影像判讀結果")]
        BankInOutAbn4028F = 224040,

        [Description("光源控制異常或光源衰退導致檢測異常, 光源照度低於下限")]
        BankInOutAbn4029L = 224041,

        [Description("光源控制異常或光源衰退導致檢測異常, 光源照度高於上限")]
        BankInOutAbn4029U = 224042,

        [Description("環境中的particle 掉落在光罩上或光罩盒內, PA counter值低於下限")]
        BankInOutAbn4030L = 224043,

        [Description("環境中的particle 掉落在光罩上或光罩盒內, PA counter值高於上限")]
        BankInOutAbn4030U = 224044,

        [Description("人為因素, 造成光罩被放置於錯誤的光罩盒")]
        BankInOutAbn4031F = 224045,

        [Description("Slot Mapping有問題, 取到錯誤的光罩盒")]
        BankInOutAbn4032F = 224046,

        [Description("夾取/下放光罩盒時, 施放過程有誤, 造成光罩盒損壞, Box Tactile Sensor低於下限")]
        BankInOutAbn4033L = 224047,

        [Description("夾取/下放光罩盒時, 施放過程有誤, 造成光罩盒損壞, Box Tactile Sensor高於上限")]
        BankInOutAbn4033U = 224048,

        [Description("夾取/下放光罩盒時, 施放過程有誤, 造成光罩盒損壞, Box Force Sensor低於下限")]
        BankInOutAbn4034L = 224049,

        [Description("夾取/下放光罩盒時, 施放過程有誤, 造成光罩盒損壞, Box Force Sensor高於上限")]
        BankInOutAbn4034U = 224050,

        [Description("夾取/下放光罩至光罩盒時, 施放過程有誤, 造成光罩損壞, Mask Tactile Sensor低於下限")]
        BankInOutAbn4035L = 224051,

        [Description("夾取/下放光罩至光罩盒時, 施放過程有誤, 造成光罩損壞, Mask Tactile Sensor高於上限")]
        BankInOutAbn4035U = 224052,

        [Description("夾取/下放光罩至光罩盒時, 施放過程有誤, 造成光罩損壞, Mask Force Sensor低於下限")]
        BankInOutAbn4036L = 224053,

        [Description("夾取/下放光罩至光罩盒時, 施放過程有誤, 造成光罩損壞, Mask Force Sensor高於上限")]
        BankInOutAbn4036U = 224054,

        [Description("開啟水晶盒上蓋失敗, Box Tactile Sensor低於下限")]
        BankInOutAbn4037L = 224055,

        [Description("開啟水晶盒上蓋失敗, Box Tactile Sensor高於上限")]
        BankInOutAbn4037U = 224056,

        [Description("開啟水晶盒上蓋失敗, Box Force Sensor低於下限")]
        BankInOutAbn4038L = 224057,

        [Description("開啟水晶盒上蓋失敗, Box Force Sensor高於上限")]
        BankInOutAbn4038U = 224058,

        [Description("關閉水晶盒上蓋失敗, Box Tactile Sensor低於下限")]
        BankInOutAbn4039L = 224059,

        [Description("關閉水晶盒上蓋失敗, Box Tactile Sensor高於上限")]
        BankInOutAbn4039U = 224060,

        [Description("關閉水晶盒上蓋失敗, Box Force Sensor低於下限")]
        BankInOutAbn4040L = 224061,

        [Description("關閉水晶盒上蓋失敗, Box Force Sensor高於上限")]
        BankInOutAbn4040U = 224062,
        #endregion Open Stage

        #region Drawer
        [Description("抽屜門鎖故障導致無法取放Box")]
        BankInOutAbn5001F = 225000,

        [Description("Operator 放錯Slot")]
        BankInOutAbn5002F = 225001,

        [Description("Robot 誤動作, 要去取出不應取出的光罩盒")]
        BankInOutAbn5003F = 225002,

        [Description("Particle可能在光罩盒投入時, 連結外部環境造成Particle汙染, PA Counter小於下限 (可能Device異常)")]
        BankInOutAbn5004L = 225003,

        [Description("Particle可能在光罩盒投入時, 連結外部環境造成Particle汙染, PA Counter大於上限")]
        BankInOutAbn5004U = 225004,

        [Description("機械老舊失能")]
        BankInOutAbn5005F = 225005,

        [Description("XCDA流量異常, 流量小於下限")]
        BankInOutAbn5006L = 225006,

        [Description("XCDA流量異常, 流量大於上限")]
        BankInOutAbn5006U = 225007,

        [Description("Drawer密閉空間 濕度超標, 濕度低於下限")]
        BankInOutAbn5007L = 225008,

        [Description("Drawer密閉空間 濕度超標, 濕度高於上限")]
        BankInOutAbn5007U = 225009,

        [Description("Slot閘門開關異常, 造成Box Robot碰撞")]
        BankInOutAbn5008F = 225010,

        [Description("BoxRobot放置不穩")]
        BankInOutAbn5009F = 225011,

        [Description("Process End, Operator太久沒取走Box")]
        BankInOutAbn5010F = 225012,

        [Description("Operator取錯光罩")]
        BankInOutAbn5011F = 225013,
        #endregion Drawer

        #region Box Transfer
        [Description("移動過程中盒子掉落, Box Tactile Sensor低於下限")]
        BankInOutAbn7001L = 227000,

        [Description("移動過程中盒子掉落, Box Tactile Sensor高於上限")]
        BankInOutAbn7001U = 227001,

        [Description("移動過程中盒子掉落, Box Force Sensor低於下限")]
        BankInOutAbn7002L = 227002,

        [Description("移動過程中盒子掉落, Box Force Sensor高於上限")]
        BankInOutAbn7002U = 227003,

        [Description("每次夾持放置BoxStage時座標皆不同(因Drawer存在緩衝空間)")]
        BankInOutAbn7003F = 227004,

        [Description("水晶盒/鐵盒方向錯誤")]
        BankInOutAbn7004F = 227005,

        [Description("水晶盒/鐵盒未上鎖導致光罩掉落")]
        BankInOutAbn7005F = 227006,

        [Description("Slot Mapping報值錯誤")]
        BankInOutAbn7006F = 227007,

        [Description("盒子內光罩存在與否與帳上不符")]
        BankInOutAbn7007F = 227008,

        [Description("從Drawer中取錯光罩盒")]
        BankInOutAbn7008F = 227009,

        [Description("OpenStage已有Box")]
        BankInOutAbn7009F = 227010,

        [Description("Box鎖扣損壞造成開啟/關閉失敗")]
        BankInOutAbn7010F = 227011,

        [Description("上蓋打開到一半掉落")]
        BankInOutAbn7011F = 227012,

        [Description("Box鎖扣已掉落")]
        BankInOutAbn7012F = 227013,

        [Description("開啟Box鎖扣失敗, Box Tactile Sensor低於下限")]
        BankInOutAbn7013L = 227014,

        [Description("開啟Box鎖扣失敗, Box Tactile Sensor高於上限")]
        BankInOutAbn7013U = 227015,

        [Description("開啟Box鎖扣失敗, Box Force Sensor低於下限")]
        BankInOutAbn7014L = 227016,

        [Description("開啟Box鎖扣失敗, Box Force Sensor高於上限")]
        BankInOutAbn7014U = 227017,

        [Description("關閉Box鎖扣失敗, Box Tactile Sensor低於下限")]
        BankInOutAbn7015L = 227018,

        [Description("關閉Box鎖扣失敗, Box Tactile Sensor高於上限")]
        BankInOutAbn7015U = 227019,

        [Description("關閉Box鎖扣失敗, Box Force Sensor低於下限")]
        BankInOutAbn7016L = 227020,

        [Description("關閉Box鎖扣失敗, Box Force Sensor高於上限")]
        BankInOutAbn7016U = 227021,

        [Description("Robot Shift, 造成定位不準")]
        BankInOutAbn7017F = 227022,

        [Description("Gripper 移動/取放光罩時，因位置誤差而撞傷光罩盒")]
        BankInOutAbn7018F = 227023,

        [Description("夾爪夾取力道不適當, 造成光罩盒損害, Box Tactile Sensor低於下限")]
        BankInOutAbn7019L = 227024,

        [Description("夾爪夾取力道不適當, 造成光罩盒損害, Box Tactile Sensor高於上限")]
        BankInOutAbn7019U = 227025
        #endregion Box Transfer
        #endregion Abnormal Processes
    }
}
