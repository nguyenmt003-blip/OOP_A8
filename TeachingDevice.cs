/* 
Nguyễn Minh Tuấn
202419010
*/

// danh sách trạng thái của thiết bị
public enum DeviceStatus
{
    Available,
    Borrowed,
    UnderMaintenance
}

// lớp trừu tượng cho thiết bị giảng dạy
public abstract class TeachingDevice
{
    public string Id { get; }
    public string Name { get; }
    public DeviceStatus Status { get; private set; }

    // các sự kiện thông báo thay đổi trạng thái và cảnh báo
    public event EventHandler<DeviceStatusChangedEventArgs>? StatusChanged;
    public event EventHandler<DeviceAlertEventArgs>? AlertRaised;

    protected TeachingDevice(string id, string name)
    {
        Id = id;
        Name = name;
        Status = DeviceStatus.Available;
    }

    // đổi trạng thái và phát sự kiện nếu có thay đổi
    public void ChangeStatus(DeviceStatus newStatus)
    {
        if (newStatus == Status)
        {
            return;
        }

        DeviceStatus oldStatus = Status;
        Status = newStatus;

        OnStatusChanged(
            new DeviceStatusChangedEventArgs(
                Id,
                oldStatus,
                newStatus,
                DateTime.Now));
    }

    // phương thức phụ trợ để phát cảnh báo
    protected void RaiseAlert(string message, AlertSeverity severity)
    {
        OnAlertRaised(
            new DeviceAlertEventArgs(
                Id,
                message,
                severity,
                DateTime.Now));
    }

    // kích hoạt sự kiện thay đổi trạng thái
    protected virtual void OnStatusChanged(DeviceStatusChangedEventArgs e)
    {
        StatusChanged?.Invoke(this, e);
    }

    // kích hoạt sự kiện cảnh báo
    protected virtual void OnAlertRaised(DeviceAlertEventArgs e)
    {
        AlertRaised?.Invoke(this, e);
    }
}
