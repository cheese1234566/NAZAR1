using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float speed;
    public float turningSpeed;
    public bool isPaused;
    public bool isGotKey;
    public bool isActivePlayer = true;

    private Vector3 movement;
    private Quaternion rotation = Quaternion.identity;

    public AudioSource footstep;
    public GameObject pauseText;


    private Menu textMenu;
    private Animator anim;
    private Animator keyAnim;
    private Rigidbody rb;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        keyAnim = FindObjectOfType<UgotKey>().GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        /*textMenu = FindObjectOfType<Menu>().GetComponent<Menu>();*/
        pauseText.SetActive(false);
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (isActivePlayer)
        {
            movement.Set(horizontal, 0, vertical);
            movement.Normalize();
        }
        

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isMoving = hasHorizontalInput || hasVerticalInput;

        anim.SetBool("IsWalking", isMoving);

        if (!isMoving)
        {
          footstep.Play();  
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                textMenu.Resume();
            } else
            {
                textMenu.Pause();
            }
        }


        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, turningSpeed * Time.deltaTime, 0);

        rotation = Quaternion.LookRotation(desiredForward);

        if (isGotKey)
        {
            keyAnim.SetBool("IsKeyPickup", false);
        }
    }

    private void OnAnimatorMove()
    {
        rb.MovePosition(rb.position + movement * speed * anim.deltaPosition.magnitude);
        rb.MoveRotation(rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Key key))
        {
            Destroy(other.gameObject);
            keyAnim.SetBool("IsKeyPickup", true);
            isGotKey = true;
        }
    }
}
