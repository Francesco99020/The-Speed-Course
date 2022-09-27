using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Button SUVBtn;
    [SerializeField] Button TruckBtn;
    [SerializeField] Button TankBtn;
    [SerializeField] Button StartBtn;
    [SerializeField] Button ExitBtn;
    [SerializeField] Button HighScoreBtn;

    [SerializeField] TextMeshProUGUI NamePrompt;

    // Start is called before the first frame update
    void Start()
    {
        //locks mouse to scene
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SUV()
    {
        SceneManagerScript.instance.playerChoice = 0;
    }

    public void Truck()
    {
        SceneManagerScript.instance.playerChoice = 1;
    }

    public void Tank()
    {
        SceneManagerScript.instance.playerChoice = 2;
    }

    public void StartGame()
    {
        if(SceneManagerScript.instance.playerName == "")
        {
            NamePrompt.color = Color.red;
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void EnterName(string name)
    {
        SceneManagerScript.instance.playerName = name;
    }

    public void HighScore()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
