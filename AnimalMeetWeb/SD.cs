using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalMeetWeb
{
    public static class SD
    {
        public static string APIBaseUrl = "http://localhost:56614/";
        public static string AnimalSubTypeAPIPath = APIBaseUrl + "api/animalsubtype/";
        public static string AnimalTypeAPIPath = APIBaseUrl + "api/animaltype/";
        public static string AuthUserAPIPath = APIBaseUrl + "api/authuser/";
        public static string CityAPIPath = APIBaseUrl + "api/city/";
        public static string PetsAPIPath = APIBaseUrl + "api/pets/";
    }
}
