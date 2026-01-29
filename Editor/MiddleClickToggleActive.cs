using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Rdt
{
    [InitializeOnLoad]
    public static class MiddleClickToggleActive
    {
        // Static constructor runs when Unity loads
        static MiddleClickToggleActive()
        {
            // Scene view middle click
            // SceneView.duringSceneGui += OnSceneGUI;

            // Hierarchy window middle click
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
        }

        // Scene view handling
        private static void OnSceneGUI(SceneView sceneView)
        {
            Event e = Event.current;

            if (e.type == EventType.MouseDown && e.button == 2) // middle click
            {
                if (Selection.activeGameObject != null)
                {
                    ToggleActive(Selection.activeGameObject);
                    e.Use(); // consume event
                }
            }
        }

        // Hierarchy handling
        private static void OnHierarchyGUI(int instanceID, Rect selectionRect)
        {
            Event e = Event.current;

            if (e.type == EventType.MouseDown && e.button == 2) // middle click
            {
                if (selectionRect.Contains(e.mousePosition))
                {
                    GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
                    if (go != null)
                    {
                        ToggleActive(go);
                        e.Use();
                    }
                }
            }
        }

        private static void ToggleActive(GameObject go)
        {
            Undo.RecordObject(go, "Toggle Active State");
            go.SetActive(!go.activeSelf);
            EditorSceneManager.MarkSceneDirty(go.scene);
            // Debug.Log("Toggled: " + go.name);
        }
    }
}
