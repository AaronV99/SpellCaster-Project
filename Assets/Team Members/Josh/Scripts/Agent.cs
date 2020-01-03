using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    public Transform goal;
    NavMeshAgent agent; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.destination = goal.position;
    }

    void OnTriggerEnter(Collider collision)
    {
        
        if(collision.gameObject.tag == "Spell")
        {
            Destroy(gameObject);
        }

    }
}