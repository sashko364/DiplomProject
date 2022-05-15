using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Camera_Rotation : MonoBehaviour {
    [SerializeField]
    private Transform self;
    [SerializeField]
    private Transform camera;

    private float mouseSensitivity = 60f;

    private float xRotation = 0.0f;

    private float yRotation = 0.0f;

        float x = 0f;
        float y = 0f;
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        y = x = 90;
        yRotation = xRotation = 90;

    }

    // Update is called once per frame
    void Update()
    {
        
        x += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        y += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        Debug.Log(x + "===" + y);
       
        xRotation = Mathf.Clamp(x, 0, 360f);
        yRotation = Mathf.Clamp(x, -180f, 180f);
        Thread.Sleep(10);

        camera.localRotation = Quaternion.Euler(self.rotation.eulerAngles);
    }
}
