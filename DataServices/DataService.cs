using Newtonsoft.Json;
using SharedLibrary.Abstractions.Entities;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseworkMedicalServerDesktopApplication.DataServices;

class DataService
{
    private readonly HttpClient _httpClient;

    public DataService(string url)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(url);
    }

    public async Task<Doctor> GetDoctorByUsernameAsync(string username)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"doctors/{username}");
        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();

            Doctor? doctor = JsonConvert.DeserializeObject<Doctor>(json);
            return doctor;
        }
        else
        {
            return null;
        }
    }

    public async Task AddNewDoctorAsync(CreateDoctorDto newDoctor)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"doctors", newDoctor);
        if (response.IsSuccessStatusCode)
        {
            MessageBox.Show($"A new account with {newDoctor.Username}" +
                $"was sucessfuly created");
        }
        else
        {
            MessageBox.Show($"A new account creation failed");
        }

    }
}
