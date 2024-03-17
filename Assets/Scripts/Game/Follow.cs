using System.Collections;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    // Follow variables
    [SerializeField]
    private bool followXPosition;
    [SerializeField]
    private bool followYPosition;
    [SerializeField]
    private bool followZPosition;

    [SerializeField]
    private bool followXRotation;
    [SerializeField]
    private bool followYRotation;
    [SerializeField]
    private bool followZRotation;

    [SerializeField]
    private Vector3 offset;

    private IEnumerator Start()
    {
        while (true)
        {
            transform.position = new Vector3(followXPosition ? target.position.x : transform.position.x, followYPosition ? target.position.y : transform.position.y, followZPosition ? target.position.z : transform.position.z) + offset;
            transform.rotation = Quaternion.Euler(new Vector3(followXRotation ? target.eulerAngles.x : transform.eulerAngles.x, followYPosition ? target.eulerAngles.y : transform.eulerAngles.y, followZRotation ? target.eulerAngles.z : transform.eulerAngles.z));

            yield return null;
        }
    }
}
