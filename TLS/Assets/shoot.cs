using UnityEngine;

public class shoot : MonoBehaviour
{
    public GameObject projectile;
    public Transform Top;
    public int speed;
    public int destroyBulletTimer;
    public float fireRate = 0.2f; // Time in seconds between shots

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            GameObject bullet = Instantiate(projectile, Top.transform.position, Top.transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.up * speed);

            Destroy(bullet, destroyBulletTimer);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        Debug.Log(collision.gameObject.tag);
        Debug.Log("YUPYUP");
    }
}
