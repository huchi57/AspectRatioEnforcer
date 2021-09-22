# AspectRatioEnforcer
Make sure the game view is in designated aspect ratio by adding pillarbox/letterbox, and resizing the camera rect.
## How to Use
1. Add `AspectRatioEnforcer.cs` to any GameObject (preferably on a Camera component).
2. Use the `Target Aspect Ratio` to set a designated aspect ratio (width divided by height).
3. (Optional) Set a Camera component reference to `Optional Camera`. If this is left to be blank, the script will try to find the `MainCamera` using `Camera.main`.
4. (Optional) Change the `Mask Color` to a desired color (default color is black).
## How Does it Work
The script finds the current monitor's aspect ratio and decides whether a pillarbox (left-right masks) or a letterbox (top-down masks) is needed. It also resizes the camera rect to make sure the visual content keeps consistent. 
*Note that this script does not change the display resolution, rather than adding solid color graphics to mask out unwanted regions.*
