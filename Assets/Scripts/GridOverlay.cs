using UnityEngine;

public class GridOverlay : MonoBehaviour
{
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float cellSize = 1f;

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
}
