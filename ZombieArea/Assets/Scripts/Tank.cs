using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour {
    public Enemy currentEnemy;

	void Start () {
		
	}
	
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ontrigger");
        if (other.tag == "Enemy") {
         currentEnemy.animator.Play("ZombieAttack");
        }
    }
}
