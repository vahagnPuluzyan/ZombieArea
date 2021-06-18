using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {
    public Enemy easy;
    public ParticleSystem dust;
    bool isCoroutineExecuting = false;

    void Start () {
	}

	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            dust.Play();
            StartCoroutine(ExecuteAfterTime(3f, () =>
            {
                easy.GameOver();
            }));     
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
