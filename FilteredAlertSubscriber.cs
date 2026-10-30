/* 
Nguyễn Minh Tuấn
202419010
*/

// subscriber lọc thông báo theo điều kiện delegate
public class FilteredAlertSubscriber
{
    private readonly AlertFilter filter;
    private readonly Action<DeviceAlertEventArgs> action;

    public FilteredAlertSubscriber(AlertFilter filter, Action<DeviceAlertEventArgs> action)
    {
        this.filter = filter ?? throw new ArgumentNullException(nameof(filter));
        this.action = action ?? throw new ArgumentNullException(nameof(action));
    }

    // xử lý cảnh báo nếu thoả mãn bộ lọc
    public void HandleAlert(object? sender, DeviceAlertEventArgs e)
    {
        if (filter(e))
        {
            action(e);
        }
    }
}
