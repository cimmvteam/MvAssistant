using MaskAutoCleaner.Manifest.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Manifest
{
    /// <summary>
    /// Assembly Factory, 根據Manifest.xml描述建立對應的Assembly objects
    /// 並且將Assembly內的Component指定至對應的devices
    /// </summary>
    public class MachineAssemblyFactory
    {
        public  MachineManifest manifest;
        private string manifestFilePath = "";
        private object assumptions;
        private delegate bool Md5Handler(MachineAssemblyDriverReference reference, string md5Str);
        private string[] debugEnvirons = { "EHB14006", /*lincyzm*/
                                           "EA317109", /*ycye*/
                                           "EA317081", /*chshenga*/
                                           "EAB16086", /*mhtsaizw*/
                                           "EA317110", /*bachena*/
                                           "EA317114"  /*tsaiyca*/};

        /// <summary>
        /// Manifest是否合法
        /// </summary>
        private bool isManifestValid;

        /// <summary>
        /// AssemblyFactory constructor
        /// </summary>
        /// <param name="manifestPath">Manifest.xml path</param>
        public MachineAssemblyFactory(string manifestPath)
        {
            this.manifestFilePath = manifestPath;

            /// 將MachineManifest.xml反序列化成MachineManifest物件
            this.manifest = MachineManifest.LoadFromXmlFile(manifestPath);
        }

        /// <summary>
        /// 驗證Manifest的合法性
        /// (1) 整份Manifest文件的MD5_CheckSum是否正確
        /// (2) Reference檔案的MD5 CheckSum是否正確
        /// </summary>
        /// <returns>true: manifest is valid; false: manifest is invalid</returns>
        /// <exception cref="ObjectNotFoundException">未指定MachineMachine.xml路徑 or manifest object is null</exception>
        public bool ValidateManifest(out string msg)
        {
#if !DEBUG
            return true;
#else
            /// 檢查manifest object & xml file是否都已指定?
            if (this.manifest == null || String.IsNullOrEmpty(this.manifestFilePath))
                throw new HalObjectNotFoundException(String.Format("Manifest xml path is not specified or manifest object is null"));

            /// 檢查MachineManifest.xml CheckSum是否正確
            try
            {
                string md5 = DoMd5CheckSumManifest();
                if (!md5.Equals(this.manifest.CheckSum))
                {
                    msg = "<Manifest CheckSum> is invalid";
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = String.Format("Failed to generate checksum data for manifest file, due to: {0}", ex.Message);
                throw new GenCheckSumException(msg, ex.InnerException);
            }

            /// 檢查Reference Tag是否正確 (file存在? CheckSum正確?)
            try
            {
                DoMd5CheckSumReferences(Md5HandlerDoValidate);
            }
            catch (Exception ex)
            {
                msg = String.Format("Reference CheckSum is invalid, due to: {0}", ex.Message);
                return false;
            }

            msg = "Success";
            return true;
#endif
        }

        /// <summary>
        /// 針對MachineManifest.xml設定CheckSum, 避免MachineManifest xml被修改 or reference files更換新版本
        /// 會對兩個部分執行CheckSum運算
        /// (1) Reference Tag
        /// (2) 整個Manifest檔案內容
        /// </summary>
        /// <returns>true: succeed to set CheckSum; false: failed to set CheckSum</returns>
        /// <exception cref="ObjectNotFoundException">未指定MachineMachine.xml路徑 or manifest object is null</exception>
        /// <exception cref="GenCheckSumException">執行CheckSum程序造成exception</exception>
        /// <exception cref="ManifestAccessException">failed to save manifest xml</exception>
        public bool GenCheckSum()
        {
            /// 檢查manifest object & xml file是否都已指定?
            if (this.manifest == null || String.IsNullOrEmpty(this.manifestFilePath))
                throw new HalObjectNotFoundException(String.Format("Manifest xml path is not specified or manifest object is null"));

            /// generate AssemblyDriver.References.Reference CheckSum value
            try { DoMd5CheckSumReferences(Md5HandlerDoAssign); }
            catch (Exception ex) { throw new GenCheckSumException(String.Format("Failed to generate checksum data for Reference tags, due to: {0}", ex.Message), ex.InnerException); }

            /// generate MachineManifest CheckSum value
            try { this.manifest.CheckSum = DoMd5CheckSumManifest(); }
            catch (Exception ex) { throw new GenCheckSumException(String.Format("Failed to generate checksum data for manifest file, due to: {0}", ex.Message), ex.InnerException); }

            /// save to MachineManifest.xml
            try { MachineManifest.SaveToXmlFile(manifest, this.manifestFilePath); }
            catch (Exception ex) { throw new ManifestAccessException(String.Format("Failed to save MachineManifest.xml file due to : {0}", ex.Message), ex.InnerException); }

            return true;
        }

        /// <summary>
        /// 將MachineManifest.xml檔案內容進行MD5運算
        /// CheckSum = MD5(MachineManifest.xml content)
        /// </summary>
        /// <returns>MD5 result</returns>
        /// <exception cref="ObjectNotFoundException">Assemblies & Manifest tag無法找到</exception>
        private string DoMd5CheckSumManifest()
        {
            /// 找出MachineManifest.xml中Manifest tag內的childred element內容
            string xml = MachineManifest.Serialize(this.manifest);
            int startIdx = xml.IndexOf("<Assemblies>");
            if (startIdx < 0)
                throw new HalObjectNotFoundException(String.Format("<{0}> tag is not found in Manifest.xml", "Assemblies"));
            int endIdx = xml.IndexOf("</Manifest>");
            if (endIdx < 0)
                throw new HalObjectNotFoundException(String.Format("<{0}> tag is not found in Manifest.xml", "/Manifest"));

            xml = xml.Substring(startIdx, endIdx - startIdx);

            /// 將Manifest childred elements內容進行MD5加密, 取得一組字串長度為32的CheckSum value
            /// 並將md5 value寫回Manifest CheckSum attribute內
            return DoMd5(xml);
        }

        /// <summary>
        /// 針對MachineManifest.xml檔案每個Reference tag 進行MD5運算
        /// CheckSum = MD5(reference file content)
        /// </summary>
        /// <param name="md5Handler">md5Handler for generating or validating</param>
        /// <exception cref="ObjectNotFoundException">未指定Reference中Path屬性</exception>
        /// <exception cref="FileNotFoundException">Reference中的Path不存在</exception>
        /// <exception cref="InvalidCheckSumException">reference file md5 value不相同於原先valid value</exception>
        private void DoMd5CheckSumReferences(Md5Handler md5Handler)
        {
            /// 找出有children elements的References elements
            var allReferences = from drivers in manifest.AssemblyDrivers
                                where drivers.References.Count > 0
                                select drivers.References;

            foreach (var references in allReferences)
            {
                foreach (var reference in references)
                {
                    /// 確認reference file是否存在
                    if (String.IsNullOrEmpty(reference.Path))
                        throw new HalObjectNotFoundException(String.Format("Path is null or empty for <Reference Description={0}> in Manifest", reference.Description));

                    if (!File.Exists(reference.Path))
                        throw new FileNotFoundException(String.Format("File({0}) is not exist for <Reference Description={1}> in Manifest", reference.Path, reference.Description));

                    string contents;
                    using (StreamReader streamReader = new StreamReader(reference.Path, Encoding.UTF8))
                    {
                        contents = streamReader.ReadToEnd();
                        string checksum = DoMd5(contents);
                        if (!md5Handler(reference, checksum))
                            throw new InvalidCheckSumException(String.Format("File({0}) checksum is invalid for <Reference Description={1}> in Manifest", reference.Path, reference.Description));
                    }
                }
            }
        }

        /// <summary>
        /// 將string轉換成UTF8編碼, 再進行MD5運算
        /// </summary>
        /// <param name="input">input string</param>
        /// <returns>Hex編碼字串, 長度32</returns>
        private string DoMd5(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] utf8Temp = Encoding.UTF8.GetBytes(input);
            byte[] targetData = md5.ComputeHash(utf8Temp);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
                byte2String += targetData[i].ToString("X2");

            return byte2String;
        }

        private bool Md5HandlerDoAssign(MachineAssemblyDriverReference reference, string md5Str)
        {
            reference.CheckSum = md5Str;
            return true;
        }

        private bool Md5HandlerDoValidate(MachineAssemblyDriverReference reference, string md5Str)
        {
            return (reference.CheckSum.Equals(md5Str));
        }

        /// <summary>
        /// 根據Manifest.xml描述內容建立assembly instances
        /// assembly instance亦會create component instances
        /// </summary>
        /// <returns>list of assembly objects</returns>
        public List<object> CreateAssemblies()
        {
            List<object> assemblies = new List<object>();

            /// 找出Level = 1的Assembly objects
            var asmbs = from assembly in manifest.Assemblies
                        where assembly.Level.Equals("1")
                        select assembly;

            foreach (var asmb in asmbs)
            {
                try { assemblies.Add(CreateAssembly(asmb)); }
                catch (Exception ex) { throw new AssemblyCreationException(String.Format("[MachineAssemblyFactory]: Failed to create assembly ({0}), due to: {1}", asmb.ID, ex.Message), ex); }
            }

            return assemblies;
        }

        /// <summary>
        /// 根據Manifest.xml描述內容建立assembly instances
        /// assembly instance亦會create component instances
        /// </summary>
        /// <returns>list of assembly objects</returns>
        public List<object> CreateAssemblies(object assmp)
        {
            //if (debugEnvirons.Contains(System.Environment.MachineName))
            {
                this.assumptions = assmp;
                List<object> assemblies = CreateAssemblies();
                return assemblies;
            }

            //return null;
        }

        /// <summary>
        /// 將Manifest的單一Assembly進行實體化binding作業
        /// </summary>
        /// <param name="assembly">MachineAssembly object</param>
        /// <returns>assembly instance which is bound to device</returns>
        public object CreateAssembly(MachineAssembly assembly)
        {
            /// 根據Device ID, 找出對應的HAL info, 並進行object實體化
            var drivers = from driver in manifest.AssemblyDrivers
                          where driver.DriverId == assembly.DriverId
                          select driver;


            if (drivers.Count() == 0)
                throw new NoDriverException(String.Format("[MachineAssemblyFactory]: Cannot find <AssemblyDriver> tag in Manifest.xml (DeviceName = {0}, ID = {1})", assembly.DeviceName, assembly.ID));

            if (drivers.Count() > 1)
                throw new MultipleDriverException(String.Format("[MachineAssemblyFactory]: Multiple <AssemblyDriver> tags in Manifest.xml (DeviceName = {0}, ID = {1})", assembly.DeviceName, assembly.ID));

            Type assemblyType = null;
            object assemblyInstance = null;
            foreach (var driverObj in drivers)
            {
                // 建立執行個體
                // TODO: 加入try catch to throw exception for load & create instance
                assemblyType = driverObj.AssignType; //System.Reflection.Assembly.LoadFrom(driverObj.HalPath).GetType(driverObj.HalImpClassRefFullName);
                assemblyInstance = Activator.CreateInstance(assemblyType, new object[] { });

                if (assemblyInstance != null)
                {
                    try
                    {
                        // 設定Assumption
                        var props = assemblyType.GetProperties();
                        var testScriptCmds = props.Where(x => x.Name == "TestScriptCmds").FirstOrDefault();
                        if (testScriptCmds != null)
                            testScriptCmds.SetValue(assemblyInstance, this.assumptions);

                        if (assemblyInstance is IManifestMachine)
                        {
                            var mm = assemblyInstance as IManifestMachine;
                            mm.ID = assembly.ID;
                            mm.DeviceConnStr = assembly.DevConnStr;
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new NoPropertyException("[MachineAssemblyFactory]: Cannot found ID or DeviceConnStr...etc properties in assembly instance", ex);
                    }
                }

                break;
            }

            if (assembly.Assemblies != null)
            {
                /// 使用遞迴方式, 建立component objects
                List<object> components = new List<object>();
                foreach (MachineAssembly subAssembly in assembly.Assemblies)
                {
                    try
                    {
                        object component = CreateAssembly(subAssembly);
                        components.Add(component);
                    }
                    catch (Exception)
                    {
                        //throw new Exception(String.Format("[MachineAssemblyFactory]: Failed to create assembly, due to {0}", ex.Message), ex);
                    }
                }

                /// 將components與properties進行binding
                if (components.Count > 0)
                    Bind(assemblyInstance, assembly.Assemblies, components);
            }

            return assemblyInstance;
        }

        /// <summary>
        /// 將assembly內包含sub-assemblies(components)與manifest所描述的driver進行binding動作
        /// 將binding至assembly的property objects
        /// </summary>
        /// <param name="assemblyInstance"></param>
        /// <param name="componentAssemblies"></param>
        /// <param name="components"></param>
        private void Bind(object assemblyInstance, MachineAssembly[] componentAssemblies, List<object> components)
        {
            int manifestPropCount = 0;
            var props = assemblyInstance.GetType().GetProperties();
            foreach (var prop in props)
            {
                // 不是MachineManifest attribute, 不處理
                if (!prop.IsDefined(typeof(MachineManifestAttribute), false)) continue;

                manifestPropCount++;
                foreach (Attribute att in prop.GetCustomAttributes(false))
                {
                    if (!att.GetType().Equals(typeof(MachineManifestAttribute))) continue;

                    DeviceEnum devEnum = (DeviceEnum)att.GetType().GetProperty("Device").GetValue(att);
                    var deviceIds = from obj in componentAssemblies
                                    where obj.DeviceName.Equals(devEnum.ToString())
                                    select obj.ID;

                    if (deviceIds.Count() == 0)
                        throw new LackAssemblyElementException(String.Format("[MachineAssemblyFactory]: Lack assembly element ({0}) in Manifest.xml", devEnum.ToString()));

                    bool isFound = false;
                    foreach (var deviceId in deviceIds)
                    {
                        foreach (var component in components)
                        {
                            Type componentType = component.GetType();
                            string id = componentType.GetProperty("ID").GetValue(component).ToString();

                            if (id.Equals(deviceId.ToString()))
                            {
                                isFound = true;
                                prop.SetValue(assemblyInstance, component);
                                break;
                            }
                        }

                        if (isFound)
                            break;
                    }

                    if (!isFound)
                        throw new LackAssemblyElementException(String.Format("[MachineAssemblyFactory]: Lack assembly driver (Device = {0}) in {1}", devEnum.ToString(), this.manifest));

                    break;
                }
            }

            if (manifestPropCount > components.Count)
            {
                Type type = assemblyInstance.GetType();
                string assemblyID = type.GetProperty("ID").GetValue(assemblyInstance).ToString();
                throw new LackAssemblyElementException(String.Format("[MachineAssemblyFactory]: The count of assembly property is larger than assembly elements in Manifest.xml, DeviceID = {0}", assemblyID));
            }
        }
    }
}
