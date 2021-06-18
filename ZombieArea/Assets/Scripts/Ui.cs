using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui : MonoBehaviour {
    public Text myText;
    public Player myPlayer;
    public Enemy enemyScript;
    public GirlEnemy girlScript;
    public HardEnemy hardScript;
    public BossEnemy bossScript;
    public Text health;
    public Slider slider;
    public Base basa;
    public Image akm;
    public Image pistol;
    public Image knife;
    public Text mainText;
    bool isCoroutineExecuting = false;

    void Start()
    {
        mainText.text = "";
        pistol.enabled = false;
        knife.enabled = false;
    }


    void Update () {
       
        health.text = enemyScript.Healt.ToString();
        float healthFloat = myPlayer.healt / 100;
        slider.value = healthFloat;
       
        if (enemyScript.noActive)
        {
           health.text =  girlScript.Healt.ToString();
        }
        if (girlScript.noGirlActive)
        {
            health.text = hardScript.Healt.ToString();
           
        }
        if (hardScript.hardActive)
        {
            health.text = bossScript.Healt.ToString();
        }

        if (myPlayer.AKM.active)
        {
            myText.text = basa.cartridges.ToString() + "/" + myPlayer.cartridgesAKM.ToString();
            akm.enabled = true;
            pistol.enabled = false;
            knife.enabled = false;
        }

        if (myPlayer.Pistol.active)
        {
            myText.text = basa.cartridges.ToString() + "/" +myPlayer.cartridgesPistol.ToString();
            pistol.enabled = true;
            akm.enabled = false;
            knife.enabled = false;
        }
        if (myPlayer.knife.active)
        {
            myText.text = "";
            knife.enabled = true;
            pistol.enabled = false;
            akm.enabled = false;
        }
    }

    IEnumerator ExecuteAfterTime(float time, Action task)
    {
        if (isCoroutineExecuting)
            yield break;
        isCoroutineExecuting = true;
        yield return new WaitForSeconds(time);
        task();
        isCoroutineExecuting = false;
    }
}
