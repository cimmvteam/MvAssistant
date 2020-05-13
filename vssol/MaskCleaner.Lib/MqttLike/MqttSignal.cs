using MaskAutoCleaner.Msg;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.MqttLike
{
    public class MqttSignal : IComparable
    {
        public object Signal;
        public MqttSignal() { }
        public MqttSignal(object obj) { this.Signal = obj; }

        public T AsObject<T>() where T : class
        {
            return this.Signal as T;
        }

        public int CompareTo(object obj)
        {
            var other = obj as MqttSignal;
            if (other == null) throw new ArgumentException("無法轉換成" + typeof(MqttSignal).Name);

            var type = this.Signal.GetType();
            if (other.Signal.GetType() != type) throw new ArgumentException("Signal必須相同");

            if (type == typeof(string))
                return this.ToObject<string>().CompareTo(other.ToObject<string>());
            if (type == typeof(float))
                return this.ToObject<float>().CompareTo(other.ToObject<float>());
            if (type == typeof(double))
                return this.ToObject<double>().CompareTo(other.ToObject<double>());
            if (type == typeof(Vector<float>))
            {
                var a = this.ToObject<Vector<float>>();
                var b = other.ToObject<Vector<float>>();
                return a.L2Norm().CompareTo(b.L2Norm());
            }
            if (type == typeof(Vector<double>))
            {
                var a = this.ToObject<Vector<double>>();
                var b = other.ToObject<Vector<double>>();
                return a.L2Norm().CompareTo(b.L2Norm());
            }



            throw new ArgumentException("無法比較的類型");
        }

        public T ToObject<T>()
        {
            return (T)this.Signal;
        }
  
        
        
        #region To Signal
        public static implicit operator MqttSignal(string obj) { return new MqttSignal(obj); }
        public static implicit operator MqttSignal(float obj) { return new MqttSignal(obj); }
        public static implicit operator MqttSignal(double obj) { return new MqttSignal(obj); }
        public static implicit operator MqttSignal(Image obj) { return new MqttSignal(obj); }
        public static implicit operator MqttSignal(Vector<float> obj) { return new MqttSignal(obj); }
        public static implicit operator MqttSignal(MsgBase obj) { return new MqttSignal(obj); }
        public static implicit operator MqttSignal(Vector<double> obj) { return new MqttSignal(obj); }
        #endregion
        
        #region From Signal
        public static implicit operator MsgBase(MqttSignal obj) { return (MsgBase)obj.Signal; }

        public static implicit operator Image(MqttSignal obj) { return (Image)obj.Signal; }

        public static implicit operator string(MqttSignal obj) { return (string)obj.Signal; }

        public static implicit operator double(MqttSignal obj) { return (double)obj.Signal; }
        public static implicit operator Vector<float>(MqttSignal obj) { return (Vector<float>)obj.Signal; }

        public static implicit operator float(MqttSignal obj) { return (float)obj.Signal; }
        public static implicit operator Vector<double>(MqttSignal obj) { return (Vector<double>)obj.Signal; }
        #endregion
    
    
    }
}
