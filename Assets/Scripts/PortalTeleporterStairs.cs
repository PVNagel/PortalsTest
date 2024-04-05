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

            // Calculate the dot product between the player's forward direction and the portal's forward direction
            float forwardDotProduct = Vector3.Dot(transform.forward, playerForward);

            // Get the direction of the player relative to the portal's normal direction
            float relativeDirection = Vector3.Dot(transform.forward, portalToPlayer);

            // If the dot product is negative, it means the player is on the correct side of the portal
            if (dotProduct < 0f && forwardDotProduct < 0f && relativeDirection > 0f)
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
