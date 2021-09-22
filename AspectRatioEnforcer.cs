using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AspectRatioEnforcer : MonoBehaviour
{
    [SerializeField]
    private float targetAspectRatio = 1.77778f;
    private float CurrentAspectRatio { get { return (float)Screen.width / (float)Screen.height; } }

    private float targetScreenWidthOrHeight;
    private float boxWidthOrHeight;
    private float inset;
    private Texture2D texture;
    private GUIStyle guiStyle;
    private new Camera camera;

    private void OnGUI()
    {
        if (texture == null)
        {
            texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, Color.black);
            texture.Apply();
        }

        if (guiStyle == null)
        {
            guiStyle = new GUIStyle();
            guiStyle.normal.background = texture;
        }

        if (camera == null)
        {
            camera = Camera.main;
        }

        // Pillarbox
        if (CurrentAspectRatio > targetAspectRatio)
        {
            targetScreenWidthOrHeight = (float)Screen.height * targetAspectRatio;
            boxWidthOrHeight = (Screen.width - targetScreenWidthOrHeight) / 2;
            GUI.Box(new Rect(0, 0, boxWidthOrHeight, Screen.height), GUIContent.none, guiStyle);
            GUI.Box(new Rect(boxWidthOrHeight + targetScreenWidthOrHeight, 0, boxWidthOrHeight, Screen.height), GUIContent.none, guiStyle);
            if (camera != null)
            {
                inset = 1f - targetAspectRatio / CurrentAspectRatio;
                camera.rect = new Rect(inset / 2, 0, 1 - inset, 1);
            }
        }

        // Letterbox
        else if (CurrentAspectRatio < targetAspectRatio)
        {
            targetScreenWidthOrHeight = (float)Screen.width / targetAspectRatio;
            boxWidthOrHeight = (Screen.height - targetScreenWidthOrHeight) / 2;
            GUI.Box(new Rect(0, 0, Screen.width, boxWidthOrHeight), GUIContent.none, guiStyle);
            GUI.Box(new Rect(0, boxWidthOrHeight + targetScreenWidthOrHeight, Screen.width, boxWidthOrHeight), GUIContent.none, guiStyle);
            if (camera != null)
            {
                inset = 1f - CurrentAspectRatio / targetAspectRatio;
                camera.rect = new Rect(0, inset / 2, 1, 1 - inset);
            }
        }

        // No box
        else
        {
            camera.rect = new Rect(0, 0, 1, 1);
        }
    }

    private void OnValidate()
    {
        if (targetAspectRatio < 0)
        {
            targetAspectRatio = 0;
        }
    }
}
