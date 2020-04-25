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
    public enum EnumPositionId
    {
        None,
        BoxTrasnfer01,
        CleanCh01,
        Cabinet,
        InspectionCh01,
        LoadPort01,
        LoadPort02,
        MaskTransfer01,
        OpenStage01,
        Cabinet01,
        Cabinet02,
        Cabinet03,
        Cabinet04,
        Cabinet05,
     
    }
}
