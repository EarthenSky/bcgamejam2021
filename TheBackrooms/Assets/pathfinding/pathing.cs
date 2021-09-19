using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pathing : MonoBehaviour
{
    private int recalculating = 0;
    private int absTimer = 0;
    GameObject player;
    public GameObject self;
    private bool goal = false;
    public GameObject target;
    private Vector3 position;
    public UnityEngine.AI.NavMeshAgent agent;
    private Vector3 destination;
    // Start is called before the first frame update
    void Start()
    {   
        player = GameObject.Find("Player");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        position = player.transform.position;
        agent.destination = player.transform.position;
        destination = agent.destination;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tgoal = target.transform.position;
        Vector3 apos = agent.transform.position;
        if(recalculating == 60 && goal){
            recalculating = -1;
            position = player.transform.position;
        }
        
        else if(Vector3.Distance(apos,tgoal)<3){
            goal = true;
        }
        else if(absTimer == 1800){
            goal = true;
        }
        else if(Vector3.Distance(apos,player.transform.position)<10){
            goal = true;
        }
        
        if(!goal){
            absTimer++;
        }
        recalculating++;
    }
}
