using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackCanvas : MonoBehaviour {
    Player player;
    public Button atk_button_one, atk_button_two, atk_button_three;
    public Button atk_one_up, atk_two_up, atk_three_up;
    public Button atk_one_down, atk_two_down, atk_three_down;
	// Use this for initialization
	void Start () {
        player = Player.player;
        atk_button_one.onClick.AddListener (() => ProcAttack(1));
        atk_button_two.onClick.AddListener (() => ProcAttack(2));
        atk_button_three.onClick.AddListener (() => ProcAttack(3));

        atk_one_up.onClick.AddListener (() => CycleAttackSlot (1, true));
        atk_two_up.onClick.AddListener (() => CycleAttackSlot (2, true));
        atk_three_up.onClick.AddListener (() => CycleAttackSlot (3, true));

        atk_one_down.onClick.AddListener (() => CycleAttackSlot (1, false));
        atk_two_down.onClick.AddListener (() => CycleAttackSlot (2, false));
        atk_three_down.onClick.AddListener (() => CycleAttackSlot (3, false));
    }

    void ProcAttack (int i)
    {
        player.ProcCombatProtocol(i);
    }

    void CycleAttackSlot (int slot, bool up)
    {
        player.AttackSlotCycle (slot, up);
    }

    public void SetAttackIcon (int slot, Sprite icon)
    {
        switch (slot)
        {
            case 1:
                atk_button_one.transform.GetChild (0).GetComponent<Image> ().sprite = icon;
                break;
            case 2:
                atk_button_two.transform.GetChild (0).GetComponent<Image> ().sprite = icon;
                break;
            case 3:
                atk_button_three.transform.GetChild (0).GetComponent<Image> ().sprite = icon;
                break;
        }
    }
}
