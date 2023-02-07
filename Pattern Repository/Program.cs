using Pattern_Repository.CSVRepositories;
using Pattern_Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Repository
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            //MessageCSVRepository messageRepository = 
            //    new MessageCSVRepository("D:\\ФАИС\\ИП-31\\ИГИ\\Pattern repository\\Pattern Repository\\Pattern Repository\\CSVFiles\\MessageCSV.txt");
            //IEnumerable<Message> messages = await messageRepository.GetAll();

            //foreach(Message m in messages)
            //{
            //    Console.WriteLine(m.ToString());
            //}

            ChatCSVRepository chatRepository =
                new ChatCSVRepository(@"D:\ФАИС\ИП-31\ИГИ\Pattern repository\Pattern Repository\Pattern Repository\CSVFiles\ChatCSV.txt");
            IEnumerable<Chat> chats = await chatRepository.FindWithCriterea(c => c.Name == "family");

            foreach(Chat c in chats)
            {
                Console.WriteLine(c.ToString());
            }

            Console.ReadKey();
        }

        public static DateTime DateFormater(string date)
        {
            try
            {
                string[] parts = date.Split('/', ' ', ':');
                return new DateTime(Convert.ToInt32(parts[2]), Convert.ToInt32(parts[0]),
                    Convert.ToInt32(parts[1]), Convert.ToInt32(parts[3]), Convert.ToInt32(parts[4]), 0);
            }
            catch
            {
                throw new Exception("can't convert");
            }
            
        }
    }
}
