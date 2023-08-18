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
    [SerializeField] private float targetMinSize = 0.5f;
    [SerializeField] private float targetMaxSize = 3.0f;
    [SerializeField] private float targetGrowthRate = 0.1f;

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
        }
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        target.position = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 playerDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerObject.position;
        float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg - 45;
        playerObject.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
