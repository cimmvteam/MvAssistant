using MaskAutoCleaner.Alarm;
using MaskAutoCleaner.Machine;
using MaskAutoCleaner.Manifest;
using MaskAutoCleaner.Mask;
using MaskAutoCleaner.MemoryStorage;
using MaskAutoCleaner.MqttLike;
using MaskAutoCleaner.Msg;
using MvLib;
using MvLib.Logging;
using MvLib.StateMachine;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MaskAutoCleaner
{


    public class MacMachineMediater : IStateMachineAlarmHandler, IDisposable
    {

        public AlarmContext AlarmMgr = new AlarmContext();

        public MvLogger Logger = MvLog.DefaultLogger;

        public MacMachineConfig MachineCfg = new MacMachineConfig();

        public MacMachineBase MachineEqp;

        public MachineManifest MachineManifest;

        public System.Windows.Forms.Form mainForm;

        //注意, 這個沒設定的話, Robot會無法連線
        public ConcurrentDictionary<string, MaskContainer> MaskContainers = new ConcurrentDictionary<string, MaskContainer>();

        public Dictionary<string, MemStorageList> MemStorageMapper = new Dictionary<string, MemStorageList>();

        public MqttBroker MqttBroker = new MqttBroker();

        public MacMachineMediater()
        {
            this.MqttBroker.EqpMediater = this;

        }

        ~MacMachineMediater() { this.Dispose(false); }
        public MacMachineBase ActiveMask
        {
            get
            {
                if (!this.MachineEqp.ContainSubMachine(EnumMachineId.ActiveMask)) return null;
                return this.MachineEqp[EnumMachineId.ActiveMask];
            }
        }

        public Type GetAssemblyTypeById(string id)
        {
            if (!this.MachineEqp.SubStateMachines.ContainsKey(id)) return null;
            return this.MachineEqp[id].GetType();
        }

        public MemoryStorage.MemStorageList GetDeviceStorageList(string uniqueName)
        {
            if (this.MemStorageMapper.ContainsKey(uniqueName))
                return this.MemStorageMapper[uniqueName];

            var ds = new MemoryStorage.MemStorageList();
            this.MemStorageMapper[uniqueName] = ds;

            return ds;
        }

        public EnumPositionId GetPositionId(string machineId)
        {
            var q = from row in this.MachineManifest.Assemblies
                    where row.ID == machineId
                    select row;
            var asb = q.First();
            return MvUtil.EnumParse<EnumPositionId>(asb.PositionId);
        }
        #region Mask Operation

        public MaskContainer MaskGetByBarcode(string barcode)
        {
            var dict = this.MaskContainers;
            var query = from row in dict.Values
                        where row.PodBarcode == barcode
                        select row;

            return query.FirstOrDefault();
        }

        public MaskContainer MaskGetByMask(MacMachineBase mask)
        {
            var dict = this.MaskContainers;
            var query = from row in dict.Values
                        where row.Mask == mask
                        select row;

            return query.FirstOrDefault();
        }

        public MaskContainer MaskGetOrCreateContainer(string position)
        {
            var dict = this.MaskContainers;
            if (dict.ContainsKey(position)) return dict[position];
            var container = new MaskContainer();
            container.Position = position;
            dict[position] = container;
            return container;
        }
        public void MaskSetActive(MacMachineBase masksm)
        {
            this.MachineEqp[EnumMachineId.ActiveMask] = masksm;
        }



        #endregion



        #region Message Porcess

        //訊息處理
        //Alarm => 給 EqpSm 處理
        //Secs => 給 SecsMgr 處理 => 會轉給 EqpSm + TAP(is needed)
        //JobNotify => 給 EqpSm + ActiveMask 處理
        //Mediater 類似窗口, 經過它才可以與別人溝通
        //Mediater 也類似戰情, 做第一線處理, 複雜的會往EQP或相關machine丟

        public void ProcAlarm(object sender, Exception ex)
        {
            var alarm = MsgFactory.CreateAlarm(sender, ex);
            this.ProcAlarm(sender, alarm);
        }

        public void ProcAlarm(object sender, Enum enumAlarm, string alarmId)
        {
            ProcAlarm(sender, new Msg.MsgAlarm()
            {
                Sender = sender,
                AlarmId = MvUtil.EnumParse<Msg.EnumAlarmId>(alarmId),
            });
        }

        public void ProcAlarm(object sender, Msg.MsgAlarm alarm)
        {
            this.Logger.Warn("AlarmId={0}; Message={1}, Exception={2}", alarm.AlarmId, alarm.Message, alarm.GetExceptionStackTrace());
            this.MachineEqp.RequestProcMsg(alarm);
        }


        public void ProcJobNotify(object sender, MsgJobNotify msg)
        {
            this.MachineEqp.RequestProcMsg(msg);
            if (this.ActiveMask != null)
                this.ActiveMask.RequestProcMsg(msg);
        }

        public void ProcSecs(object sender, MsgSecs msgSecs)
        {
            var secsMgr = this.MachineEqp.GetSubMachine<MacMachineBase>(EnumMachineId.SecsMgr);

            this.MachineEqp.RequestProcMsg(msgSecs);//都要給EQP知道
            if (!msgSecs.IsFromExternal)
                secsMgr.RequestProcMsg(msgSecs);//從裡面來的發出去
        }
        #endregion



        #region IDisposable
        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void DisposeSelf()
        {
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //

            this.DisposeSelf();

            disposed = true;
        }
        #endregion



    }
}
