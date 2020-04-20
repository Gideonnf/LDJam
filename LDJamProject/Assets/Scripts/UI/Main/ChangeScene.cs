using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    public void GoBackToGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
