using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CToolkitCs.v1_2.Paint
{


    public class HSLColor
    {
        public float Hue;
        public float Saturation;
        public float Luminosity;

        public HSLColor(float H, float S, float L)
        {
            Hue = H;
            Saturation = S;
            Luminosity = L;
        }

        public static HSLColor FromRGB(Color Clr)
        {
            return FromRGB(Clr.R, Clr.G, Clr.B);
        }




        public static HSLColor FromRGB(Byte R, Byte G, Byte B)
        {
            float _R = (R / 255f);
            float _G = (G / 255f);
            float _B = (B / 255f);

            float _Min = System.Math.Min(System.Math.Min(_R, _G), _B);
            float _Max = System.Math.Max(System.Math.Max(_R, _G), _B);
            float _Delta = _Max - _Min;

            float H = 0;
            float S = 0;
            float L = (float)((_Max + _Min) / 2.0f);

            if (_Delta != 0)
            {
                if (L < 0.5f)
                {
                    S = (float)(_Delta / (_Max + _Min));
                }
                else
                {
                    S = (float)(_Delta / (2.0f - _Max - _Min));
                }


                if (_R == _Max)
                {
                    H = (_G - _B) / _Delta;
                }
                else if (_G == _Max)
                {
                    H = 2f + (_B - _R) / _Delta;
                }
                else if (_B == _Max)
                {
                    H = 4f + (_R - _G) / _Delta;
                }
            }

            return new HSLColor(H, S, L);
        }




        public Color ToRGB()
        {
            byte r, g, b;
            if (Saturation == 0)
            {
                r = (byte)System.Math.Round(Luminosity * 255d);
                g = (byte)System.Math.Round(Luminosity * 255d);
                b = (byte)System.Math.Round(Luminosity * 255d);
            }
            else
            {
                double t1, t2;
                double th = Hue / 6.0d;

                if (Luminosity < 0.5d)
                {
                    t2 = Luminosity * (1d + Saturation);
                }
                else
                {
                    t2 = (Luminosity + Saturation) - (Luminosity * Saturation);
                }
                t1 = 2d * Luminosity - t2;

                double tr, tg, tb;
                tr = th + (1.0d / 3.0d);
                tg = th;
                tb = th - (1.0d / 3.0d);

                tr = ColorCalc(tr, t1, t2);
                tg = ColorCalc(tg, t1, t2);
                tb = ColorCalc(tb, t1, t2);
                r = (byte)System.Math.Round(tr * 255d);
                g = (byte)System.Math.Round(tg * 255d);
                b = (byte)System.Math.Round(tb * 255d);
            }
            return Color.FromArgb(r, g, b);
        }
        private static double ColorCalc(double c, double t1, double t2)
        {

            if (c < 0) c += 1d;
            if (c > 1) c -= 1d;
            if (6.0d * c < 1.0d) return t1 + (t2 - t1) * 6.0d * c;
            if (2.0d * c < 1.0d) return t2;
            if (3.0d * c < 2.0d) return t1 + (t2 - t1) * (2.0d / 3.0d - c) * 6.0d;
            return t1;
        }




        /// <summary>
        /// HSL to RGB conversion.
        /// </summary>
        /// <param name="h">The h.</param>
        /// <param name="sl">The sl.</param>
        /// <param name="l">The l.</param>
        /// <returns></returns>
        public static Color HSL2RGB(double h, double sl, double l)
        {

            double v;
            double r, g, b;
            r = l;   // default to gray
            g = l;
            b = l;
            v = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl);
            if (v > 0)
            {
                double m;
                double sv;
                int sextant;
                double fract, vsf, mid1, mid2;

                m = l + l - v;
                sv = (v - m) / v;
                h *= 6.0;
                sextant = (int)h;
                fract = h - sextant;
                vsf = v * sv * fract;
                mid1 = m + vsf;
                mid2 = v - vsf;
                switch (sextant)
                {
                    case 0:
                        r = v;
                        g = mid1;
                        b = m;
                        break;
                    case 1:
                        r = mid2;
                        g = v;
                        b = m;
                        break;
                    case 2:
                        r = m;
                        g = v;
                        b = mid1;
                        break;
                    case 3:
                        r = m;
                        g = mid2;
                        b = v;
                        break;
                    case 4:
                        r = mid1;
                        g = m;
                        b = v;
                        break;
                    case 5:
                        r = v;
                        g = m;
                        b = mid2;
                        break;
                }
            }
            var rgb = Color.FromArgb(
                Convert.ToByte(r * 255.0f), 
                Convert.ToByte(g * 255.0f), 
                Convert.ToByte(b * 255.0f)
                );
            return rgb;

        }


        // Given a Color (RGB Struct) in range of 0-255

        // Return H,S,L in range of 0-1

        /// <summary>
        /// RGB to HSL conversion
        /// </summary>
        /// <param name="rgb">The RGB.</param>
        /// <param name="h">The h.</param>
        /// <param name="s">The s.</param>
        /// <param name="l">The l.</param>
        public static void RGB2HSL(Color rgb, out double h, out double s, out double l)
        {
            double r = rgb.R / 255.0;
            double g = rgb.G / 255.0;
            double b = rgb.B / 255.0;
            double v;
            double m;
            double vm;
            double r2, g2, b2;

            h = 0; // default to black
            s = 0;
            l = 0;
            v = System.Math.Max(r, g);
            v = System.Math.Max(v, b);
            m = System.Math.Min(r, g);
            m = System.Math.Min(m, b);

            l = (m + v) / 2.0;
            if (l <= 0.0)
            {
                return;
            }
            vm = v - m;
            s = vm;
            if (s > 0.0)
            {
                s /= (l <= 0.5) ? (v + m) : (2.0 - v - m);
            }
            else
            {
                return;
            }
            r2 = (v - r) / vm;
            g2 = (v - g) / vm;
            b2 = (v - b) / vm;
            if (r == v)
            {
                h = (g == m ? 5.0 + b2 : 1.0 - g2);
            }
            else if (g == v)
            {
                h = (b == m ? 1.0 + r2 : 3.0 - b2);
            }
            else
            {
                h = (r == m ? 3.0 + g2 : 5.0 - r2);
            }
            h /= 6.0;
        }

    }









}
