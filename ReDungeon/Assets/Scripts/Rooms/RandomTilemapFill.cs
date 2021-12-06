using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomTilemapFill : MonoBehaviour
{
    private Tilemap floor, decorativeTilemap, wallDecorTop, wallDecorLR;
    [SerializeField] private TileBase[] tiles;

    private void Start()
    {
        floor = transform.Find("FloorBase").GetComponent<Tilemap>();
        decorativeTilemap = transform.Find("FloorLayer2").GetComponent<Tilemap>();
        wallDecorTop = transform.Find("WallDecorTop").GetComponent<Tilemap>();
        wallDecorLR = transform.Find("WallDecorLR").GetComponent<Tilemap>();

        foreach (var position in floor.cellBounds.allPositionsWithin)
        {
            if (CantPlaceTile(position))
                continue;

            if (Fill())
                decorativeTilemap.SetTile(position, GetRandomTile(tiles));
        }
    }

    /// <summary>
    /// Checks if the tile exists on the floor and is not busy by other tile on the same tilemap
    /// </summary>
    /// <param name="position">Position of the tile</param>
    /// <returns>true if can, false if not</returns>
    private bool CantPlaceTile(Vector3Int position) =>
        !floor.HasTile(position) || decorativeTilemap.HasTile(position) ||
        wallDecorTop.HasTile(position) || wallDecorLR.HasTile(position);

    /// <summary>
    /// Determines if the empty tile should be filled
    /// </summary>
    /// <returns>True if fill, false if not</returns>
    private bool Fill()
    {
        const int MIN = 1;
        const int MAX = 15;
        const int SPECIAL_VALUE = MIN;

        return SPECIAL_VALUE == Random.Range(MIN, MAX + 1);
    }

    /// <summary>
    /// Returns random tile from tile array
    /// </summary>
    /// <param name="tiles">Array of tiles</param>
    /// <returns>Random tile</returns>
    private TileBase GetRandomTile(TileBase[] tiles) =>
        tiles[Random.Range(0, tiles.Length)];
}
