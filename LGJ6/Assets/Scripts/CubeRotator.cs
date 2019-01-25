using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class CubeRotator : MonoBehaviour
{
    public static CubeRotator Instance;

    public UnityEvent unityEvent;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        unityEvent.Invoke();
    }

    public void Rotate(Vector2 direction)
    {
      //  transform.DORotate(, 1f, RotateMode.FastBeyond360);
    }
}
