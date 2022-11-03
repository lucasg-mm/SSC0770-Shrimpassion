using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public Transform groundCheck;
	public float speed = 2.4f;
	public float jumpForce = 200;
	public LayerMask whatIsGround;
	public GameObject weaponPrefab;
	public Transform pointWeapon;
	public float weaponSpeed = 300;

	[HideInInspector]
	public bool lookingRight = true;

	private Rigidbody2D rb2d;
	public bool isGrounded = false;
	private bool jump = false;

    private bool canDash = true;
    private bool isDashing = false;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCoolDown = 1f;

    [SerializeField] private TrailRenderer tr;
	//[HideInInspector]
	private bool isAttacking = false;


	//Animator
	private Animator anim;

	void Start () {
		anim=GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	void Update () {
        if(isDashing){
            return;
        }

		inputCheck ();
		move ();

        if(Input.GetKeyDown(KeyCode.Z)){
            StartCoroutine(Dash());
        }
	}

	void inputCheck (){

		if (Input.GetButtonDown("Jump") && isGrounded){
			jump = true;
		}

		if (Input.GetKeyDown(KeyCode.X) && !isAttacking){
			isAttacking = true;
		}
	}

	void move(){
		
		float horizontalForceButton = Input.GetAxis ("Horizontal");
		rb2d.velocity = new Vector2 (horizontalForceButton * speed, rb2d.velocity.y);
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, 0.15f, whatIsGround);
		anim.SetFloat("speedHorizontal",Mathf.Abs(horizontalForceButton));
		anim.SetBool("grounded",isGrounded);
		if ((horizontalForceButton > 0 && !lookingRight) || (horizontalForceButton < 0 && lookingRight))
			Flip ();

		if (jump) {
			rb2d.AddForce(new Vector2(0, jumpForce));
			jump = false;
		}

		if (isAttacking) {
			GameObject bullet = ObjectPool.SharedInstance.GetPooledObject(); 
 			

			if (lookingRight){
				
				if (bullet != null) {
				bullet.transform.position = pointWeapon.transform.position;
				bullet.transform.rotation = pointWeapon.transform.rotation;
				bullet.SetActive(true);
				bullet.GetComponent<Rigidbody2D>().AddForce(Vector3.right * weaponSpeed);
  			}
			}else{
				
				if (bullet != null) {
				bullet.transform.position = pointWeapon.transform.position;
				bullet.transform.rotation = pointWeapon.transform.rotation;
				bullet.SetActive(true);
				bullet.GetComponent<Rigidbody2D>().AddForce(Vector3.left * weaponSpeed);
  			}
			}
		
		}

		isAttacking = false;
	}

	void Flip(){
		lookingRight = !lookingRight;
		Vector3 myScale = transform.localScale;
		myScale.x *= -1;
		transform.localScale = myScale;
	}

    private IEnumerator Dash(){
        canDash = false;
        isDashing = true;
        float ogGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0f;
        rb2d.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);

        tr.emitting = false;
        rb2d.gravityScale = ogGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;
    }
}
