using System;
using UnityEngine;

public class NPC_SpawnManager : MonoBehaviour
{
    public NPC_Timings[] nPC_Timings;

    private int currentNpcId = 0;

    private void Start() => SpawnOnTiming();

    private void SpawnOnTiming() => Invoke(nameof(Spawn), nPC_Timings[currentNpcId].timeAfterToSpawn);

    private void Spawn()
    {
        var carcontroller = Instantiate(nPC_Timings[currentNpcId].NPC_Car.gameObject).GetComponent<NPC_CarController>();

        if (nPC_Timings[currentNpcId] != null)
        {
            carcontroller.OnCarDestroyed += SpawnOnTiming;
            currentNpcId++;
        }
    }
}


[Serializable]
public class NPC_Timings
{
    public NPC_CarController NPC_Car;
    public int timeAfterToSpawn;
}
