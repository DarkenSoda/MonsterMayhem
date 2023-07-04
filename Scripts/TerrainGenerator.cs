using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

    [SerializeField] int width = 256;
    [SerializeField] int length = 256;
    [SerializeField] int depth = 15;
    [SerializeField] float scale = 10f;

    private void Start() {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData) {
        terrainData.heightmapResolution = width + 1;

        terrainData.size = new Vector3(width, depth, length);

        terrainData.SetHeights(0, 0, GenerateHeights());

        return terrainData;
    }

    float[,] GenerateHeights () {
        float[,] heights = new float[width, length];
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < length; y++) {
                heights[x, y] = CalculateHeights(x, y);
            }
        }
        return heights;
    }

    float CalculateHeights(int x, int y) {
        float xCoord = (float)x / width * scale;
        float yCoord = (float)y / length * scale;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }

}