
using GardenEinfach.Model;
using GardenEinfach.Views.TabbedPages;
using Newtonsoft.Json;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GardenEinfach.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        string GeocodePosition = "";
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { SetProperty(ref _Name, value); }
        }

        private int _LowerValue;
        public int LowerValue
        {
            get { return _LowerValue; }
            set { SetProperty(ref _LowerValue, value); }
        }
        private int _UpperValue;
        public int UpperValue
        {
            get { return _UpperValue; }
            set { SetProperty(ref _UpperValue, value); }
        }

        private ObservableCollection<PostZahl> _PlzList;
        public ObservableCollection<PostZahl> PlzList
        {
            get { return _PlzList; }
            set { SetProperty(ref _PlzList, value); }
        }


        public SearchViewModel()
        {
            Name = "test";
            LowerValue = 15;
            UpperValue = 60;
            getData();
            SuggestionsVisibility = false;
        }

        private string _Plz;
        public string Plz
        {
            get { return _Plz; }
            set { SetProperty(ref _Plz, value);

                FilterPlz(Plz);


            }
        }

        private string _Ort;
        public string Ort
        {
            get { return _Ort; }
            set { SetProperty(ref _Ort, value);

                FilterOrt(Ort);
            }
        }
        private PostZahl _SelectedPlz;
        public PostZahl SelectedPlz
        {
            get { return _SelectedPlz; }
            set { SetProperty(ref _SelectedPlz, value);

                if (SelectedPlz != null)
                {
                    Plz = SelectedPlz.PLZ;
                    Ort = SelectedPlz.Ort;
                }
            }
        }

        private bool _SuggestionsVisibility;
        public bool SuggestionsVisibility
        {
            get { return _SuggestionsVisibility; }
            set { SetProperty(ref _SuggestionsVisibility, value); }
        }
        void FilterPlz(string entry)
        {

            if (entry.Length >= 1)
            {
                var keyword = entry;
                var suggestions = Plzs.Where(c => c.PLZ.ToLower().Contains(keyword.ToLower()));
                PlzList = new ObservableCollection<PostZahl>(suggestions);
                SuggestionsVisibility = true;
            }
            else
            {
                PlzList.Clear();
                SuggestionsVisibility = false;

            }
            
        }
        void FilterOrt(string entry)
        {

            if (entry.Length >= 1)
            {
                var keyword = entry;
                var suggestions = Plzs.Where(c => c.Ort.ToLower().Contains(keyword.ToLower()));
                PlzList = new ObservableCollection<PostZahl>(suggestions);
                SuggestionsVisibility = true;
            }
            else
            {
                PlzList.Clear();
                SuggestionsVisibility = false;

            }

        }


        public  ObservableCollection<PostZahl> Plzs { get; set; }
        private void getData()
        {
            var assembly = typeof(Search).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("GardenEinfach.Data.Data.json");
            using (var reader = new System.IO.StreamReader(stream))
            {
                var json = reader.ReadToEnd();

                List<PostZahl> mylist = JsonConvert.DeserializeObject<List<PostZahl>>(json);
                Plzs = new ObservableCollection<PostZahl>(mylist);

            }

        }
        private DelegateCommand _GetPosition;
        public DelegateCommand CommandName =>
            _GetPosition ?? (_GetPosition = new DelegateCommand(GetPositionM));

        async void GetPositionM()
        {
           await OnGetPosition();
        }

         async Task OnGetPosition()
        {
           
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    double lat = location.Latitude;
                    double longt = location.Longitude;
                    Location trofaiach = new Location(lat, longt);
                    Location Bruck = new Location(47.454379952627484, 15.332897391281387);
                    double miles = Location.CalculateDistance(trofaiach, Bruck, DistanceUnits.Kilometers);
                }

                var locations = await Geocoding.GetLocationsAsync("14000");
               // Location location = locations.FirstOrDefault();
                if (location == null)
                {
                    GeocodePosition = "Unable to detect locations";
                }
                else
                {
                    GeocodePosition =
                        $"{nameof(location.Latitude)}: {location.Latitude}\n" +
                        $"{nameof(location.Longitude)}: {location.Longitude}\n";
                }


                var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                
                Placemark placemark = placemarks.FirstOrDefault();

                string GeocodeAddress = "";
                if (placemark == null)
                {
                    GeocodeAddress = "Unable to detect placemarks.";
                }
                else
                {
                    GeocodeAddress =
                        $"{nameof(placemark.AdminArea)}: {placemark.AdminArea}\n" +
                        $"{nameof(placemark.CountryCode)}: {placemark.CountryCode}\n" +
                        $"{nameof(placemark.CountryName)}: {placemark.CountryName}\n" +
                        $"{nameof(placemark.FeatureName)}: {placemark.FeatureName}\n" +
                        $"{nameof(placemark.Locality)}: {placemark.Locality}\n" +
                        $"{nameof(placemark.PostalCode)}: {placemark.PostalCode}\n" +
                        $"{nameof(placemark.SubAdminArea)}: {placemark.SubAdminArea}\n" +
                        $"{nameof(placemark.SubLocality)}: {placemark.SubLocality}\n" +
                        $"{nameof(placemark.SubThoroughfare)}: {placemark.SubThoroughfare}\n" +
                        $"{nameof(placemark.Thoroughfare)}: {placemark.Thoroughfare}\n";
                }

            }
            catch (Exception ex)
            {
                GeocodePosition = $"Unable to detect locations: {ex.Message}";
            }
            
        }



    }
}
