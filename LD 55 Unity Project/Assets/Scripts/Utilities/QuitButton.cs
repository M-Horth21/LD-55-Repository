using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Quit);
    }

    public void Quit()
    {

        if(SceneManager.GetActiveScene().name != "TitleScreen")
        {
            AudioManager.instance.StopSong("BattleTheme");
            AudioManager.instance.StopSong("VictoryTheme");
            SceneTransition.instance.TransitionToScene("TitleScreen");
            return;
        }

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
