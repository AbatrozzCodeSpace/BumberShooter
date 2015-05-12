using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
		public bool canControl;

        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
		private bool m_JumpPressed;
		private bool m_skill;
		private bool m_skillPressed;
		private UmbrellaController m_umbrellaController;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
			canControl = true;
			m_umbrellaController = GetComponentInChildren<UmbrellaController>();
        }


        private void Update()
        {	
			if( canControl ) {
				m_JumpPressed = CrossPlatformInputManager.GetButton("Jump");
				m_skillPressed = CrossPlatformInputManager.GetButton("Skill");
	            if (!m_Jump)
	            {
	                // Read the jump input in Update so button presses aren't missed.
	                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");

	            }
				if (!m_skill) {
					m_skill = CrossPlatformInputManager.GetButtonDown("Skill");
				}
			}
        }


        private void FixedUpdate()
        {
            // Read the inputs.
			if( canControl ) {
	            bool crouch = Input.GetKey(KeyCode.LeftControl) || CrossPlatformInputManager.GetAxis("Vertical") < 0;
	            float h = CrossPlatformInputManager.GetAxis("Horizontal");
	            // Pass all parameters to the character control script.
				m_Character.setJumpPressed( m_JumpPressed );
				m_Character.setIsGrappling( transform.GetComponentInChildren<Grappler>().state == Grappler.GrappleState.HOOK );
	            m_Character.Move(h, crouch, m_Jump);
	            m_Jump = false;

				m_umbrellaController.setSkillPressed( m_skillPressed );
				if ( m_skill ) {
					//m_umbrellaController.useSkill();

					m_skill = false;
				}
			}
        }
    }
}
