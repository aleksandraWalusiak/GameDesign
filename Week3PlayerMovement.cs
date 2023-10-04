using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Week3PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody2D marioBody;
    public float upSpeed = 10;
    private bool onGroundState = true;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    public TextMeshProUGUI scoreText;
    public GameObject enemies;
    public JumpOverGoomba jumpOverGoomba;

    public GameObject GameOverScreen;

    public Animator marioAnimator;
    public AudioSource marioAudio;

    public AudioSource marioDeathAudio;

    public AudioClip marioDeath;
    public Transform gameCamera;
    public GameManager GameManager;
    int collisionLayerMask = (1 << 3) | (1 << 6) | (1 << 7);

    //public List<Animator> CoinAnimators = new List<Animator>();


    // state
    [System.NonSerialized]
    public bool alive = true;

    public float deathImpulse;
    //public Animator CoinAnimator;
    //public Animator QuestionAnimator;


    void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }

    void GameOverScene()
    {
        // stop time
        Time.timeScale = 0.0f;
        // set gameover scene

        GameManager.GameOver(); // replace this with whichever way you triggered the game over screen for Checkoff 1
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 contactNormal = other.transform.position - transform.position;
        if (other.gameObject.CompareTag("Enemy") && alive && (Mathf.Abs(contactNormal.x) > 0.5f))
        {
            Debug.Log("Collided with goomba!");
            // play death animation
            marioAnimator.Play("MarioDie");
            marioDeathAudio.PlayOneShot(marioDeathAudio.clip);
            alive = false;

        }




    }


    void PlayJumpSound()
    {
        // play jump sound
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    /*public void RestartButtonCallback(int input)
    {
        Debug.Log("Restart!");
        // reset everything
        ResetGame();
        GameOverScreen.SetActive(false);
        // resume time
        Time.timeScale = 1.0f;
    }*/

    /*public void DisplayGameOver()
    {
        GameOverScreen.SetActive(true);
    }*/

    public void ResetGame()
    {
        // reset position
        marioBody.transform.position = new Vector3(-5.33f, -4.69f, 0.0f);
        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;

        // reset animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;

        // reset camera position
        gameCamera.position = new Vector3(0, 0, -10);



        /*// reset Goomba
        foreach (Transform eachChild in enemies.transform)
        {
            eachChild.transform.localPosition = eachChild.GetComponent<EnemyMovement>().startPosition;
        }*/



        /*foreach (Animator animator in CoinAnimators)
        {

            animator.SetBool("Hit", false);
            animator.SetTrigger("GameRestart");
        }*/

        //CoinAnimator.SetBool("Hit", false);
        // CoinAnimator.SetTrigger("GameRestart");
        // QuestionAnimator.SetBool("Hit", false);
        // QuestionAnimator.SetTrigger("GameRestart");


    }



    void OnCollisionEnter2D(Collision2D col)
    {

        if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) & !onGroundState)
        {
            onGroundState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);

        }
    }



    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator.SetBool("onGround", onGroundState);

    }

    // Update is called once per frame
    void Update()
    {
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }

    void FlipMarioSprite(int value)
    {
        if (value == -1 && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.velocity.x > 0.05f)
                marioAnimator.SetTrigger("onSkid");

        }

        else if (value == 1 && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.velocity.x < -0.05f)
                marioAnimator.SetTrigger("onSkid");
        }
    }
    public float maxSpeed = 20;

    // FixedUpdate may be called once per frame. See documentation for details.
    private bool moving = false;
    void FixedUpdate()
    {
        if (alive && moving)
        {
            Move(faceRightState == true ? 1 : -1);

        }
    }

    void Move(int value)
    {

        Vector2 movement = new Vector2(value, 0);
        // check if it doesn't go beyond maxSpeed
        if (marioBody.velocity.magnitude < maxSpeed)
            marioBody.AddForce(movement * speed);
    }

    public void MoveCheck(int value)
    {
        if (value == 0)
        {
            moving = false;
        }
        else
        {
            FlipMarioSprite(value);
            moving = true;
            Move(value);
        }
    }

    private bool jumpedState = false;

    public void Jump()
    {
        if (alive && onGroundState)
        {
            // jump
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpedState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);

        }
    }

    public void JumpHold()
    {
        if (alive && jumpedState)
        {
            // jump higher
            marioBody.AddForce(Vector2.up * upSpeed * 30, ForceMode2D.Force);
            jumpedState = false;

        }
    }




}
