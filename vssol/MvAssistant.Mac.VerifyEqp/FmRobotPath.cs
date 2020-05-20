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
using MvAssistant.Mac.v1_0.JSon;

namespace MaskCleanerVerify
{
    public partial class FmRobotPath : Form
    {
      //  Type PotisionType = typeof(HalRobotMotion);
        private MvFanucRobotLdd ldd = new MvFanucRobotLdd();
        /// <summary>裝置</summary>
        private IList<DeviceInfo> DeviceInfos = null;
        /// <summary>上個訪問的點位</summary>
        private HalRobotMotion TempCurrentPosition { get; set; }
        /// <summary>準備記錄的點位集合</summary>
        private List<PositionInfo>PositionInstances { get; set; }
        public FmRobotPath()
        {
            InitializeComponent();
        }

        /// <summary>點選 Add 按鈕</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            //MvFanucRobotPosReg curr = ldd.ReadCurPosUf();
            //HalRobotMotion motion = new HalRobotMotion();
            /**
                        Fake_MvFanucRobotPosReg currentPosition = Fake_MvFanucRobotPosReg.GetNewInstance();
                        ClassHelper helper = new ClassHelper();
                        //var motion = saver.GetTargetInstanceFromSourceInstance<MvFanucRobotPosReg, HalRobotMotion>(curr, null, true);
                        var motion = helper.ClonPropertiesValue<Fake_MvFanucRobotPosReg, HalRobotMotion>(currentPosition, null, true);
                        */
            var tempPos = Fake_MvFanucRobotPosReg.GetNewInstance();
            var currentMotion = new ClassHelper().ClonPropertiesValue<Fake_MvFanucRobotPosReg, HalRobotMotion>(tempPos, null, true);
            this.TempCurrentPosition = currentMotion;
            PositionInfo newPositionInfo = GetNewPositionInfo();
            this.PositionInstances.Add(newPositionInfo);
            
            RefreshPositionInfoList();
        }



        /// <summary>刪除被選定項目</summary>
        private void ToDelete()
        {
            var selectedItems = this.LstBxPositionInfo.SelectedItems;
            foreach (var item in selectedItems)
            {
                var id = item.ToString().Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
                var selectedItem = this.PositionInstances.Where(m => m.PositionID == id).FirstOrDefault();
                if (selectedItem != null)
                {
                    this.PositionInstances.Remove(selectedItem);
                }
            }
        }

