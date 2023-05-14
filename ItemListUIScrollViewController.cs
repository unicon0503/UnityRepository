using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*@brief �����۸���Ʈ ��ũ�Ѻ� ��Ʈ�ѷ� 
*@details ������Ʈ Ǯ������ �� ���� - ������ �ε�, ���� ���ۿ� ���� ���� ó�� 
*@author ȫ����
*/
public class ItemListUIScrollViewController : UIScrollViewController<Data.Item>
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public void InitController()
    {
        LoadData();
        base.onClickCellBtnDelegateMethod = OnClickCellBtn;
        InitializeScrollView();
    }


    public void LoadData()
    {
        cellDataList = DataHandler.GetPlayerItemList();
    }

    public void OnClickCellBtn(ItemListUIScrollViewCell cell)
    {
        Debug.Log("Cell Btn Click");
        Debug.Log(cellDataList[cell.Index]);
    }
}
