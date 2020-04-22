using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagelControll : MonoBehaviour
{
    public float bottomY = 2f;
    public float topY = 5f;
    Rigidbody2D rbody;
    float speed = 3f;
    // float distance = 100f;
    // Start is called before the first frame update
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rbody.velocity = new Vector2(0, speed);
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > topY && speed > 0)
        {
            speed = -speed;
            rbody.velocity = new Vector2(0, speed);
        }
        else if (transform.position.y < bottomY && speed < 0)
        {
            speed = -speed;
            rbody.velocity = new Vector2(0, speed);
        }
    }
}
