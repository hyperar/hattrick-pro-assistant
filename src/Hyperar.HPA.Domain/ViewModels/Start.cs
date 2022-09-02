namespace Hyperar.HPA.Domain.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Hyperar.HPA.Domain.Interfaces;

    public class Start : IViewModel
    {
        public User User { get; set; }
    }
}