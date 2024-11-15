using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public static MovementScript Instance;
    public float amplitude;
    public float frequency;
    public float speed;
    public float accelerationSpeed;
    public float currentSpeed;
    public float rotationSpeed;
    public float horisontal, verttical, upAndDown;
    public float normalSpeed;
    public float slowerSpeed;
    public Animator animator;
    private Transform camera;
    private Vector3 direction;
    private Rigidbody rigidBody;


    private bool isFlyingSlow;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        camera = Camera.main.transform;
        rigidBody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        InputCheck();
        FlyMovement();
        RotationFlyChange();
        ToggleSpeed();
    }

    private void InputCheck()
    {
        horisontal = Input.GetAxisRaw("Horizontal");
        verttical = Input.GetAxisRaw("Vertical");
        upAndDown = Input.GetAxisRaw("UpAndDown");
    }

    private void FlyMovement()
    {
        currentSpeed = rigidBody.velocity.magnitude;
        direction = camera.transform.forward * verttical + camera.transform.right * horisontal + Vector3.up * upAndDown;
        direction.Normalize();
        rigidBody.AddForce(direction * accelerationSpeed * Time.deltaTime, ForceMode.Acceleration);
    }

    private void RotationFlyChange()
    {
        if (direction.magnitude != 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, camera.rotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void MakeSpeedSlower()
    {
        accelerationSpeed = slowerSpeed;
    }

    public void MakeSpeedNormal()
    {
        accelerationSpeed = normalSpeed;
    }

    private void ToggleSpeed()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isFlyingSlow)
        {
            isFlyingSlow = true;
            accelerationSpeed = 1000;
        }

        else
        {
            isFlyingSlow = false;
            accelerationSpeed = 2000;
        }
    }
}
