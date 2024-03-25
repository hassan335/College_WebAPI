namespace CollegeApp.MyLogging
{
    public class LogToDB:IMyLogger
    {
        public void log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("LogToDB");
        }


    }
}
