using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {

    [System.Serializable]  
    public class CustomItem
    {
        public GameObject[] obj;
        public GameObject[] hideObj;

    }


    public CustomItem[] count;
    
    public bool clothesFlag;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void changeItem(int num)
    {



        for(int i = 0; i<count.Length; i++)
        {
           
            if(i == num)
            {
                for (int y = 0; y < count[i].obj.Length; y++)
                {
                    count[i].obj[y].SetActive(true);
                    
                }

                if (clothesFlag)
                {
                    for (int y = 0; y < count[i].hideObj.Length; y++)
                    {
                        count[i].hideObj[y].SetActive(false);

                    }
                }
               
            }
            else
            {
                for (int y = 0; y < count[i].obj.Length; y++)
                {
                    count[i].obj[y].SetActive(false);
                }

            }
        }
    }

  
}
