using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screwdriver : MonoBehaviour
{
    // The tag that identifies screw GameObjects
    public string screwTag = "Screw";

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the screw tag
        if (collision.gameObject.CompareTag(screwTag))
        {
            // Check if the screw already has a FixedJoint component
            if (collision.gameObject.GetComponent<FixedJoint>() == null)
            {
                // Add a FixedJoint component to the screw
                FixedJoint fixedJoint = collision.gameObject.AddComponent<FixedJoint>();

                // Set the connected body of the FixedJoint to the screwdriver's Rigidbody
                fixedJoint.connectedBody = GetComponent<Rigidbody>();
            }
        }
    }
}