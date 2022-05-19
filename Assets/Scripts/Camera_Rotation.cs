using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Camera_Rotation : MonoBehaviour {
    [SerializeField]
    private Transform drone;

    [SerializeField]
    private Transform camera;
    
    [SerializeField]
    private float smoothSpeed = 1.25f;

    [SerializeField]
    private Vector3 offSet;

    private float mouseSensitivity = 150f;

    private float xRotation = 0.0f;

    private float yRotation = 0.0f;

    private bool shouldRotate;

    float x = 0f;
    float y = 0f;


    public void Start()
    {
        EventSystem.current.onDialogueTrigerEnter += onDialogueTrigerEnter;
        EventSystem.current.onDialogueTrigerExit += onDialogueTrigerExit;
        this.shouldRotate = true;
    }

    public void OnDestroy()
    {
        EventSystem.current.onDialogueTrigerEnter -= onDialogueTrigerEnter;
        EventSystem.current.onDialogueTrigerExit -= onDialogueTrigerExit;
    }
    // Update is called once per frame
    private void Update()
    {
        Rotate();
        CheckCameraOffset();
    }

    private void CheckCameraOffset() {
        Vector3 desiredPosition = drone.position + offSet;
        Vector3 smoothPosition = Vector3.Lerp(camera.position, desiredPosition, smoothSpeed);
        camera.position = smoothPosition;
    }

    public void Rotate(){
        
        if (shouldRotate)
        {
            x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            y = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            yRotation += x;
            xRotation -= y;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            camera.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            drone.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }

    public void onDialogueTrigerEnter(int id)
    {
        shouldRotate = false;
    }

    public void onDialogueTrigerExit(int id)
    {
        shouldRotate = true;
    }
}
