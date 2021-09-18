using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathing : MonoBehaviour
{
    private int recalculating = 0;
    GameObject player;
    private UnityEngine.AI.NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Cube");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(recalculating == 30){
            recalculating = -1;
            agent.destination = player.transform.position;
        }
        recalculating++;
    }
}
