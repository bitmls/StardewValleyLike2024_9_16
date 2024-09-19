using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantManager : MonoBehaviour
{
    public static PlantManager instance { get; private set; }

    public Tilemap interactableMap;
    public Tile interactableTile;
    public Tile groundHoedTile;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InitInteractableMap();
    }

    private void InitInteractableMap()
    {
        foreach (Vector3Int position in interactableMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = interactableMap.GetTile(position);
            if (tile != null)
                interactableMap.SetTile(position, interactableTile);
        }
    }

    public void HoeGround(Vector3 position)
    {
        Vector3Int tilePosition = interactableMap.WorldToCell(position);
        TileBase tile = interactableMap.GetTile(tilePosition);

        if (tile != null && tile.name == interactableTile.name)
        {
            interactableMap.SetTile(tilePosition, groundHoedTile);
        }
    }
}
