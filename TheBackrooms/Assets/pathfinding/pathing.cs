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
    public UnityEngine.AI.NavMeshAgent agent;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {   
        player = GameObject.Find("Player");
        agent = self.GetComponent<NavMeshAgent>();
        agent.destination = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tgoal = target.transform.position;
        Vector3 apos = agent.transform.position;
        if(recalculating >= 60 && goal){
            recalculating = -1;
            agent.destination = player.transform.position;
        }
        
        else if(Vector3.Distance(apos,tgoal)<7){
            goal = true;
        }
        else if(absTimer == 1800){
            goal = true;
        }
        else if(Vector3.Distance(apos,player.transform.position)<10){
            goal = true;
        }
        if(Vector3.Distance(apos,player.transform.position)>300)
        
        absTimer++;
        
        recalculating++;
    }
}
