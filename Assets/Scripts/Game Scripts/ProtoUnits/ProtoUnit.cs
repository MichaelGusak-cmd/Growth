using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoUnit : MonoBehaviour
{
    void Update()
    {
        //Move units
        //transform.Translate(0,speed*Time.deltaTime,0);
    }

    void FixedUpdate()
    {
        //Do stuff:
    }

    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerEnter2D(Collider2D other)
    {
        string tag = other.tag;
        switch (tag)
        {
            case "ControlWeak": //mouse control for weak
                Debug.Log("WEAK CONTROL");
                other.gameObject.GetComponent<MouseControl>().addToUnits(gameObject);
                break;
            case "ControlStrong": //mouse control for strong
                Debug.Log("STRONG CONTROL");
                other.gameObject.GetComponent<MouseControl>().addToUnits(gameObject);
                break;
            case "ProtoPowerPoint":
                break;
            case "HumanPowerPoint":
                break;
            case "ProtoWeak":
                break;
            case "ProtoStrong":
                break;
            case "HumanWeak":
                break;
            case "HumanStrong":
                break;
        }
        //speed = speed * -1;
    }
}