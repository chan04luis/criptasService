﻿
namespace Models
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPassword { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }
    }

}
