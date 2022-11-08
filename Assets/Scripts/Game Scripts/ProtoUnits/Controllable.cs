using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllable : MonoBehaviour
{
    public Vector3 moveDir;
    public bool newCommand = false;
    public float commandDuration = 5f;
    private float commandReceived = 0f;
    public float speed = 0.5f;
    public bool busy = false;
    // Start is called before the first frame update
    void Start()
    {
        moveDir = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        //decide what should take priority:
        // some event (combat, feeding)
        // or control, and to what degree
        // use 'busy' bool

        if (newCommand) {
            commandReceived = Time.time;
            newCommand = false;
        }

        if (commandReceived+commandDuration > Time.time) {
            gameObject.transform.localPosition += moveDir * Time.deltaTime * speed;
            //follow command until some event
        }
        else {
            //no commands given
            //roam around until some event
        }
    }
}
