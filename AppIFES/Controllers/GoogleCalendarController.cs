using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using AppIFESCalendar.Models;

namespace AppIFESCalendar.Controllers
{
    public class GoogleCalendarController : Controller
    {
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "Calendar API Quickstart";

        private GoogleCalendar googlecalendario = new GoogleCalendar();

        // GET: Categorias
        public ActionResult Adicionar()
        {


            return null;
        }

            public ActionResult Index()
        {
            googlecalendario.Credencial = Login();

            return View(googlecalendario);
        }

        public ActionResult Calendario()
        {
            googlecalendario.Eventos = GetData(Login());
            return View(googlecalendario);
        }

        public ActionResult ListaCalendario()
        {
            googlecalendario.Calendarios = CalendarSer(Login());

            return View(googlecalendario);
        }

        private CalendarService CalendarSer(UserCredential credential)
        {
            // Create Calendar Service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            return service;
        }


        private Events GetData(UserCredential credential)
        {

            // Define parameters of request.                          
            EventsResource.ListRequest request = CalendarSer(credential).Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 100;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            Events events = request.Execute();
            
            return events;                       
        }

        static UserCredential Login()
        {
            UserCredential credential;

            using (var stream = new FileStream(AppDomain.CurrentDomain.BaseDirectory+@"Components\client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync( GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None, new FileDataStore(credPath, true)).Result;
            }

            return credential;
        }
    }
}