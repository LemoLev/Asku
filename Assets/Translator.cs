using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Translator : MonoBehaviour
{
    public Dictionary<string, Dictionary<string, string>> translations = new();
    public TextAsset file;
    public string tCode;
    void Start()
    {
        CompileFile();
    }

    public void CompileFile()
    {
        translations.Clear();
        List<string> lines = new();
        foreach (string l in file.text.Split("\n"))
            lines.Add(l);
        foreach (string l in lines)
        {
            Dictionary<string, string> td = new();
            string lang = "";
            string code = "";
            string word = "";
            if (l.Contains("  "))
            {
                int li = 0;
                for (int i = lines.IndexOf(l) - 1; lines[i].Contains("  ") && lines[i].Contains("/") && !lines[i].Contains(":"); i--)
                {
                    li = i;
                    print(lines[li]);
                }
                try
                {
                    code = lines[li - 1].Replace(":", "");
                }
                catch (ArgumentOutOfRangeException)
                {
                    code = lines[lines.IndexOf(l) - 1].Replace(":", "");
                }
                lang = l.Split("/")[0].Replace("  ", "");
                word = l.Split("/")[1];
                td.Add(lang, word);
                try
                {
                    try
                    {
                        translations[code].Add(lang, word);
                        print($"Added {lang}, {word} to {code} dict");
                    }
                    catch (ArgumentException)
                    {
                        print($"Failed to add {lang}, {word} to {code} dict");
                    }
                }
                catch (KeyNotFoundException)
                {
                    translations.Add(code, td);
                    print($"Added {code}, {lang}, {word} to global dict");
                }
            }
            else continue;
        }
    }

    public string GetWord(string lang, string code)
    {
        return translations[code][lang];
    }

    public void UpdateText()
    {
        GetComponent<TextMeshProUGUI>().text = GetWord(PlayerPrefs.GetString("Lang"), tCode);
    }

    public void GetLang(string lang)
    {
        PlayerPrefs.SetString("Lang", lang);
    }
}
