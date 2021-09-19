using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathController : MonoBehaviour
{
    public bool finished = false;
    public GameObject player,enemy,node;
    public int limit = 30;
    public int current = 0;
    public int enemyLimit = 10;
    public int enemyCount = 0, downTime = 45, counter = 0;
    public List<GameObject> squares;
    public List<Vector3> vectors;
    public List<Vector3> allTiles;
    private int VectorIndex = 0, allIndex = 0, squaresIndex;
    // Start is called before the first frame update
    void Start()
    {
        Shuffle(allTiles);        
    }

    // Update is called once per frame
    void Update()
    {
        if(finished){
            if(limit>current ){
                createNode();
                current++;
            }
            if(counter == downTime && enemyCount<enemyLimit && vectors.Count>0){
                counter =-1;
                spawn();
                enemyCount++;
            }
            counter++;
        }
    }
    public void createNode(){
        Vector3 v = allTiles[allIndex];
        GameObject o = GameObject.Instantiate(node,v,Quaternion.identity, gameObject.transform);
        o.GetComponent<MeshRenderer>().enabled = false;
        squares.Add(o);
        allIndex++;

    }
    public void spawn(){
        Vector3 v = vectors[VectorIndex];
        if(Vector3.Distance(v,player.transform.position)>30){
            GameObject o = GameObject.Instantiate(enemy,v,Quaternion.identity, gameObject.transform);
            o.GetComponent<pathing>().target = squares[squaresIndex];
            o.GetComponent<pathing>().self = o;
            squaresIndex++;
            if(squaresIndex>=squares.Count){
                squaresIndex = 0;
            }
        }
    }
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
