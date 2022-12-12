using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CharacterMov : MonoBehaviour
{

    Animator animator;
    public Transform cam;
    [SerializeField]
    private TextMeshPro UseText;
    int isWalkingHash;
    int isRunningHash;
    int isStrafingRightHash;
    int isStrafingLeftHash;
    int isWalkingBackHash;
    PlayerInput input;
    Vector2 currentMovement;
    bool movementPressed;
    bool runPressed;
    private float MaxUseDistance = 6f;
    [SerializeField]
    private LayerMask UseLayers;

    void Awake()
    {

        input = new PlayerInput();

        input.CharacterControls.movement.performed += ctx =>
        {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = currentMovement.x != 0 || currentMovement.y != 0;
        };
        input.CharacterControls.run.performed += ctx => runPressed = ctx.ReadValueAsButton();
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isWalkingBackHash = Animator.StringToHash("isWalkingBack");
        isRunningHash = Animator.StringToHash("isRunning");
        isStrafingRightHash = Animator.StringToHash("isStrafingRight");
        isStrafingLeftHash = Animator.StringToHash("isStrafingLeft");
    }

    // Update is called once per frame
    void Update()
    {
        handleMovement();
        handleRotation();
        if (Input.GetKey(KeyCode.E))
        {
            handleUse();
        }

        if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, MaxUseDistance, UseLayers)&& hit.collider.TryGetComponent<Door>(out Door door))
        {
            if (door.IsOpen)
            {
                UseText.SetText("Close \"E\"\n\n");
            }
            else
            {
                UseText.SetText("Open \"E\"\n\n");
            }
            UseText.gameObject.SetActive(true);
            UseText.transform.position = hit.point - (hit.point - cam.position).normalized * 0.5f;
            UseText.transform.rotation = Quaternion.LookRotation((hit.point - cam.position).normalized);
        }
        else
        {
            UseText.gameObject.SetActive(false);
        }
    }

    void handleRotation()
    {
        //Vector3 currentPosition = transform.position;
        //Debug.Log("NYT   "+currentPosition);
        //Vector3 newPosition = new Vector3(currentMovement.x, 0, currentMovement.y);
        //Debug.Log("UUS---->  "+newPosition);
        //Vector3 positionToLookAt = currentPosition + newPosition;

        var kamera = cam.transform.rotation;
        var jtn = new Quaternion(0f,kamera.y,0f,kamera.w);
        transform.rotation = (jtn);

    }

    void handleMovement()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        if (movementPressed && !isWalking && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)&&!Input.GetKey(KeyCode.S))
        {
            animator.SetBool(isWalkingHash, true);
        }
        if (!movementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }
        if((runPressed && movementPressed)&&!isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }
        if ((!runPressed || !movementPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool(isStrafingRightHash, true);
        }
        else
        {
            animator.SetBool(isStrafingRightHash, false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool(isStrafingLeftHash, true);
        }
        else
        {
            animator.SetBool(isStrafingLeftHash, false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool(isWalkingBackHash, true);
        }
        else
        {
            animator.SetBool(isWalkingBackHash, false);
        }
    }

    void handleUse ()
    {
        if(Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, MaxUseDistance, UseLayers))
        {
            if (hit.collider.TryGetComponent<Door>(out Door door))
            {
                if (door.IsOpen)
                {
                    door.Close();
                }
                else
                {
                    door.Open(transform.position);
                }
            }
        }
    }

    void OnEnable()
    {
        input.CharacterControls.Enable();
    }

    void OnDisable()
    {
        input.CharacterControls.Disable();
    }


}
