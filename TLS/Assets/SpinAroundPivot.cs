using UnityEngine;

public class SpinAroundPivot : MonoBehaviour
{

    public float rotationSpeed = 100f;
    public Transform pivotPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        bool runInput = Input.GetButtonDown("Submit");

        float rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime * (runInput ? 4 : 1);
        if (runInput)
            Debug.Log("Dash");

        if (pivotPoint != null)
        {

            transform.RotateAround(pivotPoint.position, Vector3.up, rotationAmount);

        }
    }

    void OnDrawGizmosSelected()
    {
        if (pivotPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(pivotPoint.position, 0.1f); // Draw a small sphere at the pivot
            Gizmos.DrawLine(transform.position, pivotPoint.position); // Draw a line to the pivot
        }
    }
}
