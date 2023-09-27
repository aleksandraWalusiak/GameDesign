using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Animator BoxAnimator1;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.CompareTag("Player") && col.contacts[0].normal.y > 0.5f)
        {

            // update animator state
            BoxAnimator1.SetBool("Hit", true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
