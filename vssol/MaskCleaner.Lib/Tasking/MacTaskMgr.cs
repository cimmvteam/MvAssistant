using MvLib.TaskDispatch;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Tasking
{
    /// <summary>
    /// Singleton pattern
    /// </summary>
    public sealed class MacTaskMgr
    {
        public int LimitThreadCount = 50;
        public ConcurrentBag<MacTask> Tasks = new ConcurrentBag<MacTask>();
        private static readonly object mgrlock = new object();
        private static MacTaskMgr instance = null;
        /// <summary>
        /// 請從MacMask取得
        /// </summary>
        MacTaskMgr()
        {


        }

        public static MacTaskMgr Instance
        {
            get
            {
                lock (mgrlock)
                {
                    if (instance == null)
                    {
                        instance = new MacTaskMgr();
                    }
                    return instance;
                }
            }
        }

        public void Add(MacTask macTask)
        {
            this.Tasks.Add(macTask);
        }

        public void AddInitialThread()
        {
            ProcessThreadCollection threads = Process.GetCurrentProcess().Threads;
            foreach (ProcessThread singleThread in threads)
            {
                if (Tasks.Where(t => t.Id == singleThread.Id) == null)
                {
                    MacTask ass = new MacTask(singleThread.Id.ToString(), singleThread);
                }
            }
        }

        public void CheckThreadCountUnderLimit()
        {
            int threadCount = Process.GetCurrentProcess().Threads.Count;
            if (threadCount > LimitThreadCount)
            {
                //throw ooc
            }
            if (Tasks.Count > LimitThreadCount)
            {
                //throw oos
            }
        }

        public MacTask CreateMacTask(string name, Task t)
        {
            return new MacTask(name, t);
        }

        public void ForeachTask(Action<MacTask> act)
        {
            foreach (var t in this.Tasks)
            {
                act(t);
            }
        }

        public void gogo()
        {
            for (int i = 1; i < 10; i++)
            {
                int threadCount = Process.GetCurrentProcess().Threads.Count;
                Console.WriteLine("gogo:" + i);
                Thread.Sleep(100);
            }
        }

        public Dictionary<int, string> QueryTaskIdName()
        {
            var dict = new Dictionary<int, string>();
            foreach (var t in this.Tasks)
            {
                dict[t.Id] = t.Name;
            }
            return dict;
        }
        public void test()
        {
            MacTask a = new MacTask("test", Task.Run(() => gogo()));
            ThreadPool.SetMaxThreads(64, 32);
            ThreadPool.QueueUserWorkItem(new WaitCallback(TaskCallBack), a);
        }

        private static void TaskCallBack(Object mactask)
        {
            SpinWait.SpinUntil(() => { return ((MacTask)mactask).Task.IsCompleted; });
        }
    }
}
