/* 
Nguyễn Minh Tuấn
202419010
*/

// các mức độ cảnh báo
public enum AlertSeverity
{
    Information,
    Warning,
    Critical
}

// dữ liệu đi kèm khi phát cảnh báo thiết bị
public class DeviceAlertEventArgs : EventArgs
{
    public string DeviceId { get; }
    public string Message { get; }
    public AlertSeverity Severity { get; }
    public DateTime OccurredAt { get; }

    public DeviceAlertEventArgs(
        string deviceId,
        string message,
        AlertSeverity severity,
        DateTime occurredAt)
    {
        DeviceId = deviceId;
        Message = message;
        Severity = severity;
        OccurredAt = occurredAt;
    }
}
