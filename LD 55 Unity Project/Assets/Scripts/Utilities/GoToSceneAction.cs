using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Scene Management/Scene Change Action")]
public class GoToSceneAction : ScriptableObject
{
  /// <summary>
  /// The scene that this action will load.
  /// </summary>
  [Scene, SerializeField]
  string _targetScene;

  [SerializeField, Tooltip("if checked, the scene will be loaded additively, " +
"instead of replacing the previous scene")]
  bool _additiveScene;

  public void GoToScene()
  {
    LoadSceneMode mode = _additiveScene ? LoadSceneMode.Additive : LoadSceneMode.Single;
        SceneTransition.instance.TransitionToScene(_targetScene);
    //SceneManager.LoadScene(_targetScene, mode);
  }
}