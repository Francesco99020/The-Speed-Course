using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject SUV;
    [SerializeField] GameObject Truck;
    [SerializeField] GameObject Tank;

    [SerializeField] TextMeshProUGUI ToolTipText;
    [SerializeField] Image PauseMenu;
    [SerializeField] TextMeshProUGUI WarningTipText;
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
        StartCoroutine(DisplayToolTip());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Time.timeScale = 0;
            PauseMenu.gameObject.SetActive(true);
        }
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void UnflipCar()
    {
        if(SUV.GetComponent<CarController>().GetCurrentSpeed() < 5 && Truck.GetComponent<CarController>().GetCurrentSpeed() < 5 && Tank.GetComponent<CarController>().GetCurrentSpeed() < 5)
        {
            if (SceneManagerScript.instance.playerChoice == 0)
            {
                SUV.transform.position = new Vector3(SUV.transform.position.x, 1, SUV.transform.position.z);
                SUV.transform.rotation = Truck.transform.rotation;
            }
            if (SceneManagerScript.instance.playerChoice == 1)
            {
                Truck.transform.position = new Vector3(Truck.transform.position.x, 1, Truck.transform.position.z);
                Truck.transform.rotation = SUV.transform.rotation;
            }
            if (SceneManagerScript.instance.playerChoice == 2)
            {
                Tank.transform.position = new Vector3(Tank.transform.position.x, 1, Tank.transform.position.z);
                Tank.transform.rotation = Truck.transform.rotation;
            }
        }
        else
        {
            StartCoroutine(DisplayWarningMessage());
        }
    }

    public void ReturnToGame()
    {
        Time.timeScale = 1;
        PauseMenu.gameObject.SetActive(false);
    }

    IEnumerator DisplayToolTip()
    {
        ToolTipText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(6);
        ToolTipText.gameObject.SetActive(false);
    }

    IEnumerator DisplayWarningMessage()
    {
        WarningTipText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(5);
        WarningTipText.gameObject.SetActive(false);
    }
}
