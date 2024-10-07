using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitScript : MonoBehaviour
{
    ObjectManager manager;
    private void Start()
    {
        GameObject managerobj = GameObject.FindWithTag("Manager");
        manager = managerobj.GetComponent<ObjectManager>();
    }
    private void Update()
    {
        // 检测鼠标左键是否按下
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePosition);

            // 检查鼠标是否在当前Sprite上
            if (hit != null && hit.gameObject == gameObject)
            {
                manager.CheckAnswers();
            }
        }
    }
}
