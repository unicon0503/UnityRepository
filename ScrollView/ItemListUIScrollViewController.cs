using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*@brief 아이템리스트 스크롤뷰 컨트롤러 
*@details 오브젝트 풀링으로 셀 관리 - 데이터 로드, 유저 조작에 따른 로직 처리 
*@author 홍성윤
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
