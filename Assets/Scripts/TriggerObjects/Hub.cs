using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hub : MonoBehaviour
{
    private bool isInside = false;
    [SerializeField] public List<GameObject> hubs;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isInside = true;
            StartCoroutine(PlayerEntered());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInside = false;
            StartCoroutine(PlayExited());
        }
    }

    private IEnumerator PlayerEntered()
    {
        GameManager.Instance.RefillResources();
        yield return new WaitForSeconds(0.1f);
        if (!isInside)
        {
            yield break;
        }
        StartCoroutine(PlayerEntered());
    }
    private IEnumerator PlayExited()
    {
        GameManager.Instance.LoseBattery();
        yield return new WaitForSeconds(1f);
        if (isInside)
        {
            yield break;
        }
        StartCoroutine(PlayExited());
    }
}
