using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float cellSize = 1f;
    public GameObject roadPrefab;
    public GameObject towerGroundPrefab;
    public GameObject spawnerPrefab;
    public GameObject basePrefab;
    public GameObject decoPrefab;


    // Convert a world position into grid coordinates
    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt((worldPos.x - transform.position.x) / cellSize);
        int y = Mathf.FloorToInt((worldPos.z - transform.position.z) / cellSize);
        return new Vector2Int(x, y);
    }

    // Convert grid coordinates into the world position (center of cell)
    public Vector3 GridToWorld(int x, int y)
    {
        return transform.position + new Vector3(
            x * cellSize + cellSize / 2f,
            0,
            y * cellSize + cellSize / 2f
        );
    }

    // Draws a green overlay grid in the Scene view for debugging
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        for (int x = 0; x <= gridWidth; x++)
        {
            Vector3 start = transform.position + new Vector3(x * cellSize, 0, 0);
            Vector3 end = start + new Vector3(0, 0, gridHeight * cellSize);
            Gizmos.DrawLine(start, end);
        }

        for (int y = 0; y <= gridHeight; y++)
        {
            Vector3 start = transform.position + new Vector3(0, 0, y * cellSize);
            Vector3 end = start + new Vector3(gridWidth * cellSize, 0, 0);
            Gizmos.DrawLine(start, end);
        }
    }
    public void PlaceObject(Vector2Int gridPos, GameObject prefab, Transform parent = null)
    {
        if (gridPos.x < 0 || gridPos.x >= gridWidth || gridPos.y < 0 || gridPos.y >= gridHeight)
            return;

        Vector3 worldPos = GridToWorld(gridPos.x, gridPos.y);
        Instantiate(prefab, worldPos, Quaternion.identity, parent);
    }

}
