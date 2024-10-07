using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryController : MonoBehaviour
{
    SpriteController spritecontroller;
    private int healthcount;
    public int healthnum;

    // Start is called before the first frame update
    void Start()
    {
        GameObject spriteobj = GameObject.FindWithTag("Sprite");
        spritecontroller = spriteobj.GetComponent<SpriteController>();
    }

    // Update is called once per frame
    void Update()
    {
        healthcount = spritecontroller.health;
        if(healthnum > healthcount)
        {
            Destroy(gameObject);
        }
    }
}
