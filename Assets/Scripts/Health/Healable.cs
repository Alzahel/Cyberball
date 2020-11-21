using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Healable : MonoBehaviour
{
    private Health health = null;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    public void Heal(int _healAmount)
    {
        health.Remove(_healAmount);
    }
}
