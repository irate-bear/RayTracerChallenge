using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Tests.ExtensionTests
{
    public class DoublesExtensions
    {
        [Fact]
        public void Doubles_Absolute_Difference()
        {

            double a = 3;
            double b = 3.000000000000001;

            var actual = a.AbsDiff(b);
            
            Assert.True(actual);
        }
    }
}
