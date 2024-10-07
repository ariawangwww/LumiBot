using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    private Animator anim => GetComponent<Animator>();
    public bool success;
    // Start is called before the first frame update
    void Start()
    {
        anim.SetInteger("health", 3);
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("success", success);
    }

    public void SetSuccess(bool value)
    {
        success = value;
    }
}
