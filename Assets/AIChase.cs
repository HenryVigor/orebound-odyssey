using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public float speed;

    private float distance;

    // Set up Animator Controller
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // Get animation component
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, Player.Obj.transform.position);
        Vector2 direction = Player.Obj.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //Angle for the player rotation 

        if(distance < 4)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, Player.Obj.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);

            // Set animation values
            animator.SetBool("Is Moving", true);
            animator.SetFloat("Angle", angle);
        } else {
            animator.SetBool("Is Moving", false);
        }

    }
}
