using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void GoGameScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
