/* 
Nguyễn Minh Tuấn
202419010
*/

// ngoại lệ khi giá trị đọc không hợp lệ (ngoài khoảng 0-100)
public class InvalidDeviceReadingException : Exception
{
    public string DeviceId { get; }
    public int InvalidValue { get; }

    public InvalidDeviceReadingException(string deviceId, int invalidValue)
        : base($"Invalid reading value {invalidValue} for device '{deviceId}'. Value must be between 0 and 100.")
    {
        DeviceId = deviceId;
        InvalidValue = invalidValue;
    }

    public InvalidDeviceReadingException(string deviceId, int invalidValue, string message)
        : base(message)
    {
        DeviceId = deviceId;
        InvalidValue = invalidValue;
    }

    public InvalidDeviceReadingException(string deviceId, int invalidValue, string message, Exception innerException)
        : base(message, innerException)
    {
        DeviceId = deviceId;
        InvalidValue = invalidValue;
    }
}
