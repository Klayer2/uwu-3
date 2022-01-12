//using System.Collections.Generic;
//using UnityEngine;

//public class ThirdPersonMovementController : MonoBehaviour
//{
//    private CharacterController Controller;
//    private Transform cam;
//    public float speed;
//    private bool running;
//    private float turnSmoothTime = 0.05f;
//    private float turnSmoothVelocity;
//    public float jumpheight = 6f;
//    public float gravity;
//    public bool isGrounded;
//    public float untilJumpResetTime;
//    public Vector3 velocity;
//    [SerializeField] private float groundDistance = 0.1f;
//    [SerializeField] private LayerMask groundMask;
//    [SerializeField] private Transform groundChecker;
//    [SerializeField] private List<Collider> colliders2;

//    private void OnRenderObject()
//    {
//        gravity = -49.05f;
//    }

//    private void Awake()
//    {
//        cam = cam ?? Camera.Main;
//        Controller = gameObject.GetComponent<CharacterController>();
//        colliders2 = new List<Collider>();


//    }

//    void Update()
//    {
//        velocity.y = Mathf.Clamp(velocity.y, -20, 1000);

//        if((isGrounded) && (velocity.y < 0))
//        {

//            velocity.y = -2f;

//        }

//        if (untilJumpResetTime >= 0f)
//        {
//            untilJumpResetTime -= Time.deltaTime;
//        }

//        float horizontal = Input.GetAxisRaw("Horizontal");
//        float vertical = Input.GetAxisRaw("Vertical");
//        Vector3 direction = new Vector3(horizontal, 0f, vertical);

//        if ((Input.GetKey("left shift")) || (running == true))
//        {
//            speed = 30f;
//            running = true;
//        }



//        if (direction.magnitude >= 0.1f)
//        {

//            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
//            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
//            transform.rotation = Quaternion.Euler(0f, angle, 0f);
//            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
//            Controller.Move(moveDir * speed * Time.deltaTime);

//        }

//        else

//        {
//            running = false;
//            speed = 12f;
//        }

//        if ((Input.GetAxisRaw("Jump")) != 0 && (isGrounded) && (untilJumpResetTime <= 0))
//        {
//            velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);
//            untilJumpResetTime = 0.5f;
//        }

//        velocity.y += gravity * Time.deltaTime;

//        Controller.Move(velocity * Time.deltaTime);

//    }

//}