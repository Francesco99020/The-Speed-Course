using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CarController : Car
{
    private float verticalInput;
    private float horizontalInput;

    Vector3 movement;
    Vector3 previousPos;
    Vector3 newPos;
    Vector3 fwd;

    bool isGoingForward;

    [SerializeField] protected float rotationSpeed;

    public float carSpeedInKPH;

    [SerializeField] WheelCollider FrontLeftWheel;
    [SerializeField] WheelCollider FrontRightWheel;
    [SerializeField] WheelCollider BackLeftWheel;
    [SerializeField] WheelCollider BackRightWheel;

    protected Rigidbody CarRb;


    // Start is called before the first frame update
    void Start()
    {
        //SUV
        if(SceneManagerScript.instance.playerChoice == 0)
        {
            SetForwardSpeed(60000);
            SetBackwardSpeed(20000);
            SetMaxSpeed(190);
            SetReverseMaxSpeed(30);
        }
        //Truck
        else if(SceneManagerScript.instance.playerChoice == 1)
        {
            SetForwardSpeed(50000);
            SetBackwardSpeed(15000);
            SetMaxSpeed(150);
            SetReverseMaxSpeed(25);
        }
        //Tank
        else if(SceneManagerScript.instance.playerChoice == 2)
        {
            SetForwardSpeed(40000);
            SetBackwardSpeed(10000);
            SetMaxSpeed(120);
            SetReverseMaxSpeed(15);
        }

        CarRb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        rotationSpeed = SetCarTurningValue(carSpeedInKPH);
        SetCarTurning(horizontalInput, FrontLeftWheel, FrontRightWheel);

        newPos = transform.position;
        movement = (newPos - previousPos);

        if(Vector3.Dot(fwd, movement) < 0)
        {
            isGoingForward = false;
        }
        else
        {
            isGoingForward = true;
        }

        carSpeedInKPH = GetCurrentSpeed(CarRb);

        //Car is driving and still under max speed (Forward)
        if (verticalInput >= 0 && carSpeedInKPH < GetMaxSpeed())
        {
            DriveForward(verticalInput, FrontLeftWheel, FrontRightWheel, BackLeftWheel, BackRightWheel);
        }
        else if (isGoingForward && carSpeedInKPH > GetMaxSpeed())
        {
            Costing(verticalInput, FrontLeftWheel, FrontRightWheel, BackLeftWheel, BackRightWheel);
        }

        //Car is driving and still under max speed (Backward)
        if (verticalInput <= 0 && carSpeedInKPH < GetReverseMaxSpeed())
        {
            DriveBackward(verticalInput, FrontLeftWheel, FrontRightWheel, BackLeftWheel, BackRightWheel);
        }
        else if (!isGoingForward && verticalInput <= 0 && carSpeedInKPH > GetReverseMaxSpeed())
        {
            Costing(verticalInput, FrontLeftWheel, FrontRightWheel, BackLeftWheel, BackRightWheel);
        }

        //Breaking
        Breaking(FrontLeftWheel, FrontRightWheel, BackLeftWheel, BackRightWheel);
    }

    private void LateUpdate()
    {
        previousPos = transform.position;
        fwd = transform.forward;
    }
}
