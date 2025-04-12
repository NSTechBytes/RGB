using Rainmeter;
using System.Runtime.InteropServices;
using System;
using System.Timers;

internal class Measure
{
    private double speed, hue, startHue, endHue, saturation, brightness;
    private bool animateSaturation, animateBrightness, pulseEffect, rainbowWave, breathingEffect, randomColors, flickerEffect;
    private double saturationSpeed, brightnessSpeed, pulseSpeed, waveSpeed, flickerIntensity;
    private string[] colorPattern;
    private int colorIndex;
    private Random rand;
    private API api;

    private bool glowEffect, gradientSweep, blinkEffect, multiPulseEffect;
    private double glowSpeed, blinkSpeed, multiPulseSpeed1, multiPulseSpeed2;
    private string[] gradientColors;
    private int gradientIndex;

    // Field to track the last allocated unmanaged string pointer.
    private IntPtr lastStringPtr = IntPtr.Zero;

    internal Measure()
    {
        speed = 1.0;
        hue = 0.0;
        startHue = 0.0;
        endHue = 360.0;
        saturation = 1.0;
        brightness = 1.0;
        animateSaturation = false;
        animateBrightness = false;
        pulseEffect = false;
        rainbowWave = false;
        breathingEffect = false;
        randomColors = false;
        flickerEffect = false;
        saturationSpeed = 0.5;
        brightnessSpeed = 0.5;
        pulseSpeed = 1.0;
        waveSpeed = 1.0;
        flickerIntensity = 0.2;
        colorPattern = Array.Empty<string>();
        colorIndex = 0;
        rand = new Random();

        glowEffect = false;
        gradientSweep = false;
        blinkEffect = false;
        multiPulseEffect = false;
        glowSpeed = 2.0;
        blinkSpeed = 1.0;
        multiPulseSpeed1 = 1.5;
        multiPulseSpeed2 = 2.5;
        gradientColors = Array.Empty<string>();
        gradientIndex = 0;
    }

    internal void Reload(Rainmeter.API api, ref double maxValue)
    {
        this.api = api;

        // Read parameters
        speed = api.ReadDouble("Speed", 1.0);
        startHue = api.ReadDouble("StartHue", 0.0);
        endHue = api.ReadDouble("EndHue", 360.0);
        saturation = api.ReadDouble("Saturation", 1.0);
        brightness = api.ReadDouble("Brightness", 1.0);
        animateSaturation = api.ReadInt("AnimateSaturation", 0) == 1;
        animateBrightness = api.ReadInt("AnimateBrightness", 0) == 1;
        saturationSpeed = api.ReadDouble("SaturationSpeed", 0.5);
        brightnessSpeed = api.ReadDouble("BrightnessSpeed", 0.5);
        pulseEffect = api.ReadInt("PulseEffect", 0) == 1;
        pulseSpeed = api.ReadDouble("PulseSpeed", 1.0);
        rainbowWave = api.ReadInt("RainbowWave", 0) == 1;
        waveSpeed = api.ReadDouble("WaveSpeed", 1.0);
        breathingEffect = api.ReadInt("BreathingEffect", 0) == 1;
        randomColors = api.ReadInt("RandomColors", 0) == 1;
        flickerEffect = api.ReadInt("FlickerEffect", 0) == 1;
        flickerIntensity = api.ReadDouble("FlickerIntensity", 0.2);

        string pattern = api.ReadString("ColorPattern", "");
        colorPattern = pattern.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        colorIndex = 0;

        hue = startHue;
        saturation = Math.Max(0.0, Math.Min(1.0, saturation));
        brightness = Math.Max(0.0, Math.Min(1.0, brightness));

        glowEffect = api.ReadInt("GlowEffect", 0) == 1;
        glowSpeed = api.ReadDouble("GlowSpeed", 2.0);

        gradientSweep = api.ReadInt("GradientSweep", 0) == 1;
        string gradient = api.ReadString("GradientColors", "");
        gradientColors = gradient.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        gradientIndex = 0;

        blinkEffect = api.ReadInt("BlinkEffect", 0) == 1;
        blinkSpeed = api.ReadDouble("BlinkSpeed", 1.0);

        multiPulseEffect = api.ReadInt("MultiPulseEffect", 0) == 1;
        multiPulseSpeed1 = api.ReadDouble("MultiPulseSpeed1", 1.5);
        multiPulseSpeed2 = api.ReadDouble("MultiPulseSpeed2", 2.5);
    }

