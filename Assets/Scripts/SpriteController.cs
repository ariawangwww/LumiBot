using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    private Animator anim => GetComponent<Animator>();
    public bool success;
    public int health = 3;
    private bool isInvulnerable = false;
    public float invulnerabilityDuration = 1f;
    AudioManager manager;
    // Start is called before the first frame update
    void Start()
    {
        anim.SetInteger("health", 3);
        GameObject managerobj = GameObject.FindWithTag("Audio");
        manager = managerobj.GetComponent<AudioManager>();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvulnerable)
        {
            manager.PlayRandomSound();
            health--;
            anim.SetInteger("health", health);
            StartCoroutine(InvulnerabilityCoroutine());
        }
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }
}
