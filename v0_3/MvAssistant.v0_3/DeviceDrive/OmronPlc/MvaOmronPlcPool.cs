using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.OmronPlc
{
    public class MvaOmronPlcPool
    {
        public static Dictionary<string, MvaOmronPlcLdd> PlcMapper = new Dictionary<string, MvaOmronPlcLdd>();



        public static MvaOmronPlcLdd Create(string key)
        {
            if (PlcMapper.ContainsKey(key)) throw new Exception("PLC己經存在");
            var plc = PlcMapper[key] = new MvaOmronPlcLdd();
            return plc;
        }

        public static MvaOmronPlcLdd Get(string key)
        {
            if (!PlcMapper.ContainsKey(key)) throw new Exception("PLC不存在");
            var plc = PlcMapper[key];
            return plc;
        }
        public static MvaOmronPlcLdd GetOrDefault(string key)
        {
            if (!PlcMapper.ContainsKey(key)) return null;
            var plc = PlcMapper[key];
            return plc;
        }
        public static MvaOmronPlcLdd GetOrCreate(string key)
        {
            if (!PlcMapper.ContainsKey(key)) PlcMapper[key] = new MvaOmronPlcLdd();
            var plc = PlcMapper[key];
            return plc;
        }

    }
}
