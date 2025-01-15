
# RGB Plugin for Rainmeter

The **RGB Plugin** is a powerful and flexible plugin for Rainmeter that allows users to create and manage dynamic RGB lighting effects. The plugin provides various options for color animation, brightness control, saturation adjustments, and custom lighting patterns.

---

## Features

### Key Functionalities
- **Hue Animation**: Cycles through a spectrum of colors with customizable speed.
- **Saturation and Brightness Animation**: Gradually adjusts saturation and brightness over time.
- **Pulse Effect**: Simulates a pulsating light effect.
- **Rainbow Wave**: Creates a smooth rainbow color wave effect.
- **Breathing Effect**: Cycles saturation and brightness in a breathing pattern.
- **Random Colors**: Generates random RGB values for dynamic, unpredictable patterns.
- **Flicker Effect**: Adds intensity flickering to simulate lighting effects.
- **Color Patterns**: Supports custom RGB sequences for precise control over color transitions.
- **Glow Effect**: Creates a smooth glowing effect with adjustable speed.
- **Gradient Sweep**: Animates through a sequence of gradient colors.
- **Blink Effect**: Toggles brightness on and off at regular intervals.
- **Multi-Pulse Effect**: Combines multiple pulse animations for complex light patterns.

---

## Installation

1. **Download and Install Rainmeter**  
   Ensure you have the latest version of [Rainmeter](https://www.rainmeter.net/) installed.

2. **Install the Plugin**  
   Place the compiled `RGB.dll` plugin file into the `Plugins` folder in your Rainmeter directory:  
   `C:\Program Files\Rainmeter\Plugins`.



## Parameters

| Parameter                | Type    | Default | Description                                                                                 |
|--------------------------|---------|---------|---------------------------------------------------------------------------------------------|
| `Speed`                  | Double  | `1.0`   | Speed of hue cycling.                                                                       |
| `StartHue`               | Double  | `0.0`   | Starting hue value (0–360).                                                                |
| `EndHue`                 | Double  | `360.0` | Ending hue value (0–360).                                                                  |
| `Saturation`             | Double  | `1.0`   | Saturation level (0–1).                                                                     |
| `Brightness`             | Double  | `1.0`   | Brightness level (0–1).                                                                     |
| `AnimateSaturation`      | Integer | `0`     | Enable/Disable animated saturation (`1` for enable, `0` for disable).                       |
| `SaturationSpeed`        | Double  | `0.5`   | Speed of saturation animation.                                                              |
| `AnimateBrightness`      | Integer | `0`     | Enable/Disable animated brightness (`1` for enable, `0` for disable).                       |
| `BrightnessSpeed`        | Double  | `0.5`   | Speed of brightness animation.                                                              |
| `PulseEffect`            | Integer | `0`     | Enable/Disable pulse effect.                                                                |
| `PulseSpeed`             | Double  | `1.0`   | Speed of pulse effect.                                                                      |
| `RainbowWave`            | Integer | `0`     | Enable/Disable rainbow wave effect.                                                        |
| `WaveSpeed`              | Double  | `1.0`   | Speed of rainbow wave.                                                                      |
| `BreathingEffect`        | Integer | `0`     | Enable/Disable breathing effect.                                                            |
| `RandomColors`           | Integer | `0`     | Enable/Disable random color generation.                                                     |
| `FlickerEffect`          | Integer | `0`     | Enable/Disable flickering effect.                                                          |
| `FlickerIntensity`       | Double  | `0.2`   | Intensity of flickering.                                                                    |
| `ColorPattern`           | String  | `""`    | Pipe-separated list of RGB values (e.g., `255,0,0|0,255,0|0,0,255`).                        |
| `GlowEffect`             | Integer | `0`     | Enable/Disable glow effect.                                                                 |
| `GlowSpeed`              | Double  | `2.0`   | Speed of glow effect.                                                                       |
| `GradientSweep`          | Integer | `0`     | Enable/Disable gradient color animation.                                                    |
| `GradientColors`         | String  | `""`    | Pipe-separated list of RGB gradient colors (e.g., `255,0,0|0,255,0|0,0,255`).               |
| `BlinkEffect`            | Integer | `0`     | Enable/Disable blinking effect.                                                            |
| `BlinkSpeed`             | Double  | `1.0`   | Speed of blinking effect.                                                                   |
| `MultiPulseEffect`       | Integer | `0`     | Enable/Disable multiple pulse effects.                                                     |
| `MultiPulseSpeed1`       | Double  | `1.5`   | Speed of the first pulse.                                                                   |
| `MultiPulseSpeed2`       | Double  | `2.5`   | Speed of the second pulse.                                                                  |

---


## Contributing

Feel free to submit bug reports, feature requests, or contribute to the development of the RGB plugin. Fork this repository and submit pull requests with your enhancements.

---

## License

This plugin is licensed under the APACHE License. See the `LICENSE` file for details.

