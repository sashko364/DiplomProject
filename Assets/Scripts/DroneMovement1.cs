using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(FocusHandler))]
public class DroneMovement1 : MonoBehaviour
{
    public Rigidbody rb;
    Transform target;
    NavMeshAgent agent;

    void Start(){
        agent = GetComponent<NavMeshAgent>();
        //agent.updateRotation = false;
        //agent.updateUpAxis = false;
        GetComponent<FocusHandler>().onFocusChangedCallback += OnFocusChanged;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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

        /*if (target != null){
			move(target.position);
			// FaceTarget();
		}*/
    }

    /*public void move(Vector3 point){
        Debug.Log(agent);
        agent.SetDestination(point);
    }*/

    void OnFocusChanged(Interactable newFocus){
		if (newFocus != null){
			agent.stoppingDistance = newFocus.radius*.8f;
			agent.updateRotation = false;
			target = newFocus.interactionTransform;

		} else{
			agent.stoppingDistance = 0f;
			agent.updateRotation = true;
			target = null;
		}
	}

    void FaceTarget(){
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}
}