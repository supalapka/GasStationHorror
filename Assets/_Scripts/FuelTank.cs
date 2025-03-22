using System;
using UnityEngine;

public class FuelTank : MonoBehaviour
{
    public float requiredFuel = 35f;
    public float fuelAmount = 0f;

    public event Action OnFuelingComplete;


    public void Refuel(float amount)
    {
        fuelAmount += amount;
        if (fuelAmount > requiredFuel)
        {
            fuelAmount = requiredFuel;
            OnFuelingComplete?.Invoke();
        }
    }

    public bool IsMax() => fuelAmount == requiredFuel;
}
