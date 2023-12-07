using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class CSVParser : MonoBehaviour
{
    [SerializeField]
    private TextAsset csvFile;
    [SerializeField]
    private List<List<string>> data = new List<List<string>>();
    // Start is called before the first frame update
    void Start()
    {
        ParseCSV();
    }

    // Update is called once per frame
    void ParseCSV()
    {
        string[] lines = csvFile.text.Split('\n');

        foreach (string line in lines) 
        {
            string[] fields = line.Split(',');

            // 데이터 배열을 리스트에 추가
            data.Add(new List<string>(fields));
        }
    }

    public List<List<string>> getMapdata()
    {

        return data;
    }
}
