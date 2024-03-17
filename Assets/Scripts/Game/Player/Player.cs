using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    public float WalkSpeed => walkSpeed;
    [SerializeField]
    private float rotateSpeed;
    public float RotateSpeed => rotateSpeed;
    [SerializeField]
    private float fireRate;
    public float FireRate => fireRate;
    private float currentFireRate;

    private bool fire;

    // Requirement Components
    [SerializeField]
    private Controller controller;
    [SerializeField]
    private Rigidbody rigidbody;
    [SerializeField]
    private Bullet bulletPrefab;
    public Bullet BulletPrefab => bulletPrefab;

    private WaitForFixedUpdate waitTimeFixedDeltaTime;

    private Camera mainCamera;

    private void Awake()
    {
        waitTimeFixedDeltaTime = new WaitForFixedUpdate();

        mainCamera = Camera.main;
    }

    private IEnumerator Start()
    {
        while (true)
        {
            Move();
            RotatePlayer();
            Fire();

            yield return waitTimeFixedDeltaTime;
        }
    }

    private void Move() => rigidbody.position += (transform.right * controller.moveDirection.x + transform.forward * controller.moveDirection.y).normalized * walkSpeed * Time.fixedDeltaTime;

    private void RotatePlayer()
    {
        if (controller.rotateDirection.x != 0f)
        {
            rigidbody.rotation *= Quaternion.Euler(new Vector3(0f, controller.rotateDirection.x, 0f).normalized * rotateSpeed * Time.fixedDeltaTime).normalized;
        }
        if (controller.rotateDirection.y != 0f)
        {
            mainCamera.transform.rotation *= Quaternion.Euler(new Vector3(-controller.rotateDirection.y, 0f, 0f).normalized * rotateSpeed * Time.fixedDeltaTime).normalized;
        }
    }

    private void Fire()
    {
        if (controller.fireKeyDown && !fire)
        {
            fire = true;

            Instantiate(bulletPrefab, mainCamera.ScreenToWorldPoint(new Vector2(Screen.width >> 1, Screen.height >> 1)), Quaternion.Euler(mainCamera.transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z));
        }
        else if (fire)
        {
            currentFireRate += Time.deltaTime;

            if (currentFireRate >= fireRate)
            {
                currentFireRate -= fireRate;

                fire = false;
            }
        }
    }
}
