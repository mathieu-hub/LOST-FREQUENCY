﻿using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SimpleLight)), CanEditMultipleObjects]
public class SimpleLightEditor : Editor
{
    private SerializedProperty lightType;
    private SerializedProperty lightObj;
    private SerializedProperty electricity;
    private SerializedProperty interactiveLight;

    void OnEnable()
    {
        lightType = serializedObject.FindProperty("lightType");
        lightObj = serializedObject.FindProperty("lightObj");
        electricity = serializedObject.FindProperty("electricity");
        interactiveLight = serializedObject.FindProperty("interactiveLight");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        SimpleLight.LightType m_lightType = (SimpleLight.LightType)lightType.enumValueIndex;

        EditorGUILayout.PropertyField(lightType);
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(lightObj, new GUIContent("Light"));
        EditorGUILayout.Space();

        if (m_lightType == SimpleLight.LightType.Static)
        {
            EditorGUILayout.PropertyField(electricity, new GUIContent("Electricity"));
        }
        else if (m_lightType == SimpleLight.LightType.Flickering)
        {
            EditorGUILayout.PropertyField(electricity, new GUIContent("Electricity"));
        }
        else if (m_lightType == SimpleLight.LightType.SwitchControlled)
        {
            EditorGUILayout.PropertyField(interactiveLight, new GUIContent("Interactive Light"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}