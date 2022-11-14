using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyColission : MonoBehaviour
{

	public float damage = 1;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			other.SendMessage("ApplyDamage", damage);
			damage = 0;
			gameObject.SetActive(false);
		}
	}
}