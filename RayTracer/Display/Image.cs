using System.Text;

namespace RayTracer.Display
{
    public class Image
    {
        public static string CanvasToPPMString(Canvas canvas)
        {
            StringBuilder ppm = new StringBuilder(
                $"P3\n{canvas.Width} {canvas.Height}\n255\n",
                canvas.Height * canvas.Width * 4
            );

            int lineLength = 0;
            for (int i = 0; i < canvas.Height; i++)
            {
                for (int j = 0; j < canvas.Width; j++)
                {
                    foreach (int color in (canvas[j, i]).To256Bit())
                    {
                        if ((lineLength + color.ToString().Length) > 70)
                        {
                            lineLength = 0;
                            ppm.Remove(ppm.Length - 1, 1);
                            ppm.Append($"\n{color} ");
                        }
                        else
                        {
                            lineLength += color.ToString().Length + 1;
                            ppm.Append($"{color} ");
                        }
                    }
                }
                ppm.Remove(ppm.Length - 1, 1);
                ppm.Append("\n");
                lineLength = 0;
            }
            
            return ppm.ToString();
        }
    }
}
