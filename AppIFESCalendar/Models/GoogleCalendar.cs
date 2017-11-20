using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Calendar.v3;

namespace AppIFESCalendar.Models
{
    public class GoogleCalendar
    {
        public UserCredential Credencial { get; set; }
        public Events Eventos { get; set; }
        public CalendarService Calendarios { get; set; }
    }
}