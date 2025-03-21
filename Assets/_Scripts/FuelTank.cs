using UnityEngine;

public class FuelTank : MonoBehaviour
{
    public float requiredFuel = 35f;
    public float fuelAmount = 0f;


    public void Refuel(float amount)
    {
        fuelAmount += amount;
        if (fuelAmount > requiredFuel)
        {
            fuelAmount = requiredFuel;
        }
        Debug.Log("Fuel amount - " + fuelAmount);

    }

    public bool IsMax() => fuelAmount == requiredFuel;
}
