using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        
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
        SceneManager.LoadScene(1);
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
