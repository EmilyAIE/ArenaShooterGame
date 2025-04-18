using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class PlayerShoot : NetworkBehaviour
{
    [SerializeField] public ArenaGameInput playerControls;
    private InputAction m_fire;
    private InputAction m_swapWeapon;
    [SerializeField] private Camera m_playerCamera;

    [Header("Gun Details")]
    [SerializeField] private float m_maxShotDistance;
    [SerializeField] private Transform m_shotStartPosition;
    [SerializeField] private GameObject m_gunLine;


    [Space]
    //[SerializeField] private PlayerInventory m_inventory;

    //private HUDManager m_hudManager;

    private bool m_automaticFiring;

    //private PlayerStatus m_playerStatus;

    private void Awake()
    {
        //Initialise the input actions
        playerControls = new ArenaGameInput();

        //m_hudManager = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();

        //m_hudManager.TriggerSwap_toPistol();

        //m_playerStatus = GetComponent<PlayerStatus>();
    }

    private void OnEnable()
    {
        m_fire = playerControls.Player.Fire;
        m_fire.Enable();
        m_fire.performed += FirePistol;

        //m_swapWeapon = playerControls.Player.SwapGun;
        //m_swapWeapon.Enable();
        //m_swapWeapon.performed += SwapWeaponInput;
    }

    private void OnDisable()
    {
        m_fire.Disable();
        //m_swapWeapon.Disable();
    }

    private void Update()
    {
        //if (!m_playerStatus.isAlive) return;

        if (!isLocalPlayer)
            return;

        m_automaticFiring = m_fire.ReadValue<float>() > 0;

        if (m_automaticFiring)
        {
            FireFlameThrower();
        }
    }

    private void FirePistol(InputAction.CallbackContext context)
    {
        if (context.performed && isLocalPlayer)
        { 
            RaycastHit hit;

            Ray ray = m_playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(ray, out hit, m_maxShotDistance))
            {
                Debug.DrawRay(ray.origin, ray.direction * m_maxShotDistance, Color.green, 1f);
            }

            //if (m_inventory.guns.Count > 0)
            //{
            //    if (!m_inventory.guns[m_inventory.selectedGunIndex].automaticFire)
            //    {
            //        if (m_inventory.guns[m_inventory.selectedGunIndex].canFire && m_inventory.guns[m_inventory.selectedGunIndex].currentAmmo > 0)
            //            m_hudManager.TriggerShootAnimation();
            //        m_inventory.guns[m_inventory.selectedGunIndex].Fire();
            //    }
            //}
            //else
            //    return;
        }
    }

    private void FireFlameThrower()
    {
        //if (m_inventory.guns.Count > 0 && m_inventory.guns[m_inventory.selectedGunIndex].automaticFire)
        //{
        //    if (m_inventory.guns[m_inventory.selectedGunIndex].canFire && m_inventory.guns[m_inventory.selectedGunIndex].currentAmmo > 0)
        //        Debug.Log("Set flamethrower animation on");
        //    m_inventory.guns[m_inventory.selectedGunIndex].Fire();
        //}
    }

    public void CallReloadAnimation()
    {
        //m_hudManager.TriggerReloadAnimation();
    }

    private void SwapWeaponInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SwapWeapon();
        }
    }

    public void SwapWeapon()
    {
        //m_inventory.ChangeWeaponIndex();
        //
        //m_hudManager.AdjustAmmoText(m_inventory.guns[m_inventory.selectedGunIndex].currentAmmo);
        //
        //if (m_inventory.selectedGunIndex == 0)
        //{
        //    m_hudManager.TriggerSwap_toPistol();
        //}
        //else
        //{
        //    m_hudManager.TriggerSwap_toFlamethrower();
        //}
    }

    public void AmmoUpdate()
    {
        //m_hudManager.AdjustAmmoText(m_inventory.guns[m_inventory.selectedGunIndex].currentAmmo);
    }
}
