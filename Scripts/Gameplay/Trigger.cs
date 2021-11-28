using System.Collections;
using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Trigger : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnStay;
    public UnityEvent OnExit;

    private void OnTriggerEnter(Collider other)
    {
        OnEnter.Invoke();
    }
    private void OnTriggerStay(Collider other)
    {
        OnStay.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {
        OnExit.Invoke();
    }
}
