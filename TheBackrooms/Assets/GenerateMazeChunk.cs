using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMazeChunk : MonoBehaviour
{
    public GameObject cube;
    public GameObject floor;
    public const int width = 32;
    public const int height = 32;

    public List<bool> wallMap = new List<bool>();

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
    public static bool InMap(int x, int y, int width=width, int height=height) {
        return x > 0 && x < width && y > 0 && y < height;
    }

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(Mathf.FloorToInt(Mathf.PerlinNoise(transform.localPosition.x, transform.localPosition.y) * (float)int.MaxValue));
        List<float> map = new List<float>();
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                map.Add(Random.Range(0, 1000));
            }
        }

        // TODO: use perlin noise to seed a random number generator
        var randomX = Mathf.PerlinNoise(transform.localPosition.x, transform.localPosition.y);
        var randomY = Mathf.PerlinNoise(transform.localPosition.y, transform.localPosition.x);
        var startX = Mathf.FloorToInt(randomX * width);
        var startY = Mathf.FloorToInt(randomY * height);
        
        // Implement prim's algorithm
        List<bool> set = new List<bool>(); // this holds items in frontier
        for (int i = 0; i < width * height; i++) {
            set.Add(false);
        }

        // init our actual map -> this is our map with 
        for (int i = 0; i < width * 2 * height * 2; i++) {
            wallMap.Add(false);
        }

        List<(int, int)> frontier = new List<(int, int)>();
        frontier.Add((startX, startY));
        Debug.Log(frontier[0]);

        while (frontier.Count != 0) {
            // TODO: this sort is inefficient...
            frontier.Sort((a, b) => GetAt(map, a.Item1, a.Item2).CompareTo(GetAt(map, b.Item1, b.Item2)));
            (int, int) current = frontier[frontier.Count-1];
            frontier.RemoveAt(frontier.Count-1);

            // add to the actual map of walls & floors.
            (int, int) last = (-1, -1);
            if (InMap(current.Item1 - 1, current.Item2) && GetAt(wallMap, (current.Item1 - 1)*2, (current.Item2)*2, width*2) == true)
                last = (current.Item1 - 1, current.Item2);
            else if (InMap(current.Item1 + 1, current.Item2) && GetAt(wallMap, (current.Item1 + 1)*2, (current.Item2)*2, width*2) == true)
                last = (current.Item1 + 1, current.Item2);
            else if (InMap(current.Item1, current.Item2 - 1) && GetAt(wallMap, (current.Item1)*2, (current.Item2 - 1)*2, width*2) == true)
                last = (current.Item1, current.Item2 - 1);
            else if (InMap(current.Item1, current.Item2 + 1) && GetAt(wallMap, (current.Item1)*2, (current.Item2 + 1)*2, width*2) == true)
                last = (current.Item1, current.Item2 + 1);
            
            // write the path
            if (last != (-1, -1)) {
                if (GetAt(wallMap, (current.Item1 + last.Item1), (current.Item2 + last.Item2), width*2) == true) {
                    Debug.Log("nooooo");
                }
                SetAt(wallMap, (current.Item1 + last.Item1), (current.Item2 + last.Item2), true, width*2);
            } else {
                Debug.Log("fuck");
                //Debug.Log(frontier);
            }
            SetAt(wallMap, current.Item1*2, current.Item2*2, true, width*2);

            // add adjacent objects.
            if (InMap(current.Item1 - 1, current.Item2) && GetAt(set, current.Item1 - 1, current.Item2) == false) {
                SetAt(set, current.Item1 - 1, current.Item2, true);
                frontier.Add((current.Item1 - 1, current.Item2));
            }

            if (InMap(current.Item1 + 1, current.Item2) && GetAt(set, current.Item1 + 1, current.Item2) == false) {
                SetAt(set, current.Item1 + 1, current.Item2, true);
                frontier.Add((current.Item1 + 1, current.Item2));
            }

            if (InMap(current.Item1, current.Item2 - 1) && GetAt(set, current.Item1, current.Item2 -1) == false) {
                SetAt(set, current.Item1, current.Item2 - 1, true);
                frontier.Add((current.Item1, current.Item2 - 1));
            }

            if (InMap(current.Item1, current.Item2 + 1) && GetAt(set, current.Item1, current.Item2 + 1) == false) {
                SetAt(set, current.Item1, current.Item2 + 1, true);
                frontier.Add((current.Item1, current.Item2 + 1));
            }
        }

        GenerateMesh();
    }

    private void GenerateMesh() {
        for (int y = 0; y < height * 2; y++) {
            for (int x = 0; x < width * 2; x++) {
                if (GetAt(wallMap, x, y, width*2)) {
                    GameObject o = GameObject.Instantiate(floor, new Vector3(transform.localPosition.x + x * 10, 0, transform.localPosition.y + y * 10), Quaternion.identity, gameObject.transform);
                    //o.layer = LayerMask.NameToLayer("groundLayer");
                } else {
                    GameObject.Instantiate(cube, new Vector3(transform.localPosition.x + x * 10, 0, transform.localPosition.y + y * 10), Quaternion.identity, gameObject.transform);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
