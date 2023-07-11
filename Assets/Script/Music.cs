using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource Contents;
    public AudioSource Fighting;

    public AudioSource Button;
    public AudioSource Shoot;
    public AudioSource Hit;
    public AudioSource Scream;
    public AudioSource Cloth;
    public AudioSource LWinner;
    public AudioSource RWinner;


    public void Music_Contents()
    {
        Contents.Play();
    }
    public void UnMusic_Contents()
    {
        Contents.Stop();
    }


    public void Music_Fighting()
    {
        Fighting.Play();
    }
    public void UnMusic_Fighting()
    {
        Fighting.Stop();
    }

    public void Music_ClickButton()
    {
        Button.Play();
    }

    public void Music_Shoot()
    {
        Shoot.Play();
    }

    

    public void Music_Hit()
    {
        Hit.Play();
    }

    public void Music_Scream()
    {
        Scream.Play();
    }

    public void Music_Cloth()
    {
        Cloth.Play();
    }

    
    public void Music_LWinner()
    {
        LWinner.Play();
    }
    public void UnMusic_LWinner()
    {
        LWinner.Stop();
    }
    public void Music_RWinner()
    {
        RWinner.Play();
    }
    public void UnMusic_RWinner()
    {
        RWinner.Stop();
    }


}
