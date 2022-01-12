using UnityEngine;

public class Gravity : MonoBehaviour
{

    
    //private float jumpheight = 3f;
    private float gravity;   
    private bool isGrounded;
    private Vector3 velocity;    
    private float groundDistance = 0.2f;
    private float actualYVelocity;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundChecker;
    // Start is called before the first frame update


    private void OnRenderObject()
    {
        gravity = -49.05f;
    }


    private void Update()
    {
        GForce();
    }
    
    public void GForce()
    {
        
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);
        velocity.y += gravity * Time.deltaTime;
        actualYVelocity = velocity.y;
        velocity.y = Mathf.Clamp(actualYVelocity, -80f, 100f);
        if (!isGrounded && velocity.y >= -80f)
        {
            Updown();

        }else if(isGrounded && velocity.y <= 0f)
        {

            velocity.y = -0.1f;
        }
    }
    
    public void Updown()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);
        this.transform.position += velocity * Time.deltaTime;
    }
}
