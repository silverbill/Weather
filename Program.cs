using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.IO;


namespace ConsoleApplication
{
    public class Program
    {
            public static void Main(string[] args)
        {
            Console.WriteLine("Enter a 5 Digit Zip Code for your Wx");
            prompt().Wait();

            

            
        }
        public static async Task prompt(){
            string input = Console.ReadLine();
            string result = await getUrl("https://maps.googleapis.com/maps/api/geocode/json?address="+input+
            "=AIzaSyC_33A_wFm7dUaMlpwiUy_5huiuJ7XAkYs");
            Console.WriteLine(result);
            // deserize

            Google g = JsonConvert.DeserializeObject<Google>(result);
            double lat = g.results.ElementAt(0).geometry.location.lat;
            double lng = g.results.ElementAt(0).geometry.location.lng;
            
            String LatLng = (lat.ToString()+","+lng.ToString());
            string callDarkSky = await getUrl("https://api.darksky.net/forecast/8ffe5c523d0f256c9cf3a1dd22c7dad8/"+LatLng);
            Console.WriteLine(callDarkSky);

            DarkSky dS = JsonConvert.DeserializeObject<DarkSky>(callDarkSky);
            string currSum = dS.daily.summary;
            string currCond = dS.currently.summary;
            
            int sunSet = dS.daily.data.ElementAt(4).sunsetTime;
            int sunRise = dS.daily.data.ElementAt(3).sunriseTime;
            double temp = dS.currently.temperature;
            
            DateTime dateTimeRise = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateTimeRise = dateTimeRise.AddSeconds(sunRise).ToLocalTime();

            DateTime dateTimeSet = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateTimeSet = dateTimeSet.AddSeconds(sunSet).ToLocalTime();
            
            Console.WriteLine(currSum + " " + currCond + ", " + temp + "deg F" + ". " 
            + "Sunrise is: " + dateTimeRise + ". " + "Sunset is: " + dateTimeSet + ".");
            Console.ReadLine();

            Directory.CreateDirectory("html");
            // File.WriteAllText(@"html/index.html", .ToString());    
                                                //what goes right here.  was football

            

        }
        public static async Task<string> getUrl(string url){
            var http = new HttpClient();
            string reply = await http.GetStringAsync(url);
            return reply;

        }
        
    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }

    public class Northeast
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Southwest
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Bounds
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Northeast2
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Southwest2
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Viewport
    {
        public Northeast2 northeast { get; set; }
        public Southwest2 southwest { get; set; }
    }

    public class Geometry
    {
        public Bounds bounds { get; set; }
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }

    public class Result
    {
        public List<AddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public bool partial_match { get; set; }
        public string place_id { get; set; }
        public List<string> types { get; set; }
    }


    public class Google
    {
        public List<Result> results { get; set; }
        public string status { get; set; }
    }

//////DarkSky Classes
public class Currently
{
    public int time { get; set; }
    public string summary { get; set; }
    public string icon { get; set; }
    public int nearestStormDistance { get; set; }
    public int nearestStormBearing { get; set; }
    public int precipIntensity { get; set; }
    public int precipProbability { get; set; }
    public double temperature { get; set; }
    public double apparentTemperature { get; set; }
    public double dewPoint { get; set; }
    public double humidity { get; set; }
    public double windSpeed { get; set; }
    public int windBearing { get; set; }
    //public int visibility { get; set; }
    public double cloudCover { get; set; }
    public double pressure { get; set; }
    public double ozone { get; set; }
}

public class Datum
{
    public int time { get; set; }
    public int precipIntensity { get; set; }
    public int precipProbability { get; set; }
}

public class Minutely
{
    public string summary { get; set; }
    public string icon { get; set; }
    public List<Datum> data { get; set; }
}

public class Datum2
{
    public int time { get; set; }
    public string summary { get; set; }
    public string icon { get; set; }
    public double precipIntensity { get; set; }
    public double precipProbability { get; set; }
    public double temperature { get; set; }
    public double apparentTemperature { get; set; }
    public double dewPoint { get; set; }
    public double humidity { get; set; }
    public double windSpeed { get; set; }
    public int windBearing { get; set; }
    public double visibility { get; set; }
    public double cloudCover { get; set; }
    public double pressure { get; set; }
    public double ozone { get; set; }
    public string precipType { get; set; }
}

public class Hourly
{
    public string summary { get; set; }
    public string icon { get; set; }
    public List<Datum2> data { get; set; }
}

public class Datum3
{
    public int time { get; set; }
    public string summary { get; set; }
    public string icon { get; set; }
    public int sunriseTime { get; set; }
    public int sunsetTime { get; set; }
    public double moonPhase { get; set; }
    public double precipIntensity { get; set; }
    public double precipIntensityMax { get; set; }
    public double precipProbability { get; set; }
    public double temperatureMin { get; set; }
    public int temperatureMinTime { get; set; }
    public double temperatureMax { get; set; }
    public int temperatureMaxTime { get; set; }
    public double apparentTemperatureMin { get; set; }
    public int apparentTemperatureMinTime { get; set; }
    public double apparentTemperatureMax { get; set; }
    public int apparentTemperatureMaxTime { get; set; }
    public double dewPoint { get; set; }
    public double humidity { get; set; }
    public double windSpeed { get; set; }
    public int windBearing { get; set; }
    public double visibility { get; set; }
    public double cloudCover { get; set; }
    public double pressure { get; set; }
    public double ozone { get; set; }
    public int? precipIntensityMaxTime { get; set; }
    public string precipType { get; set; }
}

public class Daily
{
    public string summary { get; set; }
    public string icon { get; set; }
    public List<Datum3> data { get; set; }
}


public class DarkSky
{
    public double latitude { get; set; }
    public double longitude { get; set; }
    public string timezone { get; set; }
    public int offset { get; set; }
    public Currently currently { get; set; }
    public Minutely minutely { get; set; }
    public Hourly hourly { get; set; }
    public Daily daily { get; set; }
    
}


            // 1.get the geocoordinates of the zip code with the Google Geocoding API.
            // 2.push lat/long to DarkSky 
            // 3.return weather
                //Current conditions at that location.
                //daily forecast for that location (for the next 8 days).
                //Sunrise and sunset times.
                //Any current weather alerts.
        }
    }

//var web = new WebClient();
// https://api.darksky.net/forecast/8ffe5c523d0f256c9cf3a1dd22c7dad8/37.8267,-122.4233

// google  example lat/long lookup https://maps.googleapis.com/maps/api/geocode/json?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&key=YOUR_API_KEY   
// my key gets appended to end of above AIzaSyC_33A_wFm7dUaMlpwiUy_5huiuJ7XAkYs
