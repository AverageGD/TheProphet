using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private void Start()
    {
        if (instance == null)
            instance = this;


        LoadPlayerData();
    }

    public void SavePlayerData()
    {
        PlayerData playerData = new PlayerData()
        {
            maxHealth = PlayerHealthController.instance.maxHealth,
            maxMana = PlayerManaController.instance.maxMana,
            maxHealTriesCount = PlayerHealthController.instance.maxHealTriesCount,
            currency = PlayerCurrencyController.instance.currency,
            lastSafePosition = CharacterController2D.instance.lastSafePosition
        };

        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.dataPath + "/playerData.txt", json);
    }

    public void LoadPlayerData()
    {
        if (!File.Exists(Application.dataPath + "/playerData.txt"))
        {
            File.Create(Application.dataPath + "/playerData.txt");
            return;
        }

        string json = File.ReadAllText(Application.dataPath + "/playerData.txt");

        PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);

        PlayerHealthController.instance.maxHealth = playerData.maxHealth;
        PlayerManaController.instance.maxMana = playerData.maxMana;
        PlayerHealthController.instance.maxHealTriesCount = playerData.maxHealTriesCount;
        PlayerCurrencyController.instance.currency = playerData.currency;
        CharacterController2D.instance.gameObject.transform.position = playerData.lastSafePosition;
    }

    private struct PlayerData
    {
        public float maxHealth;
        public float maxMana;
        public short maxHealTriesCount;
        public Vector2 lastSafePosition;
        public int currency;
    }
}
