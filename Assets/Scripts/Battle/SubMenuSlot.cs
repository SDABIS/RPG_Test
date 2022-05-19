using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMenuSlot : MonoBehaviour
{
    private Button button;
    [SerializeField] Text text;
    private BattleAction action;

    private void Awake() {
        button = GetComponent<Button>();
    }

    public void Init(BattlePlayer player, BattleAction action) {
        gameObject.SetActive(true);

        this.action = action;

        text.text = action.ActionName;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => {
            player.SelectedAction = action;
        });
    }

}
