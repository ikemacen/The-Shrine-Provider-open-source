using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTool : MonoBehaviour
{
    private void Awake()
    {
        print("I Live!");
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(1)) {
            print("YIPEE!");
        }
    }
}
