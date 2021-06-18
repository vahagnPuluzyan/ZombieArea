using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public float healt = 100f;
	public Transform camera;
	RaycastHit hit;
    public int cartridgesAKM = 30;
    public int cartridgesPistol = 7;
    AudioSource audioSource;
    public AudioClip fireClipAKM;
    public AudioClip reloadClipAKM;
    public AudioClip fireClipPistol;
    public AudioClip reloadClipPistol;
    public AudioClip changeWeapone;
    public ParticleSystem dust;
    public GameObject Pistol;
    public GameObject AKM;
    public GameObject knife;
    UnityStandardAssets.Characters.FirstPerson.FirstPersonController contoller;
    bool isCoroutineExecuting = false;
    public Enemy currentEnemy;
    public HardEnemy currentEnemy2;
    public GirlEnemy currentEnemy1;
    public BossEnemy currentEnemy3;
    public Image blood;
    public Base basa;

    void Start(){
        audioSource = GetComponent<AudioSource>();
        Pistol.SetActive(false);
        knife.SetActive(false);
        contoller = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        blood.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AKM.SetActive(true);
            Pistol.SetActive(false);
            knife.SetActive(false);
            ChangeWeapone();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Pistol.SetActive(true);
            AKM.SetActive(false);
            knife.SetActive(false);
            ChangeWeapone();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Pistol.SetActive(false);
            AKM.SetActive(false);
            knife.SetActive(true);
            ChangeWeapone();
        }
        if (AKM.active)
        {
            if (Input.GetKeyDown(KeyCode.R) && cartridgesAKM != 30)
            {
                ReloadeAKM();
            }
            if (cartridgesAKM > 0)
            {

                if (Input.GetMouseButton(0))
                {
                    StartCoroutine(ExecuteAfterTime(0.1f, () =>{
                   cartridgesAKM -= 1;
                        audioSource.clip = fireClipAKM;
                        audioSource.Play();
                        dust.Play();
                        contoller.m_MouseLook.changeOffest(UnityEngine.Random.Range(0, 1.5f), UnityEngine.Random.Range(0, 1.5f));
                        Attack();
                    }));
                }
                else
                {
                    dust.Stop();
                }
                }
                else
                {
                    ReloadeAKM();
                }
        }
        if (Pistol.active)
        {
            if (Input.GetKeyDown(KeyCode.R) && cartridgesPistol != 7)
        {
                ReloadePistol();
        }
            if (cartridgesPistol > 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    cartridgesPistol -= 1;
                    audioSource.clip = fireClipPistol;
                    audioSource.Play();
                    dust.Play();
                    contoller.m_MouseLook.changeOffest(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
                    Attack();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    dust.Stop();
                }
            }
            else
            {
                ReloadePistol();
            }
        }
    }
    void ReloadePistol()
    {
        if (basa.cartridges > 0)
        {
            basa.cartridges -= 7;
            basa.cartridges += cartridgesPistol;
            cartridgesPistol = 7;
            audioSource.clip = reloadClipPistol;
            audioSource.Play();
        }
        else
        {
            basa.cartridges = 0;
        }
    }
    void ReloadeAKM()
    {
        if (basa.cartridges > 0)
        {
            basa.cartridges -= 30;
            basa.cartridges += cartridgesAKM;
            cartridgesAKM = 30;
            audioSource.clip = reloadClipAKM;
            audioSource.Play();
        }
        else
        {
            basa.cartridges = 0;
        }
    }
    void ChangeWeapone()
    {
        audioSource.clip = changeWeapone;
        audioSource.Play();
    }
    void Attack()
    {
        Debug.DrawRay(camera.position, camera.TransformDirection(Vector3.forward) * 30, Color.red);
        if (Physics.Raycast(camera.position, camera.TransformDirection(Vector3.forward) * 30, out hit))
        {
            if (hit.collider.name == "Easy")
            {
                currentEnemy.Damage();
            }
            if (hit.collider.name == "Girl")
            {
                currentEnemy1.Damage();
            }
            if (hit.collider.name == "Hard")
            {
                currentEnemy2.Damage();
            }
            if (hit.collider.name == "Boss")
            {
                currentEnemy3.Damage();
            }
        }
    }
    private void Damage()
    {
    healt -= 30f;
     if (healt <= 0f)
     {
      Kill();
     }
    }
    private void Kill()
   {
    Destroy(gameObject);
   }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && other.name == "Easy")
        {
            currentEnemy.audio.clip = currentEnemy.runZombie;
            currentEnemy.audio.Play();
            currentEnemy.zoombiEnemyAgent.SetDestination(currentEnemy.transform.position);
            currentEnemy.animator.Play("ZombieAttack");
            blood.enabled = true;
            StartCoroutine(ExecuteAfterTime(2f, () =>
            {
                
                currentEnemy.animator.Play("ZombieRun");
                Damage();
                currentEnemy.zoombiEnemyAgent.SetDestination(currentEnemy.startPos);
            }));
        }
        if (other.tag == "Enemy" && other.name == "Girl")
        {
            currentEnemy1.girlAudio.clip = currentEnemy1.girlRunAudio;
            currentEnemy1.girlAudio.Play();
            currentEnemy1.girlEnemyAgent.SetDestination(currentEnemy1.transform.position);
            currentEnemy1.animator.Play("ZombieAttack");
            blood.enabled = true;
            StartCoroutine(ExecuteAfterTime(2f, () =>
            {
              currentEnemy1.animator.Play("ZombieRun");
               Damage();
               currentEnemy1.girlEnemyAgent.SetDestination(currentEnemy1.startPos);
          }));
        }
        if (other.tag == "Enemy" && other.name == "Hard")
        {
            currentEnemy2.hardAudio.clip = currentEnemy2.hardRunAudio;
            currentEnemy2.hardAudio.Play();
            currentEnemy2.hardEnemyAgent.SetDestination(currentEnemy2.transform.position);
            currentEnemy2.animator.Play("ZombieAttack");
            blood.enabled = true;
            StartCoroutine(ExecuteAfterTime(2f, () =>
            {
                currentEnemy2.animator.Play("ZombieRun");
                Damage();
                currentEnemy2.hardEnemyAgent.SetDestination(currentEnemy2.startPos);
            }));
        }
        if (other.tag == "Enemy" && other.name == "Boss")
        {
            currentEnemy3.bossdAudio.clip = currentEnemy3.bossRunAudio;
            currentEnemy3.bossdAudio.Play();
            currentEnemy3.bossEnemyAgent.SetDestination(currentEnemy3.transform.position);
            currentEnemy3.animator.Play("ZombieAttack");
            blood.enabled = true;
            StartCoroutine(ExecuteAfterTime(2f, () =>
            {
                currentEnemy3.animator.Play("ZombieRun");
                Damage();
                currentEnemy3.bossEnemyAgent.SetDestination(currentEnemy3.startPos);
            }));
        }
    }
    private void OnTriggerExit(Collider other)
    {
     blood.enabled = false;
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
