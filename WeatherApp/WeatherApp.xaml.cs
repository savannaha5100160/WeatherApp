using WeatherApp.Services;

namespace WeatherApp;

public partial class WeatherApp : ContentPage     //the word in this line needs to be WeatherPage  instead of WeatherApp
{
    public List<Models.List> WeatherList;
    private double latitude;
    private double longitude;
	public WeatherApp()   //the word in this line needs to be WeatherPage instead of WeatherApp
	{
		InitializeComponent();
        WeatherList = new List<Models.List>();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await GetLocation();
        await GetWeatherDataByLocation(latitude, longitude);
    }
    public async Task GetLocation()
    {
        var location = await Geolocation.GetLocationAsync();
        latitude = location.Latitude;
        longitude = location.Longitude;
    }
    private async void TapLocation_Tapped(object sender, EventArgs e)
    {
        await GetLocation();
        await GetWeatherDataByLocation(latitude, longitude);
    }
    public async Task GetWeatherDataByLocation(double latitude, double longitude)
    {
        var result = await ApiService.GetWeather(latitude, longitude);
        UpdateUI(result);
    
    }
    private async void ImageButton_Clicked(object sender,EventArgs e)
    {
        var response = await DisplayPromptAsync(title: "", message: "", placeholder: "Search weather by city", accept: "Search", cancel: "Cancel");
        if (response != null)
        {
            await GetWeatherDataByCity(response);
        }
    }
    public async Task GetWeatherDataByCity(string city)
    {
        var result = await ApiService.GetWeatherByCity(city);
        UpdateUI(result);
    
    }
    public void UpdateUI(dynamic result)
    {
        foreach (var item in result.List)
        {
            WeatherList.Add(item);
        }
        CvWeather.ItemsSource = WeatherList;


        LblCity.Text = result.City.Name;
        LblWeatherDescription.Text = result.List[0].Weather[0].Description;
        LblTemperature.Text = result.List[0].Main.Temperature + "c";
        LblHumidity.Text = result.List[0].Main.Humidity + "%";
        LblWind.Text = result.List[0].Wind.Speed + "km/h";
        ImgWeatherIcon.Source = result.List[0].Weather[0].customIcon;
    }
}
