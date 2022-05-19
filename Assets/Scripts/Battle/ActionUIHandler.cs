using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionUIHandler : MonoBehaviour
{
    [SerializeField] Button actionButtonPrefab;
    private List<Button> actionButtons = new List<Button>();
    [SerializeField] int maxButtonCount = 4;

    private GridLayoutGroup layoutGroup;

    private void Start() {
        layoutGroup = GetComponent<GridLayoutGroup>();

        for(int i = 0; i<maxButtonCount; i++) {
            Button newButton = Instantiate(actionButtonPrefab, layoutGroup.transform);
            newButton.gameObject.SetActive(false);
            actionButtons.Add(newButton);
        }
    }

    public void ShowPlayerActions(BattlePlayer player) {

        List<BattleAction> actions = player.Actions;
        foreach (Button actionButton in actionButtons)
        {
            actionButton.gameObject.SetActive(false);
            actionButton.onClick.RemoveAllListeners();
        }
        
        for(int i = 0; i< actions.Count; i++)
        {
            BattleAction act = actions[i];
            Button actBtn = actionButtons[i];
            
            actBtn.GetComponentInChildren<Text>().text = act.ActionName;
            actBtn.gameObject.SetActive(true);
            
            actBtn.onClick.AddListener(() => {
                player.SelectedAction = act;
            });
        }
    }

    public void HidePlayerActions() {
        foreach (Button actionButton in actionButtons)
        {
            actionButton.gameObject.SetActive(false);
            actionButton.onClick.RemoveAllListeners();
        }
    }
}
