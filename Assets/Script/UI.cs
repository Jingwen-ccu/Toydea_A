using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameMode GM;
    public Music music;
    public GameObject Tutorial;
    public GameObject Contents;

    public GameObject LBoard;
    public GameObject RBoard;

    public void ClickStart()
    {
        music.Music_ClickButton();
        GM.StartPlay();
        
    }

    public void ClickTutorial()
    {
        Debug.Log("TUTORIAL");
        music.Music_ClickButton();
        //GM.CloseAndOpen();
        Tutorial.SetActive(true);
        Contents.SetActive(false);

        LBoard.SetActive(false);
        RBoard.SetActive(false);
    }

    public void LeaveTutorial()
    {
        music.Music_ClickButton();
        //GM.CloseAndOpen();
        
        Tutorial.SetActive(false);
        Contents.SetActive(true);
        LBoard.SetActive(true);
        RBoard.SetActive(true);
    }

 


}
