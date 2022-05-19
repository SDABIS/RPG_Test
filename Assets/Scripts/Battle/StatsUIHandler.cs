using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsUIHandler : MonoBehaviour
{
    [SerializeField] UIStatInfo statInfoPrefab;
    private List<UIStatInfo> statSlots = new List<UIStatInfo>();

    public void Init() {
        for(int i = 0; i< BattleManager.Instance.Players.Count; i++) {
            UIStatInfo statInfo = Instantiate(statInfoPrefab, transform);

            BattlePlayer player = BattleManager.Instance.Players[i];
            statInfo.Player = player.Stats;
            statSlots.Add(statInfo);
        }
    }

}
