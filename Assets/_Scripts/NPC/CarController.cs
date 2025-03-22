using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject NPC_GO;
    private Animator animator;

    private void Start() => animator = GetComponent<Animator>();
    public void SpawnNPC() { NPC_GO.SetActive(true); } //cals in CarArriving animation
    public void CarLeave() => animator.SetTrigger("CarLeaving");
    public void DestroyThis() => Destroy(gameObject.transform.parent.gameObject); //calls in CarLeaving animation
}
