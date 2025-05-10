using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Variables")]
    [SerializeField] private float speed = 4f;
    private Vector2 moveInput;

    [Header("References")]
    private Rigidbody2D rb;

    [Header("Animations")]
    private Animator animator;
    private const string hor = "Horizontal";
    private const string ver = "Vertical";
    private const string lasthor = "LastHorizontal";
    private const string lastver = "LastVertical";
        
    private bool isInventoryOpen = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ShowInventory();
        if (GameController.isGamePaused) return;

        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput.Normalize();

        animator.SetFloat(hor, moveInput.x);
        animator.SetFloat(ver, moveInput.y);

        if (moveInput != Vector2.zero)
        {
            animator.SetFloat(lasthor, moveInput.x);
            animator.SetFloat(lastver, moveInput.y);
        }

        Debug.Log(moveInput);
    }

    private void ShowInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isInventoryOpen = !isInventoryOpen;
            if (isInventoryOpen)
            {
                WatchCamera();
                EventManager.TriggerEvent("InventoryOpen", null);
            }
            else
            {
                EventManager.TriggerEvent("InventoryClose", null);
            }
        }
    }

    private void WatchCamera()
    {
        animator.SetFloat(hor, 0);
        animator.SetFloat(ver, 0);
        animator.SetFloat(lasthor, 0);
        animator.SetFloat(lastver, -1);
        rb.linearVelocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (GameController.isGamePaused) return;
        rb.linearVelocity = moveInput * speed;
    }


}
