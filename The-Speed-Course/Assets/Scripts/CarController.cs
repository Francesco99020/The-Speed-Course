using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float verticalInput;
    private float horizontalInput;
    [SerializeField] protected float forwardSpeed = 430000.0f;
    [SerializeField] protected float BackwardSpeed = 200000.0f;
    [SerializeField] protected float rotationSpeed = 20.0f;

    public float carSpeed;

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

        if(FrontRightWheel.isGrounded && FrontLeftWheel.isGrounded || BackRightWheel.isGrounded && BackLeftWheel.isGrounded)
        {
            //transform.Rotate(Vector3.right * Time.deltaTime * horizontalInput * rotationSpeed);
            FrontLeftWheel.steerAngle = rotationSpeed * horizontalInput;
            FrontRightWheel.steerAngle = rotationSpeed * horizontalInput;
            if (verticalInput >= 0)
            {
                CarRb.AddRelativeForce(Vector3.forward * Time.deltaTime * verticalInput * forwardSpeed, ForceMode.Impulse);
                FrontLeftWheel.motorTorque = verticalInput * Time.deltaTime * 2;
                FrontRightWheel.motorTorque = verticalInput * Time.deltaTime * 2;
                BackLeftWheel.motorTorque = verticalInput * Time.deltaTime * 2;
                BackRightWheel.motorTorque = verticalInput * Time.deltaTime * 2;
            }
            else
            {
                CarRb.AddRelativeForce(Vector3.forward * Time.deltaTime * verticalInput * BackwardSpeed);
                FrontLeftWheel.motorTorque = verticalInput * Time.deltaTime * 1;
                FrontRightWheel.motorTorque = verticalInput * Time.deltaTime * 1;
                BackLeftWheel.motorTorque = verticalInput * Time.deltaTime * 1;
                BackRightWheel.motorTorque = verticalInput * Time.deltaTime * 1;
            }
        }

        carSpeed = CarRb.velocity.magnitude * 3.6f;
    }
}
