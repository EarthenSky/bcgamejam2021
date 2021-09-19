using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathController : MonoBehaviour
{
    public bool finished = false;
    private GameObject player;
    public GameObject enemy,node;
    public int hardLimit = 210;
    public int limit = 30;
    public int current = 0;
    public int hardEnemyLimit = 200;
    public int enemyLimit = 20;
    public int enemyCount = 0, downTime = 45, counter = 0;
    
    public List<GameObject> squares;
    public List<Vector3> vectors;
    public List<Vector3> allTiles;
    private int VectorIndex = 0, allIndex = 0, squaresIndex = 0;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("Player");
        Shuffle(allTiles);        
    }

    // Update is called once per frame
    void Update() {
        // only do this after we're done generating the chunk.
        if(finished) {
            if (limit > current) {
                createNode();
                current++;
            }
            else if (counter >= downTime && enemyCount < enemyLimit){
                counter =- 1;
                spawn();
                
            }

            counter++;
        }
    }

    public void createNode() {
        Vector3 v = allTiles[allIndex];
        GameObject o = GameObject.Instantiate(node,v,Quaternion.identity, gameObject.transform);
        o.GetComponent<MeshRenderer>().enabled = false;
        squares.Add(o);
        allIndex+=5;
        allIndex = allIndex%allTiles.Count;
    }

    public void spawn(){
        Vector3 v = vectors[VectorIndex];
        VectorIndex++;
        if(Vector3.Distance(v,player.transform.position)>30){
            GameObject o = GameObject.Instantiate(enemy,v,Quaternion.identity, gameObject.transform);
            o.GetComponent<pathing>().target = squares[squaresIndex];
            o.GetComponent<pathing>().self = o;
            squaresIndex++;
            
            enemyCount++;
            if(VectorIndex>= vectors.Count){
                VectorIndex= 0;
            }
            if(squaresIndex>=squares.Count){
                squaresIndex = 0;
            }
        }
    }

    // TODO: this is duplicate
    public void Shuffle(List<Vector3> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = Random.Range(0,n+1);  
            Vector3 value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }
}
