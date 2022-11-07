using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public Camera camera;
    public GameObject Line;
    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        lineRenderer = Line.GetComponent<LineRenderer>();

        lineRenderer.startWidth = 0.25f;
        lineRenderer.endWidth = 0.07f;

        lineRenderer.enabled = false;
    }
    private bool placed = false;
    private int mouseButtonPressed = -1;
    // Update is called once per frame
    void Update()
    {
        //0 == primary button
        //1 == secondary button
        if (Input.GetMouseButtonDown(0)) { 
            mouseButtonPressed = 0;
        }
        else if (Input.GetMouseButtonUp(0)) {
            Disable();
        }
        else if (Input.GetMouseButtonDown(1)) {
            mouseButtonPressed = 1;
        }
        else if (Input.GetMouseButtonUp(1)) {
            Disable();
        }


        if (mouseButtonPressed >= 0 && !placed) {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 10.0f;
            transform.localPosition = mousePos;

            lineRenderer.SetPosition(0, GetCurrentMousePosition().GetValueOrDefault());
            lineRenderer.positionCount = 1;
            lineRenderer.enabled = true;
            placed = true;
        }

        if (placed) {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(1, GetCurrentMousePosition().GetValueOrDefault());
        }
    }

    private void Disable() {
        mouseButtonPressed = -1;
        placed = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        lineRenderer.enabled = false;
    }

    private Vector3? GetCurrentMousePosition()
     {
         var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         var plane = new Plane(Vector3.forward, Vector3.zero);
 
         float rayDistance;
         if (plane.Raycast(ray, out rayDistance))
         {
             return ray.GetPoint(rayDistance);
             
         }
 
         return null;
     }
}
