using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI GameOverMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Vehicle")
        {
            Time.timeScale = 0;
            other.gameObject.SetActive(false);
            GameOverMenu.gameObject.SetActive(true);
        }
    }
}
