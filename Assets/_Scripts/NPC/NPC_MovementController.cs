using UnityEngine;
using UnityEngine.AI;

public class NPC_MovementController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField] private Vector3 interactionZonePosition;
    [SerializeField] Transform carEnterTransform;

    [SerializeField] private DialogueSystem dialogueSystem;

    private bool isEnteringCar;


    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        interactionZonePosition = FindAnyObjectByType<NPC_InteractionZone>().transform.position;
        dialogueSystem.OnDialogueCompleted += WalkToCar;
    }


    public void WalkToInteractionZone() => agent.SetDestination(interactionZonePosition); //calls in animation in the end of exiting car
    public void WalkToCar()
    {
        agent.SetDestination(carEnterTransform.position);
        isEnteringCar = true;
    }

    private void Update()
    {
        if (agent.velocity.magnitude > 0)
            animator.SetBool("Walking", true);
        else
            animator.SetBool("Walking", false);

        if (isEnteringCar && Vector3.Distance(transform.position, carEnterTransform.position) < 0.5f)
        {
            EnterACar();
        }
    }


    private void EnterACar()
    {
        agent.enabled = false;
        animator.SetTrigger("EnteringCar");
        transform.rotation = Quaternion.Euler(0, -270, 0);
    }
}
