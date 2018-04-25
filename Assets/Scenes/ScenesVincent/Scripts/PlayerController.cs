using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb2D;
	public GameObject GraphicsSlot;

	private void Awake() {
		rb2D = GetComponent<Rigidbody2D>();
		ySize = GetComponent<Collider2D>().bounds.extents.y;
	}

	private void Update() {
		CheckJump();
		CheckMovement();
	}

	private void FixedUpdate() {
		ModifyJumpForce();
		ApplyDrag();
	}

	#region Horizontal movement related code
	[Header("Horizontal movement variables")]
	[SerializeField]
	private float controllerDeadzone = 0.19f;
	[SerializeField]
	private float moveSpeed = 6f;
	[SerializeField]
	private float sprintSpeed = 10f;
	[SerializeField]
	private float drag = 0.65f;

	private bool isFacingRight = true;

	/// <summary>
	/// This function acts as the input check
	/// </summary>
	private void CheckMovement() {
		//Debug.Log(Input.GetAxisRaw("Horizontal"));
		//Debug.Log(Input.GetButton("Sprint"));
		float speedMod = (Input.GetButton("Sprint")) ? sprintSpeed : moveSpeed;
		if(Input.GetAxis("Horizontal") < -controllerDeadzone || Input.GetAxis("Horizontal") > controllerDeadzone) {
			MoveHorizontal(Input.GetAxis("Horizontal"), speedMod);
			FlipGraphics();
		}
		LimitSpeed(speedMod);
	}

	/// <summary>
	/// This is where the horizontal movement is applied
	/// </summary>
	/// <param name="weight"> The horizontal input axis </param>
	/// <param name="speedMod"> Depends on whether or not the "Sprint" key is pressed </param>
	private void MoveHorizontal(float weight, float speedMod) {
		rb2D.velocity += new Vector2(weight * speedMod, 0f);
	}

	/// <summary>
	/// Prevents constant addition of velocity to the player and limits the speed
	/// </summary>
	/// <param name="speedMod"> Depends on whether or not the "Sprint" key is pressed </param>
	private void LimitSpeed(float speedMod) {
		rb2D.velocity = new Vector2(Mathf.Clamp(rb2D.velocity.x, -speedMod, speedMod), rb2D.velocity.y);
	}

	/// <summary>
	/// Custom drag function to slow the playermovement down faster/slower.
	/// </summary>
	private void ApplyDrag() {
		rb2D.velocity += new Vector2(rb2D.velocity.x * -(drag), 0);
	}

	/// <summary>
	/// Flips the GraphicsSlot variable
	/// </summary>
	private void FlipGraphics() {
		if(isFacingRight && Mathf.Sign(rb2D.velocity.x) < 0) {
			GraphicsSlot.transform.localScale = new Vector3(-1 * GraphicsSlot.transform.localScale.x, GraphicsSlot.transform.localScale.y, GraphicsSlot.transform.localScale.z);
			isFacingRight = !isFacingRight;
		}else if(!isFacingRight && Mathf.Sign(rb2D.velocity.x) > 0) {
			GraphicsSlot.transform.localScale = new Vector3(Mathf.Abs(GraphicsSlot.transform.localScale.x), GraphicsSlot.transform.localScale.y, GraphicsSlot.transform.localScale.z);
			isFacingRight = !isFacingRight;
		}
	}

	#endregion

	#region Jump related code
	[Header("Jump variables")]
	[SerializeField]
	private float fallMultiplier = 2.5f;
	[SerializeField]
	private float lowJumpMultiplier = 2f;
	[SerializeField]
	private float jumpForce = 5f;
	private float ySize;

	///<summary>
	///This function is called in the FixedUpdate() to improve the way jumping feels.
	///</summary>
	private void ModifyJumpForce() {
		if(rb2D.velocity.y < 0) {
			rb2D.velocity += Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
		}
		else if(rb2D.velocity.y > 0 && !Input.GetButton("Jump")) {
			rb2D.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
		}
	}

	///<summary>
	///This function is called in the Update() and acts as an input check for jumping.
	///</summary>
	private void CheckJump() {
		//Debug.DrawRay(transform.position, -Vector2.up * (ySize + .1f), Color.red, 0f);
		//Debug.Log(IsGrounded());

		if(Input.GetButtonDown("Jump")) {
			if(IsGrounded()) {
				Jump();
			}
		}
	}

	///<summary>
	///This function adds vectival velocity to the held rigidbody
	///</summary>
	private void Jump() {
		rb2D.velocity += new Vector2(0f, jumpForce);
	}

	///<summary>
	///This functions returns true when the player object is on the ground.
	///</summary>
	private bool IsGrounded() {
		return Physics2D.Raycast(transform.position, -Vector3.up, ySize + .1f, LayerMask.GetMask("Level"));
	}
	#endregion
}
