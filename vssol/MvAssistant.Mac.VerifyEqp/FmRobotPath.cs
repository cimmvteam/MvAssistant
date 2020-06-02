#define NO_DEVICE //無裝置可連接時要 define 這個條件
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
        /// <summary>最後訪問的點位</summary>
        private HalRobotMotion TempCurrentPosition { get; set; }
        /// <summary>準備記錄點位的集合</summary>
        private List<PositionInfo> PositionInstances { get; set; }

        /// <summary>Robot Path 資料的組態</summary>
        public RobotPathFileConfigSet RobotPathFileConfigSet { get; set; }

        public FmRobotPath()
        {
            InitializeComponent();
        }




        /// <summary>取得 點位資料中的最大序號</summary>
        /// <returns></returns>
        private int GetPositionInstancesMaxSn()
        {
            if (PositionInstances == null || !PositionInstances.Any())
            {
                return (int)NumUdpSn.Minimum - 1;
            }
            else
            {
                var maxSn = PositionInstances.Max(m => m.Sn);
                return maxSn;
            }
        }


        /// <summary>點位資料的 序號  是否存在</summary>
        /// <param name="sn">序號</param>
        /// <returns></returns>
        private bool IsSnExist(int sn)
        {
            if (PositionInstances == null || !PositionInstances.Any())
            {
                return false;
            }
            else
            {
                var inst = PositionInstances.Where(m => m.Sn == sn).FirstOrDefault();
                if (inst == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }


        /// <summary>找到序號為特定值的點位資料, 刪除它</summary>
        /// <param name="serNo">序號</param>
        private void RemovePositionBySerialNum(int serNo)
        {
            if (this.PositionInstances == null || !this.PositionInstances.Any())
            {
                return;
            }
            var position = this.PositionInstances.Where(m => m.Sn == serNo).FirstOrDefault();
            if (position != null)
            {
                this.PositionInstances.Remove(position);
            }
        }

        /// <summary>取得點位資料中最大序號的下個序號 </summary>
        /// <returns></returns>
        private int GetPositionInstancesNextSn()
        {
            var rtnV = GetPositionInstancesMaxSn();
            return ++rtnV;
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

            try
            {

                int? sn = GetSnFromController();
                if (!sn.HasValue || (int)sn < 1)
                {
                    MessageBox.Show("請將 Serial No 欄位設為大於0的整數");
                    return;
                }
                DialogResult dialogResult = DialogResult.None;
                if (IsSnExist(sn.Value))
                {
                    dialogResult = MessageBox.Show("Serial No : " + sn + " 已經存在,要繼續嗎?\n請按 [是]自動編號、[否]覆蓋原有資料、[取消]重新設定", "", MessageBoxButtons.YesNoCancel);
                }
                if (dialogResult == DialogResult.Yes)
                {// 自動編號
                    sn = this.GetPositionInstancesNextSn();
                }
                else if (dialogResult == DialogResult.No)
                {// 覆蓋原有資料
                 // 找到原資料刪除之   

                    this.RemovePositionBySerialNum(sn.Value);
                }
                else if (dialogResult == DialogResult.Cancel)
                {  // 取消
                    return;
                }

#if NO_DEVICE
                var currentPos = Fake_MvFanucRobotPosReg.GetNewInstance();
                var currentMotion = new ClassHelper().ClonPropertiesValue<Fake_MvFanucRobotPosReg, HalRobotMotion>(currentPos, null, true);
#else
                var currentPos = GetCurrentPosUf();
                var currentMotion = new ClassHelper().ClonPropertiesValue<MvFanucRobotPosReg, HalRobotMotion>(currentPos, null, true);
#endif                
                currentMotion.Speed = GetSpeedFromControlle();
                currentMotion.MotionType = GetMotionType();
                this.TempCurrentPosition = currentMotion;
                PositionInfo newPositionInfo = GetNewPositionInfo(sn.Value);

                this.PositionInstances.Add(newPositionInfo);

                RefreshPositionInfoList();
                NumUdpSn.Value = this.GetPositionInstancesNextSn();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public int? GetSnFromController()
        {
            int sn;
            var b = int.TryParse(NumUdpSn.Text, out sn);
            if (b) { return sn; }
            else { return default(int?); }
        }


        /// <summary>刪除被選定項目</summary>
        private void ToDelete()
        {
            var selectedItems = this.LstBxPositionInfo.SelectedItems;
            foreach (var item in selectedItems)
            {
                var sn = Convert.ToInt32(item.ToString().Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries)[0].Trim());
                var selectedItem = this.PositionInstances.Where(m => m.Sn == sn).FirstOrDefault();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }




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
                    var currentConfig = this.RobotPathFileConfigSet.GetCurrentConfig(CmbBoxDeviceName.Text);
                    JSonHelper.SaveInstanceToJsonFile(this.PositionInstances, currentConfig.PositionFileDirectory, TxtBxDevicePath.Text);
                    RefreshPositionInfoList();
                    MessageBox.Show("存檔成功");
                }
                else
                {
                    MessageBox.Show("沒有資料");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>載入</summary>
        /// <returns>int, 載入的筆數</returns>
        private int ToLoad()
        {

            this.PositionInstances = new List<PositionInfo>();
            this.PositionInstances = JSonHelper.GetInstanceFromJsonFile<List<PositionInfo>>(TxtBxDevicePath.Text);
            return this.PositionInstances.Count;
        }

        /// <summary>按入 Load 鍵</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(TxtBxDevicePath.Text))
                {
                    var datas = ToLoad();
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
                    MessageBox.Show(TxtBxDevicePath.Text + " 不存在");
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>Motion Type 選單初始化,預設停在 Position </summary>
        private void InitialMotionTypeList()
        {
            this.CmbBoxMotionType.Items.Clear();
            var motionTypeName = Enum.GetNames(typeof(HalRobotEnumMotionType));
            this.CmbBoxMotionType.Items.AddRange(motionTypeName);
            this.CmbBoxMotionType.Text = HalRobotEnumMotionType.Position.ToString();

        }


        private void FmRobotPath_Load(object sender, EventArgs e)
        {
            try
            {
                this.RobotPathFileConfigSet = RobotPathFileConfigSet.GetInstance();
                this.InitialCmbBoxDeviceName();
                this.InitialMotionTypeList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




        /// <summary>初始化存放 Device的下拉選單, 預設停在第0項</summary>
        private void InitialCmbBoxDeviceName()
        {
            CmbBoxDeviceName.Items.Clear();
            var deviceNames = this.RobotPathFileConfigSet.RobotConfigSet.Select(m => m.DeviceName).ToList();
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
                    /////  var deviceInfo = this.DeviceInfos.Where(m => m.DeviceName == CmbBoxDeviceName.Text).FirstOrDefault();
                    /////   return deviceInfo;
                    return null;
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
            var currentConfig = this.RobotPathFileConfigSet.GetCurrentConfig(CmbBoxDeviceName.Text);
            if (currentConfig == null)
            {
                txtBxDeviceIP.Text = "";
            }
            else
            {
                txtBxDeviceIP.Text = currentConfig.DeviceIP;
            }
        }




        /// <summary>顯示路徑檔案清單</summary>
        private void DisplayCurrentPathFileItems()
        {
            // 現在使用的 Device Config
            var currentRobotConfig = this.RobotPathFileConfigSet.GetCurrentConfig(CmbBoxDeviceName.Text);

            LstBxJsonList.Items.Clear();
            var s = currentRobotConfig.ConfigDetail.Select(m => m.ListItemDescription).ToArray();
            if (currentRobotConfig != null)
            {

                LstBxJsonList.Items.AddRange(s);
            }
        }

        /// <summary>選用的 Device 選項變動</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbBoxDeviceName_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                // Display Position File I
                this.DisplayCurrentPathFileItems();
                // Display Device IP
                this.DisplayCurrentDeviceInfoIP();
#if NO_DEVICE
#else
                groupBox1.Enabled = false;
                ldd = new MvFanucRobotLdd();
                this.ldd.RobotIp = this.RobotPathFileConfigSet.GetCurrentConfig(CmbBoxDeviceName.Text).DeviceIP;
                if (ldd.ConnectIfNo() != 0)
                { throw new Exception("無法連接裝置"); }
                ldd.ExecutePNS("PNS0101");
                groupBox1.Enabled = true;
#endif

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " (尚未完初始化,無法繼續操作)");
            }
            //   NumUdpSn.Value = this.GetPositionInstancesNextSn();
            //   this.Enabled = true;
            this.PositionInstances = new List<PositionInfo>();
            RefreshPositionInfoList();
            TxtBxDevicePath.Text = string.Empty;
            NumUdpSn.Value = this.GetPositionInstancesNextSn();
        }

        /// <summary>按下 Get 按鈕</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetPosition_Click(object sender, EventArgs e)
        {


            try
            {
                // 取得 MvFanucRobotPosReg 物件

                var speed = GetSpeedFromControlle();
                var motionType = GetMotionType();
#if NO_DEVICE
                var currentPos = Fake_MvFanucRobotPosReg.GetNewInstance();
                var currentMotion = new ClassHelper().ClonPropertiesValue<Fake_MvFanucRobotPosReg, HalRobotMotion>(currentPos, null, true);
#else
                var currentPos = this.GetCurrentPosUf();
                var currentMotion = new ClassHelper().ClonPropertiesValue<MvFanucRobotPosReg, HalRobotMotion>(currentPos, null, true);
#endif             
                currentMotion.Speed = speed;
                currentMotion.MotionType = motionType;

                this.TempCurrentPosition = currentMotion;
                DisplayGetPosition(currentMotion);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private HalRobotEnumMotionType GetMotionType()
        {
            HalRobotEnumMotionType rtnV;
            var b = Enum.TryParse<HalRobotEnumMotionType>(CmbBoxMotionType.Text, out rtnV);
            if (!b)
            {
                rtnV = HalRobotEnumMotionType.Position;
            }
            return rtnV;
        }

        private int GetSpeedFromControlle()
        {
            int rtnV;
            var b = int.TryParse(NumUdpSpeed.Text, out rtnV);
            if (!b) { NumUdpSpeed.Text = "0"; rtnV = 0; }
            return rtnV;
        }


        /// <summary>顯示 Get Button 所得的Position資料</summary>
        /// <param name="instance"></param>
        private void DisplayGetPosition(HalRobotMotion instance)
        {
            this.LstBxGetPosition.Items.Clear();
            PropertyInfo[] properties = typeof(HalRobotMotion).GetProperties();
            var text = "";
            foreach (var property in properties)
            {
                if (property.CanRead)
                {
                    var axisName = property.Name;
                    var value = property.GetValue(instance);
                    text += axisName + ": " + value.ToString() + ", ";
                }
            }
            text += "Speed: " + instance.Speed + ", MotionType: " + instance.MotionType.ToString();
            this.LstBxGetPosition.Items.Add(text);
        }

        /// <summary>按下 => 鈕</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddGet_Click(object sender, EventArgs e)
        {
            PositionInfo newPositionInfo = GetNewPositionInfo();
            if (newPositionInfo == null) { return; }
            int? sn = GetSnFromController();
            if (!sn.HasValue || (int)sn < 1)
            {
                MessageBox.Show("請將 Serial No 欄位設為大於0的整數");
                return;
            }
            DialogResult dialogResult = DialogResult.None;
            if (IsSnExist(sn.Value))
            {
                dialogResult = MessageBox.Show("Serial No : " + sn + " 已經存在，要繼續嗎?\n請按 [是]自動編號、[否]覆蓋原有資料、[取消]重新設定、", "", MessageBoxButtons.YesNoCancel);
            }
            if (dialogResult == DialogResult.Yes)
            {// 自動編號
                sn = this.GetPositionInstancesNextSn();
            }
            else if (dialogResult == DialogResult.No)
            {// 覆蓋原有資料
             // 找到原資料刪除之   

                this.RemovePositionBySerialNum(sn.Value);
            }
            else if (dialogResult == DialogResult.Cancel)
            {  // 取消
                return;
            }
            if (newPositionInfo != null)
            {
                newPositionInfo.Sn = sn.Value;
                this.PositionInstances.Add(newPositionInfo);
            }
            RefreshPositionInfoList();
            NumUdpSn.Value = this.GetPositionInstancesNextSn();
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
            if (this.PositionInstances != null && this.PositionInstances.Any())
            {
                PositionInstances = PositionInstances.OrderBy(m => m.Sn).ToList();
                foreach (var positionInfo in this.PositionInstances)
                {
                    var text = positionInfo.ToString();
                    this.LstBxPositionInfo.Items.Add(text);
                }
            }
        }
        private PositionInfo GetNewPositionInfo(int sn)
        {
            var rtnV = GetNewPositionInfo();
            rtnV.Sn = sn;
            return rtnV;
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
            catch (Exception ex)
            {
                MessageBox.Show("全部刪除");
            }
        }

        private MvFanucRobotPosReg GetCurrentPosUf()
        {

            // if (ldd.ConnectIfNo() != 0)
            // { throw new Exception("無法連接裝置"); }
            // ldd.ExecutePNS("PNS0101");
            var rtnV = ldd.ReadCurPosUf();
            return rtnV;
        }

        public class ClassHelper
        {
            /// <summary>從 TSourceType(來源) 型態的物件複製一份屬性資料到 TTargetType(目標) 型態的物件</summary>
            /// <typeparam name="TSourceType">來源物件的型態</typeparam>
            /// <typeparam name="TTargetType">具有預設建構式之目標物件型別</typeparam>
            /// <param name="sourceObj">來源物件</param>
            /// <param name="ignorePropertyNameCase">是否忽略屬性名稱大小寫(若僅大小寫不同而屬性名稱相同的視為相同屬性)</param>
            /// <param name="propertyNameMapping"></param>
            /// <returns>TTargetType 物件實體</returns>
            public TTargetType ClonPropertiesValue<TSourceType, TTargetType>(TSourceType sourceObj, IDictionary<string, string> propertyNameMapping, bool ignorePropertyNameCase = false)
               where TTargetType : new()
            {
                /**
                 * 取得來源物件中特定屬性名稱的屬性值
                 */
                Func<string, object> GetSourceValue = (propertyName) =>
                 {
                     object rtnV = null;
                     PropertyInfo[] sourceProperties = typeof(TSourceType).GetProperties();
                     var sourcePropertyNames = sourceProperties.Select(m => m.Name).ToList();
                     string sourcePropertyName = null;
                     if (ignorePropertyNameCase)
                     {   // 大小寫屬性視為相同
                         sourcePropertyName = sourcePropertyNames.Where(m => m.ToUpper() == propertyName.ToUpper()).FirstOrDefault();
                     }
                     else
                     {
                         sourcePropertyName = sourcePropertyNames.Where(m => m == propertyName).FirstOrDefault();
                     }
                     if (sourcePropertyName != null)
                     {
                         var sourceProperty = sourceProperties.Where(m => m.Name == sourcePropertyName).FirstOrDefault();
                         if (sourceProperty != null)
                         {
                             rtnV = sourceProperty.GetValue(sourceObj);
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


                if (propertyNameMapping != null && propertyNameMapping.Any())
                {
                    var dicKeys = propertyNameMapping.Keys.ToList();
                    foreach (var key in dicKeys)
                    {
                        string sourcePropertyName;
                        var tryResult = propertyNameMapping.TryGetValue(key, out sourcePropertyName);
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

                    rtnV = this.Path + @"\" + FileName;
                    return rtnV;
                }
            }
            public string Path
            {
                get
                {
                    string rtnV = string.Empty;
                    rtnV = @"D:\Positions";
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
            public float j7 { get; set; }
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
                float rtnV = ((RandomInst.Next(1, 10000) + 1) / 100f);
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
        private int GetPositionFileSerialNumber()
        {
            var itemTextAry = LstBxJsonList.Text.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            var serialNumber = Convert.ToInt32(itemTextAry[0]);
            return serialNumber;
        }

        private void LstBxJsonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtBxDevicePath.Text = "";
            try
            {
                var serialNumber = GetPositionFileSerialNumber();
                var deviceName = CmbBoxDeviceName.Text;
                var configDetail = this.RobotPathFileConfigSet.GetCurrentConfigDetail(deviceName, serialNumber);
                if (configDetail != null)
                {
                    var currentConfig = this.RobotPathFileConfigSet.GetCurrentConfig(deviceName);
                    // Motion Type,
                    CmbBoxMotionType.Text = ((HalRobotEnumMotionType)configDetail.MotionType).ToString();
                    // Speed,
                    // FileName
                    TxtBxDevicePath.Text = configDetail.GetPositionFileFullName(currentConfig.PositionFileDirectory);
                    if (File.Exists(TxtBxDevicePath.Text))
                    {
                        ToLoad();

                    }
                    else
                    {
                        this.PositionInstances = new List<PositionInfo>();
                    }
                    RefreshPositionInfoList();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            NumUdpSn.Value = this.GetPositionInstancesNextSn();
        }

        private void BtnResetSN_Click(object sender, EventArgs e)
        {
            if (this.PositionInstances != null && this.PositionInstances.Any())
            {
                this.PositionInstances = this.PositionInstances.OrderBy(m => m.Sn).ToList();
                int sn = 1;
                foreach (var position in PositionInstances)
                {
                    position.Sn = sn++;
                }
                RefreshPositionInfoList();
                NumUdpSn.Value = this.GetPositionInstancesNextSn();
            }
        }

        private void LstBxGetPosition_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void TxtBxDevicePath_TextChanged(object sender, EventArgs e)
        {
            if (TxtBxDevicePath.Text == string.Empty)
            {
                DisableOperateArea();
            }
            else
            {
                EnableOperateArea();
            }
        }


        private void SetOperateAreaEnaledProperty(bool enableFlag)
        {
            groupBox3.Enabled = BtnAddGet.Enabled = groupBox5.Enabled = enableFlag;
        }
        private void EnableOperateArea()
        {
            SetOperateAreaEnaledProperty(true);
        }
        private void DisableOperateArea()
        {
            SetOperateAreaEnaledProperty(false);
        }
    }
}
