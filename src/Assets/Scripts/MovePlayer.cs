using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    private Vector3 Vec;
    public Rigidbody2D Player;
    public Collider2D colPlayer;
    public Collider2D colCatWalk;
    public float force = 10;
    public float speed = 15;
    private bool stopJump = true;
    int x = 0;
    int y = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vec = transform.localPosition;
          
        Vec.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        
        if (Input.GetKeyDown(KeyCode.Space) && stopJump)
        {
            Player.AddForce(Vector3.up * force, (ForceMode2D)ForceMode.Impulse);
            stopJump = false;
        }

        if (colPlayer.IsTouching(colCatWalk))     
        {          
            stopJump = true; 
        }
        else
        {
            stopJump = false;
        }

        transform.localPosition = Vec;

    }
}
