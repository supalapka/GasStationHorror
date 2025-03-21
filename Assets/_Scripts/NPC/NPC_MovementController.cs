using UnityEngine;
using UnityEngine.AI;

public class NPC_MovementController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Vector3 interactionZonePosition;
    private Vector3 carPosition;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        interactionZonePosition = FindAnyObjectByType<NPC_InteractionZone>().transform.position;
    }
   

    public void WalkToInteractionZone() => agent.SetDestination(interactionZonePosition);
    public void WalkToCar() => agent.SetDestination(carPosition);

}
