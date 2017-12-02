using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSVN
{
    public class configServer
    {
        public string server { get; set; }
        public List<configConnection> configs { get; set; }
    }
    public class configConnection
    {
        public string dbName { get; set; }
        public string userId { get; set; }
        public string password { get; set; }
        public byte authType { get; set; }
    }
    public class JsonConfig
    {
        private readonly string _filePath = string.Empty;
        public JsonConfig(string filePath)
        {
            _filePath = filePath;
        }
        public async Task<List<configServer>> getServers()
        {
            List<configServer> list = new List<configServer>() { };
            if (string.IsNullOrEmpty(_filePath))
                return list;

            if (!File.Exists(_filePath))
                return list;

            using (StreamReader readFile = new StreamReader(_filePath))
            {
                string fileContent = await readFile.ReadToEndAsync();
                list = JsonConvert.DeserializeObject<List<configServer>>(fileContent);
            }

            return list??new List<configServer>() { };
        }

        public async void save(string server, string dbName, byte authType, string userId, string password)
        {
            List<configServer> connections = await getServers();
            configServer thisServer = connections.FirstOrDefault(x=> x.server == server);
            if(thisServer == null)
            {
                thisServer = new configServer()
                {
                    server = server,
                    configs = new List<configConnection>() { }
                };
                connections.Add(thisServer);
            }

            configConnection connectionInfo = thisServer.configs?.FirstOrDefault(x => x.dbName == dbName);
            if (connectionInfo != null)
            {
                connectionInfo.authType = authType;
                connectionInfo.userId = userId;
                connectionInfo.password = password;
            }
            else
            {
                configConnection newConnection = new configConnection()
                {
                    dbName = dbName,
                    authType = authType,
                    userId = userId,
                    password = password
                };
                thisServer.configs.Add(newConnection);
            }

            List<configServer> list = new List<configServer>() { };
            if (string.IsNullOrEmpty(_filePath))
                return;

            string fileContent = JsonConvert.SerializeObject(connections, Formatting.Indented);
            using (StreamWriter writeFile = new StreamWriter(_filePath))
            {
                await writeFile.WriteAsync(fileContent);
            }
        }
    }
}
