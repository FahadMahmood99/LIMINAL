using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UIPanelCreator))]
public class UIPanelCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        UIPanelCreator creator = (UIPanelCreator)target;

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Create UI Panels", EditorStyles.boldLabel);

        if (GUILayout.Button("Create Main Menu Panel"))
        {
            creator.CreateMainMenuPanel();
        }

        if (GUILayout.Button("Create Pause Menu Panel"))
        {
            creator.CreatePauseMenuPanel();
        }

        if (GUILayout.Button("Create Splash Screen Panel"))
        {
            creator.CreateSplashScreenPanel();
        }

        if (GUILayout.Button("Create Game Over Panel"))
        {
            creator.CreateGameOverPanel();
        }

        EditorGUILayout.Space(10);
        EditorGUILayout.HelpBox("Make sure to assign the Main Canvas reference before creating panels.", MessageType.Info);
    }
} 