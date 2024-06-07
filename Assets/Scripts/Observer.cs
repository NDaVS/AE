using System;
using System.Collections;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public static event Action OnTimedEvent;
    void Start()
    {
        // Запускаем корутину, которая будет отправлять сигнал каждые 10 секунд
        StartCoroutine(SendSignalEvery10Seconds());
    }

    IEnumerator SendSignalEvery10Seconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);

            OnTimedEvent?.Invoke();
        }
    }

}
