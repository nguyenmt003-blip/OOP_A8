/* 
Nguyễn Minh Tuấn
202419010
*/

// dịch vụ hiển thị cảnh báo lên màn hình console
public class ConsoleNotificationService
{
    public void HandleAlert(object? sender, DeviceAlertEventArgs e)
    {
        Console.WriteLine($"[{e.Severity}] {e.DeviceId}: {e.Message}");
    }
}