    internal double Update()
    {
        // Store tick count once to reuse
        int tick = Environment.TickCount;

        // Update hue
        hue += speed;
        if (hue > endHue) hue = startHue + (hue - endHue);
        else if (hue < startHue) hue = endHue - (startHue - hue);

        // Rainbow wave
        if (rainbowWave)
        {
            hue = (startHue + Math.Sin(tick * waveSpeed * Math.PI / 180.0) * (endHue - startHue) / 2.0) % 360.0;
        }

        // Animate saturation
        if (animateSaturation)
        {
            saturation = 0.5 + 0.5 * Math.Sin(hue * saturationSpeed * Math.PI / 180.0);
        }

        // Animate brightness
        if (animateBrightness)
        {
            brightness = 0.7 + 0.3 * Math.Cos(hue * brightnessSpeed * Math.PI / 180.0);
        }

        // Pulse effect
        if (pulseEffect)
        {
            brightness = 0.5 + 0.5 * Math.Sin(tick * pulseSpeed * Math.PI / 180.0);
        }

        // Breathing effect
        if (breathingEffect)
        {
            double time = tick * 0.002;
            saturation = 0.5 + 0.5 * Math.Sin(time);
            brightness = 0.5 + 0.5 * Math.Cos(time);
        }

        // Random colors
        if (randomColors)
        {
            hue = rand.NextDouble() * 360.0;
            saturation = 0.5 + 0.5 * rand.NextDouble();
            brightness = 0.5 + 0.5 * rand.NextDouble();
        }

        // Flicker effect
        if (flickerEffect)
        {
            brightness += (rand.NextDouble() * 2.0 - 1.0) * flickerIntensity;
            brightness = Math.Max(0.0, Math.Min(1.0, brightness));
        }

        // Glow effect
        if (glowEffect)
        {
            brightness = 0.5 + 0.5 * Math.Sin(tick * glowSpeed * Math.PI / 180.0);
        }

        // Gradient sweep
        if (gradientSweep && gradientColors.Length > 0)
        {
            string[] rgb = gradientColors[gradientIndex].Split(',');
            hue = Convert.ToDouble(rgb[0]) % 360.0;
            saturation = Convert.ToDouble(rgb[1]) / 255.0;
            brightness = Convert.ToDouble(rgb[2]) / 255.0;
            gradientIndex = (gradientIndex + 1) % gradientColors.Length;
        }

        // Blink effect
        if (blinkEffect)
        {
            brightness = (Math.Sin(tick * blinkSpeed * Math.PI / 180.0) > 0) ? 1.0 : 0.0;
        }

        // Multi-pulse effect
        if (multiPulseEffect)
        {
            brightness = 0.5 + 0.3 * Math.Sin(tick * multiPulseSpeed1 * Math.PI / 180.0)
                       + 0.2 * Math.Cos(tick * multiPulseSpeed2 * Math.PI / 180.0);
        }

        // Custom color pattern
        if (colorPattern.Length > 0)
        {
            string[] rgb = colorPattern[colorIndex].Split(',');
            hue = Convert.ToDouble(rgb[0]) % 360.0;
            saturation = Convert.ToDouble(rgb[1]) / 255.0;
            brightness = Convert.ToDouble(rgb[2]) / 255.0;
            colorIndex = (colorIndex + 1) % colorPattern.Length;
        }

        return 0.0;
    }

    // Returns the computed string in managed memory.
    internal string GetStringValue()
    {
        var rgb = HueToRGB(hue, saturation, brightness);
        return $"{rgb.Item1},{rgb.Item2},{rgb.Item3}";
    }

    // Gets the string pointer to be used by Rainmeter.
    internal IntPtr GetString()
    {
        string value = GetStringValue();

        // Free previous allocation if it exists.
        if (lastStringPtr != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(lastStringPtr);
            lastStringPtr = IntPtr.Zero;
        }

        lastStringPtr = Marshal.StringToHGlobalUni(value);
        return lastStringPtr;
    }

    private Tuple<int, int, int> HueToRGB(double hue, double saturation, double brightness)
    {
        double c = brightness * saturation;
        double x = c * (1.0 - Math.Abs((hue / 60.0) % 2.0 - 1.0));
        double m = brightness - c;

        double r = 0, g = 0, b = 0;
        if (hue >= 0 && hue < 60) { r = c; g = x; b = 0; }
        else if (hue >= 60 && hue < 120) { r = x; g = c; b = 0; }
        else if (hue >= 120 && hue < 180) { r = 0; g = c; b = x; }
        else if (hue >= 180 && hue < 240) { r = 0; g = x; b = c; }
        else if (hue >= 240 && hue < 300) { r = x; g = 0; b = c; }
        else if (hue >= 300 && hue < 360) { r = c; g = 0; b = x; }

        int R = (int)((r + m) * 255);
        int G = (int)((g + m) * 255);
        int B = (int)((b + m) * 255);

        return Tuple.Create(R, G, B);
    }

    // Cleanup method to free unmanaged memory.
    internal void Cleanup()
    {
        if (lastStringPtr != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(lastStringPtr);
            lastStringPtr = IntPtr.Zero;
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
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;
            measure.Cleanup();
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
            return measure.GetString();
        }
    }
}
