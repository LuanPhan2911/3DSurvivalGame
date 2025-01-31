using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCheck : MonoBehaviour
{
    [SerializeField] private Transform centerPoint;

    private float rotationSpeed = 200f;
    private Vector3 startPosition;
    private Quaternion startQuaternion;

    private void Awake()
    {
        startPosition = transform.localPosition;
        startQuaternion = transform.localRotation;
    }



    private bool isStart = false;
    private bool isFinish = false;
    public event EventHandler OnRedCheckFinishRotate;

    public EventHandler OnRedCheckFail;

    void Update()
    {
        if (isStart)
        {
            transform.RotateAround(centerPoint.position, Vector3.forward * -1, rotationSpeed * Time.deltaTime);

            if (GetAbsAngle() >= 358f)
            {
                // return to initial rotate position;
                // finish rotate around;
                isFinish = true;
                isStart = false;
                OnRedCheckFail?.Invoke(this, EventArgs.Empty);

            }
        }
        if (isFinish)
        {
            OnRedCheckFinishRotate?.Invoke(this, EventArgs.Empty);
        }

    }

    public void SetStart()
    {
        isStart = true;
        isFinish = false;
        transform.localPosition = startPosition;
        transform.rotation = startQuaternion;

    }
    public void SetFinish()
    {
        isStart = false;
        isFinish = true;

    }

    public float GetAbsAngle()
    {
        return Mathf.Clamp(Mathf.Abs(transform.rotation.eulerAngles.z - 360), 0f, 360f);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
