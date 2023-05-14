using UnityEngine;
using TMPro;

/**
*@brief 아이템 리스트뷰 셀
*@details 아이템 리스튜뷰 내 UI 표시
*@author 홍성윤
*/
public class ItemListUIScrollViewCell : UIScrollViewCell<Data.Item>
{

    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemDescText;

    /** @brief 셀 UI 출력 갱신
    *   @return 
    *   @param cellData : 아이템 데이터
    */  
    public override void UpdateCell(Data.Item cellData)
    {
        itemNameText.text = cellData.name;
        itemDescText.text = cellData.desc;
    }

    /** @brief 셀의 버튼 터치시 델리게이트 함수 실행
    *   @return 
    *   @param 
    */
    public void OnClickCellBtn()
    {
        if (onClickItemUpgradeDelegateMethod != null) onClickItemUpgradeDelegateMethod(this);
    }
}
