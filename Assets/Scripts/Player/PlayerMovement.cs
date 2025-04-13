using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{
    private Rigidbody m_rb;

    [Header("Movement Details")]
    [SerializeField] private float m_moveSpeed;
    [SerializeField] private float m_velocityMaxClamp;
    private Vector2 m_moveDirection = Vector2.zero;
    [Space]
    [SerializeField] private float m_jumpForce;

    [Header("Input")]
    [SerializeField] public ArenaGameInput playerControls;
    private InputAction m_move;
    private InputAction m_jump;

    private PlayerStatus m_playerStatus;

    [SerializeField] private bool m_allowedToJump = false;

    [SerializeField] private Transform m_cameraPos;

    private void Awake()
    {
        //Initialise the input actions
        playerControls = new ArenaGameInput();
    }

    private void OnEnable()
    {
        m_rb = GetComponent<Rigidbody>();

        m_playerStatus = GetComponent<PlayerStatus>();

        //Set the input action to the one listed in the InputActions
        m_move = playerControls.Player.Move;
        //Enable the input action
        m_move.Enable();

        m_jump = playerControls.Player.Jump;
        m_jump.Enable();
        m_jump.performed += Jump;        
    }

    private void OnDisable()
    {
        //Disable Input actions
        m_move.Disable();
        m_jump.Disable();        
    }

    private void Start()
    {
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (m_playerStatus.isAlive)
        {
            //Update move direction vector
            m_moveDirection = m_move.ReadValue<Vector2>();
        }
    }

    private bool isGrounded()
    {
        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;
        float distance = 1.01f;

        //bool onGround = Physics.SphereCast(origin, 0.5f, direction, out RaycastHit hit, distance);

        bool onGround = Physics.Raycast(origin, direction, distance);

        return onGround;
    }

    private void FixedUpdate()
    {
        //Add force to the player in an x and z to build up velocity (Doom-Like movement)
        //TODO: STOP SPEED FROM BEING ABLE TO GO SUPER FAST
        m_rb.AddRelativeForce(new Vector3(Mathf.Clamp(m_moveDirection.x * m_moveSpeed, -m_velocityMaxClamp, m_velocityMaxClamp), 0, Mathf.Clamp(m_moveDirection.y * m_moveSpeed, -m_velocityMaxClamp, m_velocityMaxClamp)), ForceMode.VelocityChange);
    }    

    private void Jump(InputAction.CallbackContext context)
    {
        if (m_allowedToJump)
        {
            if (isGrounded())
                m_rb.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }
    }
}
