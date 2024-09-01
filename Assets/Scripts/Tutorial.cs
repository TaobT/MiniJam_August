using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject CutScene;

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            CutScene.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
