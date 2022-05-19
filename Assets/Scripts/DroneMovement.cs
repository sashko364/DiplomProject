using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading;

public class DroneMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Transform camera;

    Interactable interactable;

    bool isMoving;

    float maxSpeed = 35;

    float speed = 24;

    float dragSpeed = 5;

    Vector3 direction;

    float h = 0;
    float v = 0;


    private void Start()
    {
        this.isMoving = true;
        rb.freezeRotation = true;
    }

    private void Update()
    {
        if (this.isMoving)
        {
            GetInput();
        }
        rb.drag = dragSpeed;
    }

    private void FixedUpdate()
    {
        if (this.isMoving)
        {
            MovePlayer();
        }
    }

    public void ShouldMove(bool moving)
    {
        this.isMoving = moving;
    }

    public void CheckVelocity()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }

    public void Moving(bool shouldMove)
    {
        CheckVelocity();
    }

    private void GetInput()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
    }

    public void MovePlayer()
    {
        direction = camera.forward * v + camera.right * h;
        rb.AddForce(direction.normalized * speed * 10, ForceMode.Force);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other != null)
        {
            interactable = other.GetComponentInParent<Interactable>();
            EventSystem.current.DialogueTrigerEnter(interactable.Id);
            if (Input.GetKeyDown("e"))
            {
                this.isMoving = false;
                rb.velocity = Vector3.zero;
                
                EventSystem.current.DialogueInteracted(interactable.Id);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other != null)
        {
            interactable = other.GetComponentInParent<Interactable>();
            EventSystem.current.DialogueTrigerExit(interactable.Id);
        }
        interactable = null;

    }

}