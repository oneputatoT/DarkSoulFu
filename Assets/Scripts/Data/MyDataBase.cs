using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defective.JSON;

public class MyDataBase
{
    string dataFileName = "MyData";
    public readonly JSONObject jsonText;


    public MyDataBase()
    {
        TextAsset temp = Resources.Load(dataFileName) as TextAsset;
        jsonText = new JSONObject(temp.text); 

    }
}
