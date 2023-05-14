using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
*@brief ������Ʈ Ǯ�� ��ũ�Ѻ� ��Ʈ�ѷ�
*@details ���� ������Ʈ Ǯ������ �������ִ� ��ũ�Ѻ� ��Ʈ�ѷ�
*         �� ����, �ʱ�ȭ, ���� ���� ����
*         ��Ʈ�ѷ����� ������ �� UI������� ���۽� ���� �� ��������Ʈ ����
*@author ȫ����
*/
[RequireComponent(typeof(ScrollRect))]
[RequireComponent(typeof(RectTransform))]
public abstract class UIScrollViewController<T> : MonoBehaviour
{
    [SerializeField] protected GameObject cellBase = null;
    [SerializeField] private RectOffset padding;
    [SerializeField] private float spacingHeight = 1.0f;
    [SerializeField] private RectOffset visibleRectPadding = null;

    public RectTransform ScrollViewRectTransform => GetComponent<RectTransform>();
    public ScrollRect ViewScrollRect => GetComponent<ScrollRect>();

    public delegate void OnClickCellBtnDelegateMethod(ItemListUIScrollViewCell cell);
    public OnClickCellBtnDelegateMethod onClickCellBtnDelegateMethod;

    protected List<T> cellDataList = new List<T>();
    protected LinkedList<UIScrollViewCell<T>> cells = new LinkedList<UIScrollViewCell<T>>();

    private Rect visibleRect;
    private Vector2 prevScrollPositon;

    protected virtual void Start()
    {
        ViewScrollRect.onValueChanged.AddListener(OnScrollPositionChanged);
    }

    /** @brief ��ũ�Ѻ� �ʱ�ȭ
    *   @return 
    *   @param 
    */
    protected void InitializeScrollView()
    {
        UpdateScrollViewSize();
        UpdateVisibleRect();

        if (cells.Count < 1)
        {
            Vector2 cellTop = new Vector2(0.0f, -padding.top);
            for (int i = 0; i < cellDataList.Count; i++)
            {
                float cellHeight = GetCellHeight(i);
                Vector2 cellBottom = cellTop + new Vector2(0.0f, -cellHeight);
                if ((cellTop.y <= visibleRect.y && cellTop.y >= visibleRect.y - visibleRect.height)
                    || (cellBottom.y <= visibleRect.y && cellBottom.y >= visibleRect.y - visibleRect.height))
                {
                    UIScrollViewCell<T> cell = CreateCell(i);
                    cell.Top = cellTop;
                    break;
                }
                cellTop = cellBottom + new Vector2(0.0f, spacingHeight);
            }
        }
        else
        {
            LinkedListNode<UIScrollViewCell<T>> node = cells.First;
            UpdateCell(node.Value, node.Value.Index);
            node = node.Next;

            while (node != null)
            {
                UpdateCell(node.Value, node.Previous.Value.Index + 1);
                node.Value.Top = node.Previous.Value.Bottom + new Vector2(0.0f, -spacingHeight);
                node = node.Next;
            }
        }

        FillVisibleRect();
    }

    /** @brief ��ũ�Ѻ� �� Height ����
    *   @return y : �� Height ��
    *   @param index : �� �ε��� ��
    */
    protected virtual float GetCellHeight(int index)
    {
        return cellBase.GetComponent<RectTransform>().sizeDelta.y;
    }

    /** @brief ��ũ�Ѻ� �������� ũ�� �ʱ�ȭ
    *   @return 
    *   @param 
    */
    protected void UpdateScrollViewSize()
    {
        float contentHeight = 0.0f;

        for (int i = 0; i < cellDataList.Count; i++)
        {
            contentHeight += GetCellHeight(i);

            if (i > 0)
                contentHeight += spacingHeight;
        }

        Vector2 sizeDelta = ViewScrollRect.content.sizeDelta;
        sizeDelta.y = padding.top + contentHeight + padding.bottom;
        ViewScrollRect.content.sizeDelta = sizeDelta;
    }

