using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtFollow : MonoBehaviour
{
    GameObject DirtSplatter;
    // Start is called before the first frame update
    void Start()
    {
        DirtSplatter = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Grass")
        {
            DirtSplatter.gameObject.SetActive(true);
        }
        if(other.gameObject.tag == "Road")
        {
            DirtSplatter.gameObject.SetActive(false);
        }
    }
}
