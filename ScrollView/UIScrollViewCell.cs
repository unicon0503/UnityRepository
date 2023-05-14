using UnityEngine;

/**
*@brief ½ºÅ©·Ñºä ¼¿
*@details ½ºÅ©·Ñºä ¼¿ Á¦ÀÛ½Ã »ó¼Ó ¹Þ¾Æ Àç»ç¿ë ÇÒ ¼ö ÀÖ´Â ¼¿ Å¬·¡½º
*@author È«¼ºÀ±
*/
[RequireComponent(typeof(RectTransform))]
public abstract class UIScrollViewCell<T> : MonoBehaviour
{
    public RectTransform CellRectTransform => GetComponent<RectTransform>();

    public int Index { get; set; }

    public abstract void UpdateCell(T cellData);

    public delegate void OnClickItemUpgradeDelegateMethod(ItemListUIScrollViewCell cell);
    public OnClickItemUpgradeDelegateMethod onClickItemUpgradeDelegateMethod;

    public Vector2 Top
    {
        get
        {
            CellRectTransform.GetLocalCorners(VectorUtil.cornerVectorArray);
            return CellRectTransform.anchoredPosition + new Vector2(0.0f, VectorUtil.cornerVectorArray[1].y);
        }
        set
        {
            CellRectTransform.GetLocalCorners(VectorUtil.cornerVectorArray);
            CellRectTransform.anchoredPosition = value - new Vector2(0.0f, VectorUtil.cornerVectorArray[1].y);
        }
    }

    public Vector2 Bottom
    {
        get
        {
            CellRectTransform.GetLocalCorners(VectorUtil.cornerVectorArray);
            return CellRectTransform.anchoredPosition + new Vector2(0.0f, VectorUtil.cornerVectorArray[3].y);
        }
        set
        {
            CellRectTransform.GetLocalCorners(VectorUtil.cornerVectorArray);
            CellRectTransform.anchoredPosition = value - new Vector2(0.0f, VectorUtil.cornerVectorArray[3].y);
        }
    }

    public float Height
    {
        get { return CellRectTransform.sizeDelta.y; }
        set
        {
            Vector2 sizeDelta = CellRectTransform.sizeDelta;
            sizeDelta.y = value;
            CellRectTransform.sizeDelta = sizeDelta;
        }
    }


}
