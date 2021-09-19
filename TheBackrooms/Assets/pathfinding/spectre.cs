using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class spectre : MonoBehaviour
{   
    private NavMeshAgent agent;
    GameObject player;
    public GameObject self;
    private float factor = 3f;
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
        
        if(Vector3.Distance(self.transform.position,player.transform.position)<30){
            RenderSettings.ambientLight = new Color((124/factor/255),(127/factor/255),(74/factor/255));
        }
        else{
            RenderSettings.ambientLight = new Color((124f/255),(127f/255),(74f/255));
        }
    }
}
