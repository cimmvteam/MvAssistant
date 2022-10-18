using CToolkitCs.v1_2.WinApiNative;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CToolkitCs.v1_2
{
    public class CtkMemcpyUtil
    {


        public static T FromBytes<T>(byte[] buffer)
        {
            int size = Marshal.SizeOf(typeof(T));
            IntPtr ptr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(buffer, 0, ptr, size);
                T data = (T)Marshal.PtrToStructure(ptr, typeof(T));
                return data;
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    Marshal.FreeHGlobal(ptr);
            }


        }



        public static void StructsByteCopy<T, S>(T[] source, S[] destination) where T : struct where S : struct
        {
            var lenOfSrc = source.Length * Marshal.SizeOf(typeof(T));
            var lenOfDest = destination.Length * Marshal.SizeOf(typeof(S));
            if (lenOfSrc != lenOfDest) throw new ArgumentException("Total bytes of array is different");

            GCHandle handleSrc = GCHandle.Alloc(source, GCHandleType.Pinned);
            GCHandle handleDest = GCHandle.Alloc(destination, GCHandleType.Pinned);
            try
            {
                IntPtr pointerSrc = handleSrc.AddrOfPinnedObject();
                IntPtr pointerDest = handleDest.AddrOfPinnedObject();
                byte[] buffer = new byte[lenOfSrc];
                Marshal.Copy(pointerSrc, buffer, 0, buffer.Length);
                Marshal.Copy(buffer, 0, pointerDest, buffer.Length);
            }
            finally
            {
                if (handleSrc.IsAllocated)
                    handleSrc.Free();
                if (handleDest.IsAllocated)
                    handleDest.Free();
            }
        }

        public static void StructsCopy<T, S>(T[] source, S[] destination) where T : struct where S : struct
        {
            var lenOfSrc = source.Length * Marshal.SizeOf(typeof(T));
            var lenOfDest = destination.Length * Marshal.SizeOf(typeof(S));
            if (lenOfSrc != lenOfDest) throw new ArgumentException("Total bytes of array is different");

            GCHandle handleSrc = GCHandle.Alloc(source, GCHandleType.Pinned);
            GCHandle handleDest = GCHandle.Alloc(destination, GCHandleType.Pinned);
            try
            {
                IntPtr pointerSrc = handleSrc.AddrOfPinnedObject();
                IntPtr pointerDest = handleDest.AddrOfPinnedObject();
                CtkKernel32Lib.CopyMemory(pointerDest, pointerSrc, (uint)lenOfDest);
            }
            finally
            {
                if (handleSrc.IsAllocated)
                    handleSrc.Free();
                if (handleDest.IsAllocated)
                    handleDest.Free();
            }
        }


        public static T[] StructsFromBytes<T>(byte[] source) where T : struct
        {
            T[] destination = new T[source.Length / Marshal.SizeOf(typeof(T))];
            GCHandle handle = GCHandle.Alloc(destination, GCHandleType.Pinned);
            IntPtr pointer = IntPtr.Zero;
            try
            {
                pointer = handle.AddrOfPinnedObject();
                Marshal.Copy(source, 0, pointer, source.Length);
                return destination;
            }
            finally
            {
                if (handle.IsAllocated)
                    handle.Free();
                //if (pointer != IntPtr.Zero)
                //    Marshal.FreeHGlobal(pointer);
            }
        }

        public static byte[] StructsToBytes<T>(T[] source) where T : struct
        {
            GCHandle handle = GCHandle.Alloc(source, GCHandleType.Pinned);
            IntPtr pointer = IntPtr.Zero;
            try
            {
                pointer = handle.AddrOfPinnedObject();
                byte[] destination = new byte[source.Length * Marshal.SizeOf(typeof(T))];
                Marshal.Copy(pointer, destination, 0, destination.Length);
                return destination;
            }
            finally
            {
                if (handle.IsAllocated)
                    handle.Free();
                //if (pointer != IntPtr.Zero)
                //    Marshal.FreeHGlobal(pointer);
            }
        }

        public static byte[] ToBytes<T>(T obj)
        {

            Byte[] bytes = new Byte[Marshal.SizeOf(typeof(T))];
            GCHandle pinStructure = GCHandle.Alloc(obj, GCHandleType.Pinned);
            try
            {
                Marshal.Copy(pinStructure.AddrOfPinnedObject(), bytes, 0, bytes.Length);
                return bytes;
            }
            finally { pinStructure.Free(); }
        }





    }
}
