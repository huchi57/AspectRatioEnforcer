# AspectRatioEnforcer
Make sure the game view is in designated aspect ratio by adding letterbox / pillarbox, and resizing the camera viewport.
This works both in Play Mode and Edit Mode.

## Installation
1. Download the full repository (including the `Editor` folder).
2. Import the full folder into your Unity project.
3. You can try out the test scene under the `Test` folder to see how it works.

## How to Use
1. Add `AspectRatioEnforcer.cs` to a camera object.
2. By default, `Preview In Edit Mode` is enabled, so the script should also work in Edit Mode. Uncheck to use it only in Play Mode.
3. Edit the designated `Aspect Ratio` value, or click on one of the preset buttons in the inspector.
4. You may also change the `Mask Color`.
5. Properties are exposed so they can be modified by other scripts as well. See the table below.

## Properties

| Property Name       | Type    | Description                                            |
| :---                | :---    | :---                                                   |
| `PreviewInEditMode` | `bool`  | Enable to preview mask effects in Edit Mode.           |
| `MaskColor`         | `Color` | The color of letterbox / pillarbox.                    |
| `AspectRatio`       | `float` | The designated aspect ratio. The minimum value is `0`. |

## How Does it Work
The script finds the current monitor's aspect ratio and decides whether a pillarbox (left-right masks) or a letterbox (top-down masks) is needed. It also resizes the camera rect to make sure the visual content keeps consistent. 

Note that this script *does not* change the display resolution; it only adds solid color graphics to mask out unwanted regions. This is to make sure the game viewport fits on all resolutions while preserving the aspect ratio.
