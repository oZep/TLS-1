using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject projectile;
    public Transform TopBar;
    public float speed;
    public Quaternion spawnRotation; 
    public int destroyBulletTimer;
    void Start()
    {

        GameObject EnemyBullet = Instantiate(projectile, TopBar.transform.position, spawnRotation);
        EnemyBullet.GetComponent<Rigidbody>().AddForce(transform.up * -speed);
        Destroy(EnemyBullet, destroyBulletTimer);

    }


}
