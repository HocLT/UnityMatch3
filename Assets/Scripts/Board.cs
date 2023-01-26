using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width = 7;
    public int height = 7;

    public GameObject bgTilePrefab;

    public Gem[] gems;

    // Start is called before the first frame update

    void Start()
    {
        Setup();
    }

    void Setup()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 pos = new Vector2(x, y);
                GameObject bgTile = Instantiate(bgTilePrefab, pos, Quaternion.identity);
                bgTile.transform.parent = transform;
                bgTile.name = $"BG Tile {x}, {y}";

                int gemToUse = Random.Range(0, gems.Length);
                SpawnGem(new Vector2Int(x, y), gems[gemToUse]);
            }
        }
    }

    void SpawnGem(Vector2Int pos, Gem gemToSpawn)
    {
        Gem gem = Instantiate(gemToSpawn, new Vector3(pos.x, pos.y, 0f), Quaternion.identity);
        gem.transform.parent = transform;
        gem.name = $"Gem - {pos.x}, {pos.y}";
    }
}
