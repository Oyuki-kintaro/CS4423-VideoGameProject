using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateTerrain : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap; // Reference to the Tilemap

    [SerializeField] private TileBase rockTile; // Tile for rocks
    [SerializeField] private TileBase flowerTile; // Tile for flowers
    [SerializeField] private TileBase grassTile; // Tile for grass

    [SerializeField] private int rockCount = 5; // Number of rocks to generate
    [SerializeField] private int flowerCount = 10; // Number of flowers to generate
    [SerializeField] private int grassCount = 5; // Number of flowers to generate

    private Vector3Int min;
    private Vector3Int max;

    private void Start()
    {
        BoundsInt bounds = tilemap.cellBounds;

        // Retrieve the min and max coordinates of the tilemap
        min = bounds.min;
        max = bounds.max;
        
        GenerateRocksAndFlowers(); // Procedurally generate items on the tilemap
    }

    private void GenerateRocksAndFlowers()
    {
        System.Random random = new System.Random(); // Random number generator

        // Generate rocks at random positions
        for (int i = 0; i < rockCount; i++)
        {
            Vector3Int position = new Vector3Int(random.Next(min.x, max.x), random.Next(min.y, max.y), 0);
            tilemap.SetTile(position, rockTile); // Place the rock tile at the random position
        }

        // Generate flowers at random positions
        for (int i = 0; i < flowerCount; i++)
        {
            Vector3Int position = new Vector3Int(random.Next(min.x, max.x), random.Next(min.y, max.y), 0);
            tilemap.SetTile(position, flowerTile); // Place the flower tile at the random position
        }

        // Generate flowers at random positions
        for (int i = 0; i < grassCount; i++)
        {
            Vector3Int position = new Vector3Int(random.Next(min.x, max.x), random.Next(min.y, max.y), 0);
            tilemap.SetTile(position, grassTile); // Place the flower tile at the random position
        }
    }
}

