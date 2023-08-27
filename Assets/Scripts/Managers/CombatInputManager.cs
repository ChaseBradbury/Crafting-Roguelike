using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatInputManager : MonoBehaviour
{
    private bool pointerHeld = false;
    private float targetCurrentSize;
    private bool targetGrowing = true;
    [SerializeField] private Transform target;
    [SerializeField] private Transform playerObject;
    [SerializeField] private Transform projectileTemplate;
    private float targetGrowthRateCurrent;
    private WeaponMode weaponMode;
    private int weaponRingIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        targetGrowthRateCurrent = PlayerManager.Weapon.RingFragments[weaponRingIndex].fragmentOptions.targetGrowthRate;
    }

    void FixedUpdate()
    {
        if (pointerHeld)
        {
            if (targetGrowing)
            {
                RingFragmentOptionSO fragmentOptions = PlayerManager.Weapon.RingFragments[weaponRingIndex].fragmentOptions;
                targetCurrentSize += targetGrowthRateCurrent;
                targetGrowthRateCurrent *= fragmentOptions.targetGrowthAcceleration;
                if (targetCurrentSize >= fragmentOptions.targetMaxSize)
                {
                    targetCurrentSize = fragmentOptions.targetMaxSize;
                    targetGrowing = false;
                }
            }
            else
            {
                // targetCurrentSize -= targetGrowthRate;
                // if (targetCurrentSize <= targetMinSize)
                // {
                //     targetCurrentSize = targetMinSize;
                //     targetGrowing = true;
                // }
            }
            target.localScale = new Vector3(targetCurrentSize, targetCurrentSize, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponMode == null && PlayerManager.Weapon != null)
        {
            SetWeaponMode();
        }

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        target.position = worldPos;

        Vector2 playerDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerObject.position;
        float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        playerObject.rotation = Quaternion.AngleAxis(angle - 45, Vector3.forward);

        if (Input.GetMouseButtonDown(0))
        {
            targetCurrentSize = PlayerManager.Weapon.RingFragments[weaponRingIndex].fragmentOptions.targetMinSize;
            targetGrowing = true;
            pointerHeld = true;
            target.localScale = new Vector3(targetCurrentSize, targetCurrentSize, 1);
            target.Find("Sprite").gameObject.SetActive(pointerHeld);
            targetGrowthRateCurrent = PlayerManager.Weapon.RingFragments[weaponRingIndex].fragmentOptions.targetGrowthRate;
        }
        if (Input.GetMouseButtonUp(0))
        {
            pointerHeld = false;
            target.Find("Sprite").gameObject.SetActive(pointerHeld);
            StartAttack(playerObject.position, worldPos, targetCurrentSize/2);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateWeaponMode(++weaponRingIndex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpdateWeaponMode(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UpdateWeaponMode(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UpdateWeaponMode(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UpdateWeaponMode(3);
        }
    }

    public void SetWeaponMode()
    {
        CenterFragmentSO center = PlayerManager.Weapon.CenterFragment;
        RingFragmentSO ring = PlayerManager.Weapon.RingFragments[weaponRingIndex];
        BaseFragmentSO baseFragment = PlayerManager.Weapon.BaseFragment;
        weaponMode = new WeaponMode(center, ring, baseFragment);
    }
    public void UpdateWeaponMode(int index)
    {
        index = index % PlayerManager.Weapon.RingFragments.Length;
        weaponRingIndex = index;
        weaponMode.RingFragment = PlayerManager.Weapon.RingFragments[weaponRingIndex];
        targetGrowthRateCurrent = PlayerManager.Weapon.RingFragments[weaponRingIndex].fragmentOptions.targetGrowthRate;
    }

    public void StartAttack(Vector2 playerPosition, Vector2 targetPosition, float size)
    {
        float strength = 2 * size / PlayerManager.Weapon.RingFragments[weaponRingIndex].fragmentOptions.targetMaxSize;
        Transform projectileTransform = Instantiate(projectileTemplate, transform).GetComponent<Transform>();
        projectileTransform.GetComponent<ProjectileController>().Shoot(weaponRingIndex, playerPosition, targetPosition, size, strength);
        projectileTransform.gameObject.SetActive(true);
    }
}
