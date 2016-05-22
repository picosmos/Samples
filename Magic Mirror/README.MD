##Finish the project

We need an API key to claim data from http://forecast.io that's why you need to open the App.xaml.cs and insert the key inside the construktor. It looks similar to this:

    public App() 
    { 
        //TODO: Inititialize sensitive Data 
        //Claim it here: https://developer.forecast.io/ 
        SensitiveData.ForecastIoApiKey = @"YourApiKey"; 



