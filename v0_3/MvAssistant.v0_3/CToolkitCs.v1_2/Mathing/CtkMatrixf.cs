using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Mathing
{
    public class CtkMatrixf
    {
        protected float[,] m_data;
        public float[,] Data { get { return m_data; } }

        public bool IsSquare { get { return this.RowLen == this.ColLen; } }
        public int RowLen { get { return this.m_data.GetLength(0); } }
        public int ColLen { get { return this.m_data.GetLength(1); } }




        #region Usage matrix property
        public float m11 { get { return this.m_data[0, 0]; } set { this.m_data[0, 0] = value; } }
        public float m12 { get { return this.m_data[0, 1]; } set { this.m_data[0, 1] = value; } }
        public float m13 { get { return this.m_data[0, 2]; } set { this.m_data[0, 2] = value; } }
        public float m14 { get { return this.m_data[0, 3]; } set { this.m_data[0, 3] = value; } }
        public float m21 { get { return this.m_data[1, 0]; } set { this.m_data[1, 0] = value; } }
        public float m22 { get { return this.m_data[1, 1]; } set { this.m_data[1, 1] = value; } }
        public float m23 { get { return this.m_data[1, 2]; } set { this.m_data[1, 2] = value; } }
        public float m24 { get { return this.m_data[1, 3]; } set { this.m_data[1, 3] = value; } }
        public float m31 { get { return this.m_data[2, 0]; } set { this.m_data[2, 0] = value; } }
        public float m32 { get { return this.m_data[2, 1]; } set { this.m_data[2, 1] = value; } }
        public float m33 { get { return this.m_data[2, 2]; } set { this.m_data[2, 2] = value; } }
        public float m34 { get { return this.m_data[2, 3]; } set { this.m_data[2, 3] = value; } }
        public float m41 { get { return this.m_data[3, 0]; } set { this.m_data[3, 0] = value; } }
        public float m42 { get { return this.m_data[3, 1]; } set { this.m_data[3, 1] = value; } }
        public float m43 { get { return this.m_data[3, 2]; } set { this.m_data[3, 2] = value; } }
        public float m44 { get { return this.m_data[3, 3]; } set { this.m_data[3, 3] = value; } }
        #endregion



        public CtkMatrixf(int row, int col) { this.m_data = new float[row, col]; }
        public CtkMatrixf(float[,] data) { this.m_data = data; }
        public CtkMatrixf(CtkMatrixf matrix) { this.m_data = new float[matrix.RowLen, matrix.ColLen]; this.set(matrix); }

        public virtual float this[int i, int j] { set { this.m_data[i - 1, j - 1] = value; } get { return this.m_data[i - 1, j - 1]; } }


        public void set(CtkMatrixf matrix)
        {
            if (this.RowLen != matrix.RowLen
                || this.ColLen != matrix.ColLen) { throw new CtkException("Different length of data"); }

            for (int i = 0; i < matrix.RowLen; i++)
            { for (int j = 0; j < matrix.ColLen; j++) { this.m_data[i, j] = matrix.m_data[i, j]; } }
        }

        #region Cal


        public float cofactor(int ridx, int cidx)
        { return cofactor(ridx - 1, cidx - 1); }
        public float cofactor_zb(int ridx, int cidx)
        {
            int rowlen = this.RowLen;
            int collen = this.ColLen;
            if (rowlen < 2 || collen < 2) { throw new CtkException("The Length need > 1"); }
            CtkMatrixf rs = new CtkMatrixf(rowlen - 1, collen - 1);


            int factor = (ridx + cidx) % 2 == 0 ? 1 : -1;

            for (int i = 0, i1 = 0; i < rowlen; i++)
            {
                if (i == ridx) { continue; }

                for (int j = 0, j1 = 0; j < collen; j++)
                {
                    if (j == cidx) { continue; }
                    rs.m_data[i1, j1] = this.m_data[i, j];
                    j1++;
                }
                i1++;
            }
            return factor * rs.determinant();
        }



        public float determinant()
        {
            if (!IsSquare) { throw new CtkException("The non-Square Matrix cannot determinant calculate."); }


            if (this.RowLen == 3)
            {
                return this.m_data[0, 0] * this.m_data[1, 1] * this.m_data[2, 2]
                   + this.m_data[0, 1] * this.m_data[1, 2] * this.m_data[2, 0]
                   + this.m_data[1, 0] * this.m_data[2, 1] * this.m_data[0, 2]
                   - this.m_data[0, 2] * this.m_data[1, 1] * this.m_data[2, 0]
                   - this.m_data[0, 1] * this.m_data[1, 0] * this.m_data[2, 2]
                   - this.m_data[0, 0] * this.m_data[1, 2] * this.m_data[2, 1];
            }
            if (this.RowLen == 2)
            { return this.m_data[0, 0] * this.m_data[1, 1] - this.m_data[0, 1] * this.m_data[1, 0]; }
            if (this.RowLen == 1) { return this.m_data[0, 0]; }


            float det = 0;
            for (int i = 0; i < RowLen; i++)
            {
                if (this.m_data[i, 0] == 0.0f) { continue; }
                det += this.m_data[i, 0] * this.cofactor_zb(i, 0);
            }
            return det;
        }



        public CtkMatrixf adj()
        {
            CtkMatrixf rs = new CtkMatrixf(this.ColLen, this.RowLen);

            for (int i = 0; i < rs.RowLen; i++)
            {
                for (int j = 0; j < rs.ColLen; j++)
                { rs.m_data[i, j] = this.cofactor_zb(j, i); }
            }
            return rs;
        }


        public CtkMatrixf inverse()
        { return adj() / this.determinant(); }


        #endregion





        #region Operator

        public static CtkMatrixf operator *(CtkMatrixf m, float r)
        {
            CtkMatrixf rs = new CtkMatrixf(m);

            for (int i = 0; i < rs.RowLen; i++)
            {
                for (int j = 0; j < rs.ColLen; j++)
                { rs.m_data[i, j] *= r; }
            }

            return rs;
        }
        public static CtkMatrixf operator *(float r, CtkMatrixf m)
        {
            CtkMatrixf rs = new CtkMatrixf(m);
            for (int i = 0; i < rs.RowLen; i++)
            {
                for (int j = 0; j < rs.ColLen; j++)
                { rs.m_data[i, j] *= r; }
            }
            return rs;
        }
        public static CtkMatrixf operator /(CtkMatrixf m, float r)
        {
            CtkMatrixf rs = new CtkMatrixf(m);
            for (int i = 0; i < rs.RowLen; i++)
            {
                for (int j = 0; j < rs.ColLen; j++)
                { rs.m_data[i, j] /= r; }
            }
            return rs;
        }
        public static CtkMatrixf operator /(float r, CtkMatrixf m)
        {
            CtkMatrixf rs = new CtkMatrixf(m);
            for (int i = 0; i < rs.RowLen; i++)
            {
                for (int j = 0; j < rs.ColLen; j++)
                { rs.m_data[i, j] /= r; }
            }
            return rs;
        }

        public static CtkMatrixf operator *(CtkMatrixf m1, CtkMatrixf m2)
        {
            CtkMatrixf rs = new CtkMatrixf(m1.RowLen, m1.ColLen);

            for (int i = 0; i < rs.RowLen; i++)
            {
                for (int j = 0; j < rs.ColLen; j++)
                {
                    rs.m_data[i, j] = 0;
                    for (int k = 0; k < rs.ColLen; k++)
                    { rs.m_data[i, j] += m1.m_data[i, k] * m2.m_data[k, j]; }
                }
            }
            return rs;
        }


        #endregion

        #region Row Operation
        public CtkMatrixf rowOperateExchange(int rowi, int rowj)
        { return rowOperateExchange_zb(rowi - 1, rowj - 1); }
        public CtkMatrixf rowOperateExchange_zb(int rowi, int rowj)
        {
            CtkMatrixf rs = new CtkMatrixf(this.RowLen, this.ColLen);

            for (int colIdx = 0; colIdx < rs.ColLen; colIdx++)
            {
                rs.m_data[rowi, colIdx] = this.m_data[rowj, colIdx];
                rs.m_data[rowj, colIdx] = this.m_data[rowi, colIdx];
            }

            for (int rowIdx = 0; rowIdx < rs.RowLen; rowIdx++)
            {
                if (rowIdx == rowi || rowIdx == rowj) { continue; }
                for (int colIdx = 0; colIdx < rs.ColLen; colIdx++)
                { rs.m_data[rowIdx, colIdx] = this.m_data[rowIdx, colIdx]; }
            }
            return rs;
        }
        public CtkMatrixf rowOperateMutiple(int rowi, float factor)
        { return rowOperateMutiple_zb(rowi - 1, factor); }
        public CtkMatrixf rowOperateMutiple_zb(int rowi, float factork)
        {
            CtkMatrixf rs = new CtkMatrixf(this.RowLen, this.ColLen);

            for (int colIdx = 0; colIdx < rs.ColLen; colIdx++)
            { rs.m_data[rowi, colIdx] = factork * this.m_data[rowi, colIdx]; }

            for (int rowIdx = 0; rowIdx < rs.RowLen; rowIdx++)
            {
                if (rowIdx == rowi) { continue; }
                for (int colIdx = 0; colIdx < rs.ColLen; colIdx++)
                { rs.m_data[rowIdx, colIdx] = this.m_data[rowIdx, colIdx]; }
            }
            return rs;
        }
        public CtkMatrixf rowOperateAddMultiple(int rowi, int rowj, float factor)
        { return rowOperateAddMultiple_zb(rowi - 1, rowj - 1, factor); }
        public CtkMatrixf rowOperateAddMultiple_zb(int rowi, int rowj, float factork)
        {
            CtkMatrixf rs = new CtkMatrixf(this.RowLen, this.ColLen);

            for (int colIdx = 0; colIdx < rs.ColLen; colIdx++)
            { rs.m_data[rowj, colIdx] = this.m_data[rowj, colIdx] + factork * this.m_data[rowi, colIdx]; }

            for (int rowIdx = 0; rowIdx < rs.RowLen; rowIdx++)
            {
                if (rowIdx == rowj) { continue; }
                for (int colIdx = 0; colIdx < rs.ColLen; colIdx++)
                { rs.m_data[rowIdx, colIdx] = this.m_data[rowIdx, colIdx]; }
            }
            return rs;
        }
        #endregion


        public string ToMatrixString()
        {
            System.Text.StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < this.RowLen; i++)
            {
                sb.Append("[");
                for (int j = 0; j < this.ColLen; j++)
                {
                    sb.Append(this.m_data[i, j]);
                    if (j < this.ColLen - 1) { sb.Append("\t, "); }
                }
                sb.Append("],\r\n");
            }
            sb.Append("]");
            return sb.ToString();
        }
        public override string ToString()
        {
            return this.ToMatrixString();
        }
    }
}
