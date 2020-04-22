using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{

    int score = 0;
    public Text scoreText;
    public LayerMask ground;
    public Collider2D collider2;

    public Animator animator;

    public Rigidbody2D rbody;

    public float jumpForce = 300;

    public float speed = 10;

    bool isJumping = false, isFalling = false;

    void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    bool isHunting;

    // Update is called once per frame
    void Update()
    {
        // 硬直
        if (isHunting)
        {
            if (Mathf.Abs(rbody.velocity.x) < 0.1f)
            {
                isHunting = false;
                animator.SetBool("hurt", false);
            }
            return;
        }
        // TODO: 直接掉下切换fall动画
        if (!isFalling && rbody.velocity.y < 0f && !collider2.IsTouchingLayers(ground))
        {
            isFalling = true;
            animator.SetBool("isFalling", true);
        }
        // 移动
        if (Input.GetAxis("Horizontal") != 0)
        {
            // Debug.Log(Input.GetAxis("Horizontal").ToString());
            rbody.velocity = new Vector2(Input.GetAxis("Horizontal") > 0 ? speed : -speed, rbody.velocity.y);
            // rbody.velocity = new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime, rbody.velocity.y);
            if (!isJumping && !isFalling)
            {
                animator.SetBool("isRunning", true);
            }
        }
        else
        {
            rbody.velocity = new Vector2(0, rbody.velocity.y);
            animator.SetBool("isRunning", false);
        }
        // 翻转水平方向
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
        }
        // 跳跃
        if (!isJumping && !isFalling && Input.GetButtonDown("Jump"))
        {
            // rbody.gravityScale = 1;
            rbody.AddForce(new Vector2(0, jumpForce));
            // rbody.velocity = new Vector2(rbody.velocity.x, jumpForce * Time.deltaTime);
            isJumping = true;
            animator.SetBool("isJumping", true);
            animator.SetBool("isRunning", false);
        }
        if (isJumping && rbody.velocity.y < 0)
        {
            isJumping = false;
            isFalling = true;
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }
        // if (isJumping && collider.IsTouchingLayers(ground)) {
        //     animator.SetBool("isFalling", false);

        // }
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        // Debug.Log(collisionInfo.collider.tag);
        // 落地
        if ((isJumping || isFalling) && collider2.IsTouchingLayers(ground))
        {
            isJumping = false;
            isFalling = false;
            // rbody.gravityScale = 20;
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
        // 碰到敌人干掉或掉血
        if (collisionInfo.collider.tag == "敌人")
        {
            if (isFalling)
            {
                // Destroy(collisionInfo.collider.gameObject);
                EnimyMove enemy = collisionInfo.gameObject.GetComponent<EnimyMove>();
                enemy.Hurt();
                addScore(200);
                return;
            }
            // 受伤
            animator.SetBool("hurt", true);
            DIRECTION dir = collider2.transform.position.x < collisionInfo.transform.position.x ? DIRECTION.RIGHT : DIRECTION.LEFT;
            rbody.AddForce(new Vector2(dir == DIRECTION.RIGHT ? -50f : 50f, 0f));
            isHunting = true;
        }
    }

    private void addScore(int _score)
    {
        score += _score;
        scoreText.text = score.ToString();
    }

    enum DIRECTION { LEFT, RIGHT };

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 吃樱桃，加分
        if (other.tag.Equals("樱桃"))
        {
            Debug.Log("碰到樱桃，摧毁对象，加分！");
            addScore(100);
            Destroy(other.gameObject);
        }
        if (other.tag.Equals("死亡区域"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    void FixedUpdate()
    {

    }
}
