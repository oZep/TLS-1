using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StraightBullet : MonoBehaviour
{
    public float speed; // How fast it falls
    private float initialX;
    private float initialZ;
    private hearts healthBar;

    void Start()
    {
        // Store the initial X and Z positions
        initialX = transform.position.x;
        initialZ = transform.position.z;
        healthBar = FindFirstObjectByType<hearts>();

    }


    void Update()
    {
        // Move down over time (only affect Y position)
        transform.position += Vector3.down * speed * Time.deltaTime;

        // Lock X and Z positions to their initial values
        transform.position = new Vector3(initialX, transform.position.y, initialZ);
        
        if (Input.GetButtonDown("Cancel"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "TopBar") {
            Destroy(gameObject);
            if (healthBar != null && collision.gameObject.name == "BottomBar")
            {
                healthBar.LoseHealth(1);
                Debug.Log("damage taken");
            }
            else
            {
                Debug.Log("bullet shot");
                //Debug.LogError("healthBar reference is null!");
            }
        };
    }
}