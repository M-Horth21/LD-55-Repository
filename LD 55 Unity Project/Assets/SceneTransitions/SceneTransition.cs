using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    Animator animator;

    public static SceneTransition instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);

        animator = GetComponent<Animator>();
    }
    public void TransitionToScene(string sceneToTransitionTo)
    {
        animator.SetBool("Transition", true);
        StartCoroutine(Transition(sceneToTransitionTo));
    }
    IEnumerator Transition(string sceneToTransitionTo)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneToTransitionTo);
    }
}
