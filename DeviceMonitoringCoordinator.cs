/* 
Nguyễn Minh Tuấn
202419010
*/

// lớp điều phối đăng ký và huỷ đăng ký sự kiện cho các thiết bị
public class DeviceMonitoringCoordinator : IDisposable
{
    private readonly List<TeachingDevice> devices;
    private readonly ConsoleNotificationService notificationService;
    private readonly DeviceAuditLogger auditLogger;
    private readonly MaintenanceService maintenanceService;
    private bool isSubscribed;

    public DeviceMonitoringCoordinator(
        IEnumerable<TeachingDevice> devices,
        ConsoleNotificationService notificationService,
        DeviceAuditLogger auditLogger,
        MaintenanceService maintenanceService)
    {
        this.devices = devices != null ? new List<TeachingDevice>(devices) : throw new ArgumentNullException(nameof(devices));
        this.notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        this.auditLogger = auditLogger ?? throw new ArgumentNullException(nameof(auditLogger));
        this.maintenanceService = maintenanceService ?? throw new ArgumentNullException(nameof(maintenanceService));
    }

    // đăng ký các handler lắng nghe sự kiện
    public void Subscribe()
    {
        // tránh đăng ký trùng lặp nhiều lần
        if (isSubscribed)
        {
            return;
        }

        foreach (var device in devices)
        {
            device.AlertRaised += notificationService.HandleAlert;
            device.AlertRaised += maintenanceService.HandleCriticalAlert;
            device.StatusChanged += auditLogger.HandleStatusChanged;
        }

        isSubscribed = true;
    }

    // huỷ đăng ký các handler đã tạo
    public void Unsubscribe()
    {
        if (!isSubscribed)
        {
            return;
        }

        foreach (var device in devices)
        {
            device.AlertRaised -= notificationService.HandleAlert;
            device.AlertRaised -= maintenanceService.HandleCriticalAlert;
            device.StatusChanged -= auditLogger.HandleStatusChanged;
        }

        isSubscribed = false;
    }

    // tự động huỷ đăng ký khi dispose
    public void Dispose()
    {
        Unsubscribe();
    }
}
