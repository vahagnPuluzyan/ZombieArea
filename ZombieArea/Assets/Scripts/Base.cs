using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour {
    public int cartridges;
    public bool entertedBase =  false;
    public  Image press;
    public Text pressE;

    private void Start()
    {
        press.enabled = false;
        pressE.enabled = false;
    }
    void Update () {
        if (entertedBase) {
            if (Input.GetKeyDown(KeyCode.E)) {
                cartridges = 40;
                press.enabled = false;
                pressE.enabled = false;
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        entertedBase = true;

        press.enabled = true;
        pressE.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        entertedBase = false;
        press.enabled = false;
        pressE.enabled = false;
    }
}
