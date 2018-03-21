// See LICENSE file in the root directory
//
using System;

namespace LGK.Injection.Example
{
    public class MainApplication
    {
        public static void Main(string[] args)
        {
            var injector = new InjectionManager();

            var printService1 = new PrintService1();
            var printService2 = new PrintService2();
            var gameplayManager = new GameplayManager();

            injector.Register<IPrintService>(printService1);
            injector.Register<IPrintService>("Service1", printService1);
            injector.Register<IPrintService>("Service2", printService2);

            injector.Inject(gameplayManager);

            gameplayManager.Execute();
        }

        class GameplayManager
        {
            [InjectDependency] IPrintService m_DefaultService;
            [InjectDependency("Service1")] IPrintService m_PrintService1;
            [InjectDependency("Service2")] IPrintService m_PrintService2;

            public void Execute()
            {
                m_DefaultService.Print("Default Service Message");
                m_PrintService1.Print("Service 1 Message");
                m_PrintService2.Print("Service 2 Message");
            }
        }

        interface IPrintService
        {
            void Print(string message);
        }

        class PrintService1 : IPrintService
        {
            public void Print(string message)
            {
                Console.WriteLine(string.Format("[{0}] {1}", "PrintService1", message));
            }
        }

        class PrintService2 : IPrintService
        {
            public void Print(string message)
            {
                Console.WriteLine(string.Format("[{0}] {1}", "PrintService2", message));
            }
        }
    }
}
