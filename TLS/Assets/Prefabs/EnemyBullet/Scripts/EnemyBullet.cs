using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StraightBullet : MonoBehaviour
{
    public float speed; // How fast it falls
    private float radius;
    private hearts healthBar;

    void Start()
    {
        // Distance from the center (assuming centered at origin)
        radius = new Vector2(transform.position.x, transform.position.z).magnitude;
        healthBar = FindFirstObjectByType<hearts>();
    }

    void Update()
    {
        // Move down over time
        transform.position += Vector3.down * speed * Time.deltaTime;

        // Lock bullet to the curved cylinder wall
        Vector3 flatXZ = new Vector3(transform.position.x, 0f, transform.position.z).normalized * radius;
        transform.position = new Vector3(flatXZ.x, transform.position.y, flatXZ.z);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "TopBar") {
            Destroy(gameObject);
            if (healthBar != null)
            {
                healthBar.LoseHealth(1);
            }
            else
            {
                Debug.LogError("healthBar reference is null!");
            }
        };
    }

}
