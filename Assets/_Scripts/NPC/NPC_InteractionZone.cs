using UnityEngine;

public class NPC_InteractionZone : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, new Vector3(0.7f, 0.7f, 0.7f)); 
    }
}
