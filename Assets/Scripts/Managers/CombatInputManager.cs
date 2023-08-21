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
    [SerializeField] private float targetMinSize = 0.5f;
    [SerializeField] private float targetMaxSize = 3.0f;
    [SerializeField] private float targetGrowthRate = 0.1f;
    private WeaponMode weaponMode;
    private int weaponRingIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
        if (pointerHeld)
        {
            if (targetGrowing)
            {
                targetCurrentSize += targetGrowthRate;
                if (targetCurrentSize >= targetMaxSize)
                {
                    targetCurrentSize = targetMaxSize;
                    targetGrowing = false;
                }
            }
            else
            {
                targetCurrentSize -= targetGrowthRate;
                if (targetCurrentSize <= targetMinSize)
                {
                    targetCurrentSize = targetMinSize;
                    targetGrowing = true;
                }
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
            targetCurrentSize = targetMinSize;
            targetGrowing = true;
            pointerHeld = true;
            target.localScale = new Vector3(targetCurrentSize, targetCurrentSize, 1);
            target.Find("Sprite").gameObject.SetActive(pointerHeld);
        }
        if (Input.GetMouseButtonUp(0))
        {
            pointerHeld = false;
            target.Find("Sprite").gameObject.SetActive(pointerHeld);
            StartAttack(playerObject.position, worldPos, targetCurrentSize/2, angle);
        }
    }

    public void SetWeaponMode()
    {
        weaponMode = new WeaponMode(PlayerManager.Weapon.CenterFragment, PlayerManager.Weapon.RingFragments[weaponRingIndex], PlayerManager.Weapon.BaseFragment);
    }
    public void UpdateWeaponMode()
    {
        weaponMode.RingFragment = PlayerManager.Weapon.RingFragments[weaponRingIndex];
    }

    public void StartAttack(Vector2 playerPosition, Vector2 targetPosition, float size, float angle)
    {
        Transform projectileTransform = Instantiate(projectileTemplate, transform).GetComponent<Transform>();
        projectileTransform.GetComponent<ProjectileController>().Shoot(weaponMode, playerPosition, targetPosition, size);
        projectileTransform.gameObject.SetActive(true);
    }
}
