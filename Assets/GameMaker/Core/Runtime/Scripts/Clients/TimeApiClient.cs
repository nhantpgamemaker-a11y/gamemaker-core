// TimeApiClient.cs
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System;

namespace GameMaker.Core.Runtime
{
    public static class TimeApiClient
    {
        private const string API_URL = "https://timeapi.io/api/Time/current/zone?timeZone=UTC";

        [System.Serializable]
        private class TimeApiResponse
        {
            public int year;
            public int month;
            public int day;
            public int hour;
            public int minute;
            public int seconds;
            public int milliSeconds;
            public string dateTime;    // "2024-02-04T10:30:45.123"
            public string date;        // "02/04/2024"
            public string time;        // "10:30"
        }

        public static async UniTask<ServerTimeResponse> GetServerTime()
        {
            try
            {
                using (UnityWebRequest request = UnityWebRequest.Get(API_URL))
                {
                    request.timeout = 10;

                    await request.SendWebRequest();

                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        string json = request.downloadHandler.text;
                        TimeApiResponse timeApi = JsonUtility.FromJson<TimeApiResponse>(json);

                        // Construct DateTime
                        DateTime serverTime = new DateTime(
                            timeApi.year,
                            timeApi.month,
                            timeApi.day,
                            timeApi.hour,
                            timeApi.minute,
                            timeApi.seconds,
                            timeApi.milliSeconds
                        );

                        Debug.Log($"[TimeAPI] Server time: {serverTime:yyyy-MM-dd HH:mm:ss}");

                        return new ServerTimeResponse
                        {
                            Success = true,
                            ServerTime = serverTime,
                            Message = "Time synced from TimeAPI.io"
                        };
                    }
                    else
                    {
                        Debug.LogError($"[TimeAPI] Request failed: {request.error}");
                        return new ServerTimeResponse
                        {
                            Success = false,
                            Message = request.error
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[TimeAPI] Exception: {ex.Message}");
                return new ServerTimeResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

    }
    [Serializable]
    public class ServerTimeResponse
    {
        /// <summary>
        /// Indicates if the time request was successful
        /// </summary>
        public bool Success;

        /// <summary>
        /// The server time in UTC
        /// </summary>
        public DateTime ServerTime;

        /// <summary>
        /// Additional message (error message or source info)
        /// </summary>
        public string Message;

        /// <summary>
        /// Unix timestamp (optional, for some APIs)
        /// </summary>
        public long UnixTimestamp;

        /// <summary>
        /// Source of the time data (e.g., "WorldTimeAPI", "Google", etc.)
        /// </summary>
        public string Source;

        /// <summary>
        /// Round-trip time in milliseconds (latency)
        /// </summary>
        public float RoundTripTimeMs;

        // Constructor
        public ServerTimeResponse()
        {
            Success = false;
            ServerTime = DateTime.MinValue;
            Message = string.Empty;
            UnixTimestamp = 0;
            Source = "Unknown";
            RoundTripTimeMs = 0f;
        }

        /// <summary>
        /// Create a successful response
        /// </summary>
        public static ServerTimeResponse CreateSuccess(DateTime serverTime, string source, string message = "")
        {
            return new ServerTimeResponse
            {
                Success = true,
                ServerTime = serverTime,
                Source = source,
                Message = string.IsNullOrEmpty(message) ? $"Time synced from {source}" : message,
                UnixTimestamp = ((DateTimeOffset)serverTime).ToUnixTimeSeconds()
            };
        }

        /// <summary>
        /// Create a failed response
        /// </summary>
        public static ServerTimeResponse CreateFailure(string errorMessage, string source = "Unknown")
        {
            return new ServerTimeResponse
            {
                Success = false,
                Message = errorMessage,
                Source = source,
                ServerTime = DateTime.UtcNow // Fallback to local time
            };
        }

        public override string ToString()
        {
            if (Success)
            {
                return $"[{Source}] {ServerTime:yyyy-MM-dd HH:mm:ss UTC} (RTT: {RoundTripTimeMs:F2}ms)";
            }
            else
            {
                return $"[{Source}] Failed: {Message}";
            }
        }
    }
}