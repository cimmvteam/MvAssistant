using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.IO
{
    [Serializable]
    public class CtkStreamSegmentBuffer
    {

        public byte[] Buffer;
        public int BufferValidOffset = 0;
        public int BufferValidLength = -1;

        public long AbsPosition = 0;
        public long AbsLength = -1;

        public long AbsValidEndPosition { get { return this.AbsPosition + this.BufferValidLength; } }
        public long BufferValidEndPosition { get { return this.BufferValidOffset + this.BufferValidLength; } }


        public int Read(byte[] buffer, int offset, int count, long position)
        {
            var start = (position - this.AbsPosition) + this.BufferValidOffset;//應開始 in buffer
            var end = start + count;//最大可結束位置
            //if (end > this.BufferValidEndPosition) throw new InvalidOperationException("Cannot over valid range");
            if (end > this.BufferValidEndPosition) end = this.BufferValidEndPosition;
            var cnt = end - start;
            Array.Copy(this.Buffer, start, buffer, offset, cnt);
            return (int)cnt;
        }




        public int GetAvailableCount(long position)
        {
            if (position < this.AbsPosition) return -1;//無法取得
            var cnt = (int)(this.AbsValidEndPosition - position);
            return cnt;
        }

        public bool IsEnd(long position) { return position >= this.AbsLength; }

        public bool IsInSegment(long position, long count)
        {
            //開頭比資料區間小
            if (position < this.AbsPosition) return false;

            //結束比資料區間大
            var endPos = position + count;
            if (endPos >= this.AbsLength) endPos = this.AbsLength - 1;

            var validEndPos = this.AbsValidEndPosition;
            //if (validEndPos >= this.AbsLength) validEndPos = this.AbsLength - 1;//有效資料不應超過資料Length

            if (endPos >= validEndPos) return false;

            return true;
        }


    }
}
