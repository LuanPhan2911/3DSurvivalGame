using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{


    [SerializeField] private Animator animator;

    [SerializeField] private float moveSpeed = 0.2f;
    [SerializeField] private float walkCounter;
    [SerializeField] private float waitCounter;
    [SerializeField] private bool isWalking;

    private const string IS_RUNNING = "IsRunning";


    private Vector3 stopPosition;

    private float walkTime;

    private float waitTime;

    private int walkDirection;



    // Start is called before the first frame update
    private void Start()
    {

        //So that all the prefabs don't move/stop at the same time
        walkTime = Random.Range(3, 6);
        waitTime = Random.Range(5, 7);


        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isWalking)
        {


            animator.SetBool(IS_RUNNING, true);

            walkCounter -= Time.deltaTime;

            switch (walkDirection)
            {
                case 0:
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case 1:
                    transform.localRotation = Quaternion.Euler(0f, 90, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case 2:
                    transform.localRotation = Quaternion.Euler(0f, -90, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case 3:
                    transform.localRotation = Quaternion.Euler(0f, 180, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
            }

            if (walkCounter <= 0)
            {
                stopPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                isWalking = false;
                //stop movement
                transform.position = stopPosition;
                animator.SetBool(IS_RUNNING, false);
                //reset the waitCounter
                waitCounter = waitTime;
            }


        }
        else
        {

            waitCounter -= Time.deltaTime;

            if (waitCounter <= 0)
            {
                ChooseDirection();
            }
        }
    }


    public void ChooseDirection()
    {
        walkDirection = Random.Range(0, 4);

        isWalking = true;
        walkCounter = walkTime;
    }
}
