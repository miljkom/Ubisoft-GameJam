using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hub : MonoBehaviour
{
    private bool isInside = false;
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
}
