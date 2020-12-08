using MaskAutoCleaner.v1_0.Machine.Cabinet.DrawerQueues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    public class CabinetMediater
    {
        MacMsCabinet0 StateMachine;
        DrawerForBankOutQue DrawerForBankOutQue;

        /// <summary>BnakOut處理中的盒子(帶有盒子, 要 從 Home 移到 In 開始算)</summary>
        private DrawerSatusInfo BankOutProcessDrawer{ get; set; }
        private DrawerSatusInfo BankInProcessDrawer { get; set; }


        private CabinetMediater()
        {

        }
        public CabinetMediater(MacMsCabinet0 stateMachine, DrawerForBankOutQue drawerForBankOutQue) : this()
        {
            StateMachine = stateMachine;
            DrawerForBankOutQue = drawerForBankOutQue;
        }

        /// <summary>第一個可以BnakOut 的Drawer</summary>
        /// <returns></returns>
        public bool PeekBankOut(out DrawerSatusInfo drawerInfo)
        {
            bool rtnV = DrawerCanBankOut();
            if (rtnV)
            {
                drawerInfo = null;
            }
            else
            {
                rtnV = true;
                drawerInfo = DrawerForBankOutQue.Peek();
            }
            return rtnV;
        }

        /// <summary>將可以 BnakOut 的 Drawer 放入可BankOut 的Queue</summary>
        /// <param name="drawerInfo"></param>
        public void EnqueueBankOutDrawerInfo(DrawerSatusInfo drawerInfo)
        {
            DrawerForBankOutQue.Enqueue(drawerInfo);
        }

        /// <summary>有任何 Drawer 可以 Bank Out</summary>
        /// <returns></returns>
        private bool DrawerCanBankOut()
        {
            if (DrawerForBankOutQue.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        private DrawerSatusInfo DeQueueDrawerInfo()
        {
            var drawerInfo = DrawerForBankOutQue.Dequeue();
            return drawerInfo;
        }

        public DrawerSatusInfo BankOut_Load_Move_TrayAtInWithBoxForRobotClampBox()
        {
            var drawerInfo = DeQueueDrawerInfo();

            return null;
        }
    }
}
