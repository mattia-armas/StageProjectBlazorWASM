using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Client.Shared;

public partial class TreeViewBase<Tvalue> : ComponentBase
{
    [Parameter]
    public List<Tvalue> DataSource { get; set; }
    [Parameter]
    public string Id { get; set; }
    [Parameter]
    public string ItemId { get; set; }
    [Parameter]
    public string HasChildren { get; set; }
    [Parameter]
    public string Text { get; set; }
    [Parameter]
    public string Expanded { get; set; }

    protected List<Tvalue> AllItems;
    protected Dictionary<int, bool> _caretDown = new();
    protected Dictionary<int, string> _caretcss = new();
    protected Dictionary<int, string> _nestedcss = new();

    protected override Task OnInitializedAsync()
    {
        //assigning to its new instance to avoid exceptions.
        AllItems = new List<Tvalue>();
        AllItems = DataSource.ToArray().ToList();

        if (AllItems != null)
        {
            foreach (var item in AllItems)
            {
                var _id = Convert.ToInt32(GetPropertyValue(item, Id));

                //initializing fields with default value.
                _caretDown.Add(_id, true);
                _caretcss.Add(_id, "caret");
                _nestedcss.Add(_id, "nested");
            }

        }


        return base.OnInitializedAsync();
    }


    protected override Task OnParametersSetAsync()
    {

        var Parem = AllItems.First();
        switch (GetPropertyType(Parem, ItemId))
        {
            case "Int32":
                if (Convert.ToInt32(GetPropertyValue(Parem, ItemId)) > 0)
                {
                    SetPropertyValue(Parem, ItemId, 0);
                }
                break;
            case "String":
                if (GetPropertyValue(Parem, ItemId) != "")
                {
                    SetPropertyValue(Parem, ItemId, "");
                }
                break;
            default:
                break;
        }

        return base.OnParametersSetAsync();
    }



    protected void SpanToggle(Tvalue item)
    {
        var _clckdItemid = Convert.ToInt32(GetPropertyValue(item, Id));

        _caretcss[_clckdItemid] = _caretDown[_clckdItemid] ? "caret caret-down" : "caret";
        _nestedcss[_clckdItemid] = _caretDown[_clckdItemid] ? "active" : "nested";
        _caretDown[_clckdItemid] = !_caretDown[_clckdItemid];
    }

    #region reflection methodes to get your property type, property value and also set property value 
    protected string GetPropertyValue(Tvalue item, string Property)
    {

        if (item != null)
        {
            return item.GetType().GetProperty(Property).GetValue(item, null).ToString();
        }
        return "";

    }

    protected void SetPropertyValue<T>(Tvalue item, string Property, T value)
    {
        item?.GetType().GetProperty(Property).SetValue(item, value);
    }

    protected string GetPropertyType(Tvalue item, string Property)
    {

        if (item != null)
        {
            return item.GetType().GetProperty(Property).PropertyType.Name;
        }
        return null;
    }
    #endregion
}