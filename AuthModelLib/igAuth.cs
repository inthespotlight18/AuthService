namespace AuthModelLib
{         

        public interface iGAuth
        {
            Task<string> AuthLogin();
            string ServiceTest();
            Task<string> GetProfile();

            Task<string> SendSMS(string receiverNumber, string message);

            Task<string> SendEmail(string adresantEmail, string subject, string body);

        }
    

        

    
}