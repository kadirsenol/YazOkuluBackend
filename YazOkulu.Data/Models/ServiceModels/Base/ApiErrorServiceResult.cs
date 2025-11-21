using YazOkulu.Core.Enums;
namespace YazOkulu.Data.Models.ServiceModels.Base
{
    public class ApiErrorServiceResult<T>
    {
        public T ResultObject { get; set; }
        public string Message { get; set; }
        public MessageTypeEnum MessageType { get; set; } = MessageTypeEnum.None;
        public int StatusCode { get; set; }
        public ApiErrorServiceResult() { }
        public ApiErrorServiceResult(T result) => ResultObject = result;
        public ApiErrorServiceResult(MessageTypeEnum messageType, string message)
        {
            Message = message;
            MessageType = messageType;
        }
    }
}