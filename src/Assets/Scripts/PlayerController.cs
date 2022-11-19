using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public Transform groundCheck;
	public float speed = 8f;
	public float jumpForce = 800;
	public LayerMask whatIsGround;
	public GameObject weaponPrefab;
	public Transform pointWeapon;
	public float weaponSpeed = 800;

	[HideInInspector]
	public bool lookingRight = true;

	private Rigidbody2D rb2d;
	public bool isGrounded = false;
	private bool jump = false;

    private bool canDash = true;
    private bool isDashing = false;
    private float dashingPower = 30f;
    private float dashingTime = 0.25f;
    private float dashingCoolDown = 1f;
	private float horizontalForceButton = 0f;

    [SerializeField] private TrailRenderer tr;
	//[HideInInspector]
	private bool isAttacking = false;
	private bool isAttackingMelee = false;

	//knockback
	public static bool knockRight = true;
	public static float knockForce = 0f;

	//Animator
	private Animator anim;

	private enum MovementState { idle, running, jumping, shooting_standing, shooting_jumping };

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
		if (Input.GetKeyDown(KeyCode.C) && !isAttackingMelee)
		{
			isAttackingMelee = true;
		}
	}

	private void updateAnimationState(){
		MovementState state;

		if(horizontalForceButton > 0f){
			state = MovementState.running;
		}
		else if(horizontalForceButton < 0f){
			state = MovementState.running;
		}
		else{
			state = MovementState.idle;
		}

		if(rb2d.velocity.y > .1f && !isGrounded){
			state = MovementState.jumping;
		}

		if (isAttacking)
		{
			state = MovementState.shooting_standing;
		}

		if (isAttacking && rb2d.velocity.y > .1f && !isGrounded)
		{
			state = MovementState.shooting_jumping;
		}
		//TODO animacao melee

		anim.SetInteger("player_state", (int) state);
	}

	void move(){
		horizontalForceButton = Input.GetAxis ("Horizontal");

		rb2d.velocity = new Vector2 (horizontalForceButton * speed, rb2d.velocity.y);
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, 0.15f, whatIsGround);
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
		if (isAttackingMelee)
		{
			GameObject punch = ObjectPool.SharedInstance.GetPooledObject();


			if (lookingRight)
			{

				if (punch != null)
				{
					punch.transform.position = pointWeapon.transform.position;
					punch.transform.rotation = pointWeapon.transform.rotation;
					punch.SetActive(true);
					
				}
			}
			else
			{

				if (punch != null)
				{
					punch.transform.position = pointWeapon.transform.position;
					punch.transform.rotation = pointWeapon.transform.rotation;
					punch.SetActive(true);
					
				}
			}

				//punch.SetActive(false);  TODO retirar punch da tela

		}



		if (knockForce <= 0)
		{
			rb2d.velocity = new Vector2(horizontalForceButton * speed, rb2d.velocity.y);
		}
		else
		{
			if (knockRight)
			{
				rb2d.velocity = new Vector2(-knockForce * speed, rb2d.velocity.y);
			}
			else
			{
				rb2d.velocity = new Vector2(knockForce * speed, rb2d.velocity.y);
			}
			knockForce -= Time.deltaTime;
		}

		updateAnimationState();
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
