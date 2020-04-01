using MvAssistant.DeviceDrive.FanucRobot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskTool.TestMy.SgsVerify
{
    public class RealRunMtRobot
    {
        List<MvFanucRobotInfo> positions = new List<MvFanucRobotInfo>();



        public void GenTestPoints()
        {


        }

        public List<MvFanucRobotInfo> GenHomeToBarcodeReader()
        {
            var poss = new List<MvFanucRobotInfo>();



            //Home
            poss.Add(new MvFanucRobotInfo()
            {
                x = 317,
                y = 0,
                z = 356,
                w = 179,
                p = 0,
                r = 0,
            });



            //between ic & lpa
            this.positions.Add(new MvFanucRobotInfo()
            {
                x = 215,
                y = 215,
                z = 356,
                w = 179,
                p = 0,
                r = 45,
            });

            //ICxLPA 抬頭1動
            this.positions.Add(new MvFanucRobotInfo()
            {
                x = 215,
                y = 301,
                z = 333,
                w = -157,
                p = -15,
                r = -21,
            });


            //ICxLPA 抬頭2動
            this.positions.Add(new MvFanucRobotInfo()
            {
                x = 215,
                y = 301,
                z = 333,
                w = -111,
                p = -33,
                r = -32,
            });

            //LPA 上方
            this.positions.Add(new MvFanucRobotInfo()
            {
                x = 215,
                y = 301,
                z = 333,
                w = -4,
                p = -57,
                r = -121,
            });


            //Barcode Reader
            this.positions.Add(new MvFanucRobotInfo()
            {
                x = -110,
                y = 247,
                z = 471,
                w = -79,
                p = -87,
                r = -26,
            });




            return poss;
        }
        public List<MvFanucRobotInfo> GenHome2Cc2Os()
        {
            var poss = new List<MvFanucRobotInfo>();

            //Home
            poss.Add(new MvFanucRobotInfo()
            {
                x = 317,
                y = 0,
                z = 356,
                w = 179,
                p = 0,
                r = 0,
            });


            //Between IC & CC
            this.positions.Add(new MvFanucRobotInfo()
            {
                x = 223,
                y = -225,
                z = 356,
                w = -179,
                p = -0,
                r = -45,
            });

            //Front CC 低頭
            this.positions.Add(new MvFanucRobotInfo()
            {
                x = 0,
                y = -317,
                z = 356,
                w = 179,
                p = 0,
                r = -90,
            });



            //Front of CC 抬頭
            this.positions.Add(new MvFanucRobotInfo()
            {
                x = 0,
                y = -232,
                z = 211,
                w = 104,
                p = -78,
                r = -17,
            });




            return poss;
        }

    }
}
