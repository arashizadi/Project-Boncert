using UnityEngine;

public class flight : MonoBehaviour
{
    public float speed = 5;
    Vector2 velocity;
    public Rigidbody drone;


    void FixedUpdate()
    {
        velocity.y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        velocity.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(velocity.x, 0, velocity.y);



        if (Input.GetKey(KeyCode.Space))
        {
            drone.position = drone.position + new Vector3(0, 1F, 0) * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            drone.position = drone.position + new Vector3(0, -1, 0) * speed * Time.deltaTime;
        }




    }
}
