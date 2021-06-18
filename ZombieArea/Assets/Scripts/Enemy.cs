using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {
    public GameObject player;
    bool enterted = false;
    public GameObject tank;
    public NavMeshAgent zoombiEnemyAgent;
    public Vector3 startPos;
    public int Healt;
    public Animator animator;
    bool isCoroutineExecuting = false;
    public AudioSource audio;
    public AudioClip runZombie;
    public bool noActive = false;
    public Text mainText;
    public Image mainImage;

    void Start() {
        if (mainImage != null) {
            mainImage.enabled = false;
        }
        audio = GetComponent<AudioSource>();
        if (tank != null) {
            startPos = tank.transform.position;
        }
        zoombiEnemyAgent = gameObject.GetComponent<NavMeshAgent>();
        zoombiEnemyAgent.SetDestination(startPos);
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (enterted) {
            zoombiEnemyAgent.SetDestination(player.transform.position);
        }
    }

    public void Damage() {
        if (Healt > 0) {
            Healt -= 10;
            enterted = true;
        }
        if (Healt <= 0) {
            Kill();
        }
    }
    public void Kill() {
        animator.Play("ZombieDying");
        zoombiEnemyAgent.Stop();
        audio.clip = runZombie;
        audio.Play();
        StartCoroutine(ExecuteAfterTime(3f, () => {
            gameObject.SetActive(false);
            noActive = true;
        }));
    }
    public void GameOver()
    {
        mainImage.enabled = true;
        mainText.text = "GameOver!";
        StartCoroutine(ExecuteAfterTime(2f, () => {
            SceneManager.LoadScene("MainMenu");
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
