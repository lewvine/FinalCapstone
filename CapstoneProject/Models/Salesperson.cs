using GoogleMapsApi;
using GoogleMapsApi.Entities.Geocoding.Request;
using GoogleMapsApi.Entities.Geocoding.Response;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject.Models
{
    public class Salesperson
    {
        [Key]
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public string CityAddress { get; set; }
        public string StateAddress { get; set; }
        public List<Appointment> Appointments { get; set; }
        public string ZipAddress { get; set; }
        public double? LatAddress { get; set; }
        public double? LongAddress { get; set; }
        public string EMailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public double TotalSales { get; set; } = 0;
        public double TotalPossibleSales { get; set; } = 0;
        public int TotalProjects { get; set; } = 0;
        public int TotalOpportunities { get; set; } = 0;

        public ICollection<Project> Projects { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}")]
        public DateTime? NewAppointment { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        public void SetGeocode(string address)
        {
            GeocodingRequest geocodeRequest = new GeocodingRequest()
            {
                Address = address,
                ApiKey = Utilities.APIs.MapsKey,
                SigningKey = "Lew Vine"
            };
            var geoCodingEngine = GoogleMaps.Geocode;
            GeocodingResponse geocode = geoCodingEngine.Query(geocodeRequest);
            this.LatAddress = geocode.Results.First().Geometry.Location.Latitude;
            this.LongAddress = geocode.Results.First().Geometry.Location.Longitude;
        }
        public Appointment GetNextAppointment()
        {
            return this.Appointments
                .Where(a => a.IsBooked == true)
                .OrderBy(a => a.AppointmentStart)
                .FirstOrDefault();
        }

        public bool HasProjects(List<Project> projects)
        {
            if(Projects.Count == 0)
            {
                return false;
            }
            if (Projects.Where(p => p.SalesID == this.id).FirstOrDefault() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
