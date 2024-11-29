

## RGB Rainmeter Plugin

The **RGB** plugin for Rainmeter generates dynamic RGB color values that cycle through the color spectrum. It outputs the RGB values as a string in the format `R,G,B` (e.g., `255,0,0`) and can be used in Rainmeter skins to create vibrant, color-changing effects.

---

### Features

- **Dynamic Color Cycling**: Continuously cycles through RGB colors for a smooth, animated effect.
- **Customizable Speed**: Allows you to control how quickly the colors change.
- **Easy Integration**: Outputs RGB values that can be directly used in Rainmeter meters like `FontColor` and `SolidColor`.

---

### Installation

1. **Download and Compile the Plugin**:
   - Clone this repository or download the source code.
   - Open the project in Visual Studio and compile it. The output `RGB.dll` will be located in the `bin\Release` directory.

2. **Move the DLL**:
   - Copy the compiled `RGB.dll` to the `Plugins` folder in your Rainmeter installation (e.g., `C:\Program Files\Rainmeter\Plugins`).

3. **Reload Rainmeter**:
   - Restart Rainmeter to ensure the plugin is loaded.

---

### Usage

Add a measure to your Rainmeter skin that uses the `RGB` plugin. Below is an example of a basic Rainmeter skin using the plugin.

#### Example Skin

```ini
[Rainmeter]
Update=16

[MeasureRGB]
Measure=Plugin
Plugin=RGB
Speed=5 ; Control how fast the colors cycle (default is 1.0)

[Background]
Meter=Shape 
Shape=Rectangle 0,0,(400),(100),(8)|StrokeWidth 0 | FillColor 255,255,255,100
DynamicVariables=1

[MeterRGB]
Meter=String
MeasureName=MeasureRGB
Text="RGB Color: %1"
FontColor=[MeasureRGB]
FontSize=16
AntiAlias=1
DynamicVariables=1
```

#### Parameters

- **`Speed`** (optional): Determines how quickly the colors cycle. Higher values increase the speed. Default is `1.0`.

---

### Output Format

The plugin outputs a string in the format:

```
R,G,B
```

For example:

- `255,0,0` (Red)
- `0,255,0` (Green)
- `0,0,255` (Blue)

This output can be used directly in Rainmeter for:

- `FontColor`
- `SolidColor`
- `GradientColor`

---

### How It Works

1. The plugin cycles through the **hue** (0–360) of the HSL color model.
2. The hue is converted to RGB values using a `HueToRGB` function.
3. The plugin outputs the RGB values as a comma-separated string.

---

### Advanced Usage

You can use the RGB plugin with other Rainmeter features to create more complex effects. For example, change the background color of a meter dynamically:

```ini
[Rainmeter]
Update=16

[MeasureRGB]
Measure=Plugin
Plugin=RGB
Speed=5

[Background]
Meter=Shape 
Shape=Rectangle 0,0,(400),(100),(8)|StrokeWidth 0 | FillColor 255,255,255,100
DynamicVariables=1

[MeterRGB]
Meter=String
X=200r
Y=50r
MeasureName=MeasureRGB
Text="RGB Color: %1"
FontColor=[MeasureRGB]
FontSize=16
AntiAlias=1
StringAlign=CenterCenter
DynamicVariables=1
```

---

### Building from Source

1. Install **Visual Studio** (with C# development tools).
2. Open the project file in the source directory.
3. Build the solution in `Release` mode.
4. Copy the generated `RGB.dll` to your Rainmeter `Plugins` folder.

---

### Contributing

Contributions are welcome! If you have ideas for improvements or encounter issues, feel free to:

1. Open an issue on GitHub.
2. Fork the repository, make your changes, and submit a pull request.

---

### License

This plugin is licensed under the MIT License. Feel free to use, modify, and distribute it as long as attribution is provided.

---

### Credits

Developed by **Nasir Shahbaz**. Special thanks to the Rainmeter community for their support and resources.
