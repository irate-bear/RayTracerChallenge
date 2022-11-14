using RayTracer.Tuple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracerDemo.Demos
{
    public class Projectile
    {
        private readonly Point pos;
        private Vector vel;
        public Projectile(Point pos, Vector vel)
        {
            this.pos = pos;
            this.vel = vel;
        }

        public Point Position => pos;
        public Vector Velocity { get => vel; set => vel = value; }
    }

    public class Environment
    {
        public Environment(Vector gravity, Vector friction)
        {
            Gravity = gravity;
            Wind = friction;
        }

        public Vector Gravity { get; }
        public Vector Wind { get; }
    }
}
