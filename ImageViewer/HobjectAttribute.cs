using HalconDotNet;
using System.Drawing;

namespace ImageViewer
{

    public struct HXLDAtt
    {
        public HXLDAtt(HObject xld, string color, int lineWidth)
        {
            Xld = xld;
            LineWidth = lineWidth;
            DrawColor = color;
        }
        public string DrawColor { get; set; }
        public HObject Xld { get; set; }
        public int LineWidth { get; set; }
    }
    public struct HRegionAtt
    {
        public HRegionAtt(HObject region, string name, string color, string mode, int lineWidth)
        {
            DrawColor = color;
            DrawMode = mode;
            Region = region;
            LineWidth = lineWidth;
            Name = name;
        }
        public string Name { get; set; }
        public int LineWidth { get; set; }
        public string DrawMode { get; set; }
        public string DrawColor { get; set; }
        public HObject Region { get; set; }
    }

    public struct HTextAtt
    {
        public HTextAtt(string text, string color, Point location, bool isFixed)
        {
            Text = text;
            DrawColor = color;
            DrawLocation = location;
            IsFixed = isFixed;
        }
        public string Text { get; set; }
        public string DrawColor { get; set; }
        public Point DrawLocation { get; set; }
        /// <summary>
        /// 是否固定位置
        /// </summary>
        public bool IsFixed { get; set; }
    }

    public static class HColors
    {
        public static string[] Colors = new string[] {
"red",
"green",
"blue",
"dim gray",
"gray",
"light gray",
"cyan",
"magenta",
"yellow",
"medium slate blue",
"coral",
"slate blue",
"spring green",
"orange red",
"dark olive green",
"pink",
"cadet blue",
"goldenrod",
"orange",
"gold",
"forest green",
"cornflower blue",
"navy",
"turquoise",
"dark slate blue",
"light blue",
"indian red",
"violet red",
"light steel blue",
"medium blue",
"khaki",
"violet",
"firebrick",
"midnight blue",
"sea green",
"dark turquoise",
"orchid",
"sienna",
"medium orchid",
"medium forest green",
"medium turquoise",
"medium violet red",
"salmon",
"blue violet",
"tan",
"pale green",
"sky blue",
"medium goldenrod",
"plum",
"thistle",
"dark orchid",
"maroon",
"dark green",
"steel blue",
"medium spring green",
"medium sea green",
"yellow green",
"medium aquamarine",
"lime green",
"aquamarine",
"wheat",
"green yellow",
"black",
"white",
        };
        public static string Black = "black";
        public static string White = "white";
        public static string Red = "red";
        public static string Green = "green";
        public static string Blue = "blue";
        public static string DimGray = "dim gray";
        public static string Gray = "gray";
        public static string LightGray = "light gray";
        public static string Cyan = "cyan";
        public static string Magenta = "magenta";
        public static string Yellow = "yellow";
        public static string MediumSlateBlue = "medium slate blue";
        public static string Coral = "coral";
        public static string SlateBlue = "slate blue";
        public static string SpringGreen = "spring green";
        public static string OrangeRed = "orange red";
        public static string DarkOliveGreen = "dark olive green";
        public static string Pink = "pink";
        public static string CadetBlue = "cadet blue";
        public static string Goldenrod = "goldenrod";
        public static string Orange = "orange";
        public static string Gold = "gold";
        public static string ForestGreen = "forest green";
        public static string CornflowerBlue = "cornflower blue";
        public static string Navy = "navy";
        public static string Turquoise = "turquoise";
        public static string DarkSlateBlue = "dark slate blue";
        public static string LightBlue = "light blue";
        public static string IndianRed = "indian red";
        public static string VioletRed = "violet red";
        public static string LightSteelBlue = "light steel blue";
        public static string MediumBlue = "medium blue";
        public static string Khaki = "khaki";
        public static string Violet = "violet";
        public static string Firebrick = "firebrick";
        public static string MidnightBlue = "midnight blue";
        public static string SeaGreen = "sea green";
        public static string DarkTurquoise = "dark turquoise";
        public static string Orchid = "orchid";
        public static string Sienna = "sienna";
        public static string MediumOrchid = "medium orchid";
        public static string MediumForestGreen = "medium forest green";
        public static string MediumTurquoise = "medium turquoise";
        public static string MediumVioletRed = "medium violet red";
        public static string Salmon = "salmon";
        public static string BlueViolet = "blue violet";
        public static string Tan = "tan";
        public static string PaleGreen = "pale green";
        public static string SkyBlue = "sky blue";
        public static string MediumGoldenrod = "medium goldenrod";
        public static string Plum = "plum";
        public static string Thistle = "thistle";
        public static string DarkOrchid = "dark orchid";
        public static string Maroon = "maroon";
        public static string DarkGreen = "dark green";
        public static string SteelBlue = "steel blue";
        public static string MediumSpringGreen = "medium spring green";
        public static string MediumSeaGreen = "medium sea green";
        public static string YellowGreen = "yellow green";
        public static string MediumAquamarine = "medium aquamarine";
        public static string LimeGreen = "lime green";
        public static string Aquamarine = "aquamarine";
        public static string Wheat = "wheat";
        public static string GreenYellow = "green yellow";
    }
}
