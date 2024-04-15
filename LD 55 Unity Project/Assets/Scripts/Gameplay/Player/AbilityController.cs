using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AbilityController : MonoBehaviour
{
    [SerializeField] InputActionReference mouseHold;
    [SerializeField] Transform playerCursor;
    [SerializeField] Transform playerTransform;


    [Header("Game State")]
    [SerializeField] GameState gameState;


    [Header("")]

    [Header("UI Charge Indicators")]
    [SerializeField] RectTransform punchIndicator;
    [SerializeField] RectTransform windIndicator;
    [SerializeField] RectTransform wallIndicator;
    [SerializeField] RectTransform dashIndicator;
    [SerializeField] RectTransform stunIndicator;


    [Header("")]
    [Header("UI Selected Indicator")]
    [SerializeField] List<RectTransform> abilityPositions = new List<RectTransform>();
    [SerializeField] RectTransform selectionHover;



    [Header("")]
    [Header("Ability Settings")]

    [SerializeField] PunchSettings punchSettings;
    [SerializeField] WindSettings windSettings;
    [SerializeField] WallSettings wallSettings;
    [SerializeField] DashSettings dashSettings;
    [SerializeField] StunSettings stunSettings;


    PunchAbility punchAbility;
    WindAbility windAbility;
    WallAbility wallAbility;
    DashAbility dashAbility;
    StunAbility stunAbility;


    bool IsMouseOverUI() => EventSystem.current.IsPointerOverGameObject();

    List<IAbility> abilities = new List<IAbility>();

    private const int totalAbilities = 4;
    private int numAbilities = 0;

    private int abilityIndex = 0;

    private void Awake()
    {
        punchAbility = new PunchAbility(punchSettings);
        windAbility = new WindAbility(windSettings);
        wallAbility = new WallAbility(wallSettings);
        dashAbility = new DashAbility(dashSettings, playerTransform);
        stunAbility = new StunAbility(stunSettings);


        abilities.Add(punchAbility);
        abilities.Add(windAbility);
        abilities.Add(wallAbility);
        abilities.Add(dashAbility);
        abilities.Add(stunAbility);

        for(int i = 1; i <= totalAbilities; i++)
        {
            if(gameState.GetRankOfAbility(i) >= 1)
            {
                numAbilities++;
            }
        }

        if(numAbilities > 0)
        {
            abilityIndex = 1;
        }

    }

    // Null ability exists so I don't have to do a null check when deactivating.
    IAbility currAbility = new NullAbility();

    private void CastAbility(int abilityNumber)
    {
        if (abilityNumber == 6)
        {
            if (dashAbility.recharge < .99f) return;

            Debug.Log("Dashing");
            dashAbility.Activate();

            return;
        }

        switch (abilityNumber)
        {
            case 1:
                if (punchAbility.recharge < .99f) break;
                currAbility.Deactivate();
                currAbility = punchAbility;
                currAbility.Activate();
                break;

            case 2:
                if (windAbility.recharge < .99f) break;
                currAbility.Deactivate();
                currAbility = windAbility;
                currAbility.Activate();
                break;

            case 3:
                if (wallAbility.recharge < .99f) break;
                currAbility.Deactivate();
                currAbility = wallAbility;
                currAbility.Activate();
                break;
            case 4:
                if (stunAbility.recharge < .99f) break;
                currAbility.Deactivate();
                currAbility = stunAbility;
                currAbility.Activate();
                break;

            default:
                Debug.LogWarning($"Improper Ability Number: {abilityNumber}");
                break;
        }
    }

    private void Update()
    {

        Vector2 scrollVec = Mouse.current.scroll.ReadValue();
        float scroll = scrollVec.y;

        currAbility.Logic(playerTransform.position, playerCursor.position);

        foreach (IAbility ability in abilities)
        {
            ability.Tick();
        }

        windIndicator.localScale = new Vector3(windIndicator.localScale.x, windAbility.recharge, windIndicator.localScale.z);
        wallIndicator.localScale = new Vector3(wallIndicator.localScale.x, wallAbility.recharge, wallIndicator.localScale.z);
        punchIndicator.localScale = new Vector3(punchIndicator.localScale.x, punchAbility.recharge, punchIndicator.localScale.z);
        dashIndicator.localScale = new Vector3(dashIndicator.localScale.x, dashAbility.recharge, dashIndicator.localScale.z);
        stunIndicator.localScale = new Vector3(stunIndicator.localScale.x, stunAbility.recharge, stunIndicator.localScale.z);

        if (abilityIndex != 0)
        {
        if (scroll > 0)
        {
            var nextAbility = abilityIndex - 1;
            if (nextAbility == 0) nextAbility += numAbilities;
            SetCurrAbility(nextAbility);
        }
        else if (scroll < 0)
        {
            SetCurrAbility(((abilityIndex) % numAbilities) + 1);

        }
        }




    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(playerCursor.position, .5f);
    //}

    public void SetCurrAbility(int ability)
    {
        // dash ability. Do this one immediately and don't cancel other abilities.
        if (ability == 6) // 6 is dash
        {
            CastAbility(ability);
            return;
        }

        if(ability > numAbilities)
        {
            return;
        }

        abilityIndex = ability;

        selectionHover.transform.position = abilityPositions[ability - 1].position;
    }

    private void OnEnable()
    {
        mouseHold.action.canceled += CancelAbility;
        mouseHold.action.started += StartAbility;

    }

    private void OnDisable()
    {
        mouseHold.action.canceled -= CancelAbility;
        mouseHold.action.started -= StartAbility;

    }

    private void StartAbility(InputAction.CallbackContext obj)
    {
        if ( !IsMouseOverUI()) CastAbility(abilityIndex);
    }

    private void CancelAbility(InputAction.CallbackContext obj)
    {
        currAbility.Deactivate();
        currAbility = new NullAbility();
    }
}
