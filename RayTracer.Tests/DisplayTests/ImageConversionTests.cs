using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayTracer.Display;

namespace RayTracer.Tests.DisplayTests
{
    public class ImageConversionTests
    {
        [Fact]
        public void Convert_Canvas_To_PPM_String()
        {
            Canvas canvas = new Canvas(5, 3);
            Color c1 = new Color(1.5, 0, 0);
            Color c2 = new Color(0, 0.5, 0);
            Color c3 = new Color(-0.5, 0, 1);
            canvas[0, 0] = c1;
            canvas[2, 1] = c2;
            canvas[4, 2] = c3;

            string expected = "P3\n5 3\n255\n255 0 0 0 0 0 0 0 0 0 0 0 0 0 0\n0 0 0 0 0 0 0 128 0 0 0 0 0 0 0\n0 0 0 0 0 0 0 0 0 0 0 0 0 0 255\n";
            string actual = Image.CanvasToPPMString(canvas);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PPM_Line_Less_Than_70_Chars()
        {
            Canvas canvas = new Canvas(10,2, new Color(1.0, 0.8, 0.6));

            string actual = Image.CanvasToPPMString(canvas);
            string expected = "P3\n10 2\n255\n255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204\n153 255 204 153 255 204 153 255 204 153 255 204 153\n255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204\n153 255 204 153 255 204 153 255 204 153 255 204 153\n";
            Assert.Equal(expected, actual);
        }
    }
}
