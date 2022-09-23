using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedLineScript : MonoBehaviour
{
    [SerializeField] GameObject GameManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Vehicle")
        {
            GameManager.GetComponent<GameManager>().hasCrossedTheFinishLine = true;
        }
    }
}
