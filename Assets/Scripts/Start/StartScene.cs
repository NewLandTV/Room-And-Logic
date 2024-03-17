using System.Collections;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    [SerializeField]
    private GameObject touchToStartText;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        Screen.SetResolution(3040, 1440, true);
    }

    private IEnumerator Start()
    {
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }

        touchToStartText.SetActive(false);

        Loading.instance.LoadScene(Scenes.Game);
    }
}
