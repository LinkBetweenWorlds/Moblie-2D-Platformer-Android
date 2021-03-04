using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{

    [SerializeField]
    private float _jumpforce = 5.0f;
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private float playerSpeed = 2.0f;
    private bool _grounded = false;
    private Rigidbody2D _rigid;
    private bool resetJumpNeeded = false;
    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;

    public int diamonds;

    public int Health { get; set; }

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();

        Health = 4;
    }

    void Update()
    {
        Movement();
        CheckGrounded();

        if ((CrossPlatformInputManager.GetButtonDown("A_Button") || Input.GetKeyDown(KeyCode.Space)) && _grounded == true)
        {
            _playerAnim.Attack();
        }

    }
    void Movement()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        float horizontalInput;
        if (CrossPlatformInputManager.GetButton("Left_Button"))
        {
            horizontalInput = -1;
        }
        else if (CrossPlatformInputManager.GetButton("Right_Button"))
        {
            horizontalInput = 1;
        }
        else
        {
            horizontalInput = 0;
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

        if ((Input.GetKeyDown(KeyCode.Space)  || CrossPlatformInputManager.GetButtonDown("B_Button")) && _grounded == true)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpforce);
            _grounded = false;
            resetJumpNeeded = true;
            StartCoroutine(ResetJumpNeededRoutine());
            _playerAnim.Jump(true);
        }

        _rigid.velocity = new Vector2(speed, _rigid.velocity.y);

        _playerAnim.Move(horizontalInput);
    }
    void CheckGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, _groundLayer.value);

        if (hitInfo.collider != null)
        {
            if (resetJumpNeeded == false)
            {
                _playerAnim.Jump(false);
                _grounded = true;
            }
        }
        else if (hitInfo.collider == null)
        {
            _grounded = false;
        }
    }

    void Flip(bool faceLeft)
    {
        if (faceLeft == true)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newPos = _swordArcSprite.transform.localPosition;

            newPos.x = -1.01f;

            _swordArcSprite.transform.localPosition = newPos;
        }
        else if (faceLeft == false)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newPos = _swordArcSprite.transform.localPosition;

            newPos.x = 1.01f;

            _swordArcSprite.transform.localPosition = newPos;
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
            _playerAnim.Death();
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
        yield return new WaitForSeconds(0.1f);
        resetJumpNeeded = false;
    }
}
