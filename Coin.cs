using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip CoinAudio;
    public AudioSource CoinSound;
    public Animator CoinAnimator;

    // Start is called before the first frame update
    void Start()
    {

    }

    void PlayCoinSound()
    {
        CoinSound.PlayOneShot(CoinAudio);

    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.CompareTag("Player") && col.contacts[0].normal.y > 0.5f && CoinAnimator.GetBool("Hit") == false)
        {


            // update animator state
            CoinAnimator.SetBool("Hit", true);

        }



    }



    // Update is called once per frame
    void Update()
    {

    }
}
