using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet.DrawerStatus
{
    public abstract class DrawerStatusBase
    {
        protected object LockActionOKInCrease { get; set; }
        protected object LockActionFailedInCrease { get; set; }
        protected int DrawerCount { get; set; }
        protected int ActionOKCount { get; set; }
        protected int ActionFailedCount { set; get; }
        protected bool ActionStatus { set; get; }
        protected virtual int ActionOtherErrorsCount
        {
            get
            {
                return DrawerCount - ActionOKCount - ActionFailedCount;
            }
        }
        public DrawerStatusBase(int drawerCnt)
        {
            Reset(drawerCnt);
        }
        public virtual void Reset(int drawerCnt)
        {
            DrawerCount = drawerCnt;
            Reset();
        }
        private void Reset()
        {
            LockActionOKInCrease = new object();
            LockActionFailedInCrease = new object();
            ActionStatus = false;
            ActionOKCount = 0;
            ActionFailedCount = 0;
        }

        public virtual void StartAction()
        {
            ActionStatus = true;
        }
        public virtual void StopAction()
        {
            ActionStatus = false;
        }
        public virtual bool IsActionIng()
        {
            return ActionStatus;
        }

        public virtual void ActionOkIncrease()
        {
            lock (LockActionOKInCrease)
            {
                ActionOKCount++;
            }
        }
        public virtual void ActionFailedIncrease()
        {
            lock (LockActionOKInCrease)
            {
                ActionFailedCount++;
            }
        }

        public int ActionOKDrawers
        {
            get
            {
                return ActionOKCount;
            }
        }
        public int ActionFailedDrawers
        {
            get
            {
                return ActionFailedCount;
            }
        }
        public int ActionOtherErrorDrawers
        {
            get
            {
                return ActionOtherErrorsCount;
            }
        }

        public virtual bool IsActionComplete()
        {
            if(ActionFailedCount+ ActionOKCount == DrawerCount)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
