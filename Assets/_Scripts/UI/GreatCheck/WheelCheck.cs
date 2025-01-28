using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelCheck : MonoBehaviour
{
    [SerializeField] private Transform centerPoint;

    public float rotationSpeed = 100f;

    void Update()
    {
        transform.RotateAround(centerPoint.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
