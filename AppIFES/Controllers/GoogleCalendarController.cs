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
using AppIFES.Models;
using System.Data.Entity;

namespace AppIFESCalendar.Controllers
{
    public class GoogleCalendarController : Controller
    {
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "Google Calendar API Quickstart";
        string calendarid = "primary";

        private DadosBanco db = new DadosBanco();

        private GoogleCalendar googlecalendario = new GoogleCalendar();

        // GET: Adicionar
        public ActionResult Adicionar(int idagenda, DateTime date, string titulo, string descricao, string local)
        {
            if (Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            Agenda agenda = db.Agenda.Find(idagenda);
            Disciplina disciplina = db.Disciplinas.Where(a => a.iddisciplina == agenda.iddisciplina).Include(a => a.usuario).FirstOrDefault();
            List<Alunodisciplina> alunodisciplinas = db.Alunodisciplinas.Where(a => a.iddisciplina == agenda.iddisciplina).Include(a => a.aluno).ToList();

            List<EventAttendee> Contatos = new List<EventAttendee>();
            Contatos.Add(new EventAttendee { Email = disciplina.usuario.email });
            
            foreach (var alunodisciplina in alunodisciplinas)
            {
                Contatos.Add(new EventAttendee { Email = alunodisciplina.aluno.email });
            }

            googlecalendario.Evento = new Event()
            {
                Created = date,
                Description = descricao,
                Location = local,
                Kind = "",
                GuestsCanInviteOthers = true,
                Summary = titulo,
                Sequence = idagenda,
                Start = new EventDateTime()
                {
                    DateTime = date,
                    TimeZone = "America/Boa_Vista"
                },
                End = new EventDateTime()
                {
                    DateTime = date,
                    TimeZone = "America/Boa_Vista"
                },
                Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=1" },
                Attendees = Contatos,
                Reminders = new Event.RemindersData()
                {
                    UseDefault = false,
                    Overrides = new EventReminder[] {
                        new EventReminder() { Method = "email", Minutes = 24 * 60 },
                        new EventReminder() { Method = "sms", Minutes = 24 * 60 },
                        new EventReminder() { Method = "popup", Minutes = 24 * 60 }
                    }                
                }
            };

            googlecalendario.Calendarios = CalendarSer(Login());
            String idEvento = googlecalendario.Calendarios.Events.Insert(googlecalendario.Evento, calendarid).Execute().Id;
            agenda.idevento = idEvento;

            db.Entry(agenda).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "Agenda");
        }

        // GET: Alterar
        public ActionResult Alterar(int idagenda, DateTime date, string titulo, string descricao, string local)
        {
            if (Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }


            Agenda agenda = db.Agenda.Find(idagenda);
            Disciplina disciplina = db.Disciplinas.Where(a => a.iddisciplina == agenda.iddisciplina).Include(a => a.usuario).FirstOrDefault();
            List<Alunodisciplina> alunodisciplinas = db.Alunodisciplinas.Where(a => a.iddisciplina == agenda.iddisciplina).Include(a => a.aluno).ToList();

            List<EventAttendee> Contatos = new List<EventAttendee>();
            
            Contatos.Add(new EventAttendee { Email = disciplina.usuario.email });

           foreach (var alunodisciplina in alunodisciplinas)
           {
               Contatos.Add(new EventAttendee { Email = alunodisciplina.aluno.email });
           }


           googlecalendario.Evento = new Event()
           {
               Id = agenda.idevento,
               Created = date,
               Description = descricao,
               Location = local,
               Kind = "",
               GuestsCanInviteOthers = true,
               Summary = titulo,
               Sequence = idagenda,
               Start = new EventDateTime()
               {
                   DateTime = date,
                   TimeZone = "America/Boa_Vista"
               },
               End = new EventDateTime()
               {
                   DateTime = date,
                   TimeZone = "America/Boa_Vista"
               },
               Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=2" },
               Attendees = Contatos,
               Reminders = new Event.RemindersData()
               {
                   UseDefault = false,
                   Overrides = new EventReminder[] {
                       new EventReminder() { Method = "email", Minutes = 24 * 60 },
                       new EventReminder() { Method = "sms", Minutes = 24 * 60 },
                       new EventReminder() { Method = "popup", Minutes = 24 * 60 }
                   }
               }

           };
           
            googlecalendario.Calendarios = CalendarSer(Login());
            googlecalendario.Calendarios.Events.Get(calendarid, agenda.idevento);
            googlecalendario.Calendarios.Events.Update(googlecalendario.Evento, calendarid, agenda.idevento).Execute();

            return RedirectToAction("Index", "Agenda");
        }

        public ActionResult Apagar(string idevento)
        {
            if (Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            if (idevento != null)
            {
                googlecalendario.Calendarios = CalendarSer(Login());
                googlecalendario.Calendarios.Events.Delete(calendarid, idevento).Execute();
            }
            return RedirectToAction("Index", "Agenda");
        }

        private CalendarService CalendarSer(UserCredential credential)
        {
            // Create Calendar Service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
                //ApiKey = Chave
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

            using (var stream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"Components\client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials");
                Scopes = new[] { CalendarService.Scope.Calendar };
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets, Scopes, "user", CancellationToken.None, new FileDataStore(credPath, true)).Result;
            }

            return credential;
        }
    }
}