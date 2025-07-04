using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PivotingBullet : MonoBehaviour
{
    public float speed; // How fast it falls
    private float initialX;
    private float initialZ;
    private hearts healthBar;
    private Transform pivotPoint;
    public float orbitSpeed = 45f; // Added: Degrees per second to orbit

    void Start()
    {
        // Store the initial X and Z positions
        initialX = transform.position.x;
        initialZ = transform.position.z;
        healthBar = FindFirstObjectByType<hearts>();
        
        // Added: Find pivot point
        pivotPoint = GameObject.Find("CenterPoint").transform;
    }

    void Update()
    {
        // Original falling code
        transform.position += Vector3.down * speed * Time.deltaTime;

        // Modified: Replace position lock with orbiting
        if (pivotPoint != null)
        {
            transform.RotateAround(pivotPoint.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
        else
        {
            // Fallback to original behavior if no pivot
            transform.position = new Vector3(initialX, transform.position.y, initialZ);
        }
        
        if (Input.GetButtonDown("Cancel"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        transform.RotateAround(pivotPoint.position, Vector3.up, orbitSpeed * Time.deltaTime);
        if (collision.gameObject.name != "TopBar" && collision.gameObject.name != "EnemyShip(Clone)" && collision.gameObject.name != "EnemyBullet(Clone)")
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>(), true);
            Destroy(gameObject);

            if (healthBar != null && collision.gameObject.name == "BottomBar")
            {
                healthBar.LoseHealth(1);
                Debug.Log("damage taken");
            }
            else
            {
                //Debug.Log("bullet shot");
            }
        }
    }
}