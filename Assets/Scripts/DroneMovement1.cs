using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading;

//[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(FocusHandler))]
public class DroneMovement1 : MonoBehaviour
{
    public Rigidbody rb;
    public Transform self;
    Transform target;
    //NavMeshAgent agent;

    void Start(){
        //agent = GetComponent<NavMeshAgent>();
        /*agent.updateRotation = false;
        agent.updateUpAxis = false;*/
        GetComponent<FocusHandler>().onFocusChangedCallback += OnFocusChanged;
    }

    // Update is called once per frame
    void Update()
    {
        //if(OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight)){
        if(Input.GetKey("d") && self.position.y >= 0){
            rb.AddForce(500 * Time.deltaTime, 0, 0);
        }

        //if(OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft)){
        if(Input.GetKey("a") && self.position.y >= 0){
            rb.AddForce(-500 * Time.deltaTime, 0, 0);
        }

        //if(OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp)){
        if(Input.GetKey("w") && self.position.y >= 0){
            rb.AddForce(0, 0, 500 * Time.deltaTime);
        }

        //if(OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown)){
        if(Input.GetKey("s")){
            if(self.rotation.y < 0 && self.rotation.y > (-1)){
                rb.AddForce(-500 * Time.deltaTime, 0, 0);
            }/*else if(self.rotation.y == 1){
                rb.AddForce(0, 0, 500 * Time.deltaTime);
            }else if(self.rotation.y == 180.0f){

            }*/

            
        }

        Debug.Log("rotation = " + self.rotation.y);

        if(Input.GetKey("r")){
            Thread.Sleep(500);
            self.Rotate(0.0f, 90.0f, 0.0f);
        }
    }


    void OnFocusChanged(Interactable newFocus){
		if (newFocus != null){
			//agent.stoppingDistance = newFocus.radius*.8f;
			//agent.updateRotation = false;
			target = newFocus.interactionTransform;

		} else{
			//agent.stoppingDistance = 0f;
			//agent.updateRotation = true;
			target = null;
		}
	}

    /*void FaceTarget(){
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}*/
}