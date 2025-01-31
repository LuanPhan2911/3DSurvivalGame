using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuccessRange : MonoBehaviour
{
    [SerializeField] private Image successRangeImage;
    [SerializeField] private Image perfectRangeImage;

    [SerializeField] private List<Vector3> rotateList;

    private Vector3 startRotate = Vector3.zero;
    private Quaternion startQuaternion;

    private float perfectAngle;
    private float successAngle;



    private void Awake()
    {
        startQuaternion = transform.rotation;
        perfectAngle = perfectRangeImage.fillAmount * 360;
        successAngle = successRangeImage.fillAmount * 360;


    }

    public void RandomRotate()
    {
        int randIndex = Random.Range(0, rotateList.Count);
        transform.rotation = startQuaternion;
        startRotate = rotateList[randIndex];

        transform.Rotate(startRotate);

    }
    public bool IsGoodCheck(float absAngle)
    {
        return absAngle >= Mathf.Abs(startRotate.z) && absAngle <= Mathf.Abs(startRotate.z) + successAngle;
    }
    public bool IsPerfectCheck(float absAngle)
    {
        return absAngle >= Mathf.Abs(startRotate.z) && absAngle <= Mathf.Abs(startRotate.z) + perfectAngle;
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
