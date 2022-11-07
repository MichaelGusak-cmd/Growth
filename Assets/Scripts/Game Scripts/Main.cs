using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    private float panic; 
    private float food;
    private List<PowerPoint> powerPoints = new List<PowerPoint>();



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //GETTERS, SETTERS
    public void addPanic(float x) { panic += x; }
    public float getPanic() { return panic; }

    public void addFood(float x) { food += x; }
    public float getFood() { return food; }
}
