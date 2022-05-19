using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenuUIHandler : MonoBehaviour
{
    [SerializeField] SubMenuSlot subMenuSlotPrefab;
    private List<SubMenuSlot> subMenuSlots = new List<SubMenuSlot>();

    [SerializeField] RectTransform contentPos;

    private void Awake() {
        for (int i = 0; i < 50; i++)
        {
            SubMenuSlot newSlot = Instantiate(subMenuSlotPrefab, contentPos);
            newSlot.gameObject.SetActive(false);
            subMenuSlots.Add(newSlot);
            
        }
    }

    public void ShowPlayerSubActions(BattlePlayer player, SubMenuAction action)
    {
        List<BattleAction> subActions = action.GetSubActions(player);

        foreach (SubMenuSlot slot in subMenuSlots)
        {
            slot.gameObject.SetActive(false);   
        }

        for (int i = 0; i < subActions.Count; i++)
        {
            BattleAction subAct = subActions[i];
            SubMenuSlot slot = subMenuSlots[i];
            
            slot.Init(player, subAct);
        }
    }
}
