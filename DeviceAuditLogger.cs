/* 
Nguyễn Minh Tuấn
202419010
*/

// dịch vụ ghi log lịch sử thay đổi trạng thái
public class DeviceAuditLogger
{
    private readonly List<string> entries = new();
    public IReadOnlyList<string> Entries => entries.AsReadOnly();

    // lưu lại log khi trạng thái thiết bị thay đổi
    public void HandleStatusChanged(object? sender, DeviceStatusChangedEventArgs e)
    {
        entries.Add($"{e.ChangedAt}: {e.DeviceId}, {e.OldStatus} -> {e.NewStatus}");
    }
}
