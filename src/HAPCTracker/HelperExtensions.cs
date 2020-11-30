using Microsoft.Win32;
using System;
using System.Text;
using System.Windows.Forms;

namespace HAPCTracker
{
    internal static class HelperExtensions
    {
        /// <summary>
        /// Sets <see cref="NotifyIcon.Visible"/> to false.
        /// </summary>
        /// <param name="notifyIcon"></param>
        public static void Hide(this NotifyIcon notifyIcon)
        {
            if (notifyIcon != null) notifyIcon.Visible = false;
        }

        /// <summary>
        /// Set up databinding on a <see cref="TextBox"/> control.
        /// </summary>
        /// <param name="control">The textbox to bind to</param>
        /// <param name="dataSource">The data source object</param>
        /// <param name="dataMember">The property of <paramref name="dataMember"/> to bind.</param>
        public static void Bind(this TextBox control, object dataSource, string dataMember)
            => control.DataBindings.Add(nameof(control.Text), dataSource, dataMember);

        /// <summary>
        /// Set up databinding on a <see cref="NumericUpDown"/> control.
        /// </summary>
        /// <param name="control">The textbox to bind to</param>
        /// <param name="dataSource">The data source object</param>
        /// <param name="dataMember">The property of <paramref name="dataMember"/> to bind.</param>
        public static void Bind(this NumericUpDown control, object dataSource, string dataMember)
            => control.DataBindings.Add(nameof(control.Value), dataSource, dataMember);

        /// <summary>
        /// Get a typed value from a registry entry
        /// </summary>
        /// <typeparam name="T">The type of data to r eturn</typeparam>
        /// <param name="key">The registry key to read</param>
        /// <param name="name">The name of the property</param>
        /// <param name="defaultValue">The default value to use</param>
        /// <returns></returns>
        public static T GetValue<T>(this RegistryKey key, string name, T defaultValue = default)
            => (T)key.GetValue(name, defaultValue);

        /// <summary>
        /// Unwrap all exception messages (traversing into inner exceptions) into a single string.
        /// </summary>
        public static string GetMessages(this Exception ex)
        {
            var result = new StringBuilder();
            while (ex != null)
            {
                result.Append(ex.Message);
                if (!ex.Message.EndsWith(".")) result.Append(": ");
                ex = ex.InnerException;
            }
            return result.ToString();
        }
    }
}
