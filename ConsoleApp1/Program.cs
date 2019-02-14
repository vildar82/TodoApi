using System;
using System.Threading.Tasks;
using TodoClient;
using TodoEntities;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Foo();
            Console.ReadKey();
        }

        public static async Task Foo()
        {
            var client = new Client();
            var res = await client.Add(new TodoItem {Name = "Todo " + DateTime.Now});
            Console.WriteLine(res);
            var todos = await client.GetAll();
            if (todos == null)
            {
                Console.WriteLine("пусто");
            }
            else
            {
                foreach (var todoItem in todos)
                {
                    Console.WriteLine($"{todoItem.Id}: {todoItem.Name} - {todoItem.IsComplete}");
                }
            }
        }
    }
}
