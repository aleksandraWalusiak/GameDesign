using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private float originalX;
    private float maxOffset = 5.0f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;
    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);

    public Animator GoombaAnimator;

    private Rigidbody2D enemyBody;




    public void GameRestart()
    {
        transform.localPosition = startPosition;
        originalX = transform.position.x;
        moveRight = -1;
        GoombaAnimator.SetTrigger("GameRestart");
        ComputeVelocity();
    }

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        // get the starting position
        originalX = transform.position.x;
        ComputeVelocity();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        Vector2 contactNormal = other.transform.position - transform.position;
        if (other.gameObject.CompareTag("Player") && !(Mathf.Abs(contactNormal.x) > 0.5f))
        {
            GoombaAnimator.Play("GoombaStompped");
        }
    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);

    }
    void Movegoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);

    }

    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {// move goomba

            Movegoomba();

        }
        else
        {
            // change direction
            moveRight *= -1;
            ComputeVelocity();
            Movegoomba();
        }

    }
}