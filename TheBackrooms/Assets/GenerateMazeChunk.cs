using System.Collections;
using System.Data.Common;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor.SceneManagement;

namespace UnityEngine.AI {
    public class GenerateMazeChunk : MonoBehaviour
    {
        public Material wallMat;

        public GameObject cube;
        public GameObject floor;
        public GameObject ceiling;
        public GameObject ceilingLight;

        public NavMeshSurface surf = null;

        public GameObject node;
        private GameObject pathController;

        //public Mesh myGeom;

        public const int width = 8;
        public const int height = 8;

        public List<bool> wallMap = new List<bool>();
        public List<int> ceilingRotationMap = new List<int>();

        private static float GetAt(List<float> list, int x, int y, int width=width) {
            return list[x + width * y];
        }
        private static int GetAt(List<int> list, int x, int y, int width=width) {
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

        private static void Shuffle<T>(IList<T> list) {  
            int n = list.Count;  
            while (n > 1) {  
                n--;
                int k = Random.Range(0, n + 1);  
                T value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }  
        }

        // Start is called before the first frame update
        void Start()
        {
            pathController = GameObject.Find("EnemyController");

            var trueSeed = Mathf.FloorToInt(Mathf.PerlinNoise((transform.position.x + 1000) / 5217.7f, (transform.position.z + 1201) / 5037.9f) * int.MaxValue);
            Random.InitState(trueSeed);
            List<float> map = new List<float>();
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    map.Add(Random.Range(1, 500) * Random.Range(1, 500));
                }
            }

            // TODO: use perlin noise to seed a random number generator
            var randomX = Random.Range(1, 500)/500f * Mathf.PerlinNoise((transform.position.x + 1000)/3, (transform.position.z + 1000)/3);
            var randomY = Random.Range(1, 500)/500f * Mathf.PerlinNoise((transform.position.z + 1201)/3, (transform.position.x + 1201)/3);
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

            for (int i = 0; i < width * 2 * height * 2; i++) {
                ceilingRotationMap.Add(Random.Range(0, 24));
            }

            List<(int, int, float)> frontier = new List<(int, int, float)>();
            frontier.Add((startX, startY, 0f));

            while (frontier.Count != 0) {
                // TODO: this sort is inefficient...
                frontier.Sort((a, b) => a.Item3.CompareTo(b.Item3));
                (int, int) current = (frontier[frontier.Count-1].Item1, frontier[frontier.Count-1].Item2);
                frontier.RemoveAt(frontier.Count-1);

                // add to the actual map of walls & floors.
                (int, int) last = (-1, -1);
                List<(int, int)> directions = new List<(int, int)>{(current.Item1 - 1, current.Item2), (current.Item1 + 1, current.Item2), (current.Item1, current.Item2 - 1), (current.Item1, current.Item2 + 1)};
                Shuffle(directions);
                
                foreach(var item in directions) {
                    if (InMap(item.Item1, item.Item2) && GetAt(wallMap, item.Item1*2, item.Item2*2, width*2) == true) {
                        last = item;
                        break;
                    }
                }

                // write the path
                if (last != (-1, -1)) {
                    if (GetAt(wallMap, (current.Item1 + last.Item1), (current.Item2 + last.Item2), width*2) == true) {
                        Debug.Log("nooooo");
                    }
                    SetAt(wallMap, (current.Item1 + last.Item1), (current.Item2 + last.Item2), true, width*2);
                }
                SetAt(wallMap, current.Item1*2, current.Item2*2, true, width*2);

                // add adjacent objects.
                if (InMap(current.Item1 - 1, current.Item2) && GetAt(set, current.Item1 - 1, current.Item2) == false) {
                    SetAt(set, current.Item1 - 1, current.Item2, true);
                    frontier.Add((current.Item1 - 1, current.Item2, GetAt(map, current.Item1 - 1, current.Item2)));
                }

                if (InMap(current.Item1 + 1, current.Item2) && GetAt(set, current.Item1 + 1, current.Item2) == false) {
                    SetAt(set, current.Item1 + 1, current.Item2, true);
                    frontier.Add((current.Item1 + 1, current.Item2, GetAt(map, current.Item1 + 1, current.Item2)));
                }

                if (InMap(current.Item1, current.Item2 - 1) && GetAt(set, current.Item1, current.Item2 -1) == false) {
                    SetAt(set, current.Item1, current.Item2 - 1, true);
                    frontier.Add((current.Item1, current.Item2 - 1, GetAt(map, current.Item1, current.Item2 - 1)));
                }

                if (InMap(current.Item1, current.Item2 + 1) && GetAt(set, current.Item1, current.Item2 + 1) == false) {
                    SetAt(set, current.Item1, current.Item2 + 1, true);
                    frontier.Add((current.Item1, current.Item2 + 1, GetAt(map, current.Item1, current.Item2 + 1)));
                }
            }

