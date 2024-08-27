using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CourseworkMedicalServerDesktopApplication.DataServices;

class DataService
{
    private readonly HttpClient _httpClient;

    public DataService(string url)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(url);
    }
}
