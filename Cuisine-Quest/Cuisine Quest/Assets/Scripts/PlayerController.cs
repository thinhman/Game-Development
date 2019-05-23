using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Animator anim;
    private bool playerMoving;
    public bool playerCanMove = true;
    private Vector2 lastMove;
    private Rigidbody2D rb;
    public Vector2 DirectionFacing = new Vector2(0, -1);
    private Vector2 previousFacing = new Vector2(0, -1);
    private float H_Axis;
    private float V_Axis;

    public bool cameraTransition = false;
    public Vector2 cameraTransitionDirection;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.paused && !cameraTransition)
        {
            playerMoving = false;
            H_Axis = Input.GetAxisRaw("Horizontal");
            V_Axis = Input.GetAxisRaw("Vertical");

            if ((H_Axis > 0.5f || H_Axis < -0.5f) && (V_Axis > 0.5f || V_Axis < -0.5f) && playerCanMove)
            {

                if (Mathf.Abs(DirectionFacing.x) > 0)
                {
                    V_Axis = 0;
                }
                else
                {
                    H_Axis = 0;
                }

                if (H_Axis > 0.5f)
                {
                    DirectionFacing = new Vector2(1, 0);
                }
                else if (H_Axis < -0.5f)
                {
                    DirectionFacing = new Vector2(-1, 0);
                }
                else if (V_Axis > 0.5f)
                {
                    DirectionFacing = new Vector2(0, 1);
                }
                else if (V_Axis < -0.5f)
                {
                    DirectionFacing = new Vector2(0, -1);
                }
                rb.velocity = new Vector3(H_Axis * moveSpeed, V_Axis * moveSpeed, 0f);
                playerMoving = true;
                lastMove = new Vector2(H_Axis, V_Axis);
            }
            else if ((H_Axis > 0.5f || H_Axis < -0.5f) && playerCanMove)
            {
                rb.velocity = new Vector3(H_Axis * moveSpeed, 0f, 0f);
                playerMoving = true;
                lastMove = new Vector2(H_Axis, 0f);
                if (H_Axis > 0.5f)
                {
                    DirectionFacing = new Vector2(1, 0);
                }
                else
                {
                    DirectionFacing = new Vector2(-1, 0);
                }
            }
            else if ((V_Axis > 0.5f || V_Axis < -0.5f) && playerCanMove)
            {
                rb.velocity = new Vector3(0f, V_Axis * moveSpeed, 0f);
                playerMoving = true;
                lastMove = new Vector2(0f, V_Axis);

                if (V_Axis > 0.5f)
                {
                    DirectionFacing = new Vector2(0, 1);
                }
                else
                {
                    DirectionFacing = new Vector2(0, -1);
                }
            }
//             else if (cameraTransition && !playerCanMove)
//             {
//                 rb.velocity = cameraTransitionDirection * moveSpeed;
//                 playerMoving = false;
//             }
            else
            {
                rb.velocity = Vector2.zero;
                playerMoving = false;
                //DirectionFacing = new Vector2(0, 0);
            }

            if (previousFacing != DirectionFacing)
            {
                if(GetComponent<CiscoTesting>().CurrentWeapon)
                {
                    if (GetComponent<CiscoTesting>().CurrentWeapon.AttackAbort())
                    {
                        anim.SetFloat("MoveX", H_Axis);
                        anim.SetFloat("MoveY", V_Axis);

                        anim.SetFloat("LastMoveX", lastMove.x);
                        anim.SetFloat("LastMoveY", lastMove.y);

                        previousFacing = DirectionFacing;
                    }
                }
            }
            anim.SetBool("PlayerMoving", playerMoving);
        }
        else if (cameraTransition && !playerCanMove)
        {
            float transitionMoveSpeed = 0.1f;
            if (cameraTransitionDirection.y > 0) transitionMoveSpeed = 0.25f;
            else if (cameraTransitionDirection.y < 0) transitionMoveSpeed = 0.25f;
            rb.velocity = cameraTransitionDirection * moveSpeed * transitionMoveSpeed;
            Debug.Log("Player Auto Move at " + transitionMoveSpeed + " * moveSpeed");
            anim.SetFloat("MoveX", cameraTransitionDirection.x);
            anim.SetFloat("MoveY", cameraTransitionDirection.y);
            anim.SetFloat("LastMoveX", lastMove.x);
            anim.SetFloat("LastMoveY", lastMove.y);
        }
        //else
        //{
        //    Debug.Log("Stop moving");
        //    playerMoving = false;
        //    playerCanMove = false;

        //    anim.SetFloat("MoveX", H_Axis);
        //    anim.SetFloat("MoveY", V_Axis);

        //    anim.SetFloat("LastMoveX", lastMove.x);
        //    anim.SetFloat("LastMoveY", lastMove.y);

        //    rb.velocity = Vector2.zero;

        //    anim.SetBool("PlayerMoving", playerMoving);
        //}
    }
}           
