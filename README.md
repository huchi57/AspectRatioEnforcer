# AspectRatioEnforcer
Make sure the game view is in designated aspect ratio by adding letterbox / pillarbox, and resizing the camera viewport.
This works both in Play Mode and Edit Mode.
## Installation
1. Download the full repository (including the `Editor` folder).
2. Import the full folder into your Unity project.
3. You can try out the test scene under the `Test` folder to see how it works.
## How to Use
1. Add `AspectRatioEnforcer.cs` to a camera object.
2. By default, `Preview In Edit Mode` is enabled, so the script should work in Edit Mode as well. Uncheck to use it only in Play Mode.
3. Edit the designated `Aspect Ratio` value, or click on one of the preset buttons below.
4. You may also change the `Mask Color`.
5. These properties are exposed as public, so they can be changed by other scripts:
  - `bool PreviewInEditMode`
  - `Color MaskColor`
  - `float AspectRatio`
| Property | Type | Description |
|----------|-------------|------|
| PreviewInEditMode | bool  | 1 |
| MaskColor         | Color | 2 |
| AspectRatio       | float | 3 |
## How Does it Work
The script finds the current monitor's aspect ratio and decides whether a pillarbox (left-right masks) or a letterbox (top-down masks) is needed. It also resizes the camera rect to make sure the visual content keeps consistent. 

Note that this script *does not* change the display resolution; it only adds solid color graphics to mask out unwanted regions. This is to make sure the game viewport fits on all resolutions while preserving the aspect ratio.
