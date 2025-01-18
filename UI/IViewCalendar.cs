using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UI
{
    public interface IViewCalendar
    {
        public DateTime CurrentDate { get; set; }
        public void NextView();

        public void PrevView();

        public void UpdateView();
    }
}
