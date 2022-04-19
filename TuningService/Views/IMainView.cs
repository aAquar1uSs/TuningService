using System;
using System.Data;

namespace TuningService.Views;

public interface IMainView
{
    event EventHandler<int> ShowOrderInfoViewEvent;

    event EventHandler ShowNewOrderViewEvent;

    event EventHandler UpdateAllDataEvent;

    event EventHandler<int> RemoveDataFromTableEvent;

    event EventHandler SearchEvent;

    string SearchValue { get; set; }

    void SetAllDataToDataGridView(DataTable dt);

    void InitHeadersInTable();
}