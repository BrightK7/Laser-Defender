using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemageDealer : MonoBehaviour
{
    [SerializeField] int Demage = 100;
    public int getDemage()
    {
        return Demage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
