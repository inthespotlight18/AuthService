namespace AuthModelLib
{         

        public interface iGAuth
        {
            Task<string> AuthLogin();
            string ServiceTest();
            Task GetProfile();

        }
    

        

    
}