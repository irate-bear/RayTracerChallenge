using RayTracer.Display;
using RayTracer.Materials;
using RayTracer.Matrices;
using RayTracer.Primitives;
using RayTracer.Tuple;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Scene
{
    public class World
    {
        const int COUNT = 5;
        public World() { }
        public World(ILight light, IShape[] shapes)
        {
            Light = light;
            Shapes = shapes;
        }

        public ILight Light { get; set; }
        public IShape[] Shapes { get; set; }

        public static World DefaultWorld()
        {
            ILight light = new PointLight(new Point(-10, 10, -10), new Color(1, 1, 1));
            IShape s1 = new Sphere();
            Material m = new Material();
            m.Color = new Color(0.8, 1.0, 0.6);
            m.Diffuse = 0.7;
            m.Specular = 0.2;
            s1.Material = m;
            IShape s2 = new Sphere();
            s2.Transformation = Matrix4D.Scaling(0.5, 0.5, 0.5);
            return new World(light, new IShape[] { s1, s2 });
        }

        public Intersections Intersect(Ray r)
        {
            Intersections intersections = new Intersections();
            /*Shapes
                .ToList()
                .AsParallel()
                .ForAll(s => intersections.AddIntersection(s, s.Intersects(r)));
            */
            foreach (IShape shape in Shapes)
            {
                intersections.AddIntersection(shape, shape.Intersects(r));
            }
            return intersections;
        }

        public Color ShadeHit(Computation comp, int count = COUNT)
        {
            var shadowed = IsShadowed(comp.OverPoint);
            var surface = comp.Shape
                .Material
                .Lighting(comp.Shape, Light, comp.OverPoint, comp.Eye, comp.Normal, shadowed);
            var reflected = ReflectedColor(comp, count);
            var refracted = RefractedColor(comp, count);

            var material = comp.Shape.Material;
            if (material.Reflective > 0 && material.Transparency > 0)
            {
                var reflectance = Schlick(comp);
                return surface + reflected * reflectance + refracted * (1 - reflectance);
            }
            return surface + reflected + refracted;
        }

        public Color ColorAt(Ray ray, int count = COUNT)
        {
            var intersection = Intersect(ray);
            var hit = intersection.Hit();
            if (hit == null)
            {
                return Color.Black;
            }
            return ShadeHit(hit.GetComputation(ray, intersection), count);
        }

        public bool IsShadowed(Point p)
        {
            Vector v = Light.Position - p;
            double distance = v.Magnitude();
            Vector direction = v.Normalize();

            Ray r = new Ray(p, direction);
            Intersections intersections = Intersect(r);

            Intersection h = intersections.Hit();
            return (h != null && h.T < distance);
        }

        public Color ReflectedColor(Computation comp, int count = COUNT)
        {
            if (comp.Shape.Material.Reflective == 0 || count <= 0)
            {
                return Color.Black;
            }

            var reflected = new Ray(comp.OverPoint, comp.Reflect);
            var color = ColorAt(reflected, count - 1);

            return color * comp.Shape.Material.Reflective;
        }        

        public Color RefractedColor(Computation comp, int count = COUNT)
        {
            if (count <= 0 || comp.Shape.Material.Transparency == 0) 
                return Color.Black;

            var nRatio = comp.N1 / comp.N2;
            var cosi = comp.Eye.Dot(comp.Normal);
            var sin2t = Math.Pow(nRatio, 2) * (1 - Math.Pow(cosi, 2));
            
            if (sin2t > 1) return Color.Black;

            var cost = Math.Sqrt(1.0 - sin2t);

            var direction = 
                comp.Normal * (nRatio * cosi - cost) - comp.Eye * nRatio;
            var refRay = new Ray(comp.UnderPoint, direction);
            return ColorAt(refRay, count - 1) * comp.Shape.Material.Transparency;
        }

        public static double Schlick(Computation comp)
        {
            var cos = comp.Eye.Dot(comp.Normal);
            if (comp.N1 > comp.N2)
            {
                var n = comp.N1 / comp.N2;
                var sin2t = Math.Pow(n, 2) * (1.0 - Math.Pow(cos, 2));
                if (sin2t > 1) return 1.0;

                var cost = Math.Sqrt(1.0 - sin2t);
                cos = cost;
            }
            var r = (comp.N1 - comp.N2) / (comp.N1 + comp.N2);
            var r0 = Math.Pow(r, 2);
            return r0 + (1 - r0) * Math.Pow((1 - cos), 5);
        }
    }
}
