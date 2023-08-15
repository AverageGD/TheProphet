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

        AllRoomsContainer.instance.CreateRoom(roomID);
    }

    public void SavePlayerInventory()
    {
        string inventoryData = "";

        foreach (Item item in InventoryManager.instance.items)
        {
            inventoryData += Convert.ToChar(item.id);
        }

        PlayerPrefs.SetString("InventoryItems", inventoryData);

        PlayerPrefs.Save();
    }
    public void LoadPlayerInventory()
    {
        string inventoryData = PlayerPrefs.GetString("InventoryItems");

        for (int i = 0; i < inventoryData.Length; i++)
        {
            InventoryManager.instance.Add(AllItemsContainer.instance.itemsDictionary[inventoryData[i]]);
        }
    }

    public void SavePlayerUpgrades()
    {
        string upgradesData = "";

        foreach (UpgradeAbility upgrade in UpgradeSystemManager.instance.availableUpgrades)
        {
            upgradesData += Convert.ToChar(upgrade.id);
        }

        PlayerPrefs.SetString("Upgrades", upgradesData);

        PlayerPrefs.Save();
    }
    public void LoadPlayerUpgrades()
    {
        string upgradesData = PlayerPrefs.GetString("Upgrades");

        for (int i = 0; i < upgradesData.Length; i++)
        {
            UpgradeSystemManager.instance.AddAbility(AllUpgradesContainer.instance.upgradesDictionary[upgradesData[i]]);
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
