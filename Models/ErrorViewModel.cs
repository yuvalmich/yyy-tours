using System;

namespace yyytours.Models
{
    public class ErrorViewModel
    {
        public string ErrorDescription { get; set; }
        public string ControllerToLink { get; set; }
        public string ActionToLink { get; set; }
        public string TextToLink { get; set; }

        public bool ShowErrorDescription => !string.IsNullOrEmpty(ErrorDescription);
        public bool ShowLink => !string.IsNullOrEmpty(TextToLink) && !string.IsNullOrEmpty(ActionToLink) && !string.IsNullOrEmpty(ControllerToLink);
    }
}
