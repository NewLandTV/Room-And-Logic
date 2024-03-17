using UnityEngine;
using UnityEngine.UI;

public class DontDestroyOnLoadUI : Singleton<DontDestroyOnLoadUI>
{
    [SerializeField]
    private Image progressBarImage;
    [SerializeField]
    private GameObject loadingBackgroundImage;

    public float ProgressBarFillAmount
    {
        get => progressBarImage.fillAmount;
        set => progressBarImage.fillAmount = value;
    }
    public bool loadingBackgroundImageActive
    {
        get => loadingBackgroundImage.activeSelf;
        set => loadingBackgroundImage.SetActive(value);
    }

    private void Awake() => Initialize(this);
}
