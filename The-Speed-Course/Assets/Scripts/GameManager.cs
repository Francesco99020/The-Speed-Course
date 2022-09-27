using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    List<InputEntry> entries = new List<InputEntry>();

    [SerializeField] GameObject SUV;
    [SerializeField] GameObject Truck;
    [SerializeField] GameObject Tank;

    [SerializeField] TextMeshProUGUI ToolTipText;
    [SerializeField] Image PauseMenu;
    [SerializeField] TextMeshProUGUI WarningTipText;
    [SerializeField] TextMeshProUGUI Speedometer;
    [SerializeField] TextMeshProUGUI Timer;
    [SerializeField] TextMeshProUGUI CountDownTimer;
    [SerializeField] TextMeshProUGUI FinishedText;
    [SerializeField] TextMeshProUGUI EndGameStatistics;

    [SerializeField] string filename;

    [SerializeField] Button Restartbtn;
    [SerializeField] Button BackToMenubtn;

    public float carSpeedInKPH;
    float timerNum;
    public bool hasCrossedTheFinishLine = false;
    private bool hasSaved = false;

    // Start is called before the first frame update
    void Start()
    {
        entries = FileHandler.ReadListFromJSON<InputEntry>(filename);
        //locks mouse to scene
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        Time.timeScale = 0;
        StartCoroutine(StartCountDown());

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
        if (hasCrossedTheFinishLine)
        {
            Cursor.visible = true; 
            Cursor.lockState = CursorLockMode.Confined;
            string playerVehicle;
            Speedometer.gameObject.SetActive(false);
            Timer.gameObject.SetActive(false);
            FinishedText.gameObject.SetActive(true);
            EndGameStatistics.gameObject.SetActive(true);
            EndGameStatistics.text = "Final Time: " + timerNum.ToString("#.00");
            Restartbtn.gameObject.SetActive(true);
            BackToMenubtn.gameObject.SetActive(true);
            if(SceneManagerScript.instance.playerChoice == 0)
            {
                playerVehicle = "SUV";
            }
            else if(SceneManagerScript.instance.playerChoice == 1)
            {
                playerVehicle = "Truck";
            }
            else
            {
                playerVehicle = "Tank";
            }
            if (!hasSaved)
            {
                AddUserToList(SceneManagerScript.instance.playerName, playerVehicle, timerNum);
                FileHandler.SaveToJSON<InputEntry>(entries, filename);
                hasSaved = true;
            }

        }
        else
        {
            timerNum += Time.deltaTime;
            Timer.text = timerNum.ToString("#.00");
            if (SceneManagerScript.instance.playerChoice == 0)
            {
                SUVController CarScript = SUV.GetComponent<SUVController>();
                carSpeedInKPH = Mathf.Round(CarScript.GetCurrentSpeed());
                Speedometer.text = carSpeedInKPH + " KPH";
            }
            else if (SceneManagerScript.instance.playerChoice == 1)
            {
                TruckController CarScript = Truck.GetComponent<TruckController>();
                carSpeedInKPH = Mathf.Round(CarScript.GetCurrentSpeed());
                Speedometer.text = carSpeedInKPH + " KPH";
            }
            else
            {
                TankController CarScript = Tank.GetComponent<TankController>();
                carSpeedInKPH = Mathf.Round(CarScript.GetCurrentSpeed());
                Speedometer.text = carSpeedInKPH + " KPH";
            }
        

            if (Input.GetKey(KeyCode.Escape))
            {
                Cursor.visible = true;
                Time.timeScale = 0;
                PauseMenu.gameObject.SetActive(true);
            }
        }

    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void UnflipCar()
    {
        if(SUV.GetComponent<SUVController>().GetCurrentSpeed() < 5 && Truck.GetComponent<TruckController>().GetCurrentSpeed() < 5 && Tank.GetComponent<TankController>().GetCurrentSpeed() < 5)
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
        Cursor.visible = false;
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

    IEnumerator StartCountDown()
    {
        CountDownTimer.gameObject.SetActive(true);
        //3
        CountDownTimer.text = "3";
        yield return new WaitForSecondsRealtime(1);
        //2
        CountDownTimer.text = "2";
        yield return new WaitForSecondsRealtime(1);
        //1
        CountDownTimer.text = "1";
        yield return new WaitForSecondsRealtime(1);
        //GO
        CountDownTimer.gameObject.SetActive(false);
        Speedometer.gameObject.SetActive(true);
        Timer.gameObject.SetActive(true);
        Time.timeScale = 1;
    }

    public void AddUserToList(string name, string vehicle, float time)
    {
        entries.Add(new InputEntry(name, vehicle, time));
    }
}