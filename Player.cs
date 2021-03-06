using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{


    [SerializeField]
    private LayerMask groundLayer;
    public float jumpforce = 5.0f;
    public float playerSpeed = 2.0f;
    private bool grounded = false;
    private Rigidbody2D rigid;
    private bool resetJumpNeeded = false;
    private PlayerAnimation playerAnim;
    private SpriteRenderer playerSprite;
    private SpriteRenderer swordArcSprite;

    public int diamonds;

    public int Health { get; set; }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnimation>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();

        Health = 4;
    }

    void Update()
    {
        Movement();
        CheckGrounded();

        if ((CrossPlatformInputManager.GetButtonDown("Attack") || Input.GetKeyDown("q")) && grounded == true)
        {
            playerAnim.Attack();
        }

    }
    void Movement()
    {

        //float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        float horizontalInput;
        if (CrossPlatformInputManager.GetButton("LeftButton"))
        {
            horizontalInput = -1;
        }
        else if (CrossPlatformInputManager.GetButton("RightButton"))
        {
            horizontalInput = 1;
        }
        else
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }



        var speed = horizontalInput * playerSpeed;

        if (horizontalInput > 0)
        {
            FlipPlayer(false);
        }
        else if (horizontalInput < 0)
        {
            FlipPlayer(true);
        }

        if ((Input.GetKeyDown(KeyCode.Space)  || CrossPlatformInputManager.GetButtonDown("Jump")) && grounded == true)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpforce);
            grounded = false;
            resetJumpNeeded = true;
            StartCoroutine(ResetJumpNeededRoutine());
            playerAnim.Jump(true);
        }

        rigid.velocity = new Vector2(speed, rigid.velocity.y);

        playerAnim.Move(horizontalInput);
    }
    void CheckGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 2.5f, groundLayer.value);
        //Debug.DrawRay(transform.position, Vector2.down, Color.green);

        if (hitInfo.collider != null)
        {
            if (resetJumpNeeded == false)
            {
                playerAnim.Jump(false);
                grounded = true;
            }
        }
        else if (hitInfo.collider == null)
        {
            grounded = false;
        }
    }

    void Flip(bool faceLeft)
    {
        if (faceLeft == true)
        {
            playerSprite.flipX = true;
            swordArcSprite.flipY = true;

            Vector3 newPos = swordArcSprite.transform.localPosition;

            newPos.x = -1.01f;

            swordArcSprite.transform.localPosition = newPos;
        }
        else if (faceLeft == false)
        {
            playerSprite.flipX = false;
            swordArcSprite.flipY = false;

            Vector3 newPos = swordArcSprite.transform.localPosition;

            newPos.x = 1.01f;

            swordArcSprite.transform.localPosition = newPos;
        }
    }
    private void FlipPlayer(bool faceLeft)
    {
        if (faceLeft == true)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (faceLeft == false)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void Damage()
    {
        if(Health < 1)
        {
            return;
        }
        Debug.Log("Player Damage");
        Health--;

        UIManager.Instance.UpdateLives(Health);

        if(Health < 1)
        {
            playerAnim.Death();
        }
    }

    public void AddDiamonds(int amount)
    {
        diamonds += amount;
        UIManager.Instance.UpdateDiamondCount(diamonds);
    }

    IEnumerator ResetJumpNeededRoutine()
    {
        resetJumpNeeded = true;
        yield return new WaitForSeconds(0.5f);
        resetJumpNeeded = false;
    }
}
