using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Tehtävä2
{
    static class Application
    {
        private static ConsoleControl JobMenu;  //muuttujat
        private static ConsoleControl JobDetails;
        private static ConsoleControl JobEmployees;

        private static void BindMenuData(List<Job> jobs)
        {
            if (JobMenu.Items == null)
            {
                JobMenu.Items = new List<string>();
            }
            foreach (Job e in jobs)
            {
                JobMenu.BackColor = ConsoleColor.Gray;
                JobMenu.TextColor = ConsoleColor.Blue;
                JobMenu.Items.Add($"{e.Id} {e.Title}");
            }
        }

        private static void BindDetailsData(Job job)
        {
            if (JobDetails.Items == null)
            {
                JobDetails.Items = new List<string>();
            }
            else
            {
                JobDetails.Items.Clear();
            }
            JobDetails.Items.Add("TYÖN TIEDOT");
            JobDetails.Items.Add($"Id: {job.Id}");
            JobDetails.Items.Add($"Nimi: {job.Title}");
            JobDetails.Items.Add($"Alkaa: {job.StartDate.ToShortDateString()}");
            JobDetails.Items.Add($"Loppuu: {job.EndDate.ToShortDateString()}");
        }

        private static void BindEmployeesData(Job job)
        {
            if (JobEmployees.Items == null)
            {
                JobEmployees.Items = new List<string>();
            }
            else
            {
                JobEmployees.Items.Clear();
            }
            foreach (Employee employee in Data.employees)
            {
                if (employee.Jobs.Contains(job))
                {
                    JobEmployees.Items.Add(employee.Name);
                }
            }
        }

        private static void Initialize()
        {
            JobMenu = new ConsoleControl(col: 1, row: 2, width: WindowWidth / 2 - 1, height: Data.jobs.Count);
            JobDetails = new ConsoleControl(col: WindowWidth / 2 + 1, row: 2, width: WindowWidth / 2 - 1, height: 5);
            JobEmployees = new ConsoleControl(col: WindowWidth / 2 + 1, row: JobDetails.Height + 3, width: WindowWidth / 2 - 1, height: WindowHeight - JobDetails.Height - 1);

            JobMenu.BackColor = ConsoleColor.Gray;
            JobMenu.TextColor = ConsoleColor.Blue;
            JobDetails.BackColor = ConsoleColor.Gray;
            JobDetails.TextColor = ConsoleColor.Green;
            JobEmployees.BackColor = ConsoleColor.Gray;
            JobEmployees.TextColor = ConsoleColor.Red;

            BindMenuData(Data.jobs);

            Mediator.Instance.JobChanged += Hanskaaja;
        }

        static void Hanskaaja(object sender, JobChangedEventArgs i)
        {
            BindDetailsData(i.Job);
            BindEmployeesData(i.Job);
        }

        private static void MenuSelectionChanged(int arvo)
        {
            foreach (Job job in Data.jobs)
            {
                if (job.Id.Equals(arvo))
                {
                    Mediator.Instance.OnJobChanged(JobMenu, job);
                    JobDetails.Draw();
                    JobEmployees.Draw();
                }
            }
        }

        public static void Run() //julkinen metodi Run, joka kutsuu ensin metodia Initialize ja sen jälkeen Draw
        {
            Initialize();
            JobMenu.Draw();

            do
            {
                SetCursorPosition(0, 0);
                Write("Valitse työn Id (nolla lopettaa):"); //pyydetään syöte
                if (int.TryParse(ReadLine(),out int syote)) //syöte pakotetaan kokonaisluvuksi
                {
                    if (syote == 0) //jos syöte on nolla, lopetetaan
                    {
                        SetCursorPosition(0, 10);
                        break;
                    }
                    MenuSelectionChanged(syote);
                    if (syote > JobMenu.Items.Count || syote < 1) //jos Id ei löydy, pyydetään käyttäjää syöttämään uusi luku
                    {
                        SetCursorPosition(0, 10);
                        Write("Virheellinen syöte, paina enter.");
                        ReadLine();
                    } 
                }
            } while (true);
        }
    }
}
