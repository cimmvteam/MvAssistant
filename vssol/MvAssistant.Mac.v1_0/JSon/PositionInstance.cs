using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.JSon
{
    public class PositionInstance
    {
        /// <summary>獲取點位組態檔案名稱</summary>
        public const string TransferRobotPathConfigSetConfigFile=  @"D:\Positions\RobotPathGetter\JSon\RobotPathFileConfigSetFile.json";
        /// <summary>Mask Transfer Robot 點位資料檔路徑</summary>
        public const string MTR_Path = @"D:\Positions\MTRobot\";
        /// <summary>Box Transfer Robot 點位資料檔路徑</summary>
        public const string BTR_Path = @"D:\Positions\BTRobot\";
        /// <summary>Mask Transfer Robot Device Name</summary>
        public const string MaskTransferRobotDeviceName = "Mask_Robot";
        /// <summary>Box Transfer Robot Device Name</summary>
        public const string BoxTransferRobotDeviceName = "Box_Robot";
        /// <summary>取得 PositionInstance 的 Instance 時鎖定的物件</summary>
        private readonly static object _lockObj = new object();
        /// <summary> PositionInstance 的 Instance </summary>
        private static PositionInstance _instance = null;
        /// <summary>各點位內容集合</summary>
        ///<remarks>
        ///<para>key: 存放路徑點位的完整檔案名稱</para>
        ///<para>value: 路徑點位檔案內所有點位的集合</para>
        /// </remarks>
        private IDictionary<string, List<PositionInfo>> _dicPositionInfos = new Dictionary<string, List<PositionInfo>>();
        /// <summary>建構式</summary>
        private PositionInstance() { }

        /// <summary>取得檔案實體並載入所有點位資料</summary>
        /// <returns></returns>
        public static PositionInstance GetInstance()
        {
            ///<summary>設定點位內容集合資料</summary>
            Action<string, string> setDicPositionInfos = (positionFileName,deviceName) =>
            {
                var RobotPathConfigSetConfig = JSonHelper.GetInstanceFromJsonFile<List<RobotPathConfig>>(TransferRobotPathConfigSetConfigFile);
                var configDetail = RobotPathConfigSetConfig.Where(m => m.DeviceName == deviceName).First().ConfigDetail;
                foreach (var config in configDetail)
                {
                    var key = positionFileName + config.PositionFileName;
                    List<PositionInfo> positions = default(List<PositionInfo>);
                    if (File.Exists(key))
                    {
                        positions = JSonHelper.GetInstanceFromJsonFile<List<PositionInfo>>(key);
                    }
                    _instance._dicPositionInfos.Add(key, positions);
                }
            };
            if (_instance == null)
            {
                lock (_lockObj)
                {
                    if (_instance == null)
                    {
                        _instance = new PositionInstance();
                        setDicPositionInfos(MTR_Path, MaskTransferRobotDeviceName);
                        setDicPositionInfos(BTR_Path, BoxTransferRobotDeviceName);
                    }
                }
            }
            return _instance;
        }

        /// <summary>取得指定路徑的點位資料</summary>
        /// <param name="filePath">指定路徑的檔案名稱</param>
        /// <returns></returns>
        public List<PositionInfo> GetPositionInfos(string filePath)
        {
            var key = filePath;
            if (_dicPositionInfos == default(List<PositionInfo>))
            {
                return default(List<PositionInfo>);
            }
            else if (_dicPositionInfos.ContainsKey(key))
            {
                return _dicPositionInfos[key];
            }
            else
            {
                return default(List<PositionInfo>);
            }
        }

        /// <summary>取得檔案實體並載入所有點位資料</summary>
        public static void Load()
        {
            GetInstance();
        }

        /// <summary>重新取得檔案實體並重載所有點位資料</summary>
        public static void ReLoad()
        {
            _instance = null;
            Load();
        }
    }
}
