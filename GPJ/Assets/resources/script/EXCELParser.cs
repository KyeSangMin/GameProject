using UnityEngine;
using ExcelDataReader;
using System.IO;
using System.Data;

public class ExcelParser : MonoBehaviour
{
    public string excelFilePath = "Assets/script/mapData.xlsx"; // Excel 파일 경로
    

    void Start()
    {
        // Excel 파일 읽기
        using (var stream = File.Open(excelFilePath, FileMode.Open, FileAccess.Read)) 
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                // 데이터셋에 엑셀 데이터 로드
                var result = reader.AsDataSet();

                // 시트 수 확인
                int sheetCount = result.Tables.Count;
                Debug.Log("Sheet Count: " + sheetCount);

                // 각 시트에 대한 처리
                for (int i = 0; i < sheetCount; i++)
                {
                    DataTable table = result.Tables[i];
                    Debug.Log("Sheet Name: " + table.TableName);

                    // 각 셀에 대한 처리
                    foreach (DataRow row in table.Rows)
                    {
                        foreach (var item in row.ItemArray)
                        {
                            Debug.Log(item.ToString());
                        }
                    }
                }
            }
        }
    }
}