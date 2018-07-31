using UnityEngine;
using System.Collections;

public class CustomManager : MonoBehaviour {

    public enum itemEnum { HAIR, CLOTHES, EYEBROW, EYE, MOUTH, EMOTION }
    public enum calType { DEFAULT, PLUS, MINUS}

    public int hair;
    public int clothes;
    public int eyebrow;
    public int eye;
    public int mouth;
    public int emotion;

    public ItemManager hairM;
    public ItemManager clothesM;
    public ItemManager eyebrowM;
    public ItemManager eyeM;
    public ItemManager mouthM;
    public ItemManager emotionM;

    public GameObject[] bodyTexture;

    public calType ct;

   
    // Use this for initialization
    void Start () {
        ct = calType.DEFAULT;
        callChangeItem();
        for (int i = 0 ; i <= (int)itemEnum.EMOTION; i++)
        {
            numberCheck(i);
        }
       

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void plusNum()
    {
        ct = calType.PLUS;
    }
    public void minusNum()
    {
        ct = calType.MINUS;
    }

    public void numberCheck(int num )
    {
        
        switch (num)
        {
            case (int)itemEnum.HAIR :
                
                
                if(ct == calType.PLUS)
                {
                    if (hairM.count.Length > hair+1)
                    {
                        hair++;
                    }
                }else if (ct == calType.MINUS)
                {
                    if (0 < hair)
                    {
                        hair--;
                    }
                }

                
                hairM.changeItem(hair);
                break;

            case (int)itemEnum.CLOTHES:

                if (ct == calType.PLUS)
                {
                    if (clothesM.count.Length > clothes+1)
                    {
                        clothes++;
                    }
                }
                else if (ct == calType.MINUS)
                {
                    if (0 < clothes)
                    {
                        clothes--;
                    }
                }
                callChangeItem();
                clothesM.changeItem(clothes);
                break;

            case (int)itemEnum.EYEBROW:
                if (ct == calType.PLUS)
                {
                    if (eyebrowM.count.Length > eyebrow + 1)
                    {
                        eyebrow++;
                    }
                }
                else if (ct == calType.MINUS)
                {
                    if (0 < eyebrow)
                    {
                        eyebrow--;
                    }
                }

                eyebrowM.changeItem(eyebrow);
                break;

            case (int)itemEnum.EYE:
                if (ct == calType.PLUS)
                {
                    if (eyeM.count.Length > eye + 1)
                    {
                        eye++;
                    }
                }
                else if (ct == calType.MINUS)
                {
                    if (0 < eye)
                    {
                        eye--;
                    }
                }
                eyeM.changeItem(eye);
                break;

            case (int)itemEnum.MOUTH:
                if (ct == calType.PLUS)
                {
                    if (mouthM.count.Length > mouth + 1)
                    {
                        mouth++;
                    }
                }
                else if (ct == calType.MINUS)
                {
                    if (0 < mouth)
                    {
                        mouth--;
                    }
                }
                mouthM.changeItem(mouth);
                break;

            case (int)itemEnum.EMOTION:
                if (ct == calType.PLUS)
                {
                    if (emotionM.count.Length > emotion + 1)
                    {
                        emotion++;
                    }
                }
                else if (ct == calType.MINUS)
                {
                    if (0 < emotion)
                    {
                        emotion--;
                    }
                }

                emotionM.changeItem(emotion);
                break;


            default:
                break;
        }

    }

    void callChangeItem()
    {
        for(int i=0; i<bodyTexture.Length; i++)
        {
            bodyTexture[i].SetActive(true);
        }
    }
}
