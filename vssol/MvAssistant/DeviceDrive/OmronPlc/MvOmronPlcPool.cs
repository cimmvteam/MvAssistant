using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.OmronPlc
{
    public class MvOmronPlcPool
    {
        public static Dictionary<string, MvOmronPlcLdd> PlcMapper = new Dictionary<string, MvOmronPlcLdd>();



        public static MvOmronPlcLdd Create(string key)
        {
            if (PlcMapper.ContainsKey(key)) throw new Exception("PLC己經存在");
            var plc = PlcMapper[key] = new MvOmronPlcLdd();
            return plc;
        }

        public static MvOmronPlcLdd Get(string key)
        {
            if (!PlcMapper.ContainsKey(key)) throw new Exception("PLC不存在");
            var plc = PlcMapper[key];
            return plc;
        }
        public static MvOmronPlcLdd GetOrDefault(string key)
        {
            if (!PlcMapper.ContainsKey(key)) return null;
            var plc = PlcMapper[key];
            return plc;
        }
        public static MvOmronPlcLdd GetOrCreate(string key)
        {
            if (!PlcMapper.ContainsKey(key)) PlcMapper[key] = new MvOmronPlcLdd();
            var plc = PlcMapper[key];
            return plc;
        }

    }
}
