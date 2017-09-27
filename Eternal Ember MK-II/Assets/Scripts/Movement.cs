using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour {

    Rigidbody rb;
    NavMeshAgent agent;
    Animator anim;
    bool isPlayer;

    public float walkSpeed, runSpeed, stealthSpeed;
    public float rotationSpeed;

    public Coroutine movementRoutine;

    public delegate void OnArrived();
    public OnArrived onArrived;

    private void Awake()
    {
        rb = GetComponent<Rigidbody> ();
        agent = GetComponent<NavMeshAgent> ();
        anim = GetComponent<Animator> ();
    }
    // Use this for initialization
    void Start () {
		if (GetComponent<Player>())
        {
            isPlayer = true;
        }
        if (agent != null)
        {
            agent.enabled = false;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isPlayer)
        {
            PlayerMovement ();
        }
	}

    void PlayerMovement ()
    {
        Vector2 input = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
        Vector2 modInput = input * Time.deltaTime;

        if (movementRoutine != null)
        {
            if (input.magnitude > 0.75f)
            {
                StopCoroutine (movementRoutine);
                agent.enabled = false;
                rb.isKinematic = false;
            }
            else
            {
                return;
            }
        }

        anim.SetFloat ("forwardSpeed", input.y);
        rb.MovePosition (transform.position + transform.forward * modInput.y * runSpeed);
        rb.MoveRotation (transform.rotation * Quaternion.Euler (new Vector3 (0, modInput.x * rotationSpeed, 0)));
    }

    public void ProcMove(Vector3 point, float stoppingDistance)
    {
        if (movementRoutine != null)
        {
            StopCoroutine (movementRoutine);
        }
        movementRoutine = StartCoroutine (MoveToPoint (point, stoppingDistance));
    }

    IEnumerator MoveToPoint(Vector3 point, float stoppingDistance)
    {

        anim.SetFloat ("forwardSpeed", 1);
        if (agent != null)
        {
            rb.isKinematic = true;
            agent.enabled = true;
            agent.stoppingDistance = stoppingDistance;
            agent.SetDestination (point);

            while (Vector3.Distance (transform.position, point) > stoppingDistance)
            {
                //print ("Still going");
                yield return null;
            }
        }
        else
        {
            while (Quaternion.Angle (transform.rotation, Quaternion.LookRotation (point - transform.position)) > 5)
            {
                transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (point - transform.position), 0.4f);
                yield return null;
            }
            while (Vector3.Distance (transform.position, point) > stoppingDistance)
            {
                transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (point - transform.position), 0.4f);
                transform.position = (point - transform.position).normalized * runSpeed * Time.deltaTime;
                yield return null;
            }

        }

        if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(point - transform.position)) > 5)
        {
            float x = 0;
            while (x < 1)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(point - transform.position), 0.6f);
                x += Time.deltaTime;
                yield return null;
            }
        }
        movementRoutine = null;
        rb.isKinematic = false;
        agent.ResetPath ();
        //print ("done");
        onArrived ();
        yield break;
    }

    public void StopMoving()
    {
        if (movementRoutine != null)
        {
            StopCoroutine (movementRoutine);
        }
        if (agent !=null)
        {
            agent.velocity = Vector3.zero;
            agent.ResetPath ();

        }
        anim.SetFloat ("forwardSpeed", 0);
    }
}
