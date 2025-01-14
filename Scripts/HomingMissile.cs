using UnityEditor.Rendering;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    // Public fields for customization
    public Transform target;      // The target to follow
    public float speed = 5f;      // Movement speed of the missile
    public float turnSpeed = 2f;  // Turning speed of the missile
    public ParticleSystem burst;
    public bool followTarget = false;
    public Camera followCamera;

    void Update()
    {
        if (followTarget)
        {
            burst.Play();
            if (target == null)
            {
                Debug.LogWarning("Target not assigned or destroyed");
                return;
            }

            // Calculate the direction towards the target
            Vector3 direction = (target.position - transform.position).normalized;

            // Smoothly rotate towards the target
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            // Move forward in the current direction
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
    public void StartFollowing()
    {
        Invoke("InitiateBursters", 3);
    }
    public void InitiateBursters()
    {
        Destroy(this.GetComponent<Rigidbody>());
        Destroy(this.GetComponent<CapsuleCollider>());
        followTarget = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collision is with the target
        if (other.transform == target)
        {
            Debug.Log("Target reached and destroyed!");

            // Destroy the target and the missile itself
            Destroy(target.gameObject);
            Destroy(gameObject);
        }
    }
}
