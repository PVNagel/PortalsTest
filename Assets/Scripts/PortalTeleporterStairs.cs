using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporterStairs : MonoBehaviour
{
    public Transform player;
    public Transform receiver;

    private bool playerIsOverlapping = false;

    // Update is called once per frame
    void Update()
    {
        if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            // Get the forward direction of the player
            Vector3 playerForward = player.forward;
            Vector3 playerRight = player.right;

            // Calculate the dot product between the player's forward direction and the portal's forward direction
            float forwardDotProduct = Vector3.Dot(transform.forward, playerForward);
            float rightDotProduct = Vector3.Dot(transform.forward, playerRight);

            // Check if the player is moving towards the portal (forwardDotProduct > 0) 
            // and is facing away from the portal (dotProduct > 0) or looking up/down (dotProduct > threshold)
            // and is not looking behind and to their right (rightDotProduct > 0)
            float threshold = 0.5f; // Adjust this threshold value as needed
            if (dotProduct < -threshold && forwardDotProduct < 0f && rightDotProduct > 0f)
            {
                float rotationDiff = Quaternion.Angle(transform.rotation, receiver.rotation);
                rotationDiff += 180f;
                player.Rotate(Vector3.up, rotationDiff);
                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = receiver.position + positionOffset;

                playerIsOverlapping = false;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = false;
        }
    }
}
