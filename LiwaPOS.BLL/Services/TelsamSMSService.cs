using LiwaPOS.BLL.Interfaces;
using LiwaPOS.Shared.Models;
using LiwaPOS.Shared.Services;

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
                await LoggingService.LogErrorAsync("Model is not of type TelsamSmsDTO", typeof(TelsamSmsService).Name, model.ToString(), new ArgumentException());
                return;
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
                    await LoggingService.LogErrorAsync($"SMS sending failed: {responseContent}", typeof(TelsamSmsService).Name, model.ToString(), new Exception());
                    return;
                }

                // Process successful response
                await LoggingService.LogInfoAsync($"SMS successfully sent. Package ID: {responseContent}", typeof(TelsamSmsService).Name, model.ToString());
            }
            catch (Exception ex)
            {
                // Error handling
                await LoggingService.LogErrorAsync($"An error occurred while sending SMS with Telsam: {ex.Message}", typeof(TelsamSmsService).Name, model.ToString(), new Exception());           
            }
        }
    }
}
