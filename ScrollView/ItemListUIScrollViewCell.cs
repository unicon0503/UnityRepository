using UnityEngine;
using TMPro;

/**
*@brief ������ ����Ʈ�� ��
*@details ������ ����Ʃ�� �� UI ǥ��
*@author ȫ����
*/
public class ItemListUIScrollViewCell : UIScrollViewCell<Data.Item>
{

    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemDescText;

    /** @brief �� UI ��� ����
    *   @return 
    *   @param cellData : ������ ������
    */  
    public override void UpdateCell(Data.Item cellData)
    {
        itemNameText.text = cellData.name;
        itemDescText.text = cellData.desc;
    }

    /** @brief ���� ��ư ��ġ�� ��������Ʈ �Լ� ����
    *   @return 
    *   @param 
    */
    public void OnClickCellBtn()
    {
        if (onClickItemUpgradeDelegateMethod != null) onClickItemUpgradeDelegateMethod(this);
    }
}
