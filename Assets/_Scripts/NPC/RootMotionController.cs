using UnityEngine;

public class RootMotionController : MonoBehaviour
{
    public GameObject skinGO;

    private Animator animator;

    private void Start() => animator = GetComponent<Animator>();
    public void DisableRootMotion() => animator.applyRootMotion = false;
    public void EnableRootMotion() => animator.applyRootMotion = true;
    public void DisableSkin() => skinGO.SetActive(false);
}
