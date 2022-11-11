using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPoint : MonoBehaviour
{
    public GameObject gameManager;
    public List<GameObject> prefabs;
    public List<float> costs;
    private Main main;
    // Start is called before the first frame update
    void Start()
    {
        main = gameManager.GetComponent<Main>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        int spawn = -1; //check to see if need to spawn anything and what type
        string tag = other.tag;
        switch (tag)
        {
            case "ControlWeak": //mouse control for weak
                spawn = 0;
                break;
            case "ControlStrong": //mouse control for strong
                spawn = 1;
                break;
        }

        if (spawn >= 0) {
            if (prefabs[spawn] != null) {
                if (main.getFood() > costs[spawn]) {
                    GameObject g = Instantiate(prefabs[spawn], transform.localPosition, Quaternion.identity);
                    //g.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f),Random.Range(0f, 1f));
                }
            }
        }

    }
}
