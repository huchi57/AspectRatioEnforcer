using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AspectRatioEnforcer))]
public class AspectRatioEnforcerEditor : Editor
{
    private const float _16to9 = 16f / 9f;
    private const float _16to10 = 16f / 10f;
    private const float _21to9 = 21f / 9f;
    private const float _4to3 = 4f / 3f;
    private const float _5to4 = 5f / 4f;
    private SerializedProperty _aspectRatio = default;
    private SerializedProperty AspectRatio
    {
        get
        {
            if (_aspectRatio == null)
            {
                _aspectRatio = serializedObject.FindProperty(nameof(_aspectRatio));
            }
            return _aspectRatio;
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.OnInspectorGUI();
        GUILayout.Label("Quick Set Aspect Ratio:");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("16:9"))
        {
            AspectRatio.floatValue = _16to9;
        }
        if (GUILayout.Button("16:10"))
        {
            AspectRatio.floatValue = _16to10;
        }
        if (GUILayout.Button("21:9"))
        {
            AspectRatio.floatValue = _21to9;
        }
        if (GUILayout.Button("4:3"))
        {
            AspectRatio.floatValue = _4to3;
        }
        if (GUILayout.Button("5:4"))
        {
            AspectRatio.floatValue = _5to4;
        }
        GUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
    }
}