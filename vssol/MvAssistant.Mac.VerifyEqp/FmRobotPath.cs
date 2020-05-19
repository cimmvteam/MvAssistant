using MvAssistant.DeviceDrive.FanucRobot_v42_15;
using MvAssistant.Mac.v1_0.Hal.Component.Robot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace MaskCleanerVerify
{
    public partial class FmRobotPath : Form
    {
        MvFanucRobotLdd ldd = new MvFanucRobotLdd();
        IList<DeviceInfo> DeviceInfos = null;
        private HalRobotMotion tempCurrentPosition { get; set; }
        public FmRobotPath()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            //MvFanucRobotPosReg curr = ldd.ReadCurPosUf();
            //HalRobotMotion motion = new HalRobotMotion();

            Fake_MvFanucRobotPosReg currentPosition = Fake_MvFanucRobotPosReg.GetNewInstance();
            PositionSaver saver = new PositionSaver();
            //var motion = saver.GetTargetInstanceFromSourceInstance<MvFanucRobotPosReg, HalRobotMotion>(curr, null, true);
            var motion = saver.ClonPropertiesValue<Fake_MvFanucRobotPosReg, HalRobotMotion>(currentPosition, null, true);

        }
        private void DisplayCurrentPositions()
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void FmRobotPath_Load(object sender, EventArgs e)
        {
            this.DeviceInfos = GetDeviceInfos();
            this.InitialCmbBoxDeviceName(DeviceInfos);
            return;
         
            ldd.RobotIp = "192.168.0.51";
            System.Diagnostics.Debug.Assert(ldd.ConnectIfNo() == 0);

            System.Diagnostics.Debug.Assert(ldd.AlarmReset());

            System.Diagnostics.Debug.Assert(ldd.StopProgram() == 0);
            System.Diagnostics.Debug.Assert(ldd.ExecutePNS("PNS0101"));





        }

        /// <summary>裝署清單及檔案名稱</summary>
        /// <returns></returns>
        private IList<DeviceInfo> GetDeviceInfos()
        {

            var rtnV = new List<DeviceInfo>();
            rtnV.Add(new DeviceInfo { DeviceName = "Mask Robot", DeviceIP = "192.168.0.50" ,XmlFileName="MaskRobotPosition.xml"});
            rtnV.Add(new DeviceInfo { DeviceName = "Box Robot", DeviceIP = "192.168.0.51", XmlFileName="BoxRobotPosotion.xml" });
            return rtnV;
        }

        private void InitialCmbBoxDeviceName(IList<DeviceInfo> deviceInfos)
        {
            CmbBoxDeviceName.Items.Clear();
            var deviceNames = deviceInfos.Select(m => m.DeviceName).ToList();
            if (deviceNames.Any())
            {
                CmbBoxDeviceName.Items.AddRange(deviceNames.ToArray());
                CmbBoxDeviceName.SelectedIndex = 0;
            }
        }

        /// <summary>選定的 Device Info</summary>
        private DeviceInfo CurrentDeviceInfo
        {
            get
            {
                if (this.CmbBoxDeviceName.Text != string.Empty)
                {
                    var deviceInfo = this.DeviceInfos.Where(m => m.DeviceName == CmbBoxDeviceName.Text).FirstOrDefault();
                    return deviceInfo;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>顯示目前選用 Device 的IP</summary>
        private void DisplayCurrentDeviceInfoIP()
        {
            if (this.CurrentDeviceInfo == null)
            {
                txtBxDeviceIP.Text = "";
            }
            else
            {
                txtBxDeviceIP.Text ="IP: " + this.CurrentDeviceInfo.DeviceIP;
            }
        }

        /// <summary>顯示目前選用 Device 的對應檔案</summary>
        private void DisplayCurrentDeviceInfoPath()
        {
            if (this.CurrentDeviceInfo == null)
            {
                TxtBxDevicePath.Text = "";
            }
            else
            {
                this.TxtBxDevicePath.Text ="File Path: " +this.CurrentDeviceInfo.XmlFilePath;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionName"></param>
        private void DisplayPositionDetail(string positionName)
        {
           
        }

        /// <summary>載入、顯示 點位清單</summary>
        private bool? DisplayPositionHeaders(bool isInitial,out string headerName)
        {
            headerName = string.Empty;
            this.LstBxPositionHeaders.Items.Clear();
            if (File.Exists(this.CurrentDeviceInfo.XmlFilePath))
            {

                if (LstBxPositionDetail.Items.Count > 0)
                {
                    if (isInitial)
                    {
                        LstBxPositionDetail.SelectedIndex = 0;
                    }
                    return true;
                }
                else
                {
                    return null;
                }
                
            }
            else
            {
                return false;
            }
        }
        /// <summary>選用的 Device 選項變動</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbBoxDeviceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Display Device IP
            this.DisplayCurrentDeviceInfoIP();
            // Display Device File Name
            this.DisplayCurrentDeviceInfoPath();
            // 載入、顯示點位清單(Header)
            string positionHeaderID;
            bool? hasHeaders=  this.DisplayPositionHeaders(true,out positionHeaderID);
            // 載入顯示點位明細(Detail)
            if (hasHeaders.HasValue && (bool)hasHeaders)
            { 
                // 有檔案存在, 且
                this.DisplayPositionDetail(positionHeaderID);
            }
            else
            {
                LstBxPositionDetail.Items.Clear();
            }
          
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void btnGetPosition_Click(object sender, EventArgs e)
        {
            try
            {
                var FakePos = Fake_MvFanucRobotPosReg.GetNewInstance();
                var currentMotion = new PositionSaver().ClonPropertiesValue<Fake_MvFanucRobotPosReg, HalRobotMotion>(FakePos, null, true);
                this.tempCurrentPosition = currentMotion;
                DisplayGetPosition<HalRobotMotion>(currentMotion);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.LstBxGetPosition.Items.Clear();
            }
        }

        /// <summary>顯示 Get Button 所得的Position資料</summary>
        /// <typeparam name="TDisplayInstanceType"></typeparam>
        /// <param name="instance"></param>
        private void DisplayGetPosition<TDisplayInstanceType>(TDisplayInstanceType instance )
        {
            this.LstBxGetPosition.Items.Clear();
            PropertyInfo[] properties = typeof(TDisplayInstanceType).GetProperties();
            foreach(var property in properties)
            {
                if (property.CanRead && property.CanWrite)
                {
                    
                    var axisName = property.Name;
                    var value =property.GetValue(instance);
                    var text =  axisName + ": " + value.ToString() ;
                    this.LstBxGetPosition.Items.Add(text);
                }

            }
            
        }
    }

    public class PositionHeader<TPositionType>
    {

        public string InstID { get; set; }
        public TPositionType Header { get; set; }
        public static string GetNewInstID()
        {
            DateTime thisTime = DateTime.Now;
            var rtnV = thisTime.ToString("yyyyMMddHHmmssfff");
            return rtnV;
        }
    }

    public class PositionSaver
    {
        /// <summary>從 TSourceType(來源) 型態的物件複製一份屬性資料到 TTargetType(目標) 型態的物件</summary>
        /// <typeparam name="TSourceType">來源物件的型態</typeparam>
        /// <typeparam name="TTargetType">具有預設建構式之目標物件型別</typeparam>
        /// <param name="sourceObj">來源物件</param>
        /// <param name="ignorePropertyNameCase">是否忽略屬性名稱大小寫(若僅大小寫不同而屬性名稱相同的視為相同屬性)</param>
        /// <param name="propertyNameMapping"></param>
        /// <returns>TTargetType 物件實體</returns>
        public TTargetType ClonPropertiesValue<TSourceType,TTargetType>(TSourceType sourceObj,IDictionary<string,string> propertyNameMapping,bool ignorePropertyNameCase=false)
           where TTargetType:new()
        {
            /**
             * 取得來源物件中特定屬性名稱的屬性值
             */ 
            Func<string,object> GetSourceValue = (propertyName) =>
            {
                object rtnV = null;
                PropertyInfo[] sourceProperties = typeof(TSourceType).GetProperties();
                var sourcePropertyNames = sourceProperties.Select(m=>m.Name).ToList();
                string sourcePropertyName = null;
                if (ignorePropertyNameCase)
                {   // 大小寫屬性視為相同
                    sourcePropertyName = sourcePropertyNames.Where(m =>m.ToUpper()== propertyName.ToUpper()).FirstOrDefault();
                }
                else
                {
                    sourcePropertyName = sourcePropertyNames.Where(m => m == propertyName).FirstOrDefault();
                }
                if (sourcePropertyName != null)
                {
                    var sourceProperty = sourceProperties.Where(m => m.Name == sourcePropertyName).FirstOrDefault();
                    if (sourceProperty != null )
                    {
                        rtnV =  sourceProperty.GetValue(sourceObj);
                    }
                }
                return rtnV;
            };
            var targetProperties = typeof(TTargetType).GetProperties();
            TTargetType targetInstance = new TTargetType();
            foreach (PropertyInfo property in targetProperties)
            {
                if (property.CanWrite)
                {
                    var sourceValue = GetSourceValue(property.Name);
                    if (sourceValue != null)
                    {
                        property.SetValue(targetInstance, sourceValue);
                    }
                }
            }
            
            
            if (propertyNameMapping!=null && propertyNameMapping.Any())
            {
                var dicKeys = propertyNameMapping.Keys.ToList();
                foreach(var key in dicKeys)
                {
                    string sourcePropertyName ;
                    var tryResult=propertyNameMapping.TryGetValue(key, out sourcePropertyName);
                    if (tryResult)
                    {
                        PropertyInfo targetProperty = null;
                        if (ignorePropertyNameCase)
                        {   // 大小寫視為相同
                            targetProperty = targetProperties.Where(m => m.Name.ToUpper() == key.ToUpper()).FirstOrDefault();
                        }
                        else
                        { targetProperty = targetProperties.Where(m => m.Name == key).FirstOrDefault(); }
                        if (targetProperty != null && targetProperty.CanWrite)
                        {
                            var sourceValue = GetSourceValue(sourcePropertyName);
                            if (sourceValue != null)
                            {
                                targetProperty.SetValue(targetInstance, sourceValue);
                            }
                        }
                    }

                }
            }
            
            return targetInstance;
        } 
    }

    public class DeviceInfo
    {
        /// <summary>裝置名稱</summary>
        public string DeviceName { get; set; }
        /// <summary>裝置 IP</summary>
        public string DeviceIP { get; set; }
        /// <summary>檔案名稱(不含路徑)</summary>
        public string XmlFileName { get; set; }
        /// <summary>檔案路徑及名稱</summary>
        public string XmlFilePath {
            get
            {
                string rtnV = string.Empty;
                rtnV = Application.StartupPath + XmlFileName;
                return rtnV;
            }
        }

    }

    #region 
    public class Fake_MvFanucRobotPosReg
    {
        private static Random RandomInst = null; 
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public float w { get; set; }
        public float p { get; set; }
        public float r { get; set; }
        public float e1 { get; set; }
        public float e2 { get; set; }
        public float e3 { get; set; }

        public float j1 { get; set; }
        public float j2 { get; set; }
        public float j3 { get; set; }
        public float j4 { get; set; }
        public float j5 { get; set; }
        public float j6 { get; set; }
        public float j7  { get; set; }
        public float j8 { get; set; }
        public float j9 { get; set; }

        public short c1 { get; set; }
        public short c2 { get; set; }
        public short c3 { get; set; }
        public short c4 { get; set; }
        public short c5 { get; set; }
        public short c6 { get; set; }
        public short c7 { get; set; }

        public static float GetFakePosition()
        {
            if (RandomInst == null)
            {
                RandomInst = new Random();
            }
            float rtnV =((RandomInst.Next(1, 10000) + 1) / 100f);
            return rtnV;
        }

        public static Fake_MvFanucRobotPosReg GetNewInstance()
        {

            Fake_MvFanucRobotPosReg rtnInstnce = new Fake_MvFanucRobotPosReg
            {
                x = Fake_MvFanucRobotPosReg.GetFakePosition(),
                y = Fake_MvFanucRobotPosReg.GetFakePosition(),
                z = Fake_MvFanucRobotPosReg.GetFakePosition(),
                w = Fake_MvFanucRobotPosReg.GetFakePosition(),
                p = Fake_MvFanucRobotPosReg.GetFakePosition(),
                r = Fake_MvFanucRobotPosReg.GetFakePosition(),
                e1 = Fake_MvFanucRobotPosReg.GetFakePosition(),
                e2 = Fake_MvFanucRobotPosReg.GetFakePosition(),
                e3 = Fake_MvFanucRobotPosReg.GetFakePosition(),
                j1 = Fake_MvFanucRobotPosReg.GetFakePosition(),
                j2 = Fake_MvFanucRobotPosReg.GetFakePosition(),
                j3 = Fake_MvFanucRobotPosReg.GetFakePosition(),
                j4 = Fake_MvFanucRobotPosReg.GetFakePosition(),
                j5 = Fake_MvFanucRobotPosReg.GetFakePosition(),
                j6 = Fake_MvFanucRobotPosReg.GetFakePosition(),
                j7 = Fake_MvFanucRobotPosReg.GetFakePosition(),
                j8 = Fake_MvFanucRobotPosReg.GetFakePosition(),
                j9 = Fake_MvFanucRobotPosReg.GetFakePosition(),
            };
            return rtnInstnce;
        }
    }
   

    #endregion


}
