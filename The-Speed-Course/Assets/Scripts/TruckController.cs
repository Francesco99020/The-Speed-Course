using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : Car
{
    private float verticalInput;
    private float horizontalInput;

    Vector3 movement;
    Vector3 previousPos;
    Vector3 newPos;
    Vector3 fwd;

    bool isGoingForward;

    [SerializeField] protected float rotationSpeed;

    [SerializeField] protected float carSpeedInKPH;

    [SerializeField] WheelCollider FrontLeftWheel;
    [SerializeField] WheelCollider FrontRightWheel;
    [SerializeField] WheelCollider BackLeftWheel;
    [SerializeField] WheelCollider BackRightWheel;

    [SerializeField] GameObject GameManagerObject;
    GameManager GameManagerScript;

    protected Rigidbody CarRb;

    //INHERITANCE CHILD CLASS
    // Start is called before the first frame update
    void Start()
    {
        SetForwardSpeed(80000);
        SetBackwardSpeed(30000);
        SetMaxSpeed(150);
        SetReverseMaxSpeed(25);
        CarRb = gameObject.GetComponent<Rigidbody>();
        GameManagerScript = GameManagerObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        if (GameManagerScript.hasCrossedTheFinishLine == true)
        {

        }
        else
        {
            rotationSpeed = SetCarTurningValue(carSpeedInKPH);
            SetCarTurning(horizontalInput, FrontLeftWheel, FrontRightWheel);

            newPos = transform.position;
            movement = (newPos - previousPos);

            if (Vector3.Dot(fwd, movement) < 0)
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
    }

    private void LateUpdate()
    {
        previousPos = transform.position;
        fwd = transform.forward;
    }

    public float GetCurrentSpeed()
    {
        return carSpeedInKPH;
    }
}
