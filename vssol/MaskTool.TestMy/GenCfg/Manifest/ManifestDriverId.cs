using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.GenCfg.Manifest
{
    public class ManifestDriverId
    {
        #region Assembly Driver ID

        public static Guid LoadPort = new Guid("6B15C3E9-0C45-469B-B67B-171460832B0D");

        #endregion



        public static Guid Inclinometer_OmronPlc = new Guid("84765797-E9F4-45F2-A95B-AB6ED7FAF672");
        public static Guid LaserCollision_OmronPlc = new Guid("58C1C1F7-55FC-4158-9838-9FFA86B3B3C0");
        public static Guid LaserEntry_OmronPlc = new Guid("F8E36467-4887-4839-B2E2-64BABD97C915");
        public static Guid Plc_Omron = new Guid("3C7E058F-4667-451A-8251-8851BE807FB3");
        public static Guid FanucRobot = new Guid("B2E74FC0-FFA0-467B-8F69-208FE628A693");


        public static Guid MaskGripperNrc = new Guid("A995D7DD-7795-4906-B642-16B911D48A00");
        public static Guid MaskGripperFake = new Guid("F91E5CAC-8D68-4B99-997C-138D2A9BA2DF");

        public static Guid BoxGripperFake = new Guid("E4A7500B-A364-4CA3-8859-D75FC689A366");

    }
}
