using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour {

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    [SerializeField] int xSize = 20;
    [SerializeField] int zSize = 20;

    void Start() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    void CreateShape() {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int z = 0, i = 0; z <= zSize; z++) {
            for (int x = 0; x <= xSize; x++) {
                float y = Mathf.PerlinNoise(x*.3f, z*.3f) * 2f;
                vertices[i++] = new Vector3(x, y, z);
            }
        }

        triangles = new int[xSize * zSize * 6];

        for (int z = 0, vert = 0, tris = 0; z < zSize; z++, vert++) {
            for (int x = 0; x < xSize; x++, vert++, tris += 6) {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;
            }
        }
    }

    void UpdateMesh() {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