    /** @brief �� ���� �ʱ�ȭ
    *   @return 
    *   @param index �� �ε��� ��
    */
    private UIScrollViewCell<T> CreateCell(int index)
    {
        UIScrollViewCell<T> cell = Instantiate(cellBase, ViewScrollRect.content.transform).GetComponent<UIScrollViewCell<T>>();
        cell.transform.localScale = Vector3.one;

        UpdateCell(cell, index);
        cells.AddLast(cell);

        return cell;
    }

    /** @brief �� ���� or ��Ȱ�� ����
    *   @return 
    *   @param cell: ��ũ�Ѽ� ��ü, index �� �ε��� ��
    */
    protected void UpdateCell(UIScrollViewCell<T> cell, int index)
    {
        cell.Index = index;
        if (cell.Index >= 0 && cell.Index <= cellDataList.Count - 1)
        {
            cell.gameObject.SetActive(true);
            cell.UpdateCell(cellDataList[cell.Index]);
            cell.Height = GetCellHeight(cell.Index);
        }
        else
        {
            cell.gameObject.SetActive(false);
        }
    }

    /** @brief ����Ʈ ��Ʈ �� ����
    *   @return 
    *   @param 
    */
    public void UpdateVisibleRect()
    {
        visibleRect.x = ViewScrollRect.content.anchoredPosition.x + visibleRectPadding.left;
        visibleRect.y = -ViewScrollRect.content.anchoredPosition.y + visibleRectPadding.top;

        visibleRect.width = ScrollViewRectTransform.rect.width + visibleRectPadding.left + visibleRectPadding.right;
        visibleRect.height = ScrollViewRectTransform.rect.height + visibleRectPadding.top + visibleRectPadding.bottom;
    }

    /** @brief ������ ���� ������ �� ���� ���� ��ŭ �� �߰� ����
    *   @return 
    *   @param 
    */
    private void FillVisibleRect()
    {
        if (cells.Count < 1)    return;

        UIScrollViewCell<T> lastCell = cells.Last.Value;
        int nextCellDataIndex = lastCell.Index + 1;
        Vector2 nextCellTop = lastCell.Bottom + new Vector2(0.0f, -spacingHeight);

        while (nextCellDataIndex < cellDataList.Count && nextCellTop.y >= visibleRect.y - visibleRect.height)
        {
            UIScrollViewCell<T> cell = CreateCell(nextCellDataIndex);
            cell.Top = nextCellTop;
            lastCell = cell;
            nextCellDataIndex = lastCell.Index + 1;
            nextCellTop = lastCell.Bottom + new Vector2(0.0f, -spacingHeight);
        }
    }

    /** @brief ��ũ�� ���� ����
    *   @return 
    *   @param scrollPosition : ��ũ�� ����2 ��
    */
    public void OnScrollPositionChanged(Vector2 scrollPosition)
    {
        UpdateVisibleRect();
        UpdateVisibleRect((scrollPosition.y < prevScrollPositon.y) ? 1 : -1);
        prevScrollPositon = scrollPosition;
    }

    /** @brief ��ũ�� ����Ʈ �� ���� �� ����
    *   @return 
    *   @param scrollDirection : ��ũ�� ���� ��
    */
    private void UpdateVisibleRect(int scrollDirection)
    {
        if (cells.Count < 1)    return;

        if (scrollDirection > 0)
        {
            UIScrollViewCell<T> firstCell = cells.First.Value;
            while (firstCell.Bottom.y > visibleRect.y)
            {
                UIScrollViewCell<T> lastCell = cells.Last.Value;
                UpdateCell(firstCell, lastCell.Index + 1);
                firstCell.Top = lastCell.Bottom + new Vector2(0.0f, -spacingHeight);

                cells.AddLast(firstCell);
                cells.RemoveFirst();
                firstCell = cells.First.Value;
            }
            FillVisibleRect();
        }
        else if (scrollDirection < 0)
        {
            UIScrollViewCell<T> lastCell = cells.Last.Value;
            while (lastCell.Top.y < visibleRect.y - visibleRect.height)
            {
                UIScrollViewCell<T> firstCell = cells.First.Value;
                UpdateCell(lastCell, firstCell.Index - 1);
                lastCell.Bottom = firstCell.Top + new Vector2(0.0f, spacingHeight);

                cells.AddFirst(lastCell);
                cells.RemoveLast();
                lastCell = cells.Last.Value;
            }
        }
    }
}
