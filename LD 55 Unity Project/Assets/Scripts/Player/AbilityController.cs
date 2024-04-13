using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AbilityController : MonoBehaviour
{
    [Header("UI Charge Indicators")]
    [SerializeField] RectTransform windIndicator;
    [SerializeField] RectTransform wallIndicator;



    [SerializeField] InputActionReference mouseHold;

    [SerializeField] Transform playerCursor;


    [Header("Ability Settings")]

    [SerializeField] PunchSettings punchSettings;
    [SerializeField] WindSettings windSettings;
    [SerializeField] WallSettings wallSettings;

    PunchAbility punchAbility;
    WindAbility windAbility;
    WallAbility wallAbility;


    List<IAbility> abilities = new List<IAbility>();

    private void Awake()
    {
        punchAbility = new PunchAbility(punchSettings);
        windAbility = new WindAbility(windSettings);
        wallAbility = new WallAbility(wallSettings);

        abilities.Add(punchAbility);
        abilities.Add(windAbility);
        abilities.Add(wallAbility);
    }

    // Null ability exists so I don't have to do a null check when deactivating.
    IAbility currAbility = new NullAbility();

    public void CastAbility(int abilityNumber)
    {
        switch (abilityNumber)
        {
            case 1:
                if (punchAbility.recharge < .99f) break;
                Debug.Log($"Casting ability {abilityNumber}!");
                currAbility.Deactivate();
                currAbility = punchAbility;
                currAbility.Activate();
                break;

            case 2:
                if (windAbility.recharge < .99f) break;
                Debug.Log($"Casting ability {abilityNumber}!");
                currAbility.Deactivate();
                currAbility = windAbility;
                currAbility.Activate();
                break;

            case 3:
                if (wallAbility.recharge < .99f) break;
                Debug.Log($"Casting ability {abilityNumber}!");
                currAbility.Deactivate();
                currAbility = wallAbility;
                currAbility.Activate();
                break;

            default:
                Debug.LogWarning($"Improper Ability Number: {abilityNumber}");
                break;
        }
    }

    private void Update()
    {
        foreach(IAbility ability in abilities)
        {
            ability.Tick();
        }

        windIndicator.localScale = new Vector3(windIndicator.localScale.x, windAbility.recharge, windIndicator.localScale.z);
        wallIndicator.localScale = new Vector3(windIndicator.localScale.x, wallAbility.recharge, windIndicator.localScale.z);

        currAbility.Logic(playerCursor.position);
    }




    private void OnEnable()
    {
        mouseHold.action.canceled += CancelAbility;

    }
    private void OnDisable()
    {
        mouseHold.action.canceled -= CancelAbility;

    }

    private void CancelAbility(InputAction.CallbackContext obj)
    {
        currAbility.Deactivate();
        currAbility = new NullAbility();
        Debug.Log("Canceled from mouse action");
    }
}
