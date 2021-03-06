using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CreatureAnimState
{
    IDLE,
    RUNNING,
    HIT,
    FROZEN
}

public class CreatureScript : MonoBehaviour
{
    // State variables
    public bool playerControlled = true;
    public Vector2 inputMovement = new Vector2();
    public CreatureAnimState currentAnimState = CreatureAnimState.IDLE;

    // Static values
    public float moveForce = 10f;
    public float dashForce = 20f;

    // References to components/objects
    public Rigidbody2D rigidbody;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator != null)
        {
            UpdateAnimations();
        }
        if (spriteRenderer != null)
        {
            if (inputMovement.x < -0.05f)
            {
                spriteRenderer.flipX = true;
            }
            else if (inputMovement.x > 0.05f)
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    void FixedUpdate()
    {
        ApplyMoveForce();
        //if (inputMovement.magnitude > 0)
        //{
        //    ApplyDashForce();
        //    inputMovement = Vector2.zero;
        //}
    }

    public void ApplyMoveForce()
    {
        rigidbody.AddForce(inputMovement * moveForce, ForceMode2D.Force);
    }

    public void ApplyDashForce()
    {
        rigidbody.AddForce(inputMovement.normalized * dashForce, ForceMode2D.Impulse);
    }

    private void UpdateAnimations()
    {
        //if (rigidbody.velocity.magnitude > 5f)
        if (currentAnimState != CreatureAnimState.FROZEN)
        {
            if (inputMovement.magnitude > 0.05f)
            {
                currentAnimState = CreatureAnimState.RUNNING;
            }
            else
            {
                currentAnimState = CreatureAnimState.IDLE;
            }
            animator.SetInteger("animation_state", (int)currentAnimState);
        }
    }

    public void SetSloMo(bool isSloMoOnNow)
    {
        if (isSloMoOnNow)
        {
            animator.speed = 0.5f;
        }
        else
        {
            animator.speed = 1f;
        }
    }

    public void Freeze()
    {
        animator.speed = 0f;
        currentAnimState = CreatureAnimState.FROZEN;
    }

    public void Unfreeze()
    {
        animator.speed = 1f;
        currentAnimState = CreatureAnimState.IDLE;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collided creature!");
        if (this.playerControlled)
        {
            Debug.Log("Player collided!");
            // If this is the player, and just collided with an NPC creature, tag them!
            NPCScript collidedCreature = collision.gameObject.GetComponent<NPCScript>();
            if(collidedCreature != null)
            {
                Debug.Log("Tag!");
                collidedCreature.Tag();
                //Destroy(collidedCreature.gameObject);
            }
        }
    }
}
