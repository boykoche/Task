using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    [SerializeField] private UIController uIController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Falled")
        {
            uIController.LoseLevel();
        }
    }
}
