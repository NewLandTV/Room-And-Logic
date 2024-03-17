#if UNITY_EDITOR

using System.Collections;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class DebugX : MonoBehaviour
{
    [SerializeField, Description("If enabled start and process a debug modules.")]
    private bool enableDebugMode;

    [Header("Game")]
    [SerializeField]
    private bool hideCursor;
    [SerializeField]
    private GameObject postProcess;

    [Header("Status")]
    [SerializeField]
    private bool showFPS;

    [Header("Key")]
    [SerializeField]
    private KeyCode quitKey;
    [SerializeField]
    private KeyCode togglePostProcessingKey;

    // Debug mode only variables
    private float deltaTime;

    private Player player;
    private Rigidbody playerRigidbody;
    private float playerWalkSpeed;
    private float playerRotateSpeed;
    private float playerFireRate;
    private bool fire;
    private float currentFireRate;
    private Bullet bulletPrefab;

    private Camera mainCamera;

    private WaitForFixedUpdate waitTimeFixedDeltaTime;

    private void Awake()
    {
        if (!enableDebugMode)
        {
            return;
        }

        if (hideCursor)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        player = FindObjectOfType<Player>();
        playerRigidbody = player.GetComponent<Rigidbody>();
        playerWalkSpeed = player.WalkSpeed;
        playerRotateSpeed = player.RotateSpeed;
        playerFireRate = player.FireRate;
        bulletPrefab = player.BulletPrefab;

        mainCamera = Camera.main;

        waitTimeFixedDeltaTime = new WaitForFixedUpdate();
    }

    private IEnumerator Start()
    {
        if (!enableDebugMode)
        {
            yield break;
        }

        StartCoroutine(UpdateFrame());

        while (true)
        {
            playerRigidbody.position += (player.transform.right * Input.GetAxisRaw("Horizontal") + player.transform.forward * Input.GetAxisRaw("Vertical")).normalized * playerWalkSpeed * Time.fixedDeltaTime;

            if (Input.GetAxisRaw("Mouse X") != 0f)
            {
                playerRigidbody.rotation *= Quaternion.Euler(new Vector3(0f, Input.GetAxisRaw("Mouse X"), 0f).normalized * playerRotateSpeed * Time.fixedDeltaTime).normalized;
            }

            yield return waitTimeFixedDeltaTime;
        }
    }

    private void OnGUI()
    {
        if (!enableDebugMode)
        {
            return;
        }

        if (showFPS)
        {
            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(30f, 30f, Screen.width, Screen.height);

            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = 42;
            style.normal.textColor = Color.red;

            float fps = 1f / deltaTime;

            GUI.Label(rect, string.Format("{0:0.} FPS", fps), style);
        }
    }

    private IEnumerator UpdateFrame()
    {
        while (true)
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

            if (Input.GetAxisRaw("Mouse Y") != 0f)
            {
                mainCamera.transform.rotation *= Quaternion.Euler(new Vector3(-Input.GetAxisRaw("Mouse Y"), 0f, 0f).normalized * playerRotateSpeed * Time.fixedDeltaTime).normalized;
            }

            if (Input.GetKeyDown(togglePostProcessingKey))
            {
                postProcess.SetActive(!postProcess.activeSelf);
            }

            if (Input.GetMouseButton(0) && !fire)
            {
                fire = true;

                Instantiate(bulletPrefab, mainCamera.ScreenToWorldPoint(new Vector2(Screen.width >> 1, Screen.height >> 1)), Quaternion.Euler(mainCamera.transform.eulerAngles.x, player.transform.eulerAngles.y, player.transform.eulerAngles.z));
            }
            else if (fire)
            {
                currentFireRate += Time.deltaTime;

                if (currentFireRate >= playerFireRate)
                {
                    currentFireRate -= playerFireRate;

                    fire = false;
                }
            }

            if (Input.GetKeyDown(quitKey))
            {
                EditorApplication.isPlaying = false;
            }

            yield return null;
        }
    }
}

#endif
