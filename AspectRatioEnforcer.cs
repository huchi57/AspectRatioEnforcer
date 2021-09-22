using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AspectRatioEnforcer : MonoBehaviour
{
    [SerializeField]
    private float targetAspectRatio = 1.77778f;

    [SerializeField]
    private Color maskColor = Color.black;

    [SerializeField]
    private Camera optionalCamera;

    private float CurrentAspectRatio { get { return (float)Screen.width / (float)Screen.height; } }

    private float targetScreenWidthOrHeight;
    private float boxWidthOrHeight;
    private float camerBoxInset;

    private Color currentMaskColor;
    private Texture2D maskTexture;
    private GUIStyle guiStyle;
    private new Camera camera;

    private Rect leftOrTopBox;
    private Rect rightOrBottomBox;
    private Rect cameraBox;

    [ContextMenu("Set Mask Color")]
    public void SetMaskColor(Color color)
    {
        maskTexture = new Texture2D(1, 1);
        maskTexture.SetPixel(0, 0, color);
        maskTexture.Apply();
        currentMaskColor = color;

        // Apply texture to mask
        if (guiStyle == null)
        {
            guiStyle = new GUIStyle();
        }
        guiStyle.normal.background = maskTexture;
    }

    private void OnGUI()
    {
        CheckComponentsExist();

        // Pillarbox
        if (CurrentAspectRatio > targetAspectRatio)
        {
            targetScreenWidthOrHeight = (float)Screen.height * targetAspectRatio;
            boxWidthOrHeight = (Screen.width - targetScreenWidthOrHeight) / 2;

            SetRect(ref leftOrTopBox, 0, 0, boxWidthOrHeight, Screen.height);
            SetRect(ref rightOrBottomBox, boxWidthOrHeight + targetScreenWidthOrHeight, 0, boxWidthOrHeight, Screen.height);

            GUI.Box(leftOrTopBox, GUIContent.none, guiStyle);
            GUI.Box(rightOrBottomBox, GUIContent.none, guiStyle);

            if (camera != null)
            {
                camerBoxInset = 1f - targetAspectRatio / CurrentAspectRatio;
                SetRect(ref cameraBox, camerBoxInset / 2, 0, 1 - camerBoxInset, 1);
                camera.rect = cameraBox;
            }
        }

        // Letterbox
        else if (CurrentAspectRatio < targetAspectRatio)
        {
            targetScreenWidthOrHeight = (float)Screen.width / targetAspectRatio;
            boxWidthOrHeight = (Screen.height - targetScreenWidthOrHeight) / 2;

            SetRect(ref leftOrTopBox, 0, 0, Screen.width, boxWidthOrHeight);
            SetRect(ref rightOrBottomBox, 0, boxWidthOrHeight + targetScreenWidthOrHeight, Screen.width, boxWidthOrHeight);

            GUI.Box(leftOrTopBox, GUIContent.none, guiStyle);
            GUI.Box(rightOrBottomBox, GUIContent.none, guiStyle);

            if (camera != null)
            {
                camerBoxInset = 1f - CurrentAspectRatio / targetAspectRatio;
                SetRect(ref cameraBox, 0, camerBoxInset / 2, 1, 1 - camerBoxInset);
                camera.rect = cameraBox;
            }
        }

        // No box
        else
        {
            camera.rect = new Rect(0, 0, 1, 1);
        }
    }

    private void CheckComponentsExist()
    {
        if (maskTexture == null)
        {
            // Default color: black
            SetMaskColor(maskColor);
        }

        if (optionalCamera)
        {
            camera = optionalCamera;
        }

        if (camera == null)
        {
            camera = Camera.main;
        }

        if (leftOrTopBox == null)
        {
            leftOrTopBox = new Rect();
        }

        if (rightOrBottomBox == null)
        {
            rightOrBottomBox = new Rect();
        }

        if (cameraBox == null)
        {
            cameraBox = new Rect();
        }

        if (currentMaskColor != maskColor)
        {
            SetMaskColor(maskColor);
            currentMaskColor = maskColor;
        }
    }

    private void SetRect(ref Rect rect, float x, float y, float width, float height)
    {
        rect.x = x;
        rect.y = y;
        rect.width = width;
        rect.height = height;
    }

    private void OnValidate()
    {
        if (targetAspectRatio < 0)
        {
            targetAspectRatio = 0;
        }
        SetMaskColor(maskColor);
    }
}
