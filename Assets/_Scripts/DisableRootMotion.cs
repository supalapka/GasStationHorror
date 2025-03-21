using UnityEngine;

public class RootMotionController : MonoBehaviour
{
    private Animator animator;

    private void Start() => animator = GetComponent<Animator>();
    public void DisableRootMotion() => animator.applyRootMotion = false;
    public void EnableRootMotion() => animator.applyRootMotion = true;
}
