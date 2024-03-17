using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    Start = 0,
    Game = 1
}

public class Loading : Singleton<Loading>
{
    private void Awake() => Initialize(this);

    public void LoadScene(Scenes scene) => StartCoroutine(LoadSceneCoroutine(scene));

    private IEnumerator LoadSceneCoroutine(Scenes scene)
    {
        DontDestroyOnLoadUI.instance.loadingBackgroundImageActive = true;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync((int)scene);

        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress < 0.9f)
            {
                DontDestroyOnLoadUI.instance.ProgressBarFillAmount = asyncOperation.progress;
            }
            else
            {
                float timer = 0f;

                while (timer < 1f)
                {
                    DontDestroyOnLoadUI.instance.ProgressBarFillAmount = Mathf.Lerp(0.9f, 1f, timer);

                    timer += Time.deltaTime;

                    yield return null;
                }

                asyncOperation.allowSceneActivation = true;

                DontDestroyOnLoadUI.instance.loadingBackgroundImageActive = false;
                DontDestroyOnLoadUI.instance.ProgressBarFillAmount = 0f;

                yield break;
            }

            yield return null;
        }
    }
}
