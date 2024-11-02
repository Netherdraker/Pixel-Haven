using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool FacingLeft
    {
        get { return facingLeft; }
        set { facingLeft = value; }
    }

    public static Player Instance;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;

    private PlayerControls playerControls;

    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    [SerializeField] private TrailRenderer myTrailRenderer;

    private Vector2 movement;

    private bool facingLeft = false;
    private bool isDashing = false;


    private void Awake()
    {
        Instance = this;
        playerControls = new PlayerControls();

        rb = GetComponent<Rigidbody2D>();

        myAnimator = GetComponent<Animator>();

        mySpriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
    }

    private void OnEnable()
    {

        playerControls.Enable();

    }

    private void Update()
    {

        PlayerInput();

    }

    private void FixedUpdate()
    {
        AjustPlayerFacingPosition();
        Move();
    }

    private void PlayerInput()
    {

        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {

        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));

    }

    private void AjustPlayerFacingPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRenderer.flipX = true;
            FacingLeft = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
            FacingLeft = false;
        }
    }

    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            moveSpeed *= dashSpeed;
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRountine());
        }

    }

    private IEnumerator EndDashRountine()
    {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed /= dashSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }



}
