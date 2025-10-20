using UnityEngine;
using System.Collections.Generic;

public class TowerManager : MonoBehaviour
{
    [Header("Raycast")]
    [Tooltip("Which layers can be clicked to select tower spots.")]
    [SerializeField] private LayerMask rayLayer = ~0;

    [Header("Highlight (visual)")]
    [Tooltip("Optional: prefab used as highlight. If null and highlightMaterial is set, the script will duplicate the selected spot and apply the material.")]
    [SerializeField] private GameObject highlightPrefab = null;
    [Tooltip("Optional: material to apply to a duplicated highlight when highlightPrefab is null.")]
    [SerializeField] private Material highlightMaterial = null;
    [Tooltip("If true, the script will keep one highlight instance and move it instead of destroying/instantiating every time.")]
    [SerializeField] private bool useSingleHighlightInstance = false;

    [Header("Tower")]
    [Tooltip("Y offset for spawned towers.")]
    [SerializeField] private float spawnYOffset = 0.5f;

    // runtime
    private GameObject selectedSpot = null;
    private GameObject highlightInstance = null;
    private readonly Dictionary<GameObject, bool> towerSpots = new Dictionary<GameObject, bool>();

    private const int ignoreRaycastLayer = 2; // Unity's default "Ignore Raycast" layer index

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            HandleSelection();
    }

    private void HandleSelection()
    {
        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, Mathf.Infinity, rayLayer)) return;

        GameObject clicked = hit.transform.gameObject;
        if (clicked == null) return;

        // clicking same spot toggles deselect
        if (selectedSpot == clicked)
        {
            RemoveHighlight();
            selectedSpot = null;
            return;
        }

        // new selection: remove old highlight first
        if (!useSingleHighlightInstance)
            RemoveHighlight();

        selectedSpot = clicked;

        // spawn or move highlight
        if (useSingleHighlightInstance && highlightInstance != null)
        {
            // move existing highlight to new spot and ensure correct transform/scale
            MoveHighlightTo(selectedSpot.transform);
        }
        else
        {
            CreateHighlightFor(selectedSpot);
        }

        // ensure spot is tracked
        if (!towerSpots.ContainsKey(selectedSpot))
            towerSpots[selectedSpot] = false;
    }

    private void CreateHighlightFor(GameObject spot)
    {
        if (spot == null) return;

        if (highlightPrefab != null)
        {
            highlightInstance = Instantiate(
                highlightPrefab,
                spot.transform.position,
                spot.transform.rotation
            );
        }
        else if (highlightMaterial != null)
        {
            // Duplicate the spot object visually and apply highlightMaterial
            // We instantiate the spot's GameObject so mesh/child structure looks identical
            highlightInstance = Instantiate(
                spot,
                spot.transform.position,
                spot.transform.rotation
            );

            // Remove scripts on the copy to avoid logic duplication (best-effort)
            var components = highlightInstance.GetComponents<MonoBehaviour>();
            foreach (var c in components)
            {
                Destroy(c);
            }

            // Try to find renderers and apply material
            var renderers = highlightInstance.GetComponentsInChildren<Renderer>();
            foreach (var r in renderers)
            {
                // create a new array with the highlight material applied to all slots
                Material[] mats = new Material[r.sharedMaterials.Length];
                for (int i = 0; i < mats.Length; i++)
                    mats[i] = highlightMaterial;
                r.materials = mats;
            }
        }
        else
        {
            Debug.LogWarning("TowerManager: no highlightPrefab and no highlightMaterial assigned. Assign one in the inspector.");
            return;
        }

        // Match local scale exactly
        highlightInstance.transform.localScale = spot.transform.localScale;
        highlightInstance.name = "_Highlight_" + spot.name;

        // Remove colliders on the highlight so it doesn't block raycasts or physics
        var colliders = highlightInstance.GetComponentsInChildren<Collider>();
        foreach (var c in colliders)
            Destroy(c);

        // Set layer to Ignore Raycast so highlight doesn't block selection
        SetLayerRecursively(highlightInstance, ignoreRaycastLayer);

        // If using single-instance mode, keep it but ensure old instances are removed
        if (useSingleHighlightInstance && highlightInstance != null)
        {
            // if there was an older instance (rare) ensure it's the only one
            // we already destroyed previous when toggling selection unless user toggled mode mid-run
        }
    }

    private void MoveHighlightTo(Transform spotTransform)
    {
        if (highlightInstance == null || spotTransform == null) return;
        highlightInstance.transform.position = spotTransform.position;
        highlightInstance.transform.rotation = spotTransform.rotation;
        highlightInstance.transform.localScale = spotTransform.localScale;
        // ensure it remains on ignore raycast layer and has no colliders
        SetLayerRecursively(highlightInstance, ignoreRaycastLayer);
        var colliders = highlightInstance.GetComponentsInChildren<Collider>();
        foreach (var c in colliders)
            Destroy(c);
    }

    private void RemoveHighlight()
    {
        if (highlightInstance != null)
        {
            Destroy(highlightInstance);
            highlightInstance = null;
        }
    }

    public void SpawnTower(GameObject towerPrefab)
    {
        if (selectedSpot == null) return;
        if (towerPrefab == null)
        {
            Debug.LogWarning("TowerManager.SpawnTower: towerPrefab is null.");
            return;
        }

        if (towerSpots.TryGetValue(selectedSpot, out bool hasTower) && hasTower)
            return;

        Vector3 spawnPos = selectedSpot.transform.position + Vector3.up * spawnYOffset;
        Instantiate(towerPrefab, spawnPos, Quaternion.identity);
        towerSpots[selectedSpot] = true;

        // cleanup highlight and deselect
        RemoveHighlight();
        selectedSpot = null;
    }

    private void SetLayerRecursively(GameObject go, int layer)
    {
        if (go == null) return;
        go.layer = layer;
        foreach (Transform t in go.transform)
            SetLayerRecursively(t.gameObject, layer);
    }

    private void OnDisable()
    {
        RemoveHighlight();
    }

    private void OnDestroy()
    {
        RemoveHighlight();
    }
}
