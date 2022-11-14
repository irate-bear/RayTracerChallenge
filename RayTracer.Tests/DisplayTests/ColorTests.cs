using RayTracer.Display;

namespace RayTracer.Tests.DisplayTests
{
    public class ColorTests
    {
        [Theory]
        [InlineData(0,0,0, true)]
        [InlineData(0.0,0.90,088, true)]
        [InlineData(0,1,2, true)]
        [InlineData(0.0,1.4,2.3, true)]
        [InlineData(1,2,3, true)]
        public void Color_Creation(double red, double blue, double green, bool expected)
        {
            Color color = new Color(red, green, blue);
            bool actual = color.Red == red && color.Green == green && color.Blue == blue;

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void Add_Colors()
        {
            Color c1 = new Color(0.9, 0.6, 0.75);
            Color c2 = new Color(0.7, 0.1, 0.25);

            Color actual = c1 + c2;
            Color expected = new Color(1.6, 0.7, 1.0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Subtract_Colors()
        {
            Color c1 = new Color(0.9, 0.6, 0.75);
            Color c2 = new Color(0.7, 0.1, 0.25);

            Color expected = new Color(0.2, 0.5, 0.5);
            Color actual = c1 - c2;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Multiply_Color_By_Scalar()
        {
            Color c1 = new Color(0.2, 0.3, 0.4);

            Color expected = new Color(0.4, 0.6, 0.8);
            Color actual = 2 * c1;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Multiply_Colors()
        {
            Color c1 = new Color(1, 0.2, 0.4);
            Color c2 = new Color(0.9, 1, 0.1);

            Color expected = new Color(0.9, 0.2, 0.04);
            Color actual = c1 * c2;

            Assert.Equal(expected, actual);
        }
    }
}
