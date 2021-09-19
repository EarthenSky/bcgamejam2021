using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pathController : MonoBehaviour
{
    public GameObject player,enemy,node;
    public int limit = 30;
    public int current = 0;
    public int enemyLimit = 10;
    public int enemyCount = 0, downTime = 4500, counter = 0;
    public List<GameObject> squares;
    public List<Vector3> vectors;
    public List<Vector3> allTiles;
    private int VectorIndex = 0, allIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void createNode(){
        Vector3 v = allTiles[allIndex];
        GameObject o = GameObject.Instantiate(node,v,Quaternion.identity, gameObject.transform);
        o.GetComponent<MeshRenderer>().enabled = false;
        squares.Add(o);

    }
    public void spawn(){
        Vector3 v = vectors[VectorIndex];
        if(Vector3.Distance(v,player.transform.position)>30){
            GameObject o = GameObject.Instantiate(enemy,v,Quaternion.identity, gameObject.transform);
            o.GetComponent<pathing>().self = o;
        }
    }
}
