using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class NeedleController : MonoBehaviour
{
    public int LightIntensity;

    public void Start()
    {
        Transform transform = gameObject.transform;
        LightIntensity = 1;
        transform.rotation = Quaternion.Euler(0, 0, 60);
    }

    private void Update()
    {
        GetInput();
    }
    public void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (LightIntensity > 1)
            {
                LightIntensity = LightIntensity - 1;
                transform.Rotate(Vector3.forward, 60);
            }
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (LightIntensity < 3)
            {
                LightIntensity = LightIntensity + 1;
                transform.Rotate(Vector3.forward, -60);
            }
        }
    }
}
