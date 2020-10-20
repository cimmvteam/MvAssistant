using MvAssistant.Mac.v1_0.Hal.CompRobot;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
//using static System.Net.Mime.MediaTypeNames;

namespace MvAssistant.Mac.v1_0.JSon
{
    /// <summary>擷取點位時的點位組態</summary>
    public class RobotPathFileConfigSet
    {
        private readonly static string RobotPathConfigSetConfigFile = Application.StartupPath + "\\JSon\\" + "RobotPathFileConfigSetFile.json";
        private readonly static object lockObject = new object();
        private static RobotPathFileConfigSet Instance = null;
        private List<RobotPathConfig> _robotConfigSet = null;

         // vs 2013
        // public List<RobotPathConfig> RobotConfigSet => _robotConfigSet;
        public List<RobotPathConfig> RobotConfigSet { get { return _robotConfigSet;  } }

        public static RobotPathFileConfigSet GetInstance()
        {
            if (Instance == null)
            {
                lock (lockObject)
                {
                    if (Instance == null)
                    {
                        Instance = new RobotPathFileConfigSet();
                        Instance._robotConfigSet = JSonHelper.GetInstanceFromJsonFile<List<RobotPathConfig>>(RobotPathConfigSetConfigFile);
                    }
                }
            }
            return Instance;
        }
        public RobotPathConfigDetail GetCurrentConfigDetail(string deviceName,int serialNumber)
        {
            var config = GetCurrentConfig(deviceName);
            if (config == null) { return null; }
            else
            {
                var configDetail = config.ConfigDetail.Where(m => m.SerialNumber == serialNumber).FirstOrDefault();
                return configDetail;
            }
        }

        public RobotPathConfig GetCurrentConfig(string deviceName)
        {
            RobotPathConfig rtnV;
            rtnV = _robotConfigSet.Where(m => m.DeviceName.Equals(deviceName)).FirstOrDefault();
            return rtnV;
        }
        private RobotPathFileConfigSet()
        {
          
        }
    }

    /// <summary>點位組態 Header</summary>
    /// <typeparam name="T"></typeparam>
    public class RobotPathConfig
    {
        public RobotPathConfig()
        {

        }
        public RobotPathConfig(string positiontfileDirection):this()
        {

        }
        /// <summary>裝置名稱</summary>
        public string DeviceName { get; set; }
        /// <summary>裝置IP</summary>
        public string DeviceIP { get; set; }
        /// <summary>點位檔案資料夾</summary>
        public string PositionFileDirectory { get; set; }
        /// <summary>組態檔內容明細</summary>
        public List<RobotPathConfigDetail> ConfigDetail  { get;set; }
        
    }

    /// <summary>組態檔內容明細</summary>
    public  class RobotPathConfigDetail
    {
        public RobotPathConfigDetail()
        {
        }
        /// <summary>序號</summary>
        public int SerialNumber { get; set; }
        /// <summary>點位檔檔案名</summary>
        public string PositionFileName { get; set; }
        /// <summary>用途描述</summary>
        public string UsageDescription { get; set; }
        /// <summary>內容說明</summary>
        public string PositionFileDescription { get; set; }
        public HalRobotEnumMotionType MotionType { get; set; }
        /// <summary>取得點位檔全名</summary>
        public string GetPositionFileFullName(string directoryWithEndSign)
        {
             return  directoryWithEndSign + PositionFileName;
        }
       public virtual string ListItemDescription
        {
            get
            {
                var rtnV = SerialNumber.ToString("000000") + "   |   " + PositionFileName + "   |   " + UsageDescription  + "   |   " + PositionFileDescription;
                return rtnV;
            }

        }
    }
    
}

