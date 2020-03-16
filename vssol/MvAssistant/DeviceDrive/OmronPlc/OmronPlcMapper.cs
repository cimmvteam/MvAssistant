using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.OmronPlc
{
    public class OmronPlcMapper
    {
        public static Dictionary<string, OmronPlcCompolet> PlcMapper = new Dictionary<string, OmronPlcCompolet>();



        public static OmronPlcCompolet Create(string key)
        {
            if (PlcMapper.ContainsKey(key)) throw new Exception("PLC己經存在");
            var plc = PlcMapper[key] = new OmronPlcCompolet();
            return plc;
        }

        public static OmronPlcCompolet Get(string key)
        {
            if (!PlcMapper.ContainsKey(key)) throw new Exception("PLC不存在");
            var plc = PlcMapper[key];
            return plc;
        }
        public static OmronPlcCompolet GetOrDefault(string key)
        {
            if (!PlcMapper.ContainsKey(key)) return null;
            var plc = PlcMapper[key];
            return plc;
        }
        public static OmronPlcCompolet GetOrCreate(string key)
        {
            if (!PlcMapper.ContainsKey(key)) PlcMapper[key] = new OmronPlcCompolet();
            var plc = PlcMapper[key];
            return plc;
        }

    }
}
