using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        private bool m_Item;

        private bool hitStun;
        private bool movementEnabled;
        private Vector3 externalForce;

        public int controllerNumber;

        
        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            movementEnabled = false;

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();

        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = Input.GetButtonDown("XboxA_P" + controllerNumber);
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = Input.GetAxis("LeftJoystickX_P" + controllerNumber);
            float v = Input.GetAxis("LeftJoystickY_P" + controllerNumber);
            bool crouch = Input.GetButtonDown("XboxB_P" + controllerNumber);
            bool item = Input.GetButtonDown("XboxX_P" + controllerNumber);

            if (!hitStun)
            {
                if (movementEnabled)
                {
                    // calculate move direction to pass to character
                    if (m_Cam != null)
                    {
                        // calculate camera relative direction to move:
                        m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                        m_Move = v * m_CamForward + h * m_Cam.right;
                    }
                    else
                    {
                        // we use world-relative directions in the case of no main camera
                        m_Move = v * Vector3.forward + h * Vector3.right;
                    }
#if !MOBILE_INPUT
                    // walk speed multiplier
                    if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

                    // pass all parameters to the character control script
                    m_Character.Move(m_Move, crouch, m_Jump);

                    // check for item activation press
                    if (item)
                    {
                        gameObject.BroadcastMessage("UseItem");
                    }

                    item = false;
                    m_Jump = false;
                } else
                {
                    m_Character.Move(new Vector3(0,0,0), false, false);
                }

            } else
            {
                //move based on external force rather than player input
                m_Character.ApplyExternalForce(externalForce);
            }
        }

        //move player based on being hit by an external force
        public void playerHit(Vector3 force, float hitStun)
        {
            externalForce = force;
            StartCoroutine("EnableHitStun", hitStun);
        }

        //stop player from moving when hit
        IEnumerator EnableHitStun(float time)
        {
            hitStun = true;
            m_Character.Move(new Vector3(0, 0, 0), false, false);
            yield return new WaitForSeconds(time);
            hitStun = false;
        }

        public void toggleActiveControls(bool change)
        {
            movementEnabled = change;
        }
    }
}
