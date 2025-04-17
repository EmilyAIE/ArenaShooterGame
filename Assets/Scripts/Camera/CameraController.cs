using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CameraController : NetworkBehaviour
{
    public ArenaGameInput cameraControls;
    private InputAction m_look;

    [Header("Camera Details")]
    [SerializeField] private float m_cameraSpeed;
    private Vector2 m_cameraDirection = Vector2.zero;
    [SerializeField] private float m_verticalClamp = 80f;
    private float m_cameraVerticalRotation;
    private float m_cameraHorizontalRotation;

    [Space]
    [SerializeField] private Transform m_player;

    //private PlayerStatus m_playerStatus;

    public GameObject cameraHolder;
    [SerializeField]
    private Vector3 offset;

    public override void OnStartAuthority()
    {
        cameraHolder.SetActive(true);
    }

    private void Awake()
    {
        //Initialise the input actions
        cameraControls = new ArenaGameInput();
    }

    private void OnEnable()
    {
        m_look = cameraControls.Player.Look;
        m_look.Enable();

        //m_playerStatus = GetComponentInParent<PlayerStatus>();
    }

    private void OnDisable()
    {
        m_look.Disable();
    }

    private void Start()
    {

    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        //cameraHolder.transform.position = transform.position + offset;

        /*if (Time.timeScale == 1 && m_playerStatus.isAlive)
        {
            //Update camera direction vector
            m_cameraDirection = m_look.ReadValue<Vector2>() * m_cameraSpeed;

            //Adjust the value of the camera vertical rotation
            m_cameraVerticalRotation -= m_cameraDirection.y;

            //Clamp vertical movement so to avoid gimblelock issues
            m_cameraVerticalRotation = Mathf.Clamp(m_cameraVerticalRotation, -m_verticalClamp, m_verticalClamp);

            //Set the value of the vertical roation
            transform.localEulerAngles = Vector3.right * m_cameraVerticalRotation;

            //Set the value of the horizontal rotation
            m_player.Rotate(Vector3.up * m_cameraDirection.x);
        }
        else
            return;*/

        //Update camera direction vector
        m_cameraDirection = m_look.ReadValue<Vector2>() * m_cameraSpeed;

        //Adjust the value of the camera vertical rotation
        m_cameraVerticalRotation -= m_cameraDirection.y;

        //Clamp vertical movement so to avoid gimblelock issues
        m_cameraVerticalRotation = Mathf.Clamp(m_cameraVerticalRotation, -m_verticalClamp, m_verticalClamp);

        //Set the value of the vertical roation
        cameraHolder.transform.localEulerAngles = Vector3.right * m_cameraVerticalRotation;

        //Set the value of the horizontal rotation
        m_player.Rotate(Vector3.up * m_cameraDirection.x);
    }
}
