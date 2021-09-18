using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathing : MonoBehaviour
{

    private float radius = 10;
    private bool matrix;
    private int recalculating = 0;
    GameObject player;
    private bool goal = false;
    private UnityEngine.AI.NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {   
        player = GameObject.Find("Cube");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        // getting random numbers to determine the 
        int[] array = new int[3];
        array[0] = Random.Range(0,2);
        array[1] = Random.Range(0,2);
        //array[2] = Random.Range(-radius,radius+1);
        array[1] *= -1;
        //change these variables later
        int z = (int)player.transform.position[2]/10;
        int x = (int)player.transform.position[0]/10;
        int cols = 32;
        int i = 0;

        if(array[1]<0){
            //array[1]*=radius;
        }
        else{
            array[1]++;
            //array[1]*=radius;
        }
        if(array[0] == 0){
            z+=array[1];
            x+=array[2];
        }
        else{
            x+=array[1];
            z+=array[2];
        }
        //edge cases
        if(x<0){
            x = 0;
        }
        if(z<0){
            z = 0;
        }
        
        //while(bool[z*cols+x+i] == 0){
        //    i++;
        //}
        int remainder = (z*cols+x+i)%cols;
        if(remainder<x){
            z++;
            //x = remainder+;
        }
       
        //vector3 position = new vector3(x+5,player.transform.position[1],z+5)
    }

    // Update is called once per frame
    void Update()
    {
        if(recalculating == 30 && goal){
            recalculating = -1;
            agent.destination = player.transform.position;
        }
        recalculating++;
    }
}
