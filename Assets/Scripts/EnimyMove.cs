using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnimyMove : MonoBehaviour
{

    AudioSource audioSource;

    Animator animator;
    public Transform left, right;
    float leftX, rightX;
    private bool die;

    float speedX = -3f;

    public Rigidbody2D rbody;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        leftX = left.transform.position.x;
        rightX = right.transform.position.x;
        Destroy(left.gameObject);
        Destroy(right.gameObject);
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (die) return;
        rbody.velocity = new Vector2(speedX, rbody.velocity.y);
        if (speedX < 0 && transform.position.x <= leftX)
        {
            speedX = -speedX;
            transform.localScale = new Vector3(-1, 1, 1);
            rbody.velocity = new Vector2(speedX, rbody.velocity.y);
        }
        else if (speedX > 0 && transform.position.x >= rightX)
        {
            speedX = -speedX;
            transform.localScale = new Vector3(1, 1, 1);
            rbody.velocity = new Vector2(speedX, rbody.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        // if (collisionInfo.collider.tag == "Player")
        // {
        //     speedX = -speedX;
        // }
    }

    public void Hurt()
    {
        die = true;
        animator.SetTrigger("die");
        audioSource.Play();
        // Destroy();
        // TODO: 移除物理对象
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
