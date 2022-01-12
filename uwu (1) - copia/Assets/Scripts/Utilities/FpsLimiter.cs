using System.Collections;
using UnityEngine;

public class FpsLimiter : MonoBehaviour
{
    [SerializeField] private int frameRate = 80;

    private void Awake()
    {
        StartCoroutine(changeFramerate());
    }
    IEnumerator changeFramerate()
    {
        yield return new WaitForSeconds(1);
        Application.targetFrameRate = frameRate;
    }
}
