using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class FuelingNozzle : MonoBehaviour
{
     float fuelingSpeed = 0.05f;

    [SerializeField] bool canFuel = false;
    private bool isFueling = false;
    private FuelTank fuelTank;
    private Animator animator;

    [SerializeField] private TextMeshProUGUI gasCounter;

    private void Awake() => animator = GetComponent<Animator>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<FuelTank>(out FuelTank _fuelTank))
        {
            fuelTank = _fuelTank;

            if (fuelTank.IsMax() == false)
            {
                canFuel = true;
                gasCounter.gameObject.SetActive(true);
                gasCounter.text = $"{(int)(fuelTank.fuelAmount)}/{fuelTank.requiredFuel}";

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<FuelTank>(out FuelTank _fuelTank))
        {
            canFuel = false;
            gasCounter.gameObject.SetActive(false);
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
            gasCounter.text = $"{(int)(fuelTank.fuelAmount)}/{fuelTank.requiredFuel}";
        }

        if (isFueling && Mouse.current.leftButton.isPressed == false ||
            isFueling && canFuel == false)
        {
            Debug.Log("Stop fueling");
            animator.SetTrigger("StopFuel");
            isFueling = false;
        }
    }

}
