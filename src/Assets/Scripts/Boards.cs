using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boards : MonoBehaviour
{

    public Transform  poPlayer;
    public Transform  leftLimit;
    public Transform righttLimit;
	public Transform fallLimit;
	public Transform ceilingLimit;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (poPlayer.position.x >= righttLimit.position.x || poPlayer.position.x < leftLimit.position.x)
        {

            poPlayer.transform.position = new Vector3(leftLimit.position.x, poPlayer.position.y, poPlayer.position.z);

        }              

		if (poPlayer.position.y < fallLimit.position.y || poPlayer.position.y > ceilingLimit.position.y)
        {

            poPlayer.transform.position = new Vector3(leftLimit.position.x, ceilingLimit.position.y, poPlayer.position.z);

        }     

    }
}
