using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private int damage = 5;

    private void Awake()
    {
        Instance = this;
    }

    public int GetDamage()
    {
        return damage;
    }
}
