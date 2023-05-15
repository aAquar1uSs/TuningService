using System;
using System.Data;

namespace TuningService.Views;

public delegate void ShowOrderDelegate(int index);

public delegate void RemoveOrderDelegate(int index);

public interface IMainView
{
    event ShowOrderDelegate ShowOrderInfoViewEvent;

    event EventHandler ShowNewOrderViewEvent;

    event EventHandler ShowNewMasterView;

    event EventHandler UpdateAllDataEvent;

    event RemoveOrderDelegate RemoveDataFromTableEvent;

    event EventHandler SearchEvent;

    event EventHandler ShowDeleteMasterView;

    event EventHandler ShowImportMenuView;

    string SearchValue { get; set; }

    void SetAllDataToDataGridView(DataTable dt);
}