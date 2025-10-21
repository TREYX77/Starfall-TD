using UnityEngine;
using System.Collections.Generic;

public class TowerManager : MonoBehaviour
{
    [Header("Raycast")]
    [Tooltip("Which layers can be clicked to select tower spots.")]
    [SerializeField] private LayerMask rayLayer = ~0;

    [Header("Highlight (visual)")]
    [Tooltip("Optional: material to apply to the selected spot.")]
    [SerializeField] private Material highlightMaterial = null;

    [Header("Tower")]
    [Tooltip("Y offset for spawned towers.")]
    [SerializeField] private float spawnYOffset = 0.5f;

    // runtime
    private GameObject selectedSpot = null;
    private readonly Dictionary<GameObject, bool> towerSpots = new Dictionary<GameObject, bool>();

    private Material[][] originalMaterials; // store each renderer's original materials

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            HandleSelection();
    }

    private void HandleSelection()
    {
        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, rayLayer)) return;

        GameObject clicked = hit.transform.gameObject;
        if (clicked == null) return;

        // clicking same spot toggles deselect
        if (selectedSpot == clicked)
        {
            RestoreOriginalMaterial();
            selectedSpot = null;
            return;
        }

        // new selection: restore old one first
        RestoreOriginalMaterial();

        selectedSpot = clicked;
        ApplyHighlightMaterial(selectedSpot);

        // track tower spot
        if (!towerSpots.ContainsKey(selectedSpot))
            towerSpots[selectedSpot] = false;
    }

    private void ApplyHighlightMaterial(GameObject spot)
    {
        if (highlightMaterial == null) return;

        Renderer[] renderers = spot.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return;

        originalMaterials = new Material[renderers.Length][];
        for (int i = 0; i < renderers.Length; i++)
        {
            originalMaterials[i] = renderers[i].materials;

            Material[] mats = new Material[renderers[i].sharedMaterials.Length];
            for (int j = 0; j < mats.Length; j++)
                mats[j] = highlightMaterial;

            renderers[i].materials = mats;
        }
    }

    private void RestoreOriginalMaterial()
    {
        if (selectedSpot == null || originalMaterials == null) return;

        Renderer[] renderers = selectedSpot.GetComponentsInChildren<Renderer>();
        if (renderers.Length != originalMaterials.Length) return;

        for (int i = 0; i < renderers.Length; i++)
            renderers[i].materials = originalMaterials[i];

        originalMaterials = null;
    }

    public void SpawnTower(GameObject towerPrefab)
    {
        if (selectedSpot == null) return;
        if (towerPrefab == null) return;

        // Check coins
        if (!CoinTracker.Instance.CanSpend(10))
        {
            Debug.Log("Niet genoeg coins!");
            return;
        }

        // Spend coins
        CoinTracker.Instance.SpendCoins(10);

        // Spawn tower
        Instantiate(towerPrefab, selectedSpot.transform.position + Vector3.up * spawnYOffset, Quaternion.identity);
        towerSpots[selectedSpot] = true;

        // Restore ground and deselect
        RestoreOriginalMaterial();
        selectedSpot = null;
    }

    private void OnDisable()
    {
        RestoreOriginalMaterial();
    }

    private void OnDestroy()
    {
        RestoreOriginalMaterial();
    }
}
