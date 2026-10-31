/* 
Nguyễn Minh Tuấn
202419010
*/

// khởi tạo các thiết bị
var projector = new ProjectorRemote("PRJ01", "Điều khiển máy chiếu Sony", 100);
var airConditioner = new AirConditionerRemote("AC01", "Điều khiển điều hòa Daikin", "P.501", true);
var mic = new WirelessMicrophone("MIC01", "Micro không dây Shure", 100, 100);

var devices = new List<TeachingDevice> { projector, airConditioner, mic };

// khởi tạo các dịch vụ lắng nghe
var notificationService = new ConsoleNotificationService();
var auditLogger = new DeviceAuditLogger();
var maintenanceService = new MaintenanceService();

// khởi tạo bộ điều phối và đăng ký sự kiện
using var coordinator = new DeviceMonitoringCoordinator(
    devices,
    notificationService,
    auditLogger,
    maintenanceService);

coordinator.Subscribe();
// gọi thử lần 2 để kiểm tra không bị đăng ký trùng
coordinator.Subscribe();

Console.WriteLine("=== 1. kiểm tra thay đổi trạng thái thiết bị ===");
// mượn máy chiếu
projector.ChangeStatus(DeviceStatus.Borrowed);
// thử đổi lại trạng thái trùng để kiểm tra bỏ qua
projector.ChangeStatus(DeviceStatus.Borrowed);
// trả máy chiếu về trạng thái sẵn sàng
projector.ChangeStatus(DeviceStatus.Available);

Console.WriteLine("\n=== 2. kiểm tra cảnh báo pin máy chiếu ===");
// pin giảm xuống dưới 20% phát warning
projector.UpdateBatteryLevel(15);
// pin về 0% phát critical
projector.UpdateBatteryLevel(0);

Console.WriteLine("\n=== 3. kiểm tra cảnh báo điều hòa ===");
// mất phản hồi phát critical
airConditioner.UpdateResponseState(false);
// mất phản hồi tiếp không phát lặp lại
airConditioner.UpdateResponseState(false);
// khôi phục phản hồi
airConditioner.UpdateResponseState(true);

Console.WriteLine("\n=== 4. kiểm tra cảnh báo micro không dây ===");
// chất lượng tín hiệu giảm dưới 40% phát warning
mic.UpdateSignalQuality(35);
// pin giảm dưới 30% phát warning
mic.UpdateBatteryLevel(25);
// tín hiệu về 0% phát critical
mic.UpdateSignalQuality(0);

Console.WriteLine("\n=== 5. kiểm tra danh sách log trong audit logger ===");
foreach (var entry in auditLogger.Entries)
{
    Console.WriteLine($"[audit log] {entry}");
}

Console.WriteLine("\n=== 6. kiểm tra danh sách yêu cầu bảo trì ===");
foreach (var req in maintenanceService.Requests)
{
    Console.WriteLine($"[maintenance] {req}");
}

Console.WriteLine("\n=== 7. kiểm tra delegate và filtered alert subscriber ===");
// tạo subscriber lọc chỉ nhận cảnh báo critical
var criticalSubscriber = new FilteredAlertSubscriber(
    alert => alert.Severity == AlertSeverity.Critical,
    alert => Console.WriteLine($"[filtered - critical only] {alert.DeviceId}: {alert.Message}"));

// đăng ký riêng cho micro
mic.AlertRaised += criticalSubscriber.HandleAlert;
// phát cảnh báo critical từ micro để kiểm tra bộ lọc
mic.UpdateBatteryLevel(0);
// huỷ đăng ký subscriber lọc
mic.AlertRaised -= criticalSubscriber.HandleAlert;

Console.WriteLine("\n=== 8. kiểm tra bắt ngoại lệ khi dữ liệu không hợp lệ ===");
try
{
    // thử gán giá trị pin ngoài khoảng 0-100
    mic.UpdateBatteryLevel(120);
}
catch (InvalidDeviceReadingException ex)
{
    Console.WriteLine($"[bắt ngoại lệ thành công] thiết bị: {ex.DeviceId}, giá trị sai: {ex.InvalidValue}");
    Console.WriteLine($"thông điệp: {ex.Message}");
}

Console.WriteLine("\n=== 9. kiểm tra huỷ đăng ký sự kiện ===");
// huỷ đăng ký qua coordinator
coordinator.Unsubscribe();
Console.WriteLine("đã huỷ đăng ký, thử cập nhật pin máy chiếu về 0%:");
projector.UpdateBatteryLevel(0);
Console.WriteLine("không có thông báo nào được in ra (thành công).");
