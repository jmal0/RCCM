using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RCCM.UI
{
    public partial class WFOVViewForm : Form
    {
        public WFOVViewForm(RCCMSystem rccm, WFOV camera, ObservableCollection<MeasurementSequence> cracks)
        {
            InitializeComponent();
        }
    }
}
