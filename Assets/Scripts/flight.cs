using UnityEngine;
using Mirror;

public class flight : NetworkBehaviour
{
    public float speed = 5;
    Vector2 velocity;
    //public Rigidbody drone;


    void Update()
    {
        // multiplayer: stops everyone from controlling everyone
        if (!hasAuthority) return;


        velocity.y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        velocity.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(velocity.x, 0, velocity.y);

        // fly up while holding space or down while holding shift
        if (Input.GetKey(KeyCode.Space))
        {
            //drone.position = drone.position + new Vector3(0f, 1f, 0f) * speed * Time.deltaTime;
            transform.Translate(0f, speed * Time.deltaTime, 0f);
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            //drone.position = drone.position + new Vector3(0f, -1f, 0f) * speed * Time.deltaTime;
            transform.Translate(0f, -speed * Time.deltaTime, 0f);
        }


    }
}
