using System;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public short roomID = 13;
    public Vector2 playerCorpsePosition;

    private short deathRoomID = -1;
    private int corpseCurrency;


    public short DeathRoomID
    {
        get { return deathRoomID; }

        private set { deathRoomID = value; }
    }
    public int CorpseCurrency
    {
        get { return corpseCurrency; }
        private set { corpseCurrency = value; }
    }


    private void Start()
    {
        if (instance == null)
            instance = this;


        LoadPlayerData();
        LoadPlayerInventory();
        LoadPlayerUpgrades();
        LoadVisitedRooms();
        LoadDeathInfo();
        LoadGateConditions();
        LoadBeatenBosses();

    }

    public void SavePlayerData()
    {
        PlayerData playerData = new PlayerData()
        {
            maxHealTriesCount = PlayerHealthController.instance.maxHealTriesCount,
            lastSafePosition = CharacterController2D.instance.lastSafePosition,
            roomID = roomID
        };

        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/playerData.txt", json);

        SavePlayerCurrency();
    }
    public void LoadPlayerData()
    {
        LoadPlayerCurrency();

        if (!File.Exists(Application.persistentDataPath + "/playerData.txt"))
        {
            roomID = 13;

            AllRoomsContainer.instance.CreateRoom(roomID);

            return;
        }

        string json = File.ReadAllText(Application.persistentDataPath + "/playerData.txt");

        PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);

        PlayerHealthController.instance.maxHealTriesCount = playerData.maxHealTriesCount;
        CharacterController2D.instance.gameObject.transform.position = playerData.lastSafePosition;
        roomID = playerData.roomID;
        Camera.main.transform.position = playerData.lastSafePosition;

        AllRoomsContainer.instance.CreateRoom(roomID);
    }

    public void SavePlayerCurrency()
    {
        PlayerPrefs.SetInt("Currency", PlayerCurrencyController.instance.currency);

        PlayerPrefs.Save();
    }
    private void LoadPlayerCurrency()
    {
        PlayerCurrencyController.instance.currency = PlayerPrefs.GetInt("Currency");

        CurrencyUIController.instance.UpdateCurrencyWindowValueInvoker();
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
        SavePlayerCurrency();

        PlayerPrefs.Save();
    }
    public void LoadPlayerInventory()
    {
        int inventoryDataLength = PlayerPrefs.GetInt("InventoryItemsLength");

        for (int i = 0; i < inventoryDataLength; i++)
        {
            InventoryManager.instance.Add(AllItemsContainer.instance.itemsDictionary[PlayerPrefs.GetInt("Item" + i)]);
        }

        LoadPlayerCurrency();
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

        SavePlayerCurrency();

        PlayerPrefs.Save();
    }
    public void LoadPlayerUpgrades()
    {
        int upgradesDataLength = PlayerPrefs.GetInt("UpgradesDataLength");


        for (int i = 0; i < upgradesDataLength; i++)
        {
            UpgradeSystemManager.instance.AddAbility(AllUpgradesContainer.instance.upgradesDictionary[PlayerPrefs.GetInt("Upgrade" + i)]);
        }

        LoadPlayerCurrency();
    }

    public void SaveVisitedRooms()
    {
        int roomsDataLength = AllRoomsContainer.instance.visitedRooms.Count;

        int iteration = 0;

        PlayerPrefs.SetInt("RoomsDataLength", roomsDataLength);

        foreach (int id in AllRoomsContainer.instance.visitedRooms)
        {
            PlayerPrefs.SetInt("Room" + iteration, id);
            iteration++;
        }


        PlayerPrefs.Save();
    }
    public void LoadVisitedRooms()
    {
        int roomsDataLength = PlayerPrefs.GetInt("RoomsDataLength");


        for (int i = 0; i < roomsDataLength; i++)
        {
            int? id = PlayerPrefs.GetInt("Room" + i);
            if (id != null)
                AllRoomsContainer.instance.visitedRooms.Add((int)id);
        }
    }

    public void SaveDeathInfo(int roomID)
    {
        PlayerPrefs.SetInt("DeathRoomID", roomID);

        DeathRoomID = (short)roomID;

        PlayerPrefs.SetInt("CorpseCurrency", PlayerCurrencyController.instance.currency);

        PlayerPrefs.SetFloat("LastSafePositionX", CharacterController2D.instance.lastSafePosition.x);

        PlayerPrefs.SetFloat("LastSafePositionY", CharacterController2D.instance.lastSafePosition.y);

        PlayerPrefs.Save();
    }
    public void LoadDeathInfo()
    {
        DeathRoomID = (short)PlayerPrefs.GetInt("DeathRoomID");
        CorpseCurrency = PlayerPrefs.GetInt("CorpseCurrency");

        playerCorpsePosition = new Vector2(PlayerPrefs.GetFloat("LastSafePositionX"), PlayerPrefs.GetFloat("LastSafePositionY"));
    }

    public void SaveGateCondition()
    {
        int gatesDataLength = GatesController.instance.gates.Count;

        int iteration = 0;

        foreach (bool gate in GatesController.instance.gates)
        {
            PlayerPrefs.SetInt("Gate" + iteration, Convert.ToInt16(gate));
            iteration++;
        }


        PlayerPrefs.Save();
    }
    private void LoadGateConditions()
    {
        int gatesDataLength = GatesController.instance.gates.Count;

        for (int i = 0; i < gatesDataLength; i++)
        {
            GatesController.instance.gates[i] = Convert.ToBoolean(PlayerPrefs.GetInt("Gate" + i));
        }

    }

    public void SaveBeatenBosses()
    {
        int bossesDataLength = BossManager.instance.beatenBosses.Count;

        int iteration = 0;

        foreach (int bossId in BossManager.instance.beatenBosses)
        {
            PlayerPrefs.SetInt("Boss" + iteration, bossId);
            iteration++;
        }

        PlayerPrefs.SetInt("BossesDataLength", bossesDataLength);

        PlayerPrefs.Save();
    }

    private void LoadBeatenBosses()
    {
        int bossesDataLength = PlayerPrefs.GetInt("BossesDataLength");

        for (int i = 0; i < bossesDataLength; i++)
        {
            BossManager.instance.beatenBosses.Add(PlayerPrefs.GetInt("Boss" + i));
        }
    }

    private struct PlayerData
    {
        public float maxHealth;
        public float maxMana;
        public short maxHealTriesCount;
        public short roomID;
        public Vector2 lastSafePosition;
    }
}