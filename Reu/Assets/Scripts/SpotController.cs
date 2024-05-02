using UnityEngine;

public class SpotController : MonoBehaviour
{
    public Transform[] fullLegs;           // Array of full legs (parent of upper and lower leg)
    public Transform[] upperLegs;          // Array of upper legs
    public Transform[] lowerLegs;          // Array of lower legs (knee)
    public float[] legOffsets;             // Time offset for each leg
    public float strideLength = 0.1f;      // Length of each step
    public float strideHeight = 0.05f;     // Height of each step
    public float stepDuration = 1f;        // Duration of each step
    public float lowerLegRotationAngle = 30f; // Angle of rotation for the lower leg
    public LayerMask groundLayer;          // Layer mask for the ground

    void Update()
    {
        // Calculate the current time
        float currentTime = Time.time;

        // Loop through each leg and update its position and rotation
        for (int i = 0; i < fullLegs.Length; i++)
        {
            // Calculate the phase of the leg movement based on the leg offset and current time
            float phase = (currentTime * (1f / stepDuration) + legOffsets[i]) % 1f;

            // Calculate the desired height of the leg based on the phase
            float y = Mathf.Abs(Mathf.Sin(Mathf.PI * phase)) * strideHeight;

            // Calculate the position of the leg tip
            Vector3 legTipPosition = fullLegs[i].position + fullLegs[i].up * y;

            // Raycast to detect the ground surface
            RaycastHit hit;
            if (Physics.Raycast(legTipPosition, -fullLegs[i].up, out hit, Mathf.Infinity, groundLayer))
            {
                // Adjust the position of the leg tip to touch the ground
                legTipPosition = hit.point;

                // Calculate the rotation of the leg based on the surface normal
                Quaternion legRotation = Quaternion.FromToRotation(fullLegs[i].up, hit.normal) * fullLegs[i].rotation;

                // Apply rotation to the full leg
                fullLegs[i].rotation = legRotation;

                // Calculate the rotation of the lower leg relative to the upper leg's rotation
                Quaternion lowerLegRotation = Quaternion.Inverse(upperLegs[i].rotation) * legRotation;

                // Apply the angle of rotation for the lower leg
                lowerLegRotation = Quaternion.Euler(lowerLegRotation.eulerAngles.x, lowerLegRotationAngle, lowerLegRotation.eulerAngles.z);

                // Apply rotation to the lower leg
                lowerLegs[i].rotation = lowerLegRotation;
            }
        }
    }
}
