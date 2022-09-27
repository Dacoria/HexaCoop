using UnityEngine;

public class LookToCamera : MonoBehaviour
{
    private Camera m_Camera;

    void Start()
    {
        m_Camera = Camera.main;
    }

    void Update()
    {        
        var lookDestination = new Vector3(m_Camera.transform.position.x, 0, m_Camera.transform.position.z);
        var targetDirection = lookDestination - transform.position;
        transform.rotation = Quaternion.LookRotation(-targetDirection);
    }
}
