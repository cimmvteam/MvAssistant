using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CToolkitCs.v1_2
{
    public class CtkCommandLine
    {



        public static void CmdWrite(string msg = null, params object[] obj)
        {
            if (msg != null)
            {
                Console.WriteLine();
                Console.WriteLine(msg, obj);
            }
            Console.Write(">");
        }

        public static void Run(Action<string> act = null)
        {
            var cmd = "";
            do
            {

                CmdWrite("App:");
                cmd = Console.ReadLine();
                if (act != null) act(cmd);

            } while (string.Compare(cmd, "exit", true) != 0);


        }

    }
}
