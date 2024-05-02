using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras;
    private int currentCameraIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i< cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Disable current camera
            cameras[currentCameraIndex].gameObject.SetActive(false);

            // Move to the next camera
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

            // Enable the new camera
            cameras[currentCameraIndex].gameObject.SetActive(true);
        }
        
    }
}
