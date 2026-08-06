using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuParralax : MonoBehaviour
{
    [Header("Parallax Settings")]
    [Tooltip("How much this layer follows the mouse. Lower = farther away, Higher = closer.")]
    public float parallaxMultiplier;
    
    [Tooltip("Smoothness of the movement transition.")]
    public float smoothTime = 0.3f;

    private Vector3 startPosition;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        float offsetX = mousePos.x - 0.5f;
        float offsetY = mousePos.y - 0.5f;

        Vector3 targetPosition = startPosition + new Vector3(offsetX * parallaxMultiplier, offsetY * parallaxMultiplier, 0);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
