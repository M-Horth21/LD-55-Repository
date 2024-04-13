using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Quit);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
