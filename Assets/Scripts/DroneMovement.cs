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
    private Transform self;

    Interactable interactable;

    bool isMoving;

    float maxSpeed = 25;

    private float mouseSensitivity = 100f;

    private float xRotation = 0f;

    private void Start()
    {
        this.isMoving = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        Moving(this.isMoving);
    }

    public void ShouldMove(bool moving)
    {
        this.isMoving = moving;
    }

    public void CheckVelocity()
    {
        if(rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }

    public void Moving(bool shouldMove) {
        CheckVelocity();

        if (shouldMove) {
            //if(OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight)){
            if(Input.GetKey("d")){
                rb.AddForce(500 * maxSpeed * Time.deltaTime, 0, 0);
            }

            //if(OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft)){
            if(Input.GetKey("a")){
                rb.AddForce(-500 * maxSpeed * Time.deltaTime, 0, 0);
            }

            //if(OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp)){
            if(Input.GetKey("w")){
                rb.AddForce(0, 0, 500 * maxSpeed * Time.deltaTime);
            }

            //if(OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown)){
            if(Input.GetKey("s")){
                rb.AddForce(0, 0, -500 * maxSpeed * Time.deltaTime);
            }

            //Rotate();
        }
    }

    /*public void Rotate()
    {
        float x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        Debug.Log(x +"==="+ y);
        xRotation -= y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        self.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        self.Rotate(x, y,0);

    }*/


    private void OnTriggerStay(Collider other)
    {
        if (other != null) {
            interactable = other.GetComponentInParent<Interactable>();
            EventSystem.current.DialogueTrigerEnter(interactable.Id);
            if(Input.GetKeyDown("e")){
                this.isMoving = false;
                rb.velocity = Vector3.zero;
                EventSystem.current.DialogueInteracted(interactable.Id); 
            }
        }
    }

    
    private void OnTriggerExit(Collider other)
    {

        if (other != null) {
            interactable = other.GetComponentInParent<Interactable>();
            EventSystem.current.DialogueTrigerExit(interactable.Id);
        }
        interactable = null;
        
    }

}