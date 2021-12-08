using UnityEngine;

[ExecuteInEditMode]
[DisallowMultipleComponent]
[RequireComponent(typeof(Camera))]
public class AspectRatioEnforcer : MonoBehaviour
{
    private class Masks
    {
        private Rect _mask1 = new Rect();
        private Rect _mask2 = new Rect();
        private Rect _viewport = new Rect();

        public Rect Mask1 => _mask1;
        public Rect Mask2 => _mask2;
        public Rect Viewport => _viewport;

        public void SetLetterbox(float viewportHeight, float maskHeight, float viewportIncet)
        {
            _mask1.Set(0, 0, Screen.width, maskHeight);
            _mask2.Set(0, maskHeight + viewportHeight, Screen.width, maskHeight);
            _viewport.Set(0, viewportIncet / 2, 1, 1 - viewportIncet);
        }

        public void SetPillarbox(float viewportWidth, float maskWidth, float viewportIncet)
        {
            _mask1.Set(0, 0, maskWidth, Screen.height);
            _mask2.Set(maskWidth + viewportWidth, 0, maskWidth, Screen.height);
            _viewport.Set(viewportIncet / 2, 0, 1 - viewportIncet, 1);
        }

        public void ClearBox()
        {
            _mask1 = Rect.zero;
            _mask2 = Rect.zero;
            _viewport.Set(0, 0, 1, 1);
        }
    }

    [SerializeField] private bool _previewInEditMode = true;
    [SerializeField] private Color _maskColor = Color.black;
    [SerializeField] private float _aspectRatio = 16f / 9f;

    private static Texture2D _maskTexture = null;
    private static GUIStyle _style = null;
    private Camera _camera = null;
    private Masks _mask = null;

    public bool PreviewInEditMode { get => _previewInEditMode; set => _previewInEditMode = value; }
    public Color MaskColor { get => _maskColor; set => _maskColor = value; }
    public float AspectRatio { get => _aspectRatio; set => _aspectRatio = value < 0f ? 0f : value; }

    private float ViewportInset => 1f - (ScreenRatio / _aspectRatio);
    private float ScreenRatio => Screen.width / (float)Screen.height;
    private Masks Mask
    {
        get
        {
            if (_mask == null)
            {
                _mask = new Masks();
            }
            return _mask;
        }
    }

    private void DrawMask()
    {
        if (_maskTexture == null)
        {
            _maskTexture = new Texture2D(1, 1);
        }

        if (_style == null)
        {
            _style = new GUIStyle();
        }

        _maskTexture.SetPixel(0, 0, _maskColor);
        _maskTexture.Apply();
        _style.normal.background = _maskTexture;

        GUI.Box(Mask.Mask1, GUIContent.none, _style);
        GUI.Box(Mask.Mask2, GUIContent.none, _style);
        _camera.rect = Mask.Viewport;
    }

    private void ResetCamera()
    {
        Mask.ClearBox();
        _camera.rect = Mask.Viewport;
    }

    private void OnGUI()
    {
        if (_previewInEditMode || Application.isPlaying)
        {
            if (_aspectRatio > ScreenRatio)
            {
                var viewportHeight = _aspectRatio <= 0 ? Screen.height : Screen.width / _aspectRatio;
                var maskHeight = (Screen.height - viewportHeight) / 2;
                Mask.SetLetterbox(viewportHeight, maskHeight, ViewportInset);
            }

            else if (_aspectRatio < ScreenRatio)
            {
                var viewportWidth = Screen.height * _aspectRatio;
                var maskWidth = (Screen.width - viewportWidth) / 2;
                Mask.SetPillarbox(viewportWidth, maskWidth, ViewportInset);
            }

            else
            {
                Mask.ClearBox();
            }
        }
        else
        {
            Mask.ClearBox();
        }

        DrawMask();
    }

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void OnDisable()
    {
        ResetCamera();
    }

    private void OnValidate()
    {
        if (_aspectRatio < 0)
        {
            _aspectRatio = 0;
        }
    }
}
