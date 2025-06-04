using UnityEngine;

public class die : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name != "Shooter") Destroy(gameObject);  
    }
}