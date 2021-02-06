using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public static class CustomProjectSettingsRegister
{
    const string SettingsPath = "Assets/Editor/EranDrori.CustomProjectSettings.asset";

    [SettingsProvider]
    public static SettingsProvider CreateSettingsProvider()
    {
        var logFoldout = true;

        return new SettingsProvider("Project/EranDrori.CustomProjectSettings", SettingsScope.Project)
        {
            guiHandler = (searchContext) =>
            {
                var settings = Load();
                var serialized = new SerializedObject(settings);

                EditorGUI.BeginChangeCheck();

                logFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(logFoldout, "Logging");

                if (logFoldout)
                {
                    EditorGUILayout.PropertyField(serialized.FindProperty("environment"), new GUIContent("Environment"));

                    EditorGUILayout.PropertyField(serialized.FindProperty("entityLogEnabled"), new GUIContent("Entity"));
                    EditorGUILayout.PropertyField(serialized.FindProperty("notificationsLogEnabled"), new GUIContent("Notifications"));
                    EditorGUILayout.HelpBox("Those log messages will appear on console with the prefix [ENTITY] or [NOTIFICATIONS]", MessageType.Info);
                }

                EditorGUILayout.EndFoldoutHeaderGroup();

                if (EditorGUI.EndChangeCheck())
                {
                    serialized.ApplyModifiedProperties();
                    CustomProjectSettings.Refresh(settings);
                }
            },

            keywords = new HashSet<string>(new[] { "Entity log enabled", "Notifications log enabled" })
        };
    }

    static CustomProjectSettings Load()
    {
        var settings = AssetDatabase.LoadAssetAtPath<CustomProjectSettings>(SettingsPath);

        if (settings == null)
        {
            settings = ScriptableObject.CreateInstance<CustomProjectSettings>();
            AssetDatabase.CreateAsset(settings, SettingsPath);
            AssetDatabase.SaveAssets();
        }

        return settings;
    }
}