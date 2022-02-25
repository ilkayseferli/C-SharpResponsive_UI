﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernUI
{
    public static class ThemeColor
    {
        public static Color PrimaryColor { get; set; }
        public static Color SecondaryColor { get;set; }
        public static List<string> colorList = new List<string>()
        {
            "#3F51B5",
            "#009688",
            "#FF5722",
            "#607D8B",
            "#FF9800",
            "#9C27B0",
            
        };
        public static Color ChangeColorBrightness(Color color,double correctionFactor)
        {
            double red = color.R;
            double green = color.G;
            double blue = color.B;

            //correctionfactor sıfırdan küçükse renkler koyu olur -
            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            //correctionfactor sıfırdan büyükse renkler açık olur +
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }
            return Color.FromArgb(color.A,(byte)red, (byte)green, (byte)blue);
        }
    }

}
