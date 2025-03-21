using UnityEngine;

public class FuelTank : MonoBehaviour
{
    public float maxFuel = 100f;
    public float fuelAmount = 50f;


    public void Refuel(float amount)
    {
        fuelAmount += amount;
        if (fuelAmount > maxFuel)
        {
            fuelAmount = maxFuel;
        }
        Debug.Log("Fuel amount - " + fuelAmount);

    }

    public bool IsMax() => fuelAmount == maxFuel;
}
