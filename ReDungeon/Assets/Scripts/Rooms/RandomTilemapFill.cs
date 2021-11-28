using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomTilemapFill : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase[] tiles;
    

    private void Start()
    {
        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(position))
                continue;

            if (Fill())
                tilemap.SetTile(position, GetRandomTile(tiles));
        }
    }

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
