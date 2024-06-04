using Renci.SshNet;


// doesn't use the friendly names defined in the ~/.ssh/config file
// const string host = "10.0.1.37";
// const int port = 22;
// const string username = "besmith";


const string host = "ttu-bsmith";
const int port = 22;
const string username = "blakesmith";


// using PrivateKeyFile over a password
// const string password = "nopass";

// doesn't understand the ~ for $HOME
// it has a very odd definition for folder structure
// this is output from a list of all dirs at / when connecting to backup
/*
Creating client and connecting
Connected to 10.0.1.37
Current Working Directory after connection is made /
/.
/..
/.ssh
/bin
/.bashrc
/.cache
/.profile
/.oh-my-zsh
/.zshrc
/.viminfo
/PWSH.7z
/Scores.7z
/Development.7z
/OneDrive-4-15-24.tar.gz
/Developer-4-15-24.tar.gz
/sftp_test
*/
// const string workingdirectory = "/sftp_test/";
const string workingdirectory = "/C:/Users/blakesmith/";
const string uploadfile = @"./test.txt";

PrivateKeyFile privateKeyFile = new PrivateKeyFile("/Users/besmith/.ssh/work");
// PrivateKeyFile privateKeyFile = new PrivateKeyFile("/Users/besmith/.ssh/id_rsa");

Console.WriteLine("Creating client and connecting");
using (var client = new SftpClient(host, port, username, privateKeyFile))
{
    client.Connect();
    Console.WriteLine("Connected to {0}", host);

	Console.WriteLine("Current Working Directory after connection is made {0}", client.WorkingDirectory);

	var listDir = client.ListDirectory("/");

	foreach (var dir in listDir)
	{
		Console.WriteLine(dir.FullName);
	}

    client.ChangeDirectory(workingdirectory);
    Console.WriteLine("Changed directory to {0}", workingdirectory);
 
    var listDirectory = client.ListDirectory(workingdirectory);
    Console.WriteLine("Listing directory:");
    foreach (var fi in listDirectory)
    {
        Console.WriteLine(" - " + fi.Name);
    }
 
    using (var fileStream = new FileStream(uploadfile, FileMode.Open))
    {
        Console.WriteLine("Uploading {0} ({1:N0} bytes)", uploadfile, fileStream.Length);
        client.BufferSize = 4 * 1024; // bypass Payload error large files
        client.UploadFile(fileStream, Path.GetFileName(uploadfile));
    }
}
