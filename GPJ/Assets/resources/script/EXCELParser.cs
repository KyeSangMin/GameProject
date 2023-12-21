using UnityEngine;
using ExcelDataReader;
using System.IO;
using System.Data;

public class ExcelParser : MonoBehaviour
{
    public string excelFilePath = "Assets/script/mapData.xlsx"; // Excel ���� ���
    

    void Start()
    {
        // Excel ���� �б�
        using (var stream = File.Open(excelFilePath, FileMode.Open, FileAccess.Read)) 
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                // �����ͼ¿� ���� ������ �ε�
                var result = reader.AsDataSet();

                // ��Ʈ �� Ȯ��
                int sheetCount = result.Tables.Count;
                Debug.Log("Sheet Count: " + sheetCount);

                // �� ��Ʈ�� ���� ó��
                for (int i = 0; i < sheetCount; i++)
                {
                    DataTable table = result.Tables[i];
                    Debug.Log("Sheet Name: " + table.TableName);

                    // �� ���� ���� ó��
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