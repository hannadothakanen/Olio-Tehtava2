using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tehtävä2
{
    sealed class Mediator //suljettu luokka
    {
        private static Mediator instance = new Mediator();
        public static Mediator Instance //Staattinen ominaisuus instance
        {
            get { return instance; }
        }
        private Mediator() 
        { }

        public event EventHandler<JobChangedEventArgs> JobChanged;

        public void OnJobChanged(object sender, Job job) 
        {
            EventHandler<JobChangedEventArgs> jobChangeDelegate = JobChanged as EventHandler<JobChangedEventArgs>;

            if (jobChangeDelegate != null)
            {
                jobChangeDelegate(sender, new JobChangedEventArgs() { Job = job });
            }
        }
    }
}
