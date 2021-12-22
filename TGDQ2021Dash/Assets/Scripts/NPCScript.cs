using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CreatureScript))]
public class NPCScript : MonoBehaviour
{
    public bool hasBeenTagged = false;

    // Wander process: move in random direction for 2 seconds, stay in place for 1 second; repeat.
    public float wanderDuration = 2.0f;
    public float waitDuration = 0.5f;

    public float wanderDurationMin = 0.2f;
    public float wanderDurationMax = 2.0f;

    // state vars
    public Vector2 wanderDirection = Vector2.left;
    public NPCState currentState = NPCState.VIBING;
    public float currentStateTimer = 0f;

    // physics materials
    public PhysicsMaterial2D creatureMaterial;
    public PhysicsMaterial2D frozenMaterial;

    public enum NPCState
    {
        WANDERING,
        VIBING,
        FROZEN
    }

    SpriteRenderer spriteRenderer;
    CreatureScript creatureScript;
    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        creatureScript = GetComponent<CreatureScript>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        currentStateTimer += Time.deltaTime;
        if(currentState == NPCState.VIBING)
        {
            // vibe
            creatureScript.inputMovement = Vector2.zero;

            if(currentStateTimer >= waitDuration)
            {
                currentStateTimer = 0f;
                currentState = NPCState.WANDERING;

                wanderDirection = new Vector2(
                        Random.Range(-1f, 1f),
                        Random.Range(-1f, 1f)
                        );

                wanderDuration = Random.Range(wanderDurationMin, wanderDurationMax);
            }
        }
        else if(currentState == NPCState.WANDERING)
        {
            // wander
            creatureScript.inputMovement = wanderDirection;

            if (currentStateTimer >= wanderDuration)
            {
                currentStateTimer = 0f;
                currentState = NPCState.VIBING;
            }
        }


        if (hasBeenTagged)
        {
            Tag();
        }
        else
        {
            Untag();
        }
    }

    public void Tag()
    {
        hasBeenTagged = true;
        spriteRenderer.color = new Color(
            spriteRenderer.color.r,
            spriteRenderer.color.g,
            spriteRenderer.color.b,
            0.5f
            );
        Freeze();
    }

    public void Untag()
    {
        hasBeenTagged = false;
        spriteRenderer.color = new Color(
            spriteRenderer.color.r,
            spriteRenderer.color.g,
            spriteRenderer.color.b,
            1f
            );
        Unfreeze();
    }

    public void Freeze()
    {
        currentState = NPCState.FROZEN;
        creatureScript.inputMovement = Vector2.zero;
        creatureScript.Freeze();
        rigidbody2D.sharedMaterial = frozenMaterial;
    }

    public void Unfreeze()
    {
        if (currentState == NPCState.FROZEN)
        {
            currentState = NPCState.VIBING;
        }
        creatureScript.Unfreeze();
        rigidbody2D.sharedMaterial = creatureMaterial;
    }
}
