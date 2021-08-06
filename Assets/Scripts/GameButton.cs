using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickStart()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void OnClickExit()
    {
        Application.Quit();
        Debug.Log("Button Click");
    }
    public void OnClickRule()
    {
        SceneManager.LoadScene("RuleScene");
    }
}
