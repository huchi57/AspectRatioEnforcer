# AspectRatioEnforcer
Make sure the game view is in designated aspect ratio by adding letterbox / pillarbox, and resizing the camera viewport.
This currently works *only* in Play Mode.
## Installation
1. Download the full repository, and make sure the `Editor` folder is also included.
2. Import the full folder into your Unity project.
3. There is a test scene under the `Test` folder.
## How to Use
1. Add `AspectRatioEnforcer.cs` to a camera object.
2. Select a preset from the `Aspect Ratio` dropdown in the inspector or use `Custom` to fill in a custom number.
3. (Optional) Change the `Mask Color` before entering Play Mode, or update via the `Update Mask Color` in Play Mode (since it uses property to update mask color).
## How Does it Work
The script finds the current monitor's aspect ratio and decides whether a pillarbox (left-right masks) or a letterbox (top-down masks) is needed. It also resizes the camera rect to make sure the visual content keeps consistent. 

Note that this script *does not* change the display resolution; it only adds solid color graphics to mask out unwanted regions. This is to make sure the game viewport fits on all resolutions while preserving the aspect ratio.
