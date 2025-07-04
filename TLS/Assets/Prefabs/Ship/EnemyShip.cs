using UnityEngine;

public class PivotingShip : MonoBehaviour
{
    public float speed; // How fast it falls
    public float orbitSpeed = 45f; // Degrees per second to orbit
    public GameObject bullet; // Assign bullet prefab in Inspector
    public float shootInterval = 1f; // Time between shots
    
    private hearts healthBar;
    private Transform pivotPoint;
    private float shootTimer;
    private Vector3 initialPosition;

    private int health = 10;

    void Start()
    {
        healthBar = FindFirstObjectByType<hearts>();
        pivotPoint = GameObject.Find("CenterPoint").transform;
        initialPosition = transform.position;
        shootTimer = shootInterval;
    }

    void Update()
    {
        // Original falling code
        transform.position += Vector3.down * speed * Time.deltaTime;


        // Orbiting logic
        if (pivotPoint != null)
        {
            transform.RotateAround(pivotPoint.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
        else
        {
            // Fallback to original X/Z position lock
            transform.position = new Vector3(initialPosition.x, transform.position.y, initialPosition.z);
        }

        // Shooting logic
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0 && bullet != null)
        {
            shootTimer = shootInterval;
            ShootBulletDownward();
        }

        if (Input.GetButtonDown("Cancel")||health <=0)
        {
            Destroy(gameObject);
        }
    }

    void ShootBulletDownward()
    {
        // Create bullet slightly below current position
        Vector3 spawnPosition = transform.position + Vector3.down * 0.5f;
        // Instantiate with downward rotation
        GameObject newBullet = Instantiate(bullet, spawnPosition, Quaternion.Euler(90, 0, 0));

        Physics.IgnoreCollision(newBullet.GetComponent<Collider>(), GetComponent<Collider>(), true);

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        //transform.RotateAround(pivotPoint.position, Vector3.up, orbitSpeed * Time.deltaTime);
        if (collision.gameObject.name != "TopBar")
        {
            if (healthBar != null && collision.gameObject.name == "BottomBar")
            {
                healthBar.LoseHealth(1);
                health = 0;
                Destroy(gameObject);
                Debug.Log("damage taken");
            }
            if (collision.gameObject.name == "Bullet(Clone)")
            {
                health--;
                Destroy(collision.gameObject);
                Debug.Log("health" + health);
            }
            if (collision.gameObject.name == "EnemyRocket"||collision.gameObject.name == "EnemyBullet")
            {
                
            }
            else
            {
                //Physics.IgnoreCollision(newBullet.GetComponent<Collider>(), GetComponent<Collider>(), true);
            }

        }
    }
}