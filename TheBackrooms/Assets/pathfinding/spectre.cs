using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class spectre : MonoBehaviour
{   
    private NavMeshAgent agent;
    GameObject player;
    public GameObject self;
    private int recalculating = 0;
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
        if(recalculating >= 60){
            recalculating = -1;
            agent.destination = player.transform.position;
        }        
        recalculating++;
    }
}
