using RayTracer.Display;
using RayTracer.Primitives;
using RayTracer.Tuple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.Materials
{
    public class Material
    {
        public Material() : this(0.1, 0.9, new Color(1, 1, 1), 0.9, 200.0, 0.0, 0.0, 1.0)
        {

        }
        public Material(double ambient, double diffuse, Color color, double specular, double shininess, double reflective, double transparency, double refractiveIndex)
        {
            Ambient = ambient;
            Diffuse = diffuse;
            Color = color;
            Specular = specular;
            Shininess = shininess;
            Reflective = reflective;
            Transparency = transparency;
            RefractiveIndex = refractiveIndex;
        }

        public IPattern Pattern { get; set; }
        public double Ambient { get; set; }
        public double Diffuse { get; set; }
        public Color Color { get; set; }
        public double Specular { get; set; }
        public double Shininess { get; set; }
        public double Reflective { get; set; }
        public double Transparency { get; set; }
        public double RefractiveIndex { get; set; }

        public Color Lighting(IShape shape, ILight light, Point position, Vector eye, Vector normal, bool inShadow)
        {
            Color color = 
                (Pattern is not null) ? Pattern.PatternOnShape(shape,position) : Color;

            Color eff_color = color * light.Intensity;
            Vector light_v = (light.Position - position).Normalize();

            Color ambient = eff_color * Ambient;
            double ldn =  light_v.Dot(normal);

            Color diffuse = Color.Black;
            Color specular = Color.Black;
            if (ldn >= 0)
            {
                diffuse = eff_color * Diffuse * ldn;
                double reflect_eye = (-light_v).Reflect(normal).Dot(eye);
                if (reflect_eye > 0)
                {
                    var factor = Math.Pow(reflect_eye, Shininess);
                    specular = light.Intensity * Specular * factor;
                }
            }
            if (inShadow) return ambient;
            else return ambient + diffuse + specular;
        }
    }
}
