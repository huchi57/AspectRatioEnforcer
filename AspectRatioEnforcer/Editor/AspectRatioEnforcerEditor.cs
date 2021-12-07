using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AspectRatioEnforcer))]
public class AspectRatioEnforcerEditor : Editor
{
    private AspectRatioEnforcer _target = default;
    private SerializedProperty _aspectRatio = default;
    private SerializedProperty _customAspectRatio = default;
    private SerializedProperty _maskColor = default;

    private void Awake()
    {
        _target = (AspectRatioEnforcer)target;
        _aspectRatio = serializedObject.FindProperty(nameof(_aspectRatio));
        _customAspectRatio = serializedObject.FindProperty(nameof(_customAspectRatio));
        _maskColor = serializedObject.FindProperty(nameof(_maskColor));
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_aspectRatio);
        if (_aspectRatio.enumValueIndex == (int)AspectRatioEnforcer.AspectRatio.Custom)
        {
            EditorGUILayout.PropertyField(_customAspectRatio);
        }
        EditorGUILayout.PropertyField(_maskColor);
        if (GUILayout.Button("Update Mask Color"))
        {
            _target.RefreshMaskTexture();
        }
        serializedObject.ApplyModifiedProperties();
    }
}