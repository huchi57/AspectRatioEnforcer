using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Camera))]
public class AspectRatioEnforcer : MonoBehaviour
{
    private const float _16to9 = 16f / 9f;
    private const float _16to10 = 16f / 10f;
    private const float _21to9 = 21f / 9f;
    private const float _4to3 = 4f / 3f;
    private const float _5to4 = 5f / 4f;

    [System.Serializable]
    public enum AspectRatio
    {
        [InspectorName("16:9")] _16to9,
        [InspectorName("16:10")] _16to10,
        [InspectorName("21:9")] _21to9,
        [InspectorName("4:3")] _4to3,
        [InspectorName("5:4")] _5to4,
        Custom
    }

    private class Mask
    {
        private Rect _mask1 = new Rect();
        private Rect _mask2 = new Rect();
        private Rect _viewport = new Rect();

        public Rect Mask1 => _mask1;
        public Rect Mask2 => _mask2;
        public Rect Viewport => _viewport;

        public void SetLetterbox(float viewportWidth, float maskWidth, float viewportIncet)
        {
            _mask1.Set(0, 0, Screen.width, maskWidth);
            _mask2.Set(0, maskWidth + viewportWidth, Screen.width, maskWidth);
            _viewport.Set(0, viewportIncet / 2, 1, 1 - viewportIncet);
        }

        public void SetPillarbox(float viewportHeight, float maskHeight, float viewportIncet)
        {
            _mask1.Set(0, 0, maskHeight, Screen.height);
            _mask2.Set(maskHeight + viewportHeight, 0, maskHeight, Screen.height);
            _viewport.Set(viewportIncet / 2, 0, 1 - viewportIncet, 1);
        }

        public void ClearBox()
        {
            _mask1 = Rect.zero;
            _mask2 = Rect.zero;
            _viewport.Set(0, 0, 1, 1);
        }
    }

    [SerializeField] private AspectRatio _aspectRatio = AspectRatio._16to9;
    [SerializeField] private float _customAspectRatio = 16f / 9f;
    [SerializeField] private Color _maskColor = Color.black;

    private Camera _camera = null;
    private GUIStyle _style = null;
    private Texture2D _maskTexture = null;
    private Mask _mask = new Mask();

    public Color MaskColor
    {
        get => _maskColor;
        set
        {
            _maskColor = value;
            RefreshMaskTexture();
        }
    }

    private float CameraInset => 1f - (ScreenRatio / TargetRatio);
    private float ScreenRatio => Screen.width / (float)Screen.height;

    private float TargetRatio
    {
        get
        {
            switch (_aspectRatio)
            {
                case AspectRatio._16to9: return _16to9;
                case AspectRatio._16to10: return _16to10;
                case AspectRatio._21to9: return _21to9;
                case AspectRatio._4to3: return _4to3;
                case AspectRatio._5to4: return _5to4;
                case AspectRatio.Custom: break;
                default: break;
            }
            return _customAspectRatio;
        }
    }

    private Texture2D MaskTexture
    {
        get
        {
            if (_maskTexture == null)
            {
                _maskTexture = new Texture2D(1, 1);
                RefreshMaskTexture();
            }
            return _maskTexture;
        }
    }

    private GUIStyle Style
    {
        get
        {
            if (_style == null)
            {
                _style = new GUIStyle();
            }
            _style.normal.background = MaskTexture;
            return _style;
        }
    }

    public void RefreshMaskTexture()
    {
        if (_maskTexture != null)
        {
            _maskTexture = new Texture2D(1, 1);
        }
        _maskTexture.SetPixel(0, 0, MaskColor);
        _maskTexture.Apply();
    }

    private void DrawMasks()
    {
        GUI.Box(_mask.Mask1, GUIContent.none, Style);
        GUI.Box(_mask.Mask2, GUIContent.none, Style);
        _camera.rect = _mask.Viewport;
    }

    private void OnGUI()
    {
        if (TargetRatio > ScreenRatio)
        {
            var viewportHeight = Screen.width / TargetRatio;
            var maskHeight = (Screen.height - viewportHeight) / 2;
            _mask.SetLetterbox(viewportHeight, maskHeight, CameraInset);
        }

        else if (TargetRatio < ScreenRatio)
        {
            var viewportWidth = Screen.height * TargetRatio;
            var maskWidth = (Screen.width - viewportWidth) / 2;
            _mask.SetPillarbox(viewportWidth, maskWidth, CameraInset);
        }

        else
        {
            _mask.ClearBox();
        }

        DrawMasks();
    }

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _mask = new Mask();
    }

    private void OnValidate()
    {
        if (_customAspectRatio < 0.001f)
        {
            _customAspectRatio = 0.001f;
        }
    }
}