using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    PlayerController playercontroller;
    private Animator anim => GetComponent<Animator>();
    // Start is called before the first frame update
    void Start()
    {
        GameObject playerobj = GameObject.FindWithTag("Player");
        playercontroller = playerobj.GetComponent<PlayerController>();
        anim.SetInteger("Intensity", 1);
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("Intensity", playercontroller.LightIntensity);
    }
}
