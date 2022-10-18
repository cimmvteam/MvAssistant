using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.IO
{
    public class CtkStreamSegment : Stream
    {
        protected CtkStreamSegmentBuffer segBuffer;



        #region Override

        public override bool CanRead { get { throw new NotImplementedException(); } }

        public override bool CanSeek { get { throw new NotImplementedException(); } }

        public override bool CanWrite { get { throw new NotImplementedException(); } }

        public override long Length { get { throw new NotImplementedException(); } }

        public override long Position { get; set; }

        public override void Flush() { throw new NotImplementedException(); }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (this.segBuffer.IsEnd(Position)) return 0;
            var isInSegment = this.segBuffer.IsInSegment(this.Position, count);

            if (!isInSegment)
            {
                //Do something to get data bytes
            }


            var avalible = this.segBuffer.GetAvailableCount(this.Position);
            if (avalible <= 0) throw new InvalidOperationException("Search bytes is out of range");
            if (avalible == 0) return 0;//當前無資料可讀 (不應發生)

            var cnt = this.segBuffer.Read(buffer, offset, count, this.Position);
            this.Position += cnt;
            return cnt;
        }

        public override long Seek(long offset, SeekOrigin origin) { throw new NotImplementedException(); }

        public override void SetLength(long value) { throw new NotImplementedException(); }

        public override void Write(byte[] buffer, int offset, int count) { throw new NotImplementedException(); }


        #endregion


    }
}
