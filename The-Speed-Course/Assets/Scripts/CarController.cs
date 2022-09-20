using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float verticalInput;
    private float horizontalInput;

    Vector3 movement;
    Vector3 previousPos;
    Vector3 newPos;
    Vector3 fwd;

    bool isGoingForward;

    [SerializeField] protected float forwardSpeed = 430000.0f;
    [SerializeField] protected float BackwardSpeed = 200000.0f;
    [SerializeField] protected float rotationSpeed = 10.0f;
    [SerializeField] protected float maxSpeedInKPH = 100.0f;
    [SerializeField] protected float maxReverseSpeedInKPH = 30.0f;

    public float carSpeedInKPH;

    [SerializeField] WheelCollider FrontLeftWheel;
    [SerializeField] WheelCollider FrontRightWheel;
    [SerializeField] WheelCollider BackLeftWheel;
    [SerializeField] WheelCollider BackRightWheel;
    protected Rigidbody CarRb;


    // Start is called before the first frame update
    void Start()
    {
        CarRb = GetComponent<Rigidbody>();
        //CarRb.centerOfMass = COM.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        //transform.Rotate(Vector3.right * Time.deltaTime * horizontalInput * rotationSpeed);

        if(carSpeedInKPH < 50)
        {
            rotationSpeed = 15;
        }
        else if(carSpeedInKPH > 50 && carSpeedInKPH < 70)
        {
            rotationSpeed = 10;
        }
        else
        {
            rotationSpeed = 5;
        }
        FrontLeftWheel.steerAngle = rotationSpeed * horizontalInput;
        FrontRightWheel.steerAngle = rotationSpeed * horizontalInput;

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

        //Car is driving and still under max speed (Forward)
        if (verticalInput >= 0 && carSpeedInKPH < maxSpeedInKPH)
        {
            FrontLeftWheel.motorTorque = verticalInput * Time.deltaTime * forwardSpeed;
            FrontRightWheel.motorTorque = verticalInput * Time.deltaTime * forwardSpeed;
            BackLeftWheel.motorTorque = verticalInput * Time.deltaTime * forwardSpeed;
            BackRightWheel.motorTorque = verticalInput * Time.deltaTime * forwardSpeed;
        }
        else if (isGoingForward && verticalInput >= 0 && carSpeedInKPH > maxSpeedInKPH)
        {
            FrontLeftWheel.motorTorque = verticalInput * Time.deltaTime;
            FrontRightWheel.motorTorque = verticalInput * Time.deltaTime;
            BackLeftWheel.motorTorque = verticalInput * Time.deltaTime;
            BackRightWheel.motorTorque = verticalInput * Time.deltaTime;
        }

        //Car is driving and still under max speed (Backward)
        if (verticalInput <= 0 && carSpeedInKPH < maxReverseSpeedInKPH)
        {
            FrontLeftWheel.motorTorque = verticalInput * Time.deltaTime * BackwardSpeed;
            FrontRightWheel.motorTorque = verticalInput * Time.deltaTime * BackwardSpeed;
            BackLeftWheel.motorTorque = verticalInput * Time.deltaTime * BackwardSpeed;
            BackRightWheel.motorTorque = verticalInput * Time.deltaTime * BackwardSpeed;
        }
        else if (!isGoingForward && verticalInput <= 0 && carSpeedInKPH > maxReverseSpeedInKPH)
        {
            FrontLeftWheel.motorTorque = verticalInput * Time.deltaTime;
            FrontRightWheel.motorTorque = verticalInput * Time.deltaTime;
            BackLeftWheel.motorTorque = verticalInput * Time.deltaTime;
            BackRightWheel.motorTorque = verticalInput * Time.deltaTime;
        }

        //Breaking
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FrontLeftWheel.brakeTorque = 20000;
            FrontRightWheel.brakeTorque = 20000;
            BackLeftWheel.brakeTorque = 20000;
            BackRightWheel.brakeTorque = 20000;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            FrontLeftWheel.brakeTorque = 0;
            FrontRightWheel.brakeTorque = 0;
            BackLeftWheel.brakeTorque = 0;
            BackRightWheel.brakeTorque = 0;
        }

        carSpeedInKPH = CarRb.velocity.magnitude * 3.6f;
    }

    private void LateUpdate()
    {
        previousPos = transform.position;
        fwd = transform.forward;
    }
}
