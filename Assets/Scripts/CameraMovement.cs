using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera m_Camera;
    private float m_CameraSpeed;
    private float m_ScrollSpeed;

	void Start ()
    {
        m_Camera = GetComponent<Camera>();
        m_CameraSpeed = 25f;
        m_ScrollSpeed = 50f;
	}
	

	void Update ()
    {
        float scrollWheelValue = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWheelValue > 0f)
        {
            if (m_Camera.orthographicSize > 5f)
            {
                m_Camera.orthographicSize -= m_ScrollSpeed * Time.deltaTime;
            }
        }
        else if (scrollWheelValue < 0f)
        {
            if (m_Camera.orthographicSize < 100f)
            {
                m_Camera.orthographicSize += m_ScrollSpeed * Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x - (m_CameraSpeed * Time.deltaTime), transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x + (m_CameraSpeed * Time.deltaTime), transform.position.y, transform.position.z);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (m_CameraSpeed * Time.deltaTime), transform.position.z);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (m_CameraSpeed * Time.deltaTime), transform.position.z);
        }
    }
}
