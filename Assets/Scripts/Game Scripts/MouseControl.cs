using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public Camera camera;
    public GameObject Line;
    private LineRenderer lineRenderer;
    private List<GameObject> selectedUnits;
    // Start is called before the first frame update
    void Start()
    {
        selectedUnits = new List<GameObject>();
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
            SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();

            if (mouseButtonPressed == 0) {
                gameObject.tag = "ControlWeak";
                sprite.color = new Color(1,0,0, 0.2f);
            }
            else if (mouseButtonPressed == 1) {
                gameObject.tag = "ControlStrong";
                sprite.color = new Color(0,1,0, 0.2f);
            }

            sprite.enabled = true;
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
        gameObject.transform.localPosition = new Vector3(-1000,-1000,10); //move collider far out of bounds so it can trigger things again
        for (int i = 0; i < selectedUnits.Count; i++) {
            Controllable c = selectedUnits[i].GetComponent<Controllable>();
            Vector3[] positions = new Vector3[2];
            lineRenderer.GetPositions(positions);
            c.moveDir = positions[1] - positions[0];
            c.newCommand = true;
        }
        selectedUnits.Clear();
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

     public void addToUnits(GameObject g) {
        selectedUnits.Add(g);
     }
}
