using extOSC;
using Igloo.Common;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IglooGyroInput : MonoBehaviour
{
    [SerializeField] private Camera m_Igloo2DCamera;
    [SerializeField] private RectTransform m_Crosshair;

    /// <summary>
    /// Mono start function 
    /// Assigns the NetworkManagerOSC interface to the crosshair movement function
    /// </summary>
    private void Start()
    {
        NetworkManagerOSC.instance.Setup();
        NetworkManagerOSC.instance.OnPlayerRotationWarper += SetCrosshairPosition;
    }

    /// <summary>
    /// Called on message received through OSC interface containing XIMU Gyroscope data from the Igloo Core engine suite. 
    /// Converts the gyroscope data to X Y position data for the crosshair
    /// Set's the crosshairs position back to the start if it goes beyond it's bounds
    /// applys the movement to the m_Crosshair object.
    /// </summary>
    /// <param name="name">Not Required - redundant string</param>
    /// <param name="gyroEuler">Vector3 raw gyroscope values</param>
    private void SetCrosshairPosition(string name, Vector3 gyroEuler)
    {
        // Set the gyroscope's euler angles to the corresponding X & Y values of the cameras' target texture
        float crosshairX = (gyroEuler.y / 360f) * m_Igloo2DCamera.targetTexture.width;
        float crosshairY = (-gyroEuler.x / 180f) * m_Igloo2DCamera.targetTexture.height;

        // reset the wrap around for continuity
        crosshairX %= m_Igloo2DCamera.targetTexture.width;
        crosshairY %= m_Igloo2DCamera.targetTexture.height;

        // Assign position to the crosshair object
        m_Crosshair.anchoredPosition = new Vector2(crosshairX, crosshairY);
    }

    /// <summary>
    /// Mono Update function 
    /// Handles interaction and buttons presses
    /// </summary>
    private void Update()
    {
        // Right Mouse Button Down || Xbox Button A Down
        if (Input.GetButtonDown("Fire1"))
        {
            // Interaction Method 1 - Create a raycast that hits stuff. Do something when it his what you want. 
            RaycastAtLocation(m_Crosshair.anchoredPosition3D);

            // Interaction Method 2 - Craete a mouse click at the crosshair location - Good for doing UI events
            SimulateMouseClick(m_Crosshair.anchoredPosition3D);
        }
    }


    /// <summary>
    /// Simulate mouse click if using a UI canvas for selection of objects. 
    /// Uses a raycast for selection like a regular mouse click interaction with a Overlay UI.
    /// </summary>
    /// <param name="location">Vector3 location to generate the mouse click event. </param>
    private void SimulateMouseClick(Vector3 location)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = location
        };  

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            ExecuteEvents.Execute(result.gameObject, pointerData, ExecuteEvents.pointerClickHandler);
        }
    }

    private void RaycastAtLocation(Vector3 location)
    {
        Ray ray = Camera.main.ScreenPointToRay(location);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Hit: " + hit.collider.name);

            // You can call a function on the hit object
        }
    }
}
