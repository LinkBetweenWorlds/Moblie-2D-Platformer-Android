using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

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
    private bool reloadScene = false;
    private Vector3 startPosition;

    public int diamonds;

    public int Health { get; set; }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnimation>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();

        PlayerData data = SaveSystem.LoadPlayer();

        diamonds = data.diamonds;
        Health = 4;

        GameManager.Instance.HasFlameSword = data.HasFlameSword;
        GameManager.Instance.HasBootsOfFlight = data.HasBootsOfFlight;
        GameManager.Instance.HasKeyToCastle = data.HasKeytoCastle;

        if (GameManager.Instance.HasBootsOfFlight)
        {
            jumpforce = 12;
            playerSpeed = 10;
        }
        UIManager.Instance.UpdateDiamondCount(diamonds);

        startPosition.x = data.position[0];
        startPosition.y = data.position[1];
        startPosition.z = 0;

        transform.position = startPosition;
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
            StartCoroutine(TimetilResest());
        }
        if (reloadScene)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

    IEnumerator TimetilResest()
    {
        yield return new WaitForSeconds(6.0f);
        reloadScene = true;
    }
}
