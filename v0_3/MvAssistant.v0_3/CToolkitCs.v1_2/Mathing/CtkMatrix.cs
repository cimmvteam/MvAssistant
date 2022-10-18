using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Mathing
{
    public class TkMatrix<T> : CtkOperator<TkMatrix<T>> where T : CtkOperator<T>, new()
    {
        protected T[,] m_data;
        public T[,] Data { get { return m_data; } }

        public bool IsSquare { get { return this.RowLen == this.ColLen; } }
        public int RowLen { get { return this.m_data.GetLength(0); } }
        public int ColLen { get { return this.m_data.GetLength(1); } }

        private static T _tIdentity { get { return new T().GetIdentity(); ; } }
        private static T _tZero { get { return new T().GetZero(); } }



        #region Usage matrix property
        public T m11 { get { return this.m_data[0, 0]; } set { this.m_data[0, 0] = value; } }
        public T m12 { get { return this.m_data[0, 1]; } set { this.m_data[0, 1] = value; } }
        public T m13 { get { return this.m_data[0, 2]; } set { this.m_data[0, 2] = value; } }
        public T m14 { get { return this.m_data[0, 3]; } set { this.m_data[0, 3] = value; } }
        public T m21 { get { return this.m_data[1, 0]; } set { this.m_data[1, 0] = value; } }
        public T m22 { get { return this.m_data[1, 1]; } set { this.m_data[1, 1] = value; } }
        public T m23 { get { return this.m_data[1, 2]; } set { this.m_data[1, 2] = value; } }
        public T m24 { get { return this.m_data[1, 3]; } set { this.m_data[1, 3] = value; } }
        public T m31 { get { return this.m_data[2, 0]; } set { this.m_data[2, 0] = value; } }
        public T m32 { get { return this.m_data[2, 1]; } set { this.m_data[2, 1] = value; } }
        public T m33 { get { return this.m_data[2, 2]; } set { this.m_data[2, 2] = value; } }
        public T m34 { get { return this.m_data[2, 3]; } set { this.m_data[2, 3] = value; } }
        public T m41 { get { return this.m_data[3, 0]; } set { this.m_data[3, 0] = value; } }
        public T m42 { get { return this.m_data[3, 1]; } set { this.m_data[3, 1] = value; } }
        public T m43 { get { return this.m_data[3, 2]; } set { this.m_data[3, 2] = value; } }
        public T m44 { get { return this.m_data[3, 3]; } set { this.m_data[3, 3] = value; } }
        #endregion


        public TkMatrix() { }
        public TkMatrix(int row, int col) { this.m_data = new T[row, col]; }
        public TkMatrix(T[,] data) { this.m_data = data; }
        public TkMatrix(TkMatrix<T> matrix) { this.m_data = new T[matrix.RowLen, matrix.ColLen]; this.set(matrix); }

        public virtual T this[int i, int j] { get { return this.m_data[i - 1, j - 1]; } set { this.m_data[i - 1, j - 1] = value; } }


        public void set(TkMatrix<T> matrix)
        {
            if (this.RowLen != matrix.RowLen
                || this.ColLen != matrix.ColLen) { throw new CtkException("Different length of data"); }

            for (int i = 0; i < matrix.RowLen; i++)
            { for (int j = 0; j < matrix.ColLen; j++) { this.m_data[i, j] = matrix.m_data[i, j]; } }
        }

        #region Cal


        public T cofactor(int ridx, int cidx)
        { return cofactor(ridx - 1, cidx - 1); }
        public T cofactor_zb(int ridx, int cidx)
        {
            int rowlen = this.RowLen;
            int collen = this.ColLen;
            if (rowlen < 2 || collen < 2) { throw new CtkException("The Length need > 1"); }
            TkMatrix<T> rs = new TkMatrix<T>(rowlen - 1, collen - 1);


            T factor = (ridx + cidx) % 2 == 0 ? _tIdentity : _tIdentity.Negative();

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
            return factor.Multiply(rs.determinant());
        }



        public T determinant()
        {
            if (!IsSquare) { throw new CtkException("The non-Square Matrix cannot determinant calculate."); }


            if (this.RowLen == 3)
            {
                return this.m_data[0, 0].Multiply(this.m_data[1, 1]).Multiply(this.m_data[2, 2])
                   .Add(this.m_data[0, 1].Multiply(this.m_data[1, 2]).Multiply(this.m_data[2, 0]))
                   .Add(this.m_data[1, 0].Multiply(this.m_data[2, 1]).Multiply(this.m_data[0, 2]))
                   .Subtract(this.m_data[0, 2].Multiply(this.m_data[1, 1]).Multiply(this.m_data[2, 0]))
                   .Subtract(this.m_data[0, 1].Multiply(this.m_data[1, 0]).Multiply(this.m_data[2, 2]))
                   .Subtract(this.m_data[0, 0].Multiply(this.m_data[1, 2]).Multiply(this.m_data[2, 1]));
            }
            if (this.RowLen == 2)
            { return this.m_data[0, 0].Multiply(this.m_data[1, 1]).Subtract(this.m_data[0, 1].Multiply(this.m_data[1, 0])); }
            if (this.RowLen == 1) { return this.m_data[0, 0]; }


            T det = _tZero;
            for (int i = 0; i < RowLen; i++)
            {
                if (this.m_data[i, 0].Equal(_tZero)) { continue; }
                det = det.Add(this.m_data[i, 0].Multiply(this.cofactor_zb(i, 0)));
            }
            return det;
        }



        public TkMatrix<T> adj()
        {
            TkMatrix<T> rs = new TkMatrix<T>(this.ColLen, this.RowLen);

            for (int i = 0; i < rs.RowLen; i++)
            {
                for (int j = 0; j < rs.ColLen; j++)
                { rs.m_data[i, j] = this.cofactor_zb(j, i); }
            }
            return rs;
        }


        public TkMatrix<T> inverse()
        {
            T det = this.determinant();
            if (det.Equal(_tZero)) { throw new CtkException("The matrix is non-invertible"); }
            return adj() / det;
        }


        #endregion


        #region TkOperator
        public override void Normalization()
        {
            for (int i = 0; i < RowLen; i++)
            {
                for (int j = 0; j < ColLen; j++)
                { this.m_data[i, j].Normalization(); }
            }
        }
        public override TkMatrix<T> GetNormalization()
        {
            var rs = new TkMatrix<T>(this.RowLen, this.ColLen);
            for (int i = 0; i < RowLen; i++)
            {
                for (int j = 0; j < ColLen; j++)
                { rs.m_data[i, j] = this.m_data[i, j].GetNormalization(); }
            }
            return rs;
        }


        #endregion


        #region Operator

        public static TkMatrix<T> operator *(TkMatrix<T> m, T r)
        {
            TkMatrix<T> rs = new TkMatrix<T>(m);

            for (int i = 0; i < rs.RowLen; i++)
            {
                for (int j = 0; j < rs.ColLen; j++)
                { rs.m_data[i, j] = rs.m_data[i, j].Multiply(r); }
            }

            return rs;
        }
        public static TkMatrix<T> operator *(T r, TkMatrix<T> m)
        {
            TkMatrix<T> rs = new TkMatrix<T>(m);
            for (int i = 0; i < rs.RowLen; i++)
            {
                for (int j = 0; j < rs.ColLen; j++)
                { rs.m_data[i, j] = rs.m_data[i, j].Multiply(r); }
            }
            return rs;
        }
        public static TkMatrix<T> operator /(TkMatrix<T> m, T r)
        {
            TkMatrix<T> rs = new TkMatrix<T>(m);
            for (int i = 0; i < rs.RowLen; i++)
            {
                for (int j = 0; j < rs.ColLen; j++)
                { rs.m_data[i, j] = rs.m_data[i, j].Divide(r); }
            }
            return rs;
        }
        public static TkMatrix<T> operator /(T r, TkMatrix<T> m)
        {
            TkMatrix<T> rs = new TkMatrix<T>(m);
            for (int i = 0; i < rs.RowLen; i++)
            {
                for (int j = 0; j < rs.ColLen; j++)
                { rs.m_data[i, j] = rs.m_data[i, j].Divide(r); }
            }
            return rs;
        }

        public static TkMatrix<T> operator *(TkMatrix<T> m1, TkMatrix<T> m2)
        {
            TkMatrix<T> rs = new TkMatrix<T>(m1.RowLen, m1.ColLen);

            for (int i = 0; i < rs.RowLen; i++)
            {
                for (int j = 0; j < rs.ColLen; j++)
                {
                    rs.m_data[i, j] = _tZero;
                    for (int k = 0; k < rs.ColLen; k++)
                    { rs.m_data[i, j] = rs.m_data[i, j].Add(m1.m_data[i, k].Multiply(m2.m_data[k, j])); }
                }
            }
            return rs;
        }


        #endregion

        #region Row Operation
        public TkMatrix<T> rowOperateExchange(int rowi, int rowj)
        { return rowOperateExchange_zb(rowi - 1, rowj - 1); }
        public TkMatrix<T> rowOperateExchange_zb(int rowi, int rowj)
        {
            TkMatrix<T> rs = new TkMatrix<T>(this.RowLen, this.ColLen);

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
        public TkMatrix<T> rowOperateMutiple(int rowi, T factor)
        { return rowOperateMutiple_zb(rowi - 1, factor); }
        public TkMatrix<T> rowOperateMutiple_zb(int rowi, T factork)
        {
            TkMatrix<T> rs = new TkMatrix<T>(this.RowLen, this.ColLen);

            for (int colIdx = 0; colIdx < rs.ColLen; colIdx++)
            { rs.m_data[rowi, colIdx] = factork.Multiply(this.m_data[rowi, colIdx]); }

            for (int rowIdx = 0; rowIdx < rs.RowLen; rowIdx++)
            {
                if (rowIdx == rowi) { continue; }
                for (int colIdx = 0; colIdx < rs.ColLen; colIdx++)
                { rs.m_data[rowIdx, colIdx] = this.m_data[rowIdx, colIdx]; }
            }
            return rs;
        }
        public TkMatrix<T> rowOperateAddMultiple(int rowi, int rowj, T factor)
        { return rowOperateAddMultiple_zb(rowi - 1, rowj - 1, factor); }
        public TkMatrix<T> rowOperateAddMultiple_zb(int rowi, int rowj, T factork)
        {
            TkMatrix<T> rs = new TkMatrix<T>(this.RowLen, this.ColLen);

            for (int colIdx = 0; colIdx < rs.ColLen; colIdx++)
            { rs.m_data[rowj, colIdx] = this.m_data[rowj, colIdx].Add(factork.Multiply(this.m_data[rowi, colIdx])); }

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
                    sb.Append(this.m_data[i, j].ToOperatorString());
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
