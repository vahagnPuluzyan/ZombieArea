using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GirlEnemy : MonoBehaviour{

    public GameObject player;
    bool enterted = false;
    public GameObject tank;
    public NavMeshAgent girlEnemyAgent;
    public Vector3 startPos;
    int kills = 0;
    public int Healt;
    public Animator animator;
    bool isCoroutineExecuting = false;
    public Enemy easyEnemy;
    public AudioSource girlAudio;
    public AudioClip girlRunAudio;
    public bool noGirlActive;
    int a = 0;
    void Start()
    {
        startPos = tank.transform.position;   
        girlEnemyAgent = gameObject.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        girlAudio = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (easyEnemy.noActive)
        {
            if (a  == 0) {
                a += 1;
                girlEnemyAgent.SetDestination(startPos);
            }
            if (enterted)
            {
                girlEnemyAgent.SetDestination(player.transform.position);
            }
        }
    } 

    public void Damage()
    {
        if (Healt > 0)
        {
            Healt -= 10;
            enterted = true;
        }
        if (Healt <= 0)
        {
            Kill();
        }
    }
    public void Kill()
    {
        kills += 1;
        animator.Play("ZombieDying");
        girlAudio.clip = girlRunAudio;
        girlAudio.Play();
        girlEnemyAgent.Stop();
        StartCoroutine(ExecuteAfterTime(3f, () => {
            noGirlActive = true;
            gameObject.SetActive(false);
        }));
    }

    IEnumerator ExecuteAfterTime(float time, Action task)
    {
        if (isCoroutineExecuting)
            yield break;
        isCoroutineExecuting = false;
        yield return new WaitForSeconds(time);
        task();
        isCoroutineExecuting = true;
    }
}

