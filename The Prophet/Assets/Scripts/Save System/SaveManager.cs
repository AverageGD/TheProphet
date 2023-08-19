using System;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public short roomID = 13;


    private void Start()
    {
        if (instance == null)
            instance = this;

        LoadPlayerData();
        LoadPlayerInventory();
        LoadPlayerUpgrades();

    }

    public void SavePlayerData()
    {
        PlayerData playerData = new PlayerData()
        {
            maxHealth = PlayerHealthController.instance.maxHealth,
            maxMana = PlayerManaController.instance.maxMana,
            maxHealTriesCount = PlayerHealthController.instance.maxHealTriesCount,
            currency = PlayerCurrencyController.instance.currency,
            lastSafePosition = CharacterController2D.instance.lastSafePosition,
            roomID = roomID
        };

        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.dataPath + "/playerData.txt", json);
    }
    public void LoadPlayerData()
    {
        if (!File.Exists(Application.dataPath + "/playerData.txt"))
        {
            roomID = 13;

            AllRoomsContainer.instance.CreateRoom(roomID);

            return;
        }

        string json = File.ReadAllText(Application.dataPath + "/playerData.txt");

        PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);

        PlayerHealthController.instance.maxHealth = playerData.maxHealth;
        PlayerManaController.instance.maxMana = playerData.maxMana;
        PlayerHealthController.instance.maxHealTriesCount = playerData.maxHealTriesCount;
        PlayerCurrencyController.instance.currency = playerData.currency;
        CharacterController2D.instance.gameObject.transform.position = playerData.lastSafePosition;
        roomID = playerData.roomID;
        Camera.main.transform.position = playerData.lastSafePosition;

        AllRoomsContainer.instance.CreateRoom(roomID);
    }

    public void SavePlayerInventory()
    {
        int inventoryDataLength = InventoryManager.instance.items.Count;

        int iteration = 0;

        foreach (Item item in InventoryManager.instance.items)
        {
            PlayerPrefs.SetInt("Item" + iteration, item.id);
            iteration++;
        }

        PlayerPrefs.SetInt("InventoryItemsLength", inventoryDataLength);

        PlayerPrefs.Save();
    }
    public void LoadPlayerInventory()
    {
        int inventoryDataLength = PlayerPrefs.GetInt("InventoryItemsLength");

        for (int i = 0; i < inventoryDataLength; i++)
        {
            InventoryManager.instance.Add(AllItemsContainer.instance.itemsDictionary[PlayerPrefs.GetInt("Item" + i)]);
        }
    }

    public void SavePlayerUpgrades()
    {
        int upgradesDataLength = UpgradeSystemManager.instance.availableUpgrades.Count;

        int iteration = 0;

        foreach (UpgradeAbility upgrade in UpgradeSystemManager.instance.availableUpgrades)
        {
            PlayerPrefs.SetInt("Upgrade" + iteration, upgrade.id);
            iteration++;
        }

        PlayerPrefs.SetInt("UpgradesDataLength", upgradesDataLength);

        PlayerPrefs.Save();
    }
    public void LoadPlayerUpgrades()
    {
        int upgradesDataLength = PlayerPrefs.GetInt("UpgradesDataLength");


        for (int i = 0; i < upgradesDataLength; i++)
        {
            UpgradeSystemManager.instance.AddAbility(AllUpgradesContainer.instance.upgradesDictionary[PlayerPrefs.GetInt("Upgrade" + i)]);
        }
    }

    private struct PlayerData
    {
        public float maxHealth;
        public float maxMana;
        public short maxHealTriesCount;
        public short roomID;
        public Vector2 lastSafePosition;
        public int currency;
    }
}
