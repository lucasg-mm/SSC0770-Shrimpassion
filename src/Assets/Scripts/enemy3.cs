using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy3 : MonoBehaviour
{
	private float jumpForce;
	public bool idle;
	public float damage;
	public float speed;
	private Rigidbody2D rb2d;
	private float direction;
	private Animator anim;
	public float health;
	private Vector3 vec;
	public Transform groundLocation;
	public LayerMask Ground;
	public bool grounded;
	
	private bool jumping;
	public Rigidbody2D seastar;
	public Collider2D colSeastar;
    public Collider2D colCatWalk;

	private bool isDamageable;
	public GameObject explosionEffect;

	// Use this for initialization
	void Start()
	{
		idle = true;
		damage = 0;
		speed = 0.2f;
		rb2d = GetComponent<Rigidbody2D>();
		//anim = GetComponent<Animator>();
		direction = 0;
		health = 1;
		isDamageable = false;
		jumping = false;
		jumpForce = 9.5f;
		vec = transform.localPosition;
	}

	// Update is called once per frame
	void Update()
	{
		vec = transform.localPosition;
		//jumping = false;
		//anim.SetBool("isIdle", idle);
		//rb2d.velocity = new Vector2(direction * speed, rb2d.velocity.y);
		//if (!idle)
		//{
			isDamageable = true;
		//}
		
		//Para a estrela do mar pular
		if (!jumping)     
        {
			grounded = Physics2D.OverlapCircle (groundLocation.position, 0.5f, Ground);
			//seastar.AddForce(Vector3.left * jumpForce, (ForceMode2D)ForceMode.Impulse);
			//anim.SetBool("jumping",jumping);
			
			if(grounded)
			{
				seastar.AddForce(Vector2.up * jumpForce, (ForceMode2D)ForceMode.Impulse);
				//seastar.AddForce(transform.left * 1f, (ForceMode2D)ForceMode.Impulse);
			}
            jumping = true; 
			Invoke("jumpCoolDown", 2f);
        }
		transform.localPosition = vec;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			if (idle)
			{
				idle = false;
				transform.gameObject.tag = "Enemy";
				damage = 1;

				if (other.transform.position.x < transform.position.x)
				{
					direction = -1;
				}
				else
				{
					direction = 1;
				}
			}
			else
			{
				if (LifeController.isDamageable)
				{
					if (other.transform.position.x < transform.position.x)
					{
						PlayerController.knockRight = true;
					}
					else
					{
						PlayerController.knockRight = false;
					}
					other.SendMessage("ApplyDamage", damage);
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Stop")
		{
			direction = -1 * direction;
		}
	}

	void ApplyDamage(float damage)
	{
		if (isDamageable && damage > 0)
		{
			StartCoroutine(FlashingDamage());
			health -= damage;

			if (health <= 0)
			{
				GetComponent<Renderer>().enabled = false;
				//Instantiate(explosionEffect, gameObject.transform.position, Quaternion.identity);
				Destroy(gameObject);
			}

			isDamageable = false;
			Invoke("ResetIsDamageable", 1.2f);
		}
	}

	void ResetIsDamageable()
	{
		isDamageable = true;
	}

	IEnumerator FlashingDamage()
	{
		int i = 0;
		while (i < 8)
		{
			GetComponent<Renderer>().enabled = false;
			yield return new WaitForSeconds(0.05f);
			GetComponent<Renderer>().enabled = true;
			yield return new WaitForSeconds(0.05f);
			i++;
		}
		GetComponent<Renderer>().enabled = true;
	}
	
	void jumpCoolDown()
	{
		jumping = false;
	}
}