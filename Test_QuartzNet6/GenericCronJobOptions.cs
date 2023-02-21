namespace Test_QuartzNet6;

/// <summary>
/// A base class for cron job options. 
/// </summary>
public class GenericCronJobOptions
{
    public string Schedule { get; set;}
}

/// <summary>
/// A Job specific options example
/// </summary>
public class HelloWorldJobOptions
{
    public string ImportFilePath { get; set; }
}