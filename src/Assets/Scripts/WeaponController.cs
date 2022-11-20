using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	private float beginPos;
	private float range = 20f;

	void Start () {
	
	}

	void Update () {
		if (transform.position.x - beginPos > range || beginPos - transform.position.x > range)
			gameObject.SetActive(false);
		
  				
	}
}
