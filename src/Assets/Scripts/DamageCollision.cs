using UnityEngine;
using System.Collections;

public class DamageCollision : MonoBehaviour {

	public float damage = 1;

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			other.SendMessage ("ApplyDamage",damage);
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.CompareTag("Player")){
			other.SendMessage ("ApplyDamage",damage);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.CompareTag("Player")){
			other.SendMessage ("ApplyDamage",damage);
		}
	}
}