using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public Animator BrickAnimator;
    // Start is called before the first frame update
    void Start()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // play death animation
            BrickAnimator.Play("HitBrick");

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
