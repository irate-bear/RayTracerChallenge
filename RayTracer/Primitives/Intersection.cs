using RayTracer.Tuple;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Primitives
{
    public class Intersections : IEnumerable<Intersection>
    {
        List<Intersection> intersections;

        public Intersections(params Intersection[] intersects)
        {
            if (intersects.Length == 0)
            {
                intersections = new List<Intersection>();
            }
            else
            {
                this.intersections = intersects.OrderBy(i => i.T).ToList();
            }
        }
    
        public void AddIntersection(IShape shape, double[] ts)
        {
            if (ts.Length == 0)
            {
                return;
            }
            else
            {
                foreach(var t in ts)
                {
                    intersections.Add(new Intersection(shape, t));
                }
            }
            intersections.OrderBy(i => i.T);
        }

        public Intersection this[int index]
        {
            get => intersections[index];
        }

        public Intersection[] Hits()
        {
            List<Intersection> hits = new List<Intersection>(intersections.Count);
            //intersections.AsParallel().ForAll(i => hits.Add(i));
            foreach (var intersection in intersections)
            {
                if (intersection.T >= 0) hits.Add(intersection);
            }

            return hits.ToArray();
        }

        public Intersection Hit()
        {
            var hits = Hits();
            if (hits.Length == 0)
            {
                return null;
            }
            else
            {
                return hits[0];
            }
        }

        public IEnumerator<Intersection> GetEnumerator()
        {
            foreach (var intersection in intersections)
            {
                yield return intersection;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class Intersection
    {
        public Intersection(IShape shape, double t)
        {
            Shape = shape;
            T = t;

        }
    
        public IShape Shape { get; }
        public double T { get; }
        public Computation GetComputation
            (
                Ray ray, 
                Intersections intersections = null
            )
        {
            //Computation comp = new Computation();
            var t = T;
            var shape = Shape;
            var point = ray.Position(T);
            var eye = -ray.Direction;
            var normal = Shape.NormalAt(point);
            var isInside = false;
            if (normal.Dot(eye) < 0)
            {
                normal = -normal;
                isInside = true;
            }
            var reflect = ray.Direction.Reflect(normal);
            double n1 = 0.0;
            double n2 = 0.0;
            if (intersections != null)
            {
                var shapes = new List<IShape>();
                foreach(var intersection in intersections)
                {
                    if (intersection == this)
                    {
                        if (shapes.Count == 0)
                        {
                            n1 = 1.0;
                        }
                        else
                        {
                            n1 = shapes.Last().Material.RefractiveIndex;
                        }
                    }
                    if (shapes.Contains(intersection.Shape))
                    {
                        shapes.Remove(intersection.Shape);  
                    }
                    else
                    {
                        shapes.Add(intersection.Shape);
                    }
                    if (intersection == this)
                    {
                        if (shapes.Count == 0)
                        {
                            n2 = 1.0;
                        }
                        else
                        {
                            n2 = shapes.Last().Material.RefractiveIndex;
                        }
                        //return new Computation(t, isInside, shape, point, eye, normal, n1, n2);
                    }
                }
            }
            return new Computation(t, isInside, shape, point, eye, normal, reflect, n1, n2);
        }
    }

    public class Computation
    {
        public double T { get; set; }
        public double N1 { get; set; }
        public double N2 { get; set; }
        public bool IsInside { get; set; }
        public IShape Shape { get; set; }
        public Point Point { get; set; }
        public Vector Eye { get; set; }
        public Vector Normal { get; set; }
        public Vector Reflect { get; set; }
        public Point OverPoint 
        { 
            get;
            set; 
        }
        public Point UnderPoint 
        { 
            get;
            set; 
        }
        public Computation()
        {

        }
        public Computation(double t, bool isInside, IShape shape, Point point, Vector eye, Vector normal, Vector reflect,double n1 = 1.0, double n2 = 1.0)
        {
            T = t;
            IsInside = isInside;
            Shape = shape;
            Point = point;
            Eye = eye;
            Normal = normal;
            Reflect = reflect;
            OverPoint = Point + Normal * Extensions.EPSILON;
            UnderPoint = Point - Normal * Extensions.EPSILON;
            N1 = n1;
            N2 = n2;
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var item = (Computation)obj;
            return item.Eye == Eye &&
                item.Point == Point &&
                item.T == T &&
                item.Shape == Shape &&
                item.Normal == Normal;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
