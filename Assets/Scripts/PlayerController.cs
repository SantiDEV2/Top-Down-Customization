using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Variables")]
    private float speed = 4f;
    private Vector2 moveInput;

    [Header("PlayerEconomy")]
    public int coins = 100;

    [Header("References")]
    private Rigidbody2D rb;

    [Header("Animations")]
    private Animator animator;
    private const string hor = "Horizontal";
    private const string ver = "Vertical";
    private const string lasthor = "LastHorizontal";
    private const string lastver = "LastVertical";

    [Header("Bools")]
    private bool isInventoryOpen = false;
    private bool isStoreOpen = false;
    private bool isInStoreZone = false;
    private bool isInClosetZone = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ShowInventory();
        ShowStore();

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

        if (Input.GetKeyDown(KeyCode.Backspace)) {
            PlayerPrefs.DeleteAll();
        }
    }

    private void ShowStore()
    {
        if (isInStoreZone && Input.GetKeyDown(KeyCode.E))
        {
            isStoreOpen = !isStoreOpen;
            if (isStoreOpen)
            {
                WatchCamera();
                EventManager.TriggerEvent("StoreOpen", null);
            }
            else
            {
                EventManager.TriggerEvent("StoreClose", null);
            }
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            EventManager.TriggerEvent("SceneChange", null);
        }
        if (collision.CompareTag("StoreZone"))
        {
            isInStoreZone = true;
            EventManager.TriggerEvent("EntryStore", null);
        }
        if (collision.CompareTag("ClosetZone"))
        {
            EventManager.TriggerEvent("EntryCloset", null);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("StoreZone"))
        {
            isInStoreZone = false;
            EventManager.TriggerEvent("ExitStore", null);
        }
        if (collision.CompareTag("ClosetZone"))
        {
            isInClosetZone = false;
            EventManager.TriggerEvent("ExitCloset", null);
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
