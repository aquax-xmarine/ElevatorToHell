using UnityEngine;

public class doorMovement : MonoBehaviour
{
    public Animator doorAnimator; // Reference to the Animator component
    public float openSpeed = 2f; // Speed at which the door opens/closes

    private bool isOpen = true; // Flag to track whether the door is currently open

    // Start is called before the first frame update
    void Start()
    {
        // Open the door when the game starts
        doorAnimator.SetBool("open", true);
    }

    // Called when another collider enters the trigger zone
    void OnTriggerEnter(Collider other)
    {
        // Check if the collider entering the trigger zone is the player
        if (other.CompareTag("Player"))
        {
            // Open the door if it's not already open
            if (!isOpen)
            {
                doorAnimator.SetBool("open", true); // Set "Open" parameter to true in the Animator
                isOpen = true;
            }
        }
    }
}
