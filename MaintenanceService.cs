/* 
Nguyễn Minh Tuấn
202419010
*/

// dịch vụ tạo yêu cầu bảo trì thiết bị
public class MaintenanceService
{
    private readonly List<string> requests = new();
    public IReadOnlyList<string> Requests => requests.AsReadOnly();

    // chỉ xử lý khi mức độ cảnh báo là critical
    public void HandleCriticalAlert(object? sender, DeviceAlertEventArgs e)
    {
        if (e.Severity != AlertSeverity.Critical)
        {
            return;
        }

        requests.Add($"Maintenance request for {e.DeviceId}: {e.Message}");
    }
}
