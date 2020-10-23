using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectMarker
{
    public class Marker
    {
        public List<MarkerFileData> MarkerFile = new List<MarkerFileData>();

        public void WriteData(string FileName, int Xs, int Ys, int Xe, int Ye)
        {
            
        }
        public class MarkerFileData
        {
            public string FileName;
            public List<MarkerItem> MakerList;
        }


        public class MarkerItem
        {
            public int SN, Xs, Ys, Xe, Ye;
        }


    }
}
