using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMazeChunk : MonoBehaviour
{
    public GameObject cube;
    public const int width = 32;
    public const int height = 32;

    List<bool> wallMap = new List<bool>();

    private static float GetAt(List<float> list, int x, int y, int width=width) {
        return list[x + width * y];
    }
    private static bool GetAt(List<bool> list, int x, int y, int width=width) {
        return list[x + width * y];
    }
    private static void SetAt(List<float> list, int x, int y, float set, int width=width) {
        list[x + width * y] = set;
    }
    private static void SetAt(List<bool> list, int x, int y, bool set, int width=width) {
        list[x + width * y] = set;
    }
    public static bool InMap(int x, int y) {
        return x > 0 && x < width && y > 0 && y < height;
    }

    // Start is called before the first frame update
    void Start()
    {
        List<float> map = new List<float>();
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                var nodeSeed = Mathf.PerlinNoise(transform.localPosition.x + x, transform.localPosition.y + y);
                map.Add(nodeSeed);
            }
        }

        // TODO: use perlin noise to seed a random number generator
        var randomX = Mathf.PerlinNoise(transform.localPosition.x, transform.localPosition.y);
        var randomY = Mathf.PerlinNoise(transform.localPosition.y, transform.localPosition.x);
        var startX = Mathf.FloorToInt(randomX * width);
        var startY = Mathf.FloorToInt(randomY * height);
        
        // Implement prim's algorithm
        List<bool> set = new List<bool>();
        for (int i = 0; i < width * height; i++) {
            set.Add(false);
        }

        // init our actual map
        for (int i = 0; i < width * 2 * height * 2; i++) {
            wallMap.Add(false);
        }

        List<(int, int)> frontier = new List<(int, int)>();
        frontier.Add((startX, startY));
        while (frontier.Count != 0) {
            frontier.Sort();
            (int, int) current = frontier[0];
            frontier.RemoveAt(0);

            (int, int) last = (-1, -1);
            if (InMap(current.Item1 - 1, current.Item2) && GetAt(set, current.Item1 - 1, current.Item2) == true)
                SetAt(set, current.Item1 - 1, current.Item2, true);
            else if (InMap(current.Item1 + 1, current.Item2) && GetAt(set, current.Item1 + 1, current.Item2) == false)
                SetAt(set, current.Item1 + 1, current.Item2, true);
            else if (InMap(current.Item1, current.Item2 - 1) && GetAt(set, current.Item1, current.Item2 -1) == false)
                SetAt(set, current.Item1, current.Item2 - 1, true);
            else if (InMap(current.Item1, current.Item2 + 1) && GetAt(set, current.Item1, current.Item2 + 1) == false)
                SetAt(set, current.Item1, current.Item2 + 1, true);

            // write the path
            if (last.Item1 == current.Item1) {
                SetAt(wallMap, current.Item1, (current.Item2 + last.Item2)/2, true, width*2);
            } else {
                SetAt(wallMap, (current.Item1 + last.Item1)/2, current.Item2, true, width*2);
            }
            SetAt(wallMap, current.Item1, current.Item2, true, width*2);

            if (InMap(current.Item1 - 1, current.Item2) && GetAt(set, current.Item1 - 1, current.Item2) == false)
                SetAt(set, current.Item1 - 1, current.Item2, true);

            if (InMap(current.Item1 + 1, current.Item2) && GetAt(set, current.Item1 + 1, current.Item2) == false)
                SetAt(set, current.Item1 + 1, current.Item2, true);

            if (InMap(current.Item1, current.Item2 - 1) && GetAt(set, current.Item1, current.Item2 -1) == false)
                SetAt(set, current.Item1, current.Item2 - 1, true);

            if (InMap(current.Item1, current.Item2 + 1) && GetAt(set, current.Item1, current.Item2 + 1) == false)
                SetAt(set, current.Item1, current.Item2 + 1, true);
        }

        GenerateMesh();
    }

    private void GenerateMesh() {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
