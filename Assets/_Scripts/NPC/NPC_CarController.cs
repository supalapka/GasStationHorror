using UnityEngine;
using System;

public class NPC_CarController : MonoBehaviour
{
    public event Action OnCarDestroyed;

    private void OnDestroy()
    {
        OnCarDestroyed?.Invoke();
        Debug.Log(name + " destroyed");
    }
}
