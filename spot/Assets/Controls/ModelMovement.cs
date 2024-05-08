using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelMovement : MonoBehaviour
{
    [SerializeField] private GameObject modelGameObject;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private ArticulationBody rootBody;
    [SerializeField] private GameObject animatorGameObject; // Reference to the GameObject with the animator component
    [SerializeField] Animator animator;
    private float x, y;

    private void Start()
    {
        animator = animatorGameObject.GetComponent<Animator>(); // Fix this line to get the Animator from animatorGameObject
    }

    void Update()
    {
        MoveModel();
        RotateModel(); // Call RotateModel separately
    }

    private void MoveModel()
    {
        Vector3 movement = Vector3.zero;

        // Calculate movement based on input
        movement = Movement(movement);
        Vector3 newPosition = rootBody.transform.position + modelGameObject.transform.TransformDirection(movement) * (speed * Time.deltaTime);

        animator.SetFloat("x", x);
        animator.SetFloat("y", y);

        Debug.Log("x: " + x + " y: " + y);

        rootBody.TeleportRoot(newPosition, rootBody.transform.rotation);
    }

    private void RotateModel()
    {
        // Rotate the modelGameObject based on input
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.JoystickButton0))
        {
            modelGameObject.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime); // Reverse the rotation direction
            x = -1;
            y = 0;
        }

        if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Joystick1Button2))
        {
            modelGameObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            x = 1;
            y = 0;
        }
    }

    private Vector3 Movement(Vector3 movement)
    {
        // Get the forward and right directions relative to the object's rotation
        Vector3 forwardDirection = modelGameObject.transform.forward;
        Vector3 rightDirection = modelGameObject.transform.right;

        // Calculate movement based on input
        if (Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal") < 0)
        {
            movement -= rightDirection; // Move left relative to the object's right direction
            x = -1;
            y = 0;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") > 0)
        {
            movement += rightDirection; // Move right relative to the object's right direction
            x = 1;
            y = 0;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetAxis("Vertical") > 0)
        {
            movement += forwardDirection; // Move forward relative to the object's forward direction
            x = 0;
            y = -1;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetAxis("Vertical") < 0)
        {
            movement -= forwardDirection; // Move backward relative to the object's forward direction
            x = 0;
            y = 1;
        }
        
        if (!Input.anyKey)
        {
            StartCoroutine(StopAnimation());
        }

        return movement;
    }

    private IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds
        x = 0;
        y = 0;
    }
}
