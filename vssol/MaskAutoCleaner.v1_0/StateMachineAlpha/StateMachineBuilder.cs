using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace MaskAutoCleaner.v1_0.StateMachineAlpha
{
    public class StateMachineBuilder
    {
        public static void BuildStateMachineFromXML(ref Dictionary<string,StateMachine> sms, string document)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(document);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw ex;
            }
            StateMachine tmp;
            XmlNode sm = doc.FirstChild;
            string smName = sm.Attributes["Name"].Value;
            string smClass = sm.Attributes["Class"].Value;
            XmlNodeList tempNodes = sm.SelectNodes("State");
           
            if (!sms.ContainsKey(smName))
            {
                Type type = null;
                //Assembly ass = Assembly.LoadFrom("MaskAutoCleaner.dll");
                foreach (Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
                {
                    type = ass.GetType(smClass);
                    if (type != null)
                    {
                        break;
                    }
                }

                tmp = (StateMachine)Activator.CreateInstance(type);
                sms.Add(smName,tmp);
            }
            else
            {
                StateMachine findtmp = sms[smName];
                tmp = findtmp;
            }
            foreach (XmlNode stateNode in tempNodes)
            {
                State s;

                if (stateNode.Attributes["Type"].Value.Equals("ExceptionState"))
                {
                    s = new ExceptionState(stateNode.Attributes["Name"].Value);
                }
                else
                {
                    s = new State(stateNode.Attributes["Name"].Value);
                }
                addEvent(ref sms, ref s, stateNode, "Entry");
                addEvent(ref sms, ref s, stateNode, "Exit");
                tmp.AddState(s);
            }
            tempNodes = sm.SelectNodes("Transition");
            foreach (XmlNode transitionNode in tempNodes)
            {
                State FromState = tmp.States[transitionNode.SelectSingleNode("FromState").Attributes["Name"].Value];
                State ToState = tmp.States[transitionNode.SelectSingleNode("ToState").Attributes["Name"].Value];
                Transition s;
                if (transitionNode.Attributes["Type"].Value.Equals("Deferral"))
                {
                    s = new DeferralTransition(FromState, ToState,
                    tmp.GetType().GetMethod(transitionNode.SelectSingleNode("Guard").Attributes["Name"].Value),
                    tmp.GetType().GetMethod(transitionNode.SelectSingleNode("Action").Attributes["Name"].Value),
                    transitionNode.Attributes["Name"].Value);
                }
                else
                {
                    s = new Transition(FromState, ToState,
                    tmp.GetType().GetMethod(transitionNode.SelectSingleNode("Guard").Attributes["Name"].Value),
                    tmp.GetType().GetMethod(transitionNode.SelectSingleNode("Action").Attributes["Name"].Value),
                    transitionNode.Attributes["Name"].Value);
                }

                tmp.AddTransition(s);
            }
        }

        private static void addEvent(ref Dictionary<string,StateMachine> sms, ref State s, XmlNode stateNode, string ee)
        {
            XmlNodeList sEntries = stateNode.SelectNodes(ee);
            if (sEntries != null)
            {
                foreach (XmlNode sEntry in sEntries)
                {
                    string DeviceName = sEntry.Attributes["Device"].Value;
                    string className = sEntry.Attributes["Class"].Value;
                    string eventName = sEntry.Attributes["Event"].Value;
                    EventInfo eventInfo = s.GetType().GetEvent("On" + ee);
                    if (!sms.ContainsKey(DeviceName))
                    {
                        Type type = null;
                        //Assembly ass = Assembly.LoadFrom("MaskAutoCleaner.dll");
                        foreach (Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
                        {
                            type = ass.GetType(className);
                            if (type != null)
                            {
                                break;
                            }
                        }
                        StateMachine tmp = (StateMachine)Activator.CreateInstance(type);
                        sms.Add(DeviceName,tmp);
                        MethodInfo minfo = tmp.GetType().GetMethod(eventName);
                        Delegate handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, tmp, minfo);
                        eventInfo.AddEventHandler(s, handler);
                    }
                    else
                    {
                        //List<StateMachine> sms_tmp = sms.FindAll(tsm => tsm.GetType().ToString().Equals(className));
                        //foreach (StateMachine tmp in sms_tmp)
                        //{
                        //    MethodInfo minfo = tmp.GetType().GetMethod(eventName);
                        //    Delegate handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, tmp, minfo);
                        //    eventInfo.AddEventHandler(s, handler);
                        //}
                        StateMachine tmp = sms[DeviceName];
                        MethodInfo minfo = tmp.GetType().GetMethod(eventName);
                        Delegate handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, tmp, minfo);
                        eventInfo.AddEventHandler(s, handler);
                    }
                }
            }
        }
    }
}
