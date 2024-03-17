using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject bulletHitParticlePrefab;

    private WaitForSeconds waitTime5f;

    private void Awake() => waitTime5f = new WaitForSeconds(5f);

    private IEnumerator Start()
    {
        StartCoroutine(DestroyBullet());

        while (true)
        {
            if (Physics.Raycast(transform.position, transform.forward, 3f, 1 << 10))
            {
                Destroy(Instantiate(bulletHitParticlePrefab, transform.position, Quaternion.identity), 2f);
                Destroy(gameObject);

                yield break;
            }

            transform.position += transform.forward * speed * Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator DestroyBullet()
    {
        yield return waitTime5f;

        Destroy(Instantiate(bulletHitParticlePrefab, transform.position, Quaternion.identity), 2f);
        Destroy(gameObject);
    }
}
