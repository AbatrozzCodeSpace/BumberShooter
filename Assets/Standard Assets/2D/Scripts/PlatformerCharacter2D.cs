using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
		private float currentHp = 1.0f;

        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

		private bool m_doubleJumped = false;
		private float m_defaultGravity;
		private bool m_JumpPressed;
		private bool m_IsGrappling;

		private GameObject umbrellaObject;
		private UmbrellaController umbrellaController;

		public bool onLadder;
		public bool onTopLadder;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
			umbrellaObject = GameObject.Find( "Umbrella" );
			umbrellaController = umbrellaObject.GetComponent<UmbrellaController>();
			////Debug.Log( umbrellaController != null );
			m_defaultGravity = m_Rigidbody2D.gravityScale;
			m_IsGrappling = false;
			onLadder = false;
			onTopLadder = false;

//			colliders = gameObject.GetComponents<Collider2D>();
//			//Debug.Log( colliders.Length );
        }


        private void FixedUpdate()
        {
            m_Grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
				if (colliders[i].gameObject != gameObject && colliders[i].gameObject.tag != "Ladder" && !onTopLadder ) {
					m_Grounded = true;
					m_doubleJumped = false;
					m_Rigidbody2D.gravityScale = m_defaultGravity;
				}
            }
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
        }

		public void setJumpPressed( bool jump ) {
			m_JumpPressed = jump;
		}

		public void setIsGrappling( bool grap ){
			m_IsGrappling = grap;
		}

        public void Move(float move, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }
			//Debug.Log ( "ON LADDER :" + onLadder + " GROUNDED :" + m_Grounded );
			if( onLadder && !m_Grounded ) {
				//Debug.Log ( "ON LADDER" );
				crouch = false;
			}
			
			// Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
				////Debug.Log ("GRAPPLING " + m_IsGrappling);
				if( !m_IsGrappling ) {
                	m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);
				}

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }


			if ( onLadder ) {
				if( ( !m_Grounded && Input.GetAxisRaw("Vertical") != 0.0f ) || onTopLadder ) {
					m_Rigidbody2D.gravityScale = 0.0f;
				}
				m_Rigidbody2D.velocity = new Vector2( m_Rigidbody2D.velocity.x , Input.GetAxisRaw("Vertical") * 10 );
			} else 

				if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
				////Debug.Log ("1");
            } else 

			// handle for double jump
			if (!m_Grounded && jump && !m_Anim.GetBool("Ground") && !m_doubleJumped)
			{
				// Add a vertical force to the player.
				m_Grounded = false;
				m_doubleJumped = true;
				m_Anim.SetBool("Ground", false);
				m_Rigidbody2D.velocity = new Vector2( m_Rigidbody2D.velocity.x,  0.0f );
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce * 0.5f));
				m_Rigidbody2D.gravityScale = m_defaultGravity * 0.3f;
				umbrellaController.setUmbrellaRotation( new Vector2( 0, -1 ) );
				umbrellaController.setOpen();

				//Debug.Log ( "Double Jump - G = " + m_Rigidbody2D.gravityScale ) ;

			} else if (!m_Grounded && !m_JumpPressed && !m_Anim.GetBool("Ground") && m_doubleJumped) {
				// case when released the jump button
				m_Rigidbody2D.gravityScale = m_defaultGravity;
				//Debug.Log ( "G = " + m_Rigidbody2D.gravityScale );
				umbrellaController.setClosed();
				Vector3 mouseVector = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				//print("mouse pos " +mouseVector);
				umbrellaController.setUmbrellaRotation(new Vector2(mouseVector.x-transform.position.x,-mouseVector.y+transform.position.y));

//				if( Input.GetAxis( "Um_X" ) == 0 && Input.GetAxis( "Um_Y" ) == 0 ) {
//					// we will set umbrella from d-pad
//					umbrellaController.setUmbrellaRotation( new Vector2( Input.GetAxis( "Horizontal" ), -Input.GetAxis( "Vertical" ) ) );
//				}
//				else {
//					umbrellaController.setUmbrellaRotation( new Vector2 (Input.GetAxis( "Um_X" ), Input.GetAxis( "Um_Y" ) ) );
//					
//				}
			} else {
				if( !m_doubleJumped ) {
					m_Rigidbody2D.gravityScale = m_defaultGravity;
				}
			}
			if ( !m_doubleJumped ) { // not jumping!
//				if ( Input.GetAxisRaw( "Guard" ) == 1 ) {
//					umbrellaController.setOpen();
//				} else {
//					umbrellaController.setClosed();
//				}
				if (Input.GetMouseButtonDown(1)){
					umbrellaController.setOpen();
				}
				if (Input.GetMouseButtonUp(1)){
					umbrellaController.setClosed();
				}
				Vector3 mouseVector = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				//print("mouse pos " +mouseVector);
				umbrellaController.setUmbrellaRotation(new Vector2(mouseVector.x-transform.position.x,-mouseVector.y+transform.position.y));


//				// if there is no input from right analog
//				if( Input.GetAxis( "Um_X" ) == 0 && Input.GetAxis( "Um_Y" ) == 0 ) {
//					// we will set umbrella from d-pad
//					umbrellaController.setUmbrellaRotation( new Vector2( Input.GetAxis( "Horizontal" ), -Input.GetAxis( "Vertical" ) ) );
//				}
//				else {
//					umbrellaController.setUmbrellaRotation( new Vector2 (Input.GetAxis( "Um_X" ), Input.GetAxis( "Um_Y" ) ) );
//
//				}

			}

			// ladder

			
			////Debug.Log ( "GRAVITY :" + m_Rigidbody2D.gravityScale );
		}

		
		private void Flip()
		{
			// Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }


		public float getCurrentHealth() {
			return currentHp;
		}
    }
}
