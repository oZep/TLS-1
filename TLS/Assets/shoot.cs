using UnityEngine;
using UnityEngine.ProBuilder;

public class shoot : MonoBehaviour
{


    // Update is called once per frame
    public GameObject projectile;
    public Transform Top;
    public int speed;
    public int destroyBulletTimer;
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            GameObject bullet = Instantiate(projectile, Top.transform.position, Top.transform.rotation) as GameObject;
            bullet.GetComponent<Rigidbody>().AddForce(transform.up*speed);
            
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
