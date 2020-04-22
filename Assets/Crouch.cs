using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    Animator animator;
    public Collider2D headColl;
    public Transform ceiling;
    public LayerMask ground;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            animator.SetBool("crouch", true);
            headColl.enabled = false;
        }
        if (Input.GetButtonUp("Crouch") && animator.GetBool("crouch"))
        {
            if (!Physics2D.OverlapCircle(ceiling.position, 0.2f, ground))
            {
                animator.SetBool("crouch", false);
                headColl.enabled = true;
            }
        }
    }
}
