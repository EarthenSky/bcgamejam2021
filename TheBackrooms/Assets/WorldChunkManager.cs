using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldChunkManager : MonoBehaviour
{
    int lastChunkX = 0;
    int lastChunkY = 0;
    int chunkSize = 8;
    public GameObject chunk; // prefab

    public GameObject chunkA;
    public GameObject chunkN;
    public GameObject chunkE;
    public GameObject chunkS;
    public GameObject chunkW;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        chunkA = GameObject.Instantiate(chunk, new Vector3(0, 0, 0), Quaternion.identity, transform);
        
        chunkN = GameObject.Instantiate(chunk, new Vector3(0, 0, 2 * chunkSize * 5), Quaternion.identity, transform);
        chunkE = GameObject.Instantiate(chunk, new Vector3(2 * chunkSize * 5, 0, 0), Quaternion.identity, transform);
        chunkS = GameObject.Instantiate(chunk, new Vector3(0, 0, -2 * chunkSize * 5), Quaternion.identity, transform);
        chunkW = GameObject.Instantiate(chunk, new Vector3(-2 * chunkSize * 5, 0, 0), Quaternion.identity, transform);
        
        var surf = gameObject.AddComponent<NavMeshSurface>();
        surf.useGeometry = NavMeshCollectGeometry.PhysicsColliders;
        surf.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        int chunkX = Mathf.FloorToInt((player.transform.position.x + transform.position.x)/ 5 / (2*chunkSize));
        int chunkY = Mathf.FloorToInt((player.transform.position.z + transform.position.z)/ 5 / (2*chunkSize));
        if (lastChunkX != chunkX || lastChunkY != chunkY) {
            if (chunkX > lastChunkX) { // E
                Destroy(chunkN);
                Destroy(chunkW);
                Destroy(chunkS);

                chunkW = chunkA;
                chunkA = chunkE;

                chunkN = GameObject.Instantiate(chunk, new Vector3(chunkX * 2*chunkSize * 5, 0, (chunkY+1) * 2*chunkSize * 5), Quaternion.identity, transform);
                chunkE = GameObject.Instantiate(chunk, new Vector3((chunkX+1) * 2*chunkSize * 5, 0, chunkY * 2*chunkSize * 5), Quaternion.identity, transform);
                chunkS = GameObject.Instantiate(chunk, new Vector3(chunkX * 2*chunkSize * 5, 0, (chunkY-1) * 2*chunkSize * 5), Quaternion.identity, transform);
            } else if (chunkX < lastChunkX) { // W
                Destroy(chunkN);
                Destroy(chunkS);
                Destroy(chunkE);

                chunkE = chunkA;
                chunkA = chunkW;

                chunkN = GameObject.Instantiate(chunk, new Vector3(chunkX * 2*chunkSize * 5, 0, (chunkY+1) * 2*chunkSize * 5), Quaternion.identity, transform);
                chunkS = GameObject.Instantiate(chunk, new Vector3(chunkX * 2*chunkSize * 5, 0, (chunkY-1) * 2*chunkSize * 5), Quaternion.identity, transform);
                chunkW = GameObject.Instantiate(chunk, new Vector3((chunkX-1) * 2*chunkSize * 5, 0, chunkY * 2*chunkSize * 5), Quaternion.identity, transform);
            } else if (chunkY > lastChunkY) { // N
                Destroy(chunkS);
                Destroy(chunkE);
                Destroy(chunkW);

                chunkS = chunkA;
                chunkA = chunkN;

                chunkN = GameObject.Instantiate(chunk, new Vector3(chunkX * 2*chunkSize * 5, 0, (chunkY+1) * 2*chunkSize * 5), Quaternion.identity, transform);
                chunkE = GameObject.Instantiate(chunk, new Vector3((chunkX+1) * 2*chunkSize * 5, 0, chunkY * 2*chunkSize * 5), Quaternion.identity, transform);
                chunkW = GameObject.Instantiate(chunk, new Vector3((chunkX-1) * 2*chunkSize * 5, 0, chunkY * 2*chunkSize * 5), Quaternion.identity, transform);
            } else if (chunkY < lastChunkY) { // S
                Destroy(chunkE);
                Destroy(chunkN);
                Destroy(chunkW);
                
                chunkN = chunkA;
                chunkA = chunkS;
                
                chunkE = GameObject.Instantiate(chunk, new Vector3((chunkX+1) * 2*chunkSize * 5, 0, chunkY * 2*chunkSize * 5), Quaternion.identity, transform);
                chunkS = GameObject.Instantiate(chunk, new Vector3(chunkX * 2*chunkSize * 5, 0, (chunkY-1) * 2*chunkSize * 5), Quaternion.identity, transform);
                chunkW = GameObject.Instantiate(chunk, new Vector3((chunkX-1) * 2*chunkSize * 5, 0, chunkY * 2*chunkSize * 5), Quaternion.identity, transform);
            }

            //NavMesh.RemoveAllNavMeshData();
            var surf = gameObject.GetComponent<NavMeshSurface>();
            surf.useGeometry = NavMeshCollectGeometry.PhysicsColliders;
            surf.BuildNavMesh();
                        
            lastChunkX = chunkX;
            lastChunkY = chunkY;
        }
    }
}
