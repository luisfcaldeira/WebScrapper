using Core.Infra.IoC;
using Crawlers.Domains.Interfaces.DAL.Repositories;
using System;
using System.Threading.Tasks;

namespace ConsoleApp.Multithread.DbInteractions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var iocMapper = new IocMapper();
            var pageRepository = iocMapper.Get<IPageRepository>();

            Task[] tasks = new Task[2];
            TaskFactory taskFactory = new TaskFactory();

            tasks[0] = taskFactory.StartNew(async () =>
            {
                foreach(var page in pageRepository.GetAll())
                {
                    WriteLine(1, page.Url);
                }
            });

            tasks[1] = taskFactory.StartNew(async () =>
            {
                foreach (var page in pageRepository.GetAll())
                {
                    WriteLine(2, page.Url);
                }
            });

            Task.WaitAll(tasks);
        }

        private static void WriteLine(int thread, string what)
        {
            Console.WriteLine($"[{thread}] {what}");
        }
    }
}
