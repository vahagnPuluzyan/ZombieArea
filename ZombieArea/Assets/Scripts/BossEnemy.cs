using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossEnemy : MonoBehaviour {

    public GameObject player;
    bool enterted = false;
    public GameObject tank;
    public NavMeshAgent bossEnemyAgent;
    public Vector3 startPos;
    int kills = 0;
    public int Healt;
    public Animator animator;
    bool isCoroutineExecuting = false;
    public AudioSource bossdAudio;
    public AudioClip bossRunAudio;
    public HardEnemy hardEnemy;
    int a = 0;
    public Text mainText;
    public Image mainImage;

    void Start()
    {
        if (tank != null)
        {
            startPos = tank.transform.position;
        }
        bossEnemyAgent = gameObject.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        bossdAudio = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (hardEnemy.hardActive){
            if (a == 0) {
                bossEnemyAgent.SetDestination(startPos);
                a += 1;
            }
            if (enterted)
            {
                bossEnemyAgent.SetDestination(player.transform.position);
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
        bossEnemyAgent.Stop();
        bossdAudio.clip = bossRunAudio;
        bossdAudio.Play();
        mainText.text = "You Win!";
        mainImage.enabled = true;
        StartCoroutine(ExecuteAfterTime(3f, () => {
            SceneManager.LoadScene("MainMenu");
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