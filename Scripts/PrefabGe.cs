using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PrefabGe : MonoBehaviour
{
    [SerializeField] GameObject TestObject;
    [SerializeField] GameObject canvas;
    public JewelStatusData dd;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i<21; i++)
        {
            GameObject PF = Instantiate(TestObject);
            PF.transform.SetParent(GameObject.Find("JewelContent").transform, false);
            PF.name = i.ToString();
            Text PFname = PF.transform.GetChild(0).GetComponent<Text>();
            PFname.text = dd.JewelStatusList[i].Name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void push(Button sender)
    {
        int ID = int.Parse(sender.name);
        Text name = GameObject.Find("Name").GetComponent<Text>();
        name.text = "Åú" + dd.JewelStatusList[ID].Name;
        Text JapN = GameObject.Find("JapName").GetComponent<Text>();
        JapN.text = dd.JewelStatusList[ID].Wamei;
        Text CN = GameObject.Find("ChemName").GetComponent <Text>();
        CN.text = dd.JewelStatusList[ID].chemical;
        Text Co = GameObject.Find("color").GetComponent<Text>();
        Co.text = dd.JewelStatusList[ID].color;
        Text ProA = GameObject.Find("PA").GetComponent<Text>();
        ProA.text = dd.JewelStatusList[ID].production;
        Text mh = GameObject.Find("MohsHardness").GetComponent<Text>();
        mh.text = "ÉÇÅ[ÉXçdìx: " + dd.JewelStatusList[ID].MH.ToString();
    }
}
