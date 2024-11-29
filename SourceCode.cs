using System;
using System.Runtime.InteropServices;
using Rainmeter;

namespace PluginRGB
{
    internal class Measure
    {
        private double speed; // Speed at which colors change
        private double hue; // Current hue value (0-360)
        private API api;

        internal Measure()
        {
            speed = 1.0; // Default speed
            hue = 0.0; // Start with red
        }

        // Called when the measure is reloaded in Rainmeter
        internal void Reload(Rainmeter.API api, ref double maxValue)
        {
            this.api = api;

            // Read the speed parameter (optional)
            speed = api.ReadDouble("Speed", 1.0);
            if (speed <= 0)
            {
                speed = 1.0;
                api.Log(API.LogType.Warning, "PluginRGB.dll: Speed must be greater than 0. Defaulting to 1.0.");
            }

            hue = 0.0; // Reset the hue on reload
        }

        // Called by Rainmeter on each update cycle
        internal double Update()
        {
            // Increment hue based on speed
            hue += speed;
            if (hue > 360.0) // Keep hue in the range [0, 360]
            {
                hue -= 360.0;
            }

            return 0.0; // No numeric value needed
        }

        // Converts the current hue to an RGB value and returns it as a string
        internal string GetString()
        {
            var rgb = HueToRGB(hue);
            return $"{rgb.Item1},{rgb.Item2},{rgb.Item3}"; // Return as "R,G,B"
        }

        // Convert hue to RGB (0-255 for each channel)
        private Tuple<int, int, int> HueToRGB(double hue)
        {
            double s = 1.0, v = 1.0; // Full saturation and brightness
            double c = v * s;
            double x = c * (1.0 - Math.Abs((hue / 60.0) % 2.0 - 1.0));
            double m = v - c;

            double r = 0, g = 0, b = 0;
            if (hue >= 0 && hue < 60) { r = c; g = x; b = 0; }
            else if (hue >= 60 && hue < 120) { r = x; g = c; b = 0; }
            else if (hue >= 120 && hue < 180) { r = 0; g = c; b = x; }
            else if (hue >= 180 && hue < 240) { r = 0; g = x; b = c; }
            else if (hue >= 240 && hue < 300) { r = x; g = 0; b = c; }
            else if (hue >= 300 && hue < 360) { r = c; g = 0; b = x; }

            // Convert to 0-255 range
            int R = (int)((r + m) * 255);
            int G = (int)((g + m) * 255);
            int B = (int)((b + m) * 255);

            return Tuple.Create(R, G, B);
        }
    }

    public static class Plugin
    {
        [DllExport]
        public static void Initialize(ref IntPtr data, IntPtr rm)
        {
            data = GCHandle.ToIntPtr(GCHandle.Alloc(new Measure()));
        }

        [DllExport]
        public static void Finalize(IntPtr data)
        {
            GCHandle.FromIntPtr(data).Free();
        }

        [DllExport]
        public static void Reload(IntPtr data, IntPtr rm, ref double maxValue)
        {
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;
            measure.Reload(new Rainmeter.API(rm), ref maxValue);
        }

        [DllExport]
        public static double Update(IntPtr data)
        {
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;
            return measure.Update();
        }

        [DllExport]
        public static IntPtr GetString(IntPtr data)
        {
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;
            string value = measure.GetString();
            return Marshal.StringToHGlobalUni(value);
        }
    }
}
