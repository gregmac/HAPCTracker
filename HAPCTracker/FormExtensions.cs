using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAPCTracker
{
    public static class FormExtensions
    {
        /// <summary>
        /// Sets <see cref="NotifyIcon.Visible"/> to false.
        /// </summary>
        /// <param name="notifyIcon"></param>
        public static void Hide(this NotifyIcon notifyIcon)
        {
            if (notifyIcon != null) notifyIcon.Visible = false;
        }
        
    }
}
