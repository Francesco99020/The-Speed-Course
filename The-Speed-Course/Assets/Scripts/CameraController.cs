using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    GameObject Vehicle;

    [SerializeField] List<GameObject> VehicleList = new List<GameObject>();

    Vector3 offset = new Vector3(0f, 2.03f, -2.58f);

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManagerScript.instance.playerChoice == 0)
        {
            Vehicle = VehicleList[0];
        }
        else if(SceneManagerScript.instance.playerChoice == 1)
        {
            Vehicle = VehicleList[1];
        }
        else
        {
            Vehicle = VehicleList[2];
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vehicle.transform.position + offset;
    }
}
