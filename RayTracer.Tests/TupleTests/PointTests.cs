using RayTracer.Tuple;
namespace RayTracer.Tests.TupleTests;

public class PointTests
{
    [Fact]
    public void TwoPointsAreEqual()
    {
        Point p1 = new Point(1, 1.1, 2.1);
        Point p2 = new Point(1, 1.1, 2.1);

        var res = p1.Equals(p2);

        Assert.True(res);
    }

    [Fact]
    public void TwoTuplesAreNotEqual()
    {
        Point p1 = new Point(1.12346, 1.120001, 2.1);
        Point p2 = new Point(1.12345, 1.120002, 2.1);

        var res = p1.Equals(p2);

        Assert.False(res);
    }
}