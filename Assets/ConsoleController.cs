using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ConsoleController : MonoBehaviour
{
    public GameObject con;
    public TMP_InputField inFi;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            con.SetActive(!con.activeSelf);
        }
    }

    public void LineDone(string result)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            inFi.text = "";
            con.GetComponent<Console>().SendLine($"MUR>{result}");
            List<string> given = result.Split(' ').ToList<string>();
            try
            {
                StartCoroutine(given[0][..1].ToUpper() + given[0][1..].ToLower(), given);
            }
            catch
            { 
                con.GetComponent<Console>().SendLine("Command not found.");
            }
        }
    }

    private IEnumerator Test(string[] args)
    {
        con.GetComponent<Console>().SendLine($"Hello, {args[1]}!");
        yield return null;
        if (args.Length > 2)
        {
            string big = "Unused args: ";
            foreach (string arg in args)
            {
                if(Array.IndexOf(args, arg) > 1)
                    big += Array.IndexOf(args, arg) < args.Length -1 ? arg + ", " : arg;
            }
            big += "\n";
            con.GetComponent<Console>().SendLine(big);
        }
    }
}
