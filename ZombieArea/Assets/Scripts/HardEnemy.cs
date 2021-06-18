using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HardEnemy : MonoBehaviour
{
    public GameObject player;
    bool enterted = false;
    public GameObject tank;
    public NavMeshAgent hardEnemyAgent;
    public Vector3 startPos;
    int kills = 0;
    public int Healt;
    public Animator animator;
    bool isCoroutineExecuting = false;
    public Enemy easyEnemy;
    public AudioSource hardAudio;
    public AudioClip hardRunAudio;
    public GirlEnemy girlEnemy;
    public bool hardActive = false;
    int a = 0;
    void Start()
    {
        if (tank != null)
        {
            startPos = tank.transform.position;
        }
        hardEnemyAgent = gameObject.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        hardAudio = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (girlEnemy.noGirlActive)
        {
            if (a==0) {
                hardEnemyAgent.SetDestination(startPos);
                a += 1;
            }
            if (enterted)
            {
                hardEnemyAgent.SetDestination(player.transform.position);
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
        hardEnemyAgent.Stop();
        hardAudio.clip = hardRunAudio;
        hardAudio.Play();
        StartCoroutine(ExecuteAfterTime(3f, () => {
            hardActive = true;
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