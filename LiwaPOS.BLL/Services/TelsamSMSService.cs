using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Models;

namespace LiwaPOS.BLL.Services
{
    public class TelsamSmsService : ISmsService
    {
        private readonly HttpClient _httpClient;

        public TelsamSmsService()
        {
            _httpClient = new HttpClient();
        }

        public async Task SendSmsAsync<T>(T model)
        {
            if (model is not TelsamSmsDTO smsDto)
            {
                throw new ArgumentException("Model is not of type TelsamSMSDTO");
            }

            // API URL'yi oluştur
            string requestUrl = $"https://sms.telsam.com.tr:9588/direct/?cmd=sendsms" +
                                $"&kullanici={smsDto.Username}" +
                                $"&sifre={smsDto.Password}" +
                                $"&mesaj={Uri.EscapeDataString(smsDto.Message)}" +
                                $"&gsm={smsDto.ToPhoneNumber}" +
                                $"&baslik={Uri.EscapeDataString(smsDto.Title)}" +
                                $"&ahs={smsDto.AHS}" +
                                $"&nlss={smsDto.NLSS}";

            try
            {
                // Send GET request
                HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);

                // Check the response
                string responseContent = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"SMS sending failed: {responseContent}");
                }

                // Process successful response
                Console.WriteLine($"SMS successfully sent. Package ID: {responseContent}");
            }
            catch (Exception ex)
            {
                // Error handling
                throw new Exception($"An error occurred while sending SMS with Telsam: {ex.Message}");
            }
        }
    }
}
