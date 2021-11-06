using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // State variables
    public bool playerControlled = true;
    public Vector2 inputMovement = new Vector2();

    // Static values
    public float moveForce = 10f;
    public float dashForce = 20f;

    // References to components/objects
    public Rigidbody2D rigidbody;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ApplyMoveForce();
    }

    public void ApplyMoveForce()
    {
        rigidbody.AddForce(inputMovement * moveForce, ForceMode2D.Force);
    }

    public void ApplyDashForce()
    {
        rigidbody.AddForce(inputMovement * dashForce, ForceMode2D.Impulse);
    }
}
