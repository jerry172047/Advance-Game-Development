using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float minThrottle;    // 20
    [SerializeField] private float maxThrottle;     // 100
    [SerializeField] private float rotationSpeed;

    public Transform missile1;
    public Transform missile2;
    public Transform missile3;
    public Transform missile4;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = speed + 1;
            speed = Mathf.Clamp(speed, minThrottle, maxThrottle);           // Limits the speed value between minThrottle and maxThrottle
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = speed - 1;
            speed = Mathf.Clamp(speed, minThrottle, maxThrottle);
        }

        this.transform.position += transform.forward * Time.deltaTime * speed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (missile1 != null)
            {
                InitiateRocket(missile1);
                missile1 = null;
            }
            else if (missile2 != null)
            {
                InitiateRocket(missile2);
                missile2 = null;
            }
            else if (missile3 != null)
            {
                InitiateRocket(missile3);
                missile3 = null;
            }
            else if (missile4 != null)
            {
                InitiateRocket(missile4);
                missile4 = null;
            }
        }
        if (speed > 0)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Vector3 rotation = transform.forward * Time.deltaTime * rotationSpeed;
                transform.Rotate(-rotation.x, -rotation.y, -rotation.z);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Vector3 rotation = transform.forward * Time.deltaTime * rotationSpeed;
                transform.Rotate(rotation.x, rotation.y, rotation.z);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Vector3 rotation = transform.right * Time.deltaTime * rotationSpeed;
                transform.Rotate(rotation.x, rotation.y, rotation.z);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                Vector3 rotation = transform.right * Time.deltaTime * rotationSpeed;
                transform.Rotate(-rotation.x, -rotation.y, -rotation.z);
            }
        }

        float verticalInput = Input.GetAxis("Vertical");   // W/S or Up/Down Arrow keys
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow keys

        // Rotate the plane up or down based on vertical input
        transform.Rotate(Vector3.right, verticalInput * rotationSpeed * Time.deltaTime);

        // Roll the plane based on horizontal input
        transform.Rotate(Vector3.forward, -horizontalInput * rotationSpeed * Time.deltaTime);
    }
    public void InitiateRocket(Transform missileObject)
    {
        missileObject.transform.SetParent(null);
        missileObject.AddComponent<Rigidbody>();
        missileObject.AddComponent<CapsuleCollider>();
        if (missileObject.TryGetComponent(out HomingMissile missile))
        {
            missile.StartFollowing();
            Camera.main.enabled = false;
            missile.followCamera.enabled = true;
        }
    }
}
