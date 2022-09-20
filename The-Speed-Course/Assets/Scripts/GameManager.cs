using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject SUV;
    [SerializeField] GameObject Truck;
    [SerializeField] GameObject Tank;
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManagerScript.instance.playerChoice == 0)
        {
            SUV.gameObject.SetActive(true);
        }
        else if(SceneManagerScript.instance.playerChoice == 1)
        {
            Truck.gameObject.SetActive(true);
        }
        else
        {
            Tank.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
