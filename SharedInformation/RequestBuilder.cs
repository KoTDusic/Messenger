using System;
using System.Text;

namespace SharedInformation
{
    public static class RequestBuilder
    {
        public static string BuildRequest<T>(RequestHeaders header, T message, string key = null)
        {
            var sb = new StringBuilder();
            sb.Append(header);
            sb.Append(Constants.RequestDelimiter);
            if (key != null)
            {
                sb.Append(key);
                sb.Append(Constants.RequestDelimiter);
            }
            sb.Append(Serializer.Serialize(message));
            return sb.ToString();
        }

        public static ParsedRequest ParseResponse(string response)
        {
            var headers = response?.Split(Constants.RequestDelimiter);
            if (headers == null)
            {
                return new ParsedRequest();
            }
            if (Enum.TryParse(headers[0], out RequestHeaders header))
            {
                var key = headers[1];
                var item = headers[2];
                return new ParsedRequest {Header = header, Key = key, Response = item};
            }
            return new ParsedRequest();
        }
    }
}