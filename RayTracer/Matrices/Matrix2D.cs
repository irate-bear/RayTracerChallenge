using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Matrices
{
    public class Matrix2D : Matrix
    {
        public Matrix2D(double[][] values) : base(values)
        {
        }

        public Matrix2D() : base(2, 2)
        {
        }

        public Matrix2D(params double[] values) : base(2, 2, values)
        {
        }

        public double Determinant()
        {
            return this[0,0] * this[1,1] - 
                this[0,1] * this[1,0];
        }
    }
}
