using UnityEngine;
using UnityEngine.AI;

public class NPC_MovementController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] private Vector3 interactionZonePosition;
    private Vector3 carPosition;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        interactionZonePosition = FindAnyObjectByType<NPC_InteractionZone>().transform.position;
        animator = GetComponent<Animator>();
    }
   

    public void WalkToInteractionZone() => agent.SetDestination(interactionZonePosition);
    public void WalkToCar() => agent.SetDestination(carPosition);

    private void Update()
    {
        if (agent.velocity.magnitude > 0)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

}
