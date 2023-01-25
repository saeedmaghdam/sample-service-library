// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

const string SERVER_URL = "192.168.88.245";

var solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.Parent!.ToString();
var binDirectory = Path.Combine(solutionDirectory, "SERVICE_NAME", "bin", "Debug", "net6.0");

Console.Write("Enter total number of instances: ");
int totalNumberOfServiceInstances = int.Parse(Console.ReadLine());


int portNumber = 6100;
for (int i = 0; i < totalNumberOfServiceInstances; i++)
{
    portNumber += 1;
    RunProc("ITnCare.CM.exe", $"--urls \"http://{SERVER_URL}:{portNumber}\"", binDirectory.Replace("SERVICE_NAME", "ITnCare.CM"));
    Thread.Sleep(1000);
}

portNumber = 6200;
for (int i = 0; i < totalNumberOfServiceInstances; i++)
{
    portNumber += 1;
    RunProc("ITnCare.Commission.exe", $"--urls \"http://{SERVER_URL}:{portNumber}\"", binDirectory.Replace("SERVICE_NAME", "ITnCare.Commission"));
    Thread.Sleep(1000);
}

portNumber = 7100;
for (int i = 0; i < totalNumberOfServiceInstances; i++)
{
    portNumber += 1;
    RunProc("ITnCare.OM.Incoming.exe", $"--urls \"http://{SERVER_URL}:{portNumber}\"", binDirectory.Replace("SERVICE_NAME", "ITnCare.OM.Incoming"));
    Thread.Sleep(1000);
}

portNumber = 7500;
RunProc("ITnCare.Gateway.exe", $"--urls \"http://{SERVER_URL}:{portNumber}\"", binDirectory.Replace("SERVICE_NAME", "ITnCare.Gateway"));
Thread.Sleep(1000);

static bool RunProc(string exe, string args, string workingDir)
{
    var prevWorking = Environment.CurrentDirectory;
    try
    {
        Environment.CurrentDirectory = workingDir;
        Process proc = new Process
        {
            StartInfo =
            {
               FileName =  exe,
               CreateNoWindow = false,
               RedirectStandardInput = false,
               WindowStyle = ProcessWindowStyle.Normal,
               UseShellExecute = false,
               RedirectStandardError = false,
               RedirectStandardOutput = false,
               Arguments = args,
            }
        };

        proc.Start();
        
        return true;
    }
    finally
    {
        Environment.CurrentDirectory = prevWorking;
    }
}