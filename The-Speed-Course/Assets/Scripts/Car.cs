using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private float forwardSpeed;
    private float BackwardSpeed;
    private float rotationSpeed;
    private float maxSpeedInKPH;
    private float maxReverseSpeedInKPH;
    private float currentSpeed;

    //INHERITANCE PARENT CLASS
    protected float SetCarTurningValue(float carSpeedInKPH)
    {
        if (carSpeedInKPH < 50)
        {
            rotationSpeed = 15;
        }
        else if (carSpeedInKPH > 50 && carSpeedInKPH < 70)
        {
            rotationSpeed = 10;
        }
        else if(carSpeedInKPH > 70 && carSpeedInKPH < 100)
        {
            rotationSpeed = 5;
        }
        else if(carSpeedInKPH > 100 && carSpeedInKPH < 120)
        {
            rotationSpeed = 2;
        }
        else
        {
            rotationSpeed = 1;
        }
        return rotationSpeed;
    }

    //POLYMORPHISM METHOD OVERLOADING
    protected float SetCarTurningValue(float carSpeedInKPH, float baseSpeedLimit)
    {
        if (carSpeedInKPH < baseSpeedLimit)
        {
            rotationSpeed = 15;
        }
        else if (carSpeedInKPH > baseSpeedLimit && carSpeedInKPH < baseSpeedLimit+20)
        {
            rotationSpeed = 10;
        }
        else if (carSpeedInKPH > baseSpeedLimit+20 && carSpeedInKPH < baseSpeedLimit+40)
        {
            rotationSpeed = 5;
        }
        else if (carSpeedInKPH > baseSpeedLimit+40 && carSpeedInKPH < baseSpeedLimit+50)
        {
            rotationSpeed = 2;
        }
        else
        {
            rotationSpeed = 1;
        }
        return rotationSpeed;
    }

    protected float GetCurrentSpeed(Rigidbody CarRb)
    {
        currentSpeed = CarRb.velocity.magnitude * 3.6f;
        return currentSpeed;
    }

    protected void Breaking(WheelCollider FrontLeftWheel, WheelCollider FrontRightWheel, WheelCollider BackLeftWheel, WheelCollider BackRightWheel)
    {
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
    }

    protected void DriveForward(float verticalInput ,WheelCollider FrontLeftWheel, WheelCollider FrontRightWheel, WheelCollider BackLeftWheel, WheelCollider BackRightWheel)
    {
        if(currentSpeed > 100)
        {
            FrontLeftWheel.motorTorque = verticalInput * Time.deltaTime * forwardSpeed;
            FrontRightWheel.motorTorque = verticalInput * Time.deltaTime * forwardSpeed;
            BackLeftWheel.motorTorque = verticalInput * Time.deltaTime * forwardSpeed;
            BackRightWheel.motorTorque = verticalInput * Time.deltaTime * forwardSpeed;
        } 
        else if(currentSpeed > 70)
        {
            FrontLeftWheel.motorTorque = verticalInput * Time.deltaTime * (forwardSpeed / 2);
            FrontRightWheel.motorTorque = verticalInput * Time.deltaTime * (forwardSpeed / 2);
            BackLeftWheel.motorTorque = verticalInput * Time.deltaTime * (forwardSpeed / 2);
            BackRightWheel.motorTorque = verticalInput * Time.deltaTime * (forwardSpeed / 2);
        }
        else if(currentSpeed > 40)
        {
            FrontLeftWheel.motorTorque = verticalInput * Time.deltaTime * (forwardSpeed / 3);
            FrontRightWheel.motorTorque = verticalInput * Time.deltaTime * (forwardSpeed / 3);
            BackLeftWheel.motorTorque = verticalInput * Time.deltaTime * (forwardSpeed / 3);
            BackRightWheel.motorTorque = verticalInput * Time.deltaTime * (forwardSpeed / 3);
        }
        else
        {
            FrontLeftWheel.motorTorque = verticalInput * Time.deltaTime * (forwardSpeed / 4);
            FrontRightWheel.motorTorque = verticalInput * Time.deltaTime * (forwardSpeed / 4);
            BackLeftWheel.motorTorque = verticalInput * Time.deltaTime * (forwardSpeed / 4);
            BackRightWheel.motorTorque = verticalInput * Time.deltaTime * (forwardSpeed / 4);
        }
    }

    protected void Costing(float verticalInput, WheelCollider FrontLeftWheel, WheelCollider FrontRightWheel, WheelCollider BackLeftWheel, WheelCollider BackRightWheel)
    {
        FrontLeftWheel.motorTorque = verticalInput * Time.deltaTime;
        FrontRightWheel.motorTorque = verticalInput * Time.deltaTime;
        BackLeftWheel.motorTorque = verticalInput * Time.deltaTime;
        BackRightWheel.motorTorque = verticalInput * Time.deltaTime;
    }

    protected void DriveBackward(float verticalInput, WheelCollider FrontLeftWheel, WheelCollider FrontRightWheel, WheelCollider BackLeftWheel, WheelCollider BackRightWheel)
    {
        if(currentSpeed > 20)
        {
            FrontLeftWheel.motorTorque = verticalInput * Time.deltaTime * BackwardSpeed;
            FrontRightWheel.motorTorque = verticalInput * Time.deltaTime * BackwardSpeed;
            BackLeftWheel.motorTorque = verticalInput * Time.deltaTime * BackwardSpeed;
            BackRightWheel.motorTorque = verticalInput * Time.deltaTime * BackwardSpeed;
        }
        else if(currentSpeed > 10)
        {
            FrontLeftWheel.motorTorque = verticalInput * Time.deltaTime * (BackwardSpeed / 2);
            FrontRightWheel.motorTorque = verticalInput * Time.deltaTime * (BackwardSpeed / 2);
            BackLeftWheel.motorTorque = verticalInput * Time.deltaTime * (BackwardSpeed / 2);
            BackRightWheel.motorTorque = verticalInput * Time.deltaTime * (BackwardSpeed / 2);
        }
        else
        {
            FrontLeftWheel.motorTorque = verticalInput * Time.deltaTime * (BackwardSpeed / 3);
            FrontRightWheel.motorTorque = verticalInput * Time.deltaTime * (BackwardSpeed / 3);
            BackLeftWheel.motorTorque = verticalInput * Time.deltaTime * (BackwardSpeed / 3);
            BackRightWheel.motorTorque = verticalInput * Time.deltaTime * (BackwardSpeed / 3);
        }
    }

    protected void SetCarTurning(float horizontalInput, WheelCollider FrontLeftWheel, WheelCollider FrontRightWheel)
    {
        FrontLeftWheel.steerAngle = rotationSpeed * horizontalInput;
        FrontRightWheel.steerAngle = rotationSpeed * horizontalInput;
    }

    //ENCAPSULATION
    //Setters
    protected void SetForwardSpeed(float fSpeed)
    {
        forwardSpeed = fSpeed;
    }

    protected void SetBackwardSpeed(float bSpeed)
    {
        BackwardSpeed = bSpeed;
    }

    protected void SetMaxSpeed(float mSpeed)
    {
        maxSpeedInKPH = mSpeed;
    }

    protected void SetReverseMaxSpeed(float rSpeed)
    {
        maxReverseSpeedInKPH = rSpeed;
    }

    //Getters
    protected float GetForwardSpeed()
    {
        return forwardSpeed;
    }

    protected float GetBackwardSpeed()
    {
        return BackwardSpeed;
    }

    protected float GetMaxSpeed()
    {
        return maxSpeedInKPH;
    }

    protected float GetReverseMaxSpeed()
    {
        return maxReverseSpeedInKPH;
    }
}
