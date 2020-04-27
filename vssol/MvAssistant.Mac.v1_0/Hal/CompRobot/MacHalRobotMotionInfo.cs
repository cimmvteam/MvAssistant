using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvAssistant.Mac.v1_0.Hal.CompRobot
{
    public class MacHalRobotMotionInfo
    {
        public int UserFrame = 9;
        public int UserTool = 0;
        public int MotionType = 1;
        public int Speed = 50;
        public int isMoveTCP = 0;
        public bool isWaitCommandComplete = true;
        private float targetX;
        public float TargetX
        {
            get { return targetX; }
            set { targetX = value; }
        }
        private float targetY;

        public float TargetY
        {
            get { return targetY; }
            set { targetY = value; }
        }
        private float targetZ;

        public float TargetZ
        {
            get { return targetZ; }
            set { targetZ = value; }
        }
        private float targetW;

        public float TargetW
        {
            get { return targetW; }
            set { targetW = value; }
        }
        private float targetP;

        public float TargetP
        {
            get { return targetP; }
            set { targetP = value; }
        }
        private float targetR;

        public float TargetR
        {
            get { return targetR; }
            set { targetR = value; }
        }
    }

    public class CurrentPOS
    {
        private float currentX;

        public float CurrentX
        {
            get { return currentX; }
            set { currentX = value; }
        }
        private float currentY;

        public float CurrentY
        {
            get { return currentY; }
            set { currentY = value; }
        }
        private float currentZ;

        public float CurrentZ
        {
            get { return currentZ; }
            set { currentZ = value; }
        }
        private float currentW;

        public float CurrentW
        {
            get { return currentW; }
            set { currentW = value; }
        }
        private float currentP;

        public float CurrentP
        {
            get { return currentP; }
            set { currentP = value; }
        }
        private float currentR;

        public float CurrentR
        {
            get { return currentR; }
            set { currentR = value; }
        }

        private float currentE1;

        public float CurrentE1
        {
            get { return currentE1; }
            set { currentE1 = value; }
        }

        private float currentJ1;

        public float CurrentJ1
        {
            get { return currentJ1; }
            set { currentJ1 = value; }
        }
        private float currentJ2;

        public float CurrentJ2
        {
            get { return currentJ2; }
            set { currentJ2 = value; }
        }
        private float currentJ3;

        public float CurrentJ3
        {
            get { return currentJ3; }
            set { currentJ3 = value; }
        }
        private float currentJ4;

        public float CurrentJ4
        {
            get { return currentJ4; }
            set { currentJ4 = value; }
        }
        private float currentJ5;

        public float CurrentJ5
        {
            get { return currentJ5; }
            set { currentJ5 = value; }
        }
        private float currentJ6;

        public float CurrentJ6
        {
            get { return currentJ6; }
            set { currentJ6 = value; }
        }

        private float currentJ7;

        public float CurrentJ7
        {
            get { return currentJ7; }
            set { currentJ7 = value; }
        }

        private short userFrame;

        public short UserFrame
        {
            get { return userFrame; }
            set { userFrame = value; }
        }
        private short userTool;

        public short UserTool
        {
            get { return userTool; }
            set { userTool = value; }
        }
    }
}
