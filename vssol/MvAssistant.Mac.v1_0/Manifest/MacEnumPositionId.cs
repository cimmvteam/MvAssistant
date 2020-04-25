using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Manifest
{
    /// <summary>
    /// 實體物理位置識別字符
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MacEnumPositionId
    {
        None,

        #region Assembly

        BoxTrasnfer01,
        CleanCh01,
        Cabinet01,
        InspectionCh01,
        LoadPort01,
        LoadPort02,
        MaskTransfer01,
        OpenStage01,
        #endregion




        #region Drawer

        Drawer01,
        Drawer02,
        Drawer03,
        Drawer04,
        Drawer05,
        Drawer06,
        Drawer07,
        Drawer08,
        Drawer09,
        Drawer10,
        Drawer11,
        Drawer12,
        Drawer13,
        Drawer14,
        Drawer15,
        Drawer16,
        Drawer17,
        Drawer18,
        Drawer19,
        Drawer20,
        Drawer21,
        Drawer22,
        Drawer23,
        Drawer24,
        Drawer25,
        Drawer26,
        Drawer27,
        Drawer28,
        Drawer29,
        Drawer30,
        Drawer31,
        Drawer32,
        Drawer33,
        Drawer34,
        Drawer35,

        #endregion


    }
}
