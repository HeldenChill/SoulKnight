using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class CustomEditorTool : EditorWindow
{
    [MenuItem("Open Scene/Start Scene")]
    public static void OpenScene(string sceneName, bool isGameplay = false)
    {

        if (isGameplay)
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene("Assets/Game/Scenes/Game/" + sceneName + ".unity");
            }
        }
        else
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene("Assets/Game/Scenes/" + sceneName + ".unity");
            }
        }

    }
    [MenuItem("Open Scene/Start Scene")]
    public static void OpenLoadStart()
    {
        OpenScene("StartScene");
    }
    [MenuItem("Open Scene/Waiting Room")]
    public static void OpenWaitingRoom()
    {
        OpenScene("WaitingRoom");
    }

    [MenuItem("Open Scene/Game Scene")]
    public static void GameScene()
    {
        OpenScene("GameScene");
    }
    [MenuItem("Open Scene/Pathfinding")]
    public static void Pathfinding()
    {
        OpenScene("Pathfinding");
    }
}
