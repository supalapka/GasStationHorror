using UnityEngine;
using UnityEngine.InputSystem;

public class FuelingNozzle : MonoBehaviour
{
     float fuelingSpeed = 0.05f;

    [SerializeField] bool canFuel = false;
    private bool isFueling = false;
    private FuelTank fuelTank;
    private Animator animator;

    private void Awake() => animator = GetComponent<Animator>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<FuelTank>(out FuelTank _fuelTank))
        {
            fuelTank = _fuelTank;

            if (fuelTank.IsMax() == false)
                canFuel = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<FuelTank>(out FuelTank _fuelTank))
        {
            canFuel = false;
        }
    }


    private void Update()
    {
        if (canFuel)
            canFuel = !fuelTank.IsMax();

        if (canFuel && Mouse.current.leftButton.isPressed)
        {
            Debug.Log("Trying to fuel");
            if(isFueling == false)
            {
                isFueling = true;
                animator.SetTrigger("StartFuel");
                Debug.Log("Start fueling");
            }

            fuelTank.Refuel(fuelingSpeed);
        }

        if(isFueling && Mouse.current.leftButton.isPressed == false ||
            isFueling && canFuel == false)
        {
            Debug.Log("Stop fueling");
            animator.SetTrigger("StopFuel");
            isFueling = false;
        }
    }

}
