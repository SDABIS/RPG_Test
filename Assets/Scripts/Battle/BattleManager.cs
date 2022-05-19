using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleManager : Singleton<BattleManager>
{
    List<BattleCharacter> characters = new List<BattleCharacter>();
    List<BattlePlayer> players = new List<BattlePlayer>();
    List<BattleEnemy> enemies = new List<BattleEnemy>();

    public List<BattlePlayer> Players => players;

    private EnemyParty enemyParty;

    [SerializeField] List<Transform> PlayerPlaceholders = new List<Transform>();
    [SerializeField] List<Transform> EnemyPlaceholders = new List<Transform>();
    [SerializeField] int maxPlayers = 3;
    [SerializeField] int requiredSpeedCounter = 100;
    [SerializeField] float turnDelay = 0.5f;

    [SerializeField] GameObject turnIndicatorPrefab;
    private GameObject turnIndicator;
    [SerializeField] GameObject targetIndicatorPrefab;
    private GameObject targetIndicator;

    [SerializeField] ActionUIHandler actionUI;
    [SerializeField] StatsUIHandler statsUI;

    [SerializeField] SubMenuUIHandler subMenuUI;

    private BattleCharacter currentCharacter;
    // Start is called before the first frame update
    protected void Start()
    {
        for (int i = 0; (i < PersistentParty.Instance.Characters.Count) && (i < maxPlayers); i++)
        {
            CharacterStats character = PersistentParty.Instance.Characters[i];
            BattlePlayer battlePlayer = Instantiate((BattlePlayer)character.CharacterInfo.BattleCharacter, PlayerPlaceholders[i].position, Quaternion.identity);

            battlePlayer.Stats = character;
            foreach (BattleAction act in character.CharacterInfo.Actions)
            {
                battlePlayer.AddAction(act);
            }

            characters.Add(battlePlayer);
            players.Add(battlePlayer);

        }

        enemyParty = PersistentParty.Instance.CurrentZone.GetRandomEnemyParty();

        for (int i = 0; (i < enemyParty.Enemies.Count); i++)
        {
            CharacterInfo enemy = enemyParty.Enemies[i];
            BattleEnemy battleEnemy = Instantiate((BattleEnemy)enemy.BattleCharacter, EnemyPlaceholders[i].position, Quaternion.identity);

            battleEnemy.Stats = new CharacterStats(enemy);
            foreach (BattleAction act in enemy.Actions)
            {
                battleEnemy.AddAction(act);
            }

            characters.Add(battleEnemy);
            enemies.Add(battleEnemy);

        }

        turnIndicator = Instantiate(turnIndicatorPrefab);
        turnIndicator.SetActive(false);

        targetIndicator = Instantiate(targetIndicatorPrefab);
        targetIndicator.SetActive(false);

        statsUI.Init();
        EventBroker.Instance.OnCharacterDeath.AddListener(HandleCharacterDeath);

    }

    private void HandleCharacterDeath(BattleCharacter character)
    {
        characters.Remove(character);
        if (character is BattleEnemy)
        {
            enemies.Remove((BattleEnemy)character);

            targetIndicator.transform.parent = null;
            targetIndicator.SetActive(false);

            Destroy(character.gameObject);


            if (enemies.Count == 0)
            {
                HandleBattleEnd();
            }
        }
        else if (character is BattlePlayer) players.Remove((BattlePlayer)character);


    }

    private void HandleBattleEnd()
    {
        int exp = enemyParty.CalculateEXPreward();
        foreach (BattlePlayer player in players)
        {
            player.Stats.Exp += exp;
        }

        EventBroker.Instance.CallBattleEnd();
    }


    // Update is called once per frame
    void Update()
    {
        if (currentCharacter == null)
        {
            ChooseNextTurn();
        }
    }

    private void ChooseNextTurn()
    {
        while (currentCharacter == null)
        {

            characters = characters.OrderByDescending(chr => chr.SpeedCounter).ToList();

            if (characters[0].SpeedCounter > requiredSpeedCounter)
            {
                currentCharacter = characters[0];
                currentCharacter.DecreaseSpeedCounter(requiredSpeedCounter);
                currentCharacter.OnTurnEnd.AddListener(HandleTurnEnd);

                turnIndicator.SetActive(true);
                turnIndicator.transform.parent = currentCharacter.gameObject.transform;
                turnIndicator.transform.localPosition = new Vector3(-1, 0, 0);

                break;
            }

            foreach (BattleCharacter chr in characters)
            {
                chr.IncreaseSpeedCounter();
            }
        }

        currentCharacter.ExecuteTurn();
    }

    private void HandleTurnEnd()
    {
        currentCharacter.OnTurnEnd.RemoveAllListeners();
        Invoke("StartNewTurn", turnDelay);
    }

    private void StartNewTurn()
    {
        currentCharacter = null;
    }

    public void ShowPlayerActions(BattlePlayer player)
    {
        actionUI.ShowPlayerActions(player);
    }

    public void ShowPlayerSubActions(BattlePlayer player, SubMenuAction action)
    {
        subMenuUI.gameObject.SetActive(true);
        subMenuUI.ShowPlayerSubActions(player, action);
    }

    public void HidePlayerActions()
    {
        actionUI.HidePlayerActions();
    }

    public void HidePlayerSubActions()
    {
        subMenuUI.gameObject.SetActive(false);
    }

    public BattleCharacter ChangeCurrentTarget(BattleCharacter target, int selectChange, TargetChooseAction.TargetType type)
    {
        BattleCharacter newTarget = null;
        List<BattleCharacter> targetList = null;

        switch (type)
        {
            case TargetChooseAction.TargetType.ENEMY:
                targetList = enemies.ToList<BattleCharacter>();
                break;
            case TargetChooseAction.TargetType.PLAYER:
                targetList = players.ToList<BattleCharacter>();
                break;
            case TargetChooseAction.TargetType.ALL:
                targetList = characters;
                break;

        }

        if (target == null) newTarget = targetList[0];
        else
        {

            int targetIndex = targetList.IndexOf(target);
            targetIndex = (targetIndex + selectChange) % targetList.Count;
            if (targetIndex < 0) targetIndex += targetList.Count;

            newTarget = targetList[targetIndex];

        }

        targetIndicator.SetActive(true);
        targetIndicator.transform.parent = newTarget.gameObject.transform;
        targetIndicator.transform.localPosition = new Vector3(-1, 0, 0);


        return newTarget;
    }

    public void EndTarget()
    {
        targetIndicator.SetActive(false);
    }
}
