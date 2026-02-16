using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitsApp.Core.Services
{
    public enum ToastLevel { Success, Error }

    public class ToastService
    {
        // Событие, на которое подпишется UI-компонент
        public event Action<string, string, ToastLevel> OnShow;

        public void ShowSuccess(string message, string title = "Успех")
            => OnShow?.Invoke(title, message, ToastLevel.Success);

        public void ShowError(string message, string title = "Ошибка")
            => OnShow?.Invoke(title, message, ToastLevel.Error);
    }
}