            for (int i = 0; i < 32; i++) {
                int x = Random.Range(1, width*2-1);
                int y = Random.Range(1, height*2-1);
                SetAt(wallMap, x, y, true, width*2);
            }

            var bottomSeed = Mathf.FloorToInt(Mathf.PerlinNoise((transform.position.x + 1000+width*5)/100f, (transform.position.z + 1000)/100f) * int.MaxValue);
            var rightSeed = Mathf.FloorToInt(Mathf.PerlinNoise((transform.position.x + 1000+width*5*2)/100f, (transform.position.z + 1000+height*5)/100f) * int.MaxValue);
            var topSeed = Mathf.FloorToInt(Mathf.PerlinNoise((transform.position.x + 1000+width*5)/100f, (transform.position.z + 1000+2*height*5)/100f) * int.MaxValue);
            var leftSeed = Mathf.FloorToInt(Mathf.PerlinNoise((transform.position.x + 1000)/100f, (transform.position.z + 1000+height*5)/100f) * int.MaxValue);

            Debug.Log(topSeed);
            Debug.Log(bottomSeed);

            Random.InitState(bottomSeed);
            Random.Range(1, 8);
            for (int i = 0; i < Random.Range(3, 9); i++) {
                var v = Random.Range(2, width*2-3);
                SetAt(wallMap, v, 0, true, width*2);
                SetAt(wallMap, v, 1, true, width*2);
            }

            Random.InitState(rightSeed);
            Random.Range(1, 8);
            for (int i = 0; i < Random.Range(3, 9); i++) {
                SetAt(wallMap, width*2-1, Random.Range(2, height*2-3), true, width*2);
            }

            Random.InitState(topSeed);
            Random.Range(1, 8);
            for (int i = 0; i < Random.Range(3, 9); i++) {
                SetAt(wallMap, Random.Range(2, width*2-3), height*2-1, true, width*2);
            }

            Random.InitState(leftSeed);
            Random.Range(1, 8);
            for (int i = 0; i < Random.Range(3, 9); i++) {
                var v = Random.Range(2, height*2-3);
                SetAt(wallMap, 0, v, true, width*2);
                SetAt(wallMap, 1, v, true, width*2);
            }

            GenerateMesh();
        }
        
        private void GenerateMesh() {
            int counter = 0;

            GameObject groundTiles = new GameObject("groundTiles");
            GameObject ceilingList = new GameObject("ceilingList");
            groundTiles.transform.parent = transform;
            ceilingList.transform.parent = transform;

            for (int y = 0; y < height * 2; y++) {
                for (int x = 0; x < width * 2; x++) {
                    if (GetAt(wallMap, x, y, width*2)) {
                        // TODO: do in-a-row walls for performance...
                        GameObject obj = GameObject.Instantiate(floor, new Vector3(transform.localPosition.x + x * 5, 0, transform.localPosition.z + y * 5), Quaternion.identity, groundTiles.transform);
                        obj.layer = LayerMask.NameToLayer("Ground");
                        surf = obj.AddComponent<NavMeshSurface>();

                        // spread out nodes
                        if(counter == width*height/20){
                            pathController.GetComponent<pathController>().allTiles.Add(new Vector3(x*5,-2,y*5));
                            pathController.GetComponent<pathController>().vectors.Add(new Vector3(x*5,2,y*5));
                            counter = 0;
                        }
                        counter++;
                        
                    } else {
                        GameObject b = GameObject.Instantiate(cube, new Vector3(transform.localPosition.x + x * 5, 0, transform.localPosition.z + y * 5), Quaternion.identity, groundTiles.transform);
                    }
                }
            }

            for (int y = 0; y < height * 2; y++) {
                for (int x = 0; x < width * 2; x++) {
                    if (GetAt(wallMap, x, y, width*2)) {
                        var rot = GetAt(ceilingRotationMap, x, y, width*2); // no longer checks for rotation
                        if (rot == 0) {
                            GameObject ceil = GameObject.Instantiate(ceilingLight, new Vector3(transform.localPosition.x + x * 5, 4, transform.localPosition.z + y * 5), Quaternion.identity, ceilingList.transform);
                            ceil.transform.Rotate(new Vector3(0, 0, 180));
                        } else {
                            GameObject ceil = GameObject.Instantiate(ceiling, new Vector3(transform.localPosition.x + x * 5, 4, transform.localPosition.z + y * 5), Quaternion.identity, ceilingList.transform);
                            ceil.transform.Rotate(new Vector3(0, 0, 180));
                        }
                    }
                }
            }
            
            pathController.GetComponent<pathController>().finished = true;
            surf.BuildNavMesh();
        }

        // Update is called once per frame
        void Update() {

        }

    }
}
