using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2
{
    public class CtkFileFormat
    {


        public bool IsOfficeFile(byte[] buffer)
        {
            var heads = new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 };
            if (buffer.Length < heads.Length) return false;
            for (var idx = 0; idx < heads.Length; idx++)
                if (buffer[idx] != heads[idx]) return false;
            return true;
        }

        public static bool IsGZip(byte[] buffer)
        {
            var heads = new byte[] { 0x1F, 0x8B };
            if (buffer.Length < heads.Length) return false;
            for (var idx = 0; idx < heads.Length; idx++)
                if (buffer[idx] != heads[idx]) return false;
            return true;
        }

    }

}
