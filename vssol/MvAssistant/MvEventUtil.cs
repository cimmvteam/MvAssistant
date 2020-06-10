using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MvAssistant
{

    public class MvEventUtil
    {

        /// <summary>
        /// 移除某個event裡的所有delegate
        /// 無法使用, 因為event無法以EventHandler形式傳遞
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eh"></param>
        public static void RemoveEventHandlers<T>(EventHandler<T> eh) where T : EventArgs
        {
            //Delegate.RemoveAll(,)
            var handlers = eh.GetInvocationList().ToList();
            foreach (var hdl in handlers)
                eh -= (EventHandler<T>)hdl;
        }

        /// <summary>
        /// 移除某個event裡的所有delegate
        /// </summary>
        /// <param name="dlgt"></param>
        /// <param name="target"></param>
        /// <param name="flags"></param>
        public static void RemoveEventHandlersOfDlgtByTarget(Delegate dlgt, Object target
            , BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
        {
            //Delegate.RemoveAll(,)
            var theEvent = dlgt.Target.GetType().GetEvent(dlgt.Method.Name, flags);
            var handlers = dlgt.GetInvocationList().ToList();
            foreach (var hdl in handlers)
                if (hdl.Target == target)
                    RemoveSubscriberEvenIfItsPrivate(theEvent, dlgt.Target, hdl, flags);
        }


        /// <summary>
        /// 移除某個event裡的所有delegate
        /// </summary>
        /// <param name="dlgt"></param>
        /// <param name="target"></param>
        /// <param name="flags"></param>
        public static void RemoveEventHandlersOfTypeByTarget(Type type, Object target
            , BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
        {
            //傳入類型代表要處理static類型的EventHandler

            foreach (DelegateInfo eventOfOwning in GetDelegates(type, flags))
            {
                //Run過所有訂閱者
                foreach (Delegate subscriber in eventOfOwning.GetInvocationList())
                {
                    if (subscriber.Target == target)
                    {
                        EventInfo theEvent = eventOfOwning.GetEventInfo(flags);
                        RemoveSubscriberEvenIfItsPrivate(theEvent, target, subscriber, flags);
                    }
                }
            }
        }


        /// <summary>
        /// 在 owningObject 中, 移除所有與 targetObj 有關 event 的 delegate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventOwner"></param>
        /// <param name="target"></param>
        public static void RemoveEventHandlersOfOwnerByTarget(Object eventOwner, Object target
            , BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
        {
            if (eventOwner == null || target == null) return;

            foreach (DelegateInfo dlgtInfo in GetDelegates(eventOwner, flags))
            {
                //Run過所有訂閱者
                foreach (Delegate subscriber in dlgtInfo.GetInvocationList())
                {
                    if (subscriber.Target == target)
                    {
                        EventInfo theEvent = dlgtInfo.GetEventInfo(flags);
                        RemoveSubscriberEvenIfItsPrivate(theEvent, eventOwner, subscriber, flags);
                    }
                }
            }
        }




        /// <summary>
        /// 清除 owningObject 裡所有 event 的 delegate
        /// </summary>
        /// <param name="filterFunc"></param>
        /// <param name="eventOwner"></param>
        /// <param name="flags"></param>
        public static void RemoveEventHandlersOfOwnerByFilter(Object eventOwner, Func<Delegate, bool> filterFunc = null,
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
        {
            if (eventOwner == null) return;
            if (filterFunc == null) filterFunc = dlgt => true;

            foreach (DelegateInfo eventFromOwningObject in GetDelegates(eventOwner, flags))
            {
                foreach (Delegate subscriber in eventFromOwningObject.GetInvocationList())
                {
                    if (filterFunc(subscriber))
                    {
                        EventInfo theEvent = eventFromOwningObject.GetEventInfo(flags);
                        RemoveSubscriberEvenIfItsPrivate(theEvent, eventOwner, subscriber, flags);
                    }
                }
            }
        }




        /// <summary>
        /// 取得owningObject裡所有宣告為Delegate的Field
        /// </summary>
        /// <param name="owningObject"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        private static DelegateInfo[] GetDelegates(object owningObject, BindingFlags flags)
        {

            var delegates = new List<DelegateInfo>();
            var potentialEvetns = new List<FieldInfo>();

            var type = owningObject.GetType();
            do
            {
                potentialEvetns.AddRange(type.GetFields(flags));
                type = type.BaseType;
            } while (type != null);


            foreach (FieldInfo privateFieldInfo in potentialEvetns)
            {
                Delegate eventFromOwningObject = privateFieldInfo.GetValue(owningObject) as Delegate;
                //可以成功轉為Delegate的, 記錄下來
                if (eventFromOwningObject != null)
                {
                    delegates.Add(new DelegateInfo(eventFromOwningObject, privateFieldInfo, owningObject));
                }
            }




            return delegates.ToArray();
        }

        private static DelegateInfo[] GetDelegates(Type owningType, BindingFlags flags)
        {

            var delegates = new List<DelegateInfo>();
            var potentialEvetns = new List<FieldInfo>();

            var type = owningType;
            do
            {
                potentialEvetns.AddRange(owningType.GetFields(flags));
                type = type.BaseType;
            } while (type != null);




            foreach (FieldInfo privateFieldInfo in potentialEvetns)
            {
                Delegate eventFromOwningObject = privateFieldInfo.GetValue(null) as Delegate;
                //可以成功轉為Delegate的, 記錄下來
                if (eventFromOwningObject != null)
                {
                    delegates.Add(new DelegateInfo(eventFromOwningObject, privateFieldInfo, owningType));
                }
            }




            return delegates.ToArray();
        }

        // You can use eventInfo.RemoveEventHandler(owningObject, subscriber) 
        // unless it's a private delegate
        private static void RemoveSubscriberEvenIfItsPrivate(
          EventInfo eventInfo, object owningObject, Delegate subscriber, BindingFlags flags)
        {
            MethodInfo privateRemoveMethod = eventInfo.GetRemoveMethod(true);
            privateRemoveMethod.Invoke(owningObject, flags, null, new object[] { subscriber }, CultureInfo.CurrentCulture);
        }
        private class DelegateInfo
        {
            public readonly Delegate delegateInformation;
            public readonly FieldInfo fieldInfo;
            public readonly object owningObject;
            public readonly Type owningType;

            public DelegateInfo(Delegate delegateInformation, FieldInfo fieldInfo, object owningObject)
            {
                this.delegateInformation = delegateInformation;
                this.fieldInfo = fieldInfo;
                this.owningObject = owningObject;
                if (owningObject is Type) this.owningType = owningObject as Type;
            }

            public EventInfo GetEventInfo(BindingFlags flags)
            {
                if (this.owningType == null)
                    return owningObject.GetType().GetEvent(fieldInfo.Name, flags);

                return this.owningType.GetEvent(fieldInfo.Name, flags);
            }

            public Delegate[] GetInvocationList()
            {
                return delegateInformation.GetInvocationList();
            }
        }
    }
}
