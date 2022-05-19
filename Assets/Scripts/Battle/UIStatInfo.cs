using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatInfo : MonoBehaviour
{
    private CharacterStats player;
    public CharacterStats Player {
        get => player;
        set {
            player = value;

            player.OnHpManaChange.AddListener(UpdateText);
            UpdateText();
        }
    }
    [SerializeField] Text nameText;
    [SerializeField] Text healthText;
    [SerializeField] Text manaText;

    private void UpdateText() {
        nameText.text = player.CharacterInfo.name;
        healthText.text = "" + player.CurrentHp + " / " + player.MaxHP;
        manaText.text = "" + player.CurrentMana + " / " + player.MaxMana;
    }
}
