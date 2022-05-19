using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentParty : Singleton<PersistentParty>
{
    private List<CharacterStats> characters;
    private Inventory inventory;
    private Zone currentZone;

    public List<CharacterStats> Characters => characters;
    public Inventory Inventory => inventory;
    public Zone CurrentZone => currentZone;

    [SerializeField] CharacterDatabase initialPlayerDB;
    [SerializeField] Zone initialZone;

    private void Start() {
        DontDestroyOnLoad(gameObject);

        characters = FileManager.Instance.Data.characters;
        inventory = FileManager.Instance.Data.inventory;
        currentZone = FileManager.Instance.Data.currentZone;

        if (characters == null) {
            characters = new List<CharacterStats>();

            foreach (CharacterInfo initialPlayer in initialPlayerDB.CharacterInfos)
            {
                characters.Add(new CharacterStats(initialPlayer));
            }
            
        }
        if(currentZone == null) {
            currentZone = initialZone;
        }
    }

}