        /// <summary>按下 Delete 鍵</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedItems = this.LstBxPositionInfo.SelectedItems;
                if (selectedItems.Count > 0)
                {
                    ToDelete();
                    RefreshPositionInfoList();
                    MessageBox.Show("完成刪除");
                }
                else
                {
                    MessageBox.Show("請選定要刪除的項目");
                }
            }
            catch(Exception  ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }


        /**
        /// <summary>存檔</summary>
        public void ToSave()
        {
            if (!Directory.Exists(this.CurrentDeviceInfo.Path))
            {
                Directory.CreateDirectory(this.CurrentDeviceInfo.Path);
            }
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(this.PositionInstances);
            StreamWriter sw = new StreamWriter(this.CurrentDeviceInfo.FilePath, false, Encoding.Default);
            sw.Write(json);
            sw.Flush();
            sw.Close();
        }*/


        /// <summary>按下 Save 鍵</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.PositionInstances != null && this.PositionInstances.Any())
                {
                    //ToSave();
                    JSonHelper.SaveInstanceToJsonFile(this.PositionInstances, this.CurrentDeviceInfo.Path, this.CurrentDeviceInfo.FilePath);
                    RefreshPositionInfoList();
                    MessageBox.Show("存檔成功");
                }
                else
                {
                    MessageBox.Show("沒有資料");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>載入</summary>
        /// <returns>int, 載入的筆數</returns>
        private int ToLoad()
        {
            /*
            StreamReader sr = new StreamReader(this.CurrentDeviceInfo.FilePath, Encoding.Default);
            var json=sr.ReadToEnd();
            sr.Close();
            this.PositionInstances = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PositionInfo>>(json);
            return this.PositionInstances.Count;*/
            this.PositionInstances = new List<PositionInfo>();
            this.PositionInstances=JSonHelper.GetInstanceFromJsonFile<List<PositionInfo>>(this.CurrentDeviceInfo.FilePath);
            return this.PositionInstances.Count;
        }

        /// <summary>按入 Load 鍵</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(this.CurrentDeviceInfo.FilePath))
                {
                    var datas=ToLoad();
                    if (datas > 0)
                    {
                        RefreshPositionInfoList();
                        MessageBox.Show("資料己經載入");
                    }
                    else
                    {
                        MessageBox.Show("沒有資料");
                    }
                }
                else
                {
                    MessageBox.Show(this.CurrentDeviceInfo.FilePath + " 不存在");
                }
            }
            catch(Exception ex)
            {

            }
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

        /// <summary>裝置清單及檔案名稱</summary>
        /// <returns></returns>
        private IList<DeviceInfo> GetDeviceInfos()
        {

            var rtnV = new List<DeviceInfo>();
            rtnV.Add(new DeviceInfo { DeviceName = "Mask Robot", DeviceIP = "192.168.0.50" ,FileName="MaskRobotPosition.json"});
            rtnV.Add(new DeviceInfo { DeviceName = "Box Robot", DeviceIP = "192.168.0.51", FileName="BoxRobotPosotion.json" });
            return rtnV;
        }


        /// <summary>初始化存放 Device Name 的下拉選單</summary>
        /// <param name="deviceInfos"></param>
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
                this.TxtBxDevicePath.Text ="File Path: " +this.CurrentDeviceInfo.FilePath;
            }
        }

       
         /// <summary>選用的 Device 選項變動</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbBoxDeviceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Display Device IP
                this.DisplayCurrentDeviceInfoIP();
                // Display Device File Name
                this.DisplayCurrentDeviceInfoPath();
                if (File.Exists(this.CurrentDeviceInfo.FilePath))
                {
                    ToLoad();
                   
                }
                else
                {
                    this.PositionInstances = new List<PositionInfo>();
                }
                RefreshPositionInfoList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        /// <summary>按下 Get 按鈕</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetPosition_Click(object sender, EventArgs e)
        {
            try
            {
                // 取得 MvFanucRobotPosReg 物件
                var tempPos = Fake_MvFanucRobotPosReg.GetNewInstance();
                var currentMotion = new ClassHelper().ClonPropertiesValue<Fake_MvFanucRobotPosReg, HalRobotMotion>(tempPos, null, true);
                this.TempCurrentPosition = currentMotion;
                DisplayGetPosition(currentMotion);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }

        /// <summary>顯示 Get Button 所得的Position資料</summary>
        /// <param name="instance"></param>
        private void DisplayGetPosition(HalRobotMotion instance )
        {
            this.LstBxGetPosition.Items.Clear();
            PropertyInfo[] properties = typeof(HalRobotMotion).GetProperties();
            foreach(var property in properties)
            {
                if (property.CanRead)
                {
                    
                    var axisName = property.Name;
                    var value =property.GetValue(instance);
                    var text =  axisName + ": " + value.ToString() ;
                    this.LstBxGetPosition.Items.Add(text);
                }

            }
            
        }

        /// <summary>按下 => 鈕</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddGet_Click(object sender, EventArgs e)
        {
            PositionInfo newPositionInfo = GetNewPositionInfo();
            if (newPositionInfo != null)
            {
                this.PositionInstances.Add(newPositionInfo);
            }
            RefreshPositionInfoList();
        }

        private void BasicInitial()
        {
            this.LstBxPositionInfo.Items.Clear();
            this.TempCurrentPosition = null;
            this.LstBxGetPosition.Items.Clear();
        }
        private void RefreshPositionInfoList()
        {
            BasicInitial();
            if (this.PositionInstances!=null && this.PositionInstances.Any())
            {
                foreach(var positionInfo in this.PositionInstances)
                {
                    var text = positionInfo.ToString();
                    this.LstBxPositionInfo.Items.Add(text);
                }
            }
        }

        private PositionInfo GetNewPositionInfo()
        {
            PositionInfo positionInfo = null;
            if (this.TempCurrentPosition != null)
            {
                positionInfo = new PositionInfo
                {
                    Position = this.TempCurrentPosition,
                    PositionID = PositionInfo.GetNewInstID()
                };

            }
            return positionInfo;
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
               this.PositionInstances = new List<PositionInfo>();
               this.TempCurrentPosition = null;
                RefreshPositionInfoList();
            }
            catch(Exception ex)
            {
                MessageBox.Show("全部刪除");
            }
        }
    }

    /*
    public class PositionInfo
    {

        public string PositionID { get; set; }
        public HalRobotMotion Position { get; set; }
        public static string GetNewInstID()
        {
            DateTime thisTime = DateTime.Now;
            var rtnV = thisTime.ToString("yyyyMMddHHmmssfff");
            return rtnV;
        }
        public override string ToString()
        {
            string text = PositionID + " | ";
            PropertyInfo[] properties = typeof(HalRobotMotion).GetProperties();
            foreach(var property in properties)
            {
                if (property.CanRead)
                {
                    text = text+ property.Name +": " + property.GetValue(this.Position).ToString() + ", ";
                }
            }
            return text;
        }
    }
    */
    public class ClassHelper
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
        public string FileName { get; set; }
        /// <summary>檔案路徑及名稱</summary>
        public string FilePath
        {
            get
            {
                string rtnV = string.Empty;

                rtnV =this.Path + @"\" + FileName;
                return rtnV;
            }
        }
        public string Path
        {
            get
            {
                string rtnV = string.Empty;
                rtnV = @"D:\Positions" ;
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
