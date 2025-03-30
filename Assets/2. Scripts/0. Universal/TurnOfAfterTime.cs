using UnityEngine;
using System.Collections;
public class TurnOfAfterTime : MonoBehaviour
{
    public float waitTime;
    private void OnEnable()
    {
        StartCoroutine(TurnOff());
    }

    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(waitTime);
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        this.gameObject.SetActive(false);
    }
}
