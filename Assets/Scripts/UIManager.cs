using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject pauseScreen;

    [Header("Screen Animation Parameters")]
    [SerializeField] private string animatorParameterClose = "close";
    [SerializeField] private float screenAnimationDuration = 1.5f;

    private bool canPause = true;

    private void Update()
    {
        if (!canPause) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseAndUnpauseGame();
        }
    }

    private void PauseAndUnpauseGame()
    {
        if (!canPause) return;

        canPause = false;

        if (Time.timeScale == 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;

            Animator screenAnimator = pauseScreen.GetComponent<Animator>();
            screenAnimator.SetTrigger(animatorParameterClose);

            StartCoroutine(PlayAndDeactivate(pauseScreen));
        }

        else
        {
            Cursor.lockState = CursorLockMode.None;

            pauseScreen.SetActive(true);

            StartCoroutine(PlayAndPauseGame());
        }
    }

    private IEnumerator PlayAndDeactivate(GameObject screen)
    {
        yield return new WaitForSeconds(screenAnimationDuration);

        canPause = true;
        screen.SetActive(false);
    }

    private IEnumerator PlayAndPauseGame()
    {
        yield return new WaitForSeconds(screenAnimationDuration);

        canPause = true;
        Time.timeScale = 0;
    }

    public void OnUnpause()
    {
        PauseAndUnpauseGame();
    }

    public void OnExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
