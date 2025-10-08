#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GridManager grid = (GridManager)target;

        if (GUILayout.Button("Place Road at (0,0)"))
        {
            grid.PlaceObject(new Vector2Int(0, 0), grid.roadPrefab);
        }
    }
}
#endif
