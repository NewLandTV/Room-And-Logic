using System.Collections;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private IEnumerator Start()
    {
        while (true)
        {
            transform.rotation *= Quaternion.Euler(Vector3.right * speed * Time.deltaTime);

            yield return null;
        }
    }
}
