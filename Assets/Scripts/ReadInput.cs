using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadInput : MonoBehaviour
{
    string input;

    public void ReadStringInput(string input)
    {
        this.input = input;
        print($"The input is {this.input}");
    }
}
