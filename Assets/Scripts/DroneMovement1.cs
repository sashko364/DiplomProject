using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading;

public class DroneMovement1 : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Transform self;

    Interactable interactable;

    void Update() {
        Moving(true);
    }


    public void Moving(bool shouldMove) {
        if (shouldMove) {
            //if(OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight)){
            if(Input.GetKey("d")){
                rb.AddForce(500 * Time.deltaTime, 0, 0);
            }

            //if(OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft)){
            if(Input.GetKey("a")){
                rb.AddForce(-500 * Time.deltaTime, 0, 0);
            }

            //if(OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp)){
            if(Input.GetKey("w")){
                rb.AddForce(0, 0, 500 * Time.deltaTime);
            }

            //if(OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown)){
            if(Input.GetKey("s")){
                rb.AddForce(0, 0, -500 * Time.deltaTime);
            }

            if(Input.GetKey("r")){
                Thread.Sleep(500);
                self.Rotate(0.0f, 90.0f, 0.0f);
            }

        }
    }

    
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("coliding:" + other);
        if (other != null) {
            interactable = other.GetComponentInParent<Interactable>();
            //Debug.Log("interactable:" + interactable);
            EventSystem.current.DialogueTrigerEnter(interactable.Id);
            if(Input.GetKeyDown("e")){
                EventSystem.current.DialogueInteracted(interactable.Id); 
            }
        }
    }

    
    private void OnTriggerExit(Collider other)
    {

        if (other != null) {
            interactable = other.GetComponentInParent<Interactable>();
            //Debug.Log("uncoliding:" + other);
            EventSystem.current.DialogueTrigerExit(interactable.Id);
        }
        interactable = null;
        
    }

}