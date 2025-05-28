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
    bool isCoroutineRunning;
    public bool isTyping;

    public List<string> submittedLines;
    int sli;

    List<string> addVars = new();
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && !isTyping)
        {
            con.SetActive(!con.activeSelf);
        }

        if (submittedLines != new List<string>() && isTyping)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) && sli+1 != submittedLines.Count)
            {
                sli++;
                inFi.text = submittedLines[sli];
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow)  && sli-1 >= 0)
            {
                sli--;
                inFi.text = submittedLines[sli];
            }
        }
    }

    public void LineDone(string result)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            inFi.text = "";
            con.GetComponent<Console>().SendLine($"MUR>{result}");
            addVars.Clear();
            result = GetAllQVarsAndRepRes(result).TrimEnd();
            if((submittedLines.Count != 0 && submittedLines[^1] != result) || submittedLines.Count == 0)
                submittedLines.Add(result);
            List<string> given = result.Split(' ').ToList<string>();
            foreach (string v in addVars)
            {
                print(v);
                given.Add(v);
            }
            foreach (string a in given)
            {
                print(a);
            }
            StartCoroutine(given[0][..1].ToUpper() + given[0][1..].ToLower(), given);
            if (!isCoroutineRunning)
                con.GetComponent<Console>().SendLine("Command not found.");
            else
                isCoroutineRunning = false;
            sli = submittedLines.Count;
        }
    }

    public IEnumerator Hello(List<string> args)
    {
        isCoroutineRunning = true;
        con.GetComponent<Console>().SendLine($"Hello, {args[1]}!");
        yield return null;
        GetUnusedArgs(args, 2);
    }

    public void SetIsTyping(bool it)
    {
        isTyping = it;
    }

    string GetAllQVarsAndRepRes(string res)
    {
        string resCLess = res.Replace(res.Split(' ')[0], "")[1..];
        if (resCLess.Contains('"') && resCLess[(resCLess.IndexOf('"') + 1)..].Contains('"'))
        {
            addVars.Add(resCLess.Substring(resCLess.IndexOf('"') + 1, resCLess[(resCLess.IndexOf('"') + 1)..].IndexOf('"')));
            res = res.Replace(resCLess, "");
            if (res.Contains('"') && res[(res.IndexOf('"') + 1)..].Contains('"'))
            {
                GetAllQVarsAndRepRes(res);
            }
        }
        return res;
    }

    private void GetUnusedArgs(List<string> allArgs, int usedArgCount)
    {
        if (allArgs.Count > usedArgCount)
        {
            string big = "Unused args: ";
            foreach (string arg in allArgs)
            {
                if (allArgs.IndexOf(arg) > usedArgCount-1)
                    big += allArgs.IndexOf(arg) < allArgs.Count - 1 ? arg + ", " : arg;
            }
            big += "\n";
            con.GetComponent<Console>().SendLine(big);
        }
    }
}
