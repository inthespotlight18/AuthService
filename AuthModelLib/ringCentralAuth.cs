using RingCentral;


namespace AuthModelLib
{
    public class RingCentralAuth : iGAuth
    {
        static string RECIPIENT;
        static RestClient? restClient;

        const string clientId = "9Jj0p7Ph3dSdAlx8g9Tll2";
        const string secret = "YxbUCO9tMYlaYAKv6DmgW7WSV3vWUihyJenFiARsXfBZ";
        const string uri = "https://platform.devtest.ringcentral.com";

        private static string fromNumber;
        //private static string fromNumber   // property
        //{
        //    get { return fromNumber; }
        //    set { fromNumber = value; }
        //}

        public async Task<string> GetProfile()
        {
            Console.WriteLine("RingCentralAuth->GetProfile started: ");
            try
            {
                await AuthLogin();
                

                // For the purpose of testing the code, we put the SMS recipient number in the environment variable.
                // Feel free to set the SMS recipient directly.
                //RECIPIENT = Environment.GetEnvironmentVariable("SMS_RECIPIENT");
                RECIPIENT = "12369713928";

                await read_extension_phone_number_detect_sms_feature();

                Console.WriteLine("RingCentralAuth->GetProfile(): OK");
                return "OK";
            }
            catch (Exception ex)
            {
                string exceptionMessage = "RingCentralAuth->GetProfile(): [" + ex.Message + "]";
                Console.WriteLine(exceptionMessage);
                return "FAIL|error";
            }
        }

        public async Task<string> AuthLogin()
        {
            try
            {
                // Instantiate the SDK
                restClient = new RestClient(
                    clientId, secret, uri, "DaniilAPP");

                // Authenticate a user using a personal JWT token
                await restClient.Authorize("eyJraWQiOiI4NzYyZjU5OGQwNTk0NGRiODZiZjVjYTk3ODA0NzYwOCIsInR5cCI6IkpXVCIsImFsZyI6IlJTMjU2In0.eyJhdWQiOiJodHRwczovL3BsYXRmb3JtLmRldnRlc3QucmluZ2NlbnRyYWwuY29tL3Jlc3RhcGkvb2F1dGgvdG9rZW4iLCJzdWIiOiI4NTI2NzAwMDUiLCJpc3MiOiJodHRwczovL3BsYXRmb3JtLmRldnRlc3QucmluZ2NlbnRyYWwuY29tIiwiZXhwIjoxNzMxNjI4Nzk5LCJpYXQiOjE2OTk5MDIwNDAsImp0aSI6IndqY3V2ei1qUVNDUkpRcEk2TW1RTWcifQ.edFJnw_Ff4b053xTJSvidW6yGTDMWzIWwI9W7fhHDpWjIeAwYbhmuVZG_ZCuKe-eZQ4xlXXOPDolxbd7I1ZSHeTJcHFOL9bx-ixjRXTlxwvGVZFdFInyLNx9GLvRZ-qAsZe5V8bLo-dpEqgHcfeWcJpD1BkThiV2oB6cMjMQ8kPIxsO8lJ1HKCoTllCklTp5VehW6nV6SdLyNFu5DqVB04y0VNpeMk6hx42p9nZRIFD8oyKKQ75JuwWBNW0NulBLIk5Kx-d7an8ong_YIdIZeWC5QZ2TGHBfSZgAHn_Kph-dLvB3ggWsnYjsV0UWpNjtwEuhjRUmjHnsUoJuDTpZcA");


                Console.WriteLine("RingCentralAuth->AuthLogin() : OK");
                return "OK";

            }
            catch (Exception ex)
            {
                string exceptionMessage = "RingCentralAuth->AuthLogin(): [" + ex.Message + "]";
                Console.WriteLine(exceptionMessage);
                return "FAIL|error";
            }
        }

        /*
          Read phone number(s) that belongs to the authenticated user and detect if a phone number
          has the SMS capability
        */
        static private async Task read_extension_phone_number_detect_sms_feature()
        {
            try
            {
                var resp = await restClient.Restapi().Account().Extension().PhoneNumber().Get();
                foreach (var record in resp.records)
                {
                    foreach (var feature in record.features)
                    {
                        if (feature == "SmsSender")
                        {
                            // If a user has multiple phone numbers, check and decide which number
                            // to be used for sending SMS message.

                            fromNumber = record.phoneNumber;
                           
                            return;
                        }
                    }
                }
                if (resp.records.Length == 0)
                {
                    Console.WriteLine("This user does not own a phone number!");
                }
                else
                {
                    Console.WriteLine("None of this user's phone number(s) has the SMS capability!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("read_extension_phone_number_detect_sms_feature()->: [{ 0}]", ex.Message);
            }
        }

        /*
         Check the sending message status until it's no longer in the queued status
        */
        static private async Task check_message_status(string messageId)
        {
            try
            {
                var resp = await restClient.Restapi().Account().Extension().MessageStore(messageId).Get();
                Console.WriteLine("SMS message status: " + resp.messageStatus);
                if (resp.messageStatus == "Queued")
                {
                    Thread.Sleep(2000);
                    await check_message_status(resp.id.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("RingCentralAuth->check_message_status(): [{0}]", ex.Message);
            }
        }

        public string ServiceTest()
        {
            return "RingCentralAuth->ServiceTest()";
        }

        public async Task<string> SendSMS(string receiverNumber, string message)
        {
            string res = await GetProfile();

            if (res.Contains("OK"))
            {
                try
                {
                    var requestBody = new CreateSMSMessage();
                    requestBody.from = new MessageStoreCallerInfoRequest
                    {
                        phoneNumber = fromNumber
                    };
                    requestBody.to = new MessageStoreCallerInfoRequest[] 
                    {
                        new MessageStoreCallerInfoRequest { phoneNumber = receiverNumber }
                    };
                    // To send group messaging, add more (max 10 recipients) 'phoneNumber' object. E.g.
                    /*
                    requestBody.to = new MessageStoreCallerInfoRequest[] {
                      new MessageStoreCallerInfoRequest { phoneNumber = "Recipient_1_Number" },
                      new MessageStoreCallerInfoRequest { phoneNumber = "Recipient_2_Number" }
                    };
                    */
                    requestBody.text = message;

                    var resp = await restClient.Restapi().Account().Extension().Sms().Post(requestBody);
                    Console.WriteLine("SMS sent. Message id: " + resp.id.ToString());
                    await check_message_status(resp.id.ToString());

                    Console.WriteLine("RingCentralAuth->SendSMS() : OK");
                    return "OK";

                }
                catch (Exception ex)
                {
                    string exceptionMessage = "RingCentralAuth->SendSMS(): [" + ex.Message + "]";
                    Console.WriteLine(exceptionMessage);
                    return "FAIL|error";
                }
            }
            else
            {
                return "Couldn't load profile";
            }
                
        }

        public async Task<string> SendEmail(string adresantEmail, string subject, string body)
        {
            return "This functionality is not implemented in RingCentral class";
        }
    }
}