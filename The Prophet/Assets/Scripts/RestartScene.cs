using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    private void Update()
    {

        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            && Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();

            System.IO.File.Delete(Application.persistentDataPath + "/playerData.txt");

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }
    }
}
