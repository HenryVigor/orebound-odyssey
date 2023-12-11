using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public float speed;

    private float distance;

    // Prevent moving when damaged
    public bool canMove = true;

    // For idle roaming - enemies moving slightly when not following player
    public bool canRoam = true;
    private bool isRoaming = false;
    private Vector2 roamPos;

    // Set up Animator Controller
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // Get animation component
        animator = GetComponent<Animator>();
        // Set default idle pos
        roamPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, Player.Obj.transform.position);
        Vector2 direction = Player.Obj.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Angle for the player rotation 

        if(distance < 4 && canMove)
        {
            isRoaming = false;
            transform.position = Vector2.MoveTowards(this.transform.position, Player.Obj.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);

            // Set animation values
            animator.SetBool("Is Moving", true);
            animator.SetFloat("Angle", angle);
        } 
        else if (canRoam && isRoaming)
        {
            Vector2 roamDir = roamPos - new Vector2(transform.position.x, transform.position.y);
            roamDir.Normalize();
            float roamAngle = Mathf.Atan2(roamDir.y, roamDir.x) * Mathf.Rad2Deg;
            transform.position = Vector2.MoveTowards(transform.position, roamPos, speed/2 * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * roamAngle);
            // Set animation values
            if (Mathf.Approximately(transform.position.x, roamPos.x) && Mathf.Approximately(transform.position.y, roamPos.y))
            {
                animator.SetBool("Is Moving", false);
            }
            else
            {
                animator.SetBool("Is Moving", true);
                animator.SetFloat("Angle", roamAngle);
            }
        }
        else if (canRoam && distance < 24 && canMove && !isRoaming)
        {
            roamPos = new Vector2(transform.position.x + Random.Range(-1.5f, 1.5f), transform.position.y + Random.Range(-1.5f, 1.5f));
            isRoaming = true;
            Invoke("ResetRoaming", Random.Range(6f, 12f));
        }
        else {
            animator.SetBool("Is Moving", false);
        }

    }

    private void ResetRoaming()
    {
        isRoaming = false;
    }


}
